﻿/*
	Alphora Dataphor
	© Copyright 2000-2009 Alphora
	This file is licensed under a modified BSD-license which can be found here: http://dataphor.org/dataphor_license.txt
*/

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Alphora.Dataphor.DAE.Server
{
	public class CachedPlanHeader
	{
		public CachedPlanHeader(string AStatement, string ALibraryName, int AContextHashCode, bool AInApplicationTransaction)
		{
			Statement = AStatement;
			LibraryName = ALibraryName;
			ContextHashCode = AContextHashCode;
			InApplicationTransaction = AInApplicationTransaction;
		}
		
		public string Statement;
		public string LibraryName;
		public int ContextHashCode; // Hash of the names of all types present on the context for the process
		public bool InApplicationTransaction;
		
		/// <summary>This flag will be set to true if the plan results in an error on open, indicating that it is invalid, and should not be returned to the plan cache.</summary>
		public bool IsInvalidPlan;
	
		public override int GetHashCode()
		{
			return Statement.GetHashCode() ^ LibraryName.GetHashCode() ^ ContextHashCode ^ InApplicationTransaction.GetHashCode();
		}
		
		public override bool Equals(object AObject)
		{
			CachedPlanHeader LCachedPlanHeader = AObject as CachedPlanHeader;
			return 
				(LCachedPlanHeader != null) 
					&& (LCachedPlanHeader.Statement == Statement) 
					&& (LCachedPlanHeader.LibraryName == LibraryName) 
					&& (LCachedPlanHeader.ContextHashCode == ContextHashCode) 
					&& (LCachedPlanHeader.InApplicationTransaction == InApplicationTransaction);
		}
	}
	
	public class CachedPlans : List
	{
		public new ServerPlanBase this[int AIndex]
		{
			get { return (ServerPlanBase)base[AIndex]; }
			set { base[AIndex] = value; }
		}
	}
	
	public class PlanCache : System.Object
	{
		public PlanCache(int ACacheSize) : base() 
		{
			FSize = ACacheSize;
			if (FSize > 1)
				FPlans = new FixedSizeCache(FSize);
		}
		
		private int FSize;
		public int Size { get { return FSize; } }
		
		private FixedSizeCache FPlans; // FixedSizeCache ( <CachedPlanHeader>, <CachedPlans> )
		
		private void DisposeCachedPlan(ServerProcess AProcess, ServerPlanBase APlan)
		{
			try
			{
				APlan.BindToProcess(AProcess.ServerSession.Server.FSystemProcess);
				APlan.Dispose();
			}
			catch
			{
				// ignore disposal exceptions
			}
		}
		
		private void DisposeCachedPlans(ServerProcess AProcess, CachedPlans APlans)
		{
			foreach (ServerPlanBase LPlan in APlans)
				DisposeCachedPlan(AProcess, LPlan);
		}
		
		private CachedPlanHeader GetPlanHeader(ServerProcess AProcess, string AStatement, int AContextHashCode)
		{
			return new CachedPlanHeader(AStatement, AProcess.Plan.CurrentLibrary.Name, AContextHashCode, AProcess.ApplicationTransactionID != Guid.Empty);
		}

		/// <summary>Gets a cached plan for the given statement, if available.</summary>
		/// <remarks>
		/// If a plan is found, it is referenced for the LRU, and disowned by the cache.
		/// The client must call Release to return the plan to the cache.
		/// If no plan is found, null is returned and the cache is unaffected.
		/// </remarks>
		public ServerPlanBase Get(ServerProcess AProcess, string AStatement, int AContextHashCode)
		{
			ServerPlanBase LPlan = null;
			CachedPlanHeader LHeader = GetPlanHeader(AProcess, AStatement, AContextHashCode);
			CachedPlans LBumped = null;
			lock (this)
			{
				if (FPlans != null)
				{
					CachedPlans LPlans = FPlans[LHeader] as CachedPlans;
					if (LPlans != null)
					{
						for (int LPlanIndex = LPlans.Count - 1; LPlanIndex >= 0; LPlanIndex--)
						{
							LPlan = LPlans[LPlanIndex];
							LPlans.RemoveAt(LPlanIndex);
							if (AProcess.Plan.Catalog.PlanCacheTimeStamp > LPlan.PlanCacheTimeStamp)
							{
								DisposeCachedPlan(AProcess, LPlan);
								LPlan = null;
							}
							else
							{
								LBumped = (CachedPlans)FPlans.Reference(LHeader, LPlans);
								break;
							}
						}
					}
				}
			}
			
			if (LBumped != null)
				DisposeCachedPlans(AProcess, LBumped);

			if (LPlan != null)
				LPlan.BindToProcess(AProcess);

			return LPlan;
		}

		/// <summary>Adds the given plan to the plan cache.</summary>		
		/// <remarks>
		/// The plan is not contained within the cache after this call, it is assumed in use by the client.
		/// This call simply reserves storage and marks the plan as referenced for the LRU.
		/// </remarks>
		public void Add(ServerProcess AProcess, string AStatement, int AContextHashCode, ServerPlanBase APlan)
		{
			CachedPlans LBumped = null;
			CachedPlanHeader LHeader = GetPlanHeader(AProcess, AStatement, AContextHashCode);
			APlan.Header = LHeader;
			APlan.PlanCacheTimeStamp = AProcess.Plan.Catalog.PlanCacheTimeStamp;

			lock (this)
			{
				if (FPlans != null)
				{
					CachedPlans LPlans = FPlans[LHeader] as CachedPlans;
					if (LPlans == null)
						LPlans = new CachedPlans();
					LBumped = (CachedPlans)FPlans.Reference(LHeader, LPlans);
				}
			}
			
			if (LBumped != null)
				DisposeCachedPlans(AProcess, LBumped);
		}
		
		/// <summary>Releases the given plan and returns whether or not it was returned to the cache.</summary>
		/// <remarks>
		/// If the plan is returned to the cache, the client is no longer responsible for the plan, it is owned by the cache.
		/// If the plan is not returned to the cache, the cache client is responsible for disposing the plan.
		///	</remarks>
		public bool Release(ServerProcess AProcess, ServerPlanBase APlan)
		{
			CachedPlanHeader LHeader = APlan.Header;

			lock (this)
			{
				if (FPlans != null)
				{
					CachedPlans LPlans = FPlans[LHeader] as CachedPlans;
					if (LPlans != null)
					{
						LPlans.Add(APlan);
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Clears the plan cache, disposing any plans it contains.</summary>		
		public void Clear(ServerProcess AProcess)
		{
			lock (this)
			{
				if (FPlans != null)
				{
					foreach (DictionaryEntry LEntry in FPlans)
						DisposeCachedPlans(AProcess, LEntry.Value as CachedPlans);
					
					FPlans.Clear();
				}
			}
		}

		/// <summary>Resizes the cache to the specified size.</summary>
		/// <remarks>
		/// Resizing the cache has the effect of clearing the entire cache.
		/// </remarks>
		public void Resize(ServerProcess AProcess, int ASize)
		{
			lock (this)
			{
				if (FPlans != null)
				{
					Clear(AProcess);
					FPlans = null;
				}
				
				FSize = ASize;
				if (FSize > 1)
					FPlans = new FixedSizeCache(FSize);
			}
		}
	}
}