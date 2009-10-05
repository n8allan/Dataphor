/*
	Dataphor
	© Copyright 2000-2008 Alphora
	This file is licensed under a modified BSD-license which can be found here: http://dataphor.org/dataphor_license.txt
*/
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Cryptography;

using Alphora.Dataphor;
using Alphora.Dataphor.BOP;
using Alphora.Dataphor.DAE;
using Alphora.Dataphor.DAE.Server;
using Alphora.Dataphor.DAE.Streams;
using Alphora.Dataphor.DAE.Language;
using Alphora.Dataphor.DAE.Language.D4;
using Alphora.Dataphor.DAE.Runtime;
using Alphora.Dataphor.DAE.Runtime.Data;
using Alphora.Dataphor.DAE.Runtime.Instructions;
using D4 = Alphora.Dataphor.DAE.Language.D4;

namespace Alphora.Dataphor.DAE.Schema
{
	public class LibraryReference : System.Object, ICloneable
	{
		public LibraryReference() : base() {}
		public LibraryReference(string AName) : base()
		{
			Name = AName;
		}
		
		public LibraryReference(string AName, VersionNumber AVersion) : base()
		{
			Name = AName;
			Version = AVersion;
		}
		
		private string FName;
		public string Name
		{
			get { return FName; }
			set { FName = value == null ? String.Empty : value; }
		}
		
		private VersionNumber FVersion = new VersionNumber(-1, -1, -1, -1);
		public VersionNumber Version
		{
			get { return FVersion; }
			set { FVersion = value; }
		}
		
		public override bool Equals(object AObject)
		{
			return AObject is LibraryReference && (Compare(this, (LibraryReference)AObject) == 0);
		}
		
		public override int GetHashCode()
		{
			return FName.GetHashCode() ^ FVersion.GetHashCode();
		}
		
		public object Clone()
		{
			return new LibraryReference(FName, FVersion);
		}

		public static int Compare(LibraryReference ALeftValue, LibraryReference ARightValue)
		{
			int LResult = String.Compare(ALeftValue.Name, ARightValue.Name);
			if (LResult == 0)
				LResult = VersionNumber.Compare(ALeftValue.Version, ARightValue.Version);
			return LResult;
		}
	}

	#if USETYPEDLIST
	public class LibraryReferences : TypedList
	{
		public LibraryReferences() : base(typeof(LibraryReference)) {}
		
		public new LibraryReference this[int AIndex]
		{
			get { return (LibraryReference)base[AIndex]; }
			set { base[AIndex] = value; }
		}
		
		protected override void Validate(object AValue)
		{
			base.Validate(AValue);
			if (Contains(((LibraryReference)AValue).Name))
				throw new SchemaException(SchemaException.Codes.DuplicateObjectName, ((LibraryReference)AValue).Name);
		}
	#else
	public class LibraryReferences : ValidatingBaseList<LibraryReference>
	{
		protected override void  Validate(LibraryReference AValue)
		{
 			 //base.Validate(AValue);
 			 if (Contains(AValue.Name))
 				throw new SchemaException(SchemaException.Codes.DuplicateObjectName, AValue.Name);
		}
	#endif
	
		public LibraryReference this[string AName]
		{
			get { return this[IndexOf(AName)]; }
			set { this[IndexOf(AName)] = value; }
		}
		
		public int IndexOf(string AName)
		{
			for (int LIndex = 0; LIndex < Count; LIndex++)
				if (String.Compare(this[LIndex].Name, AName) == 0)
					return LIndex;
			return -1;
		}
		
		public bool Contains(string AName)
		{
			return IndexOf(AName) >= 0;
		}
	}
	
	public static class Environments
	{
		public const string WindowsServer = "WindowsServer";
	}
		
	public class FileReference : System.Object, ICloneable
	{
		public FileReference() : base() {}
		public FileReference(string AFileName) : base()
		{
			FFileName = AFileName;
		}
		
		public FileReference(string AFileName, bool AIsAssembly) : base()
		{
			FFileName = AFileName;
			FIsAssembly = AIsAssembly;
		}
		
		public FileReference(string AFileName, bool AIsAssembly, IEnumerable<string> AEnvironments) : base()
		{
			FFileName = AFileName;
			FIsAssembly = AIsAssembly;
			FEnvironments.AddRange(AEnvironments);
		}
		
		private string FFileName;
		public string FileName
		{
			get { return FFileName; }
			set { FFileName = value; }
		}
		
		private bool FIsAssembly;
		public bool IsAssembly
		{
			get { return FIsAssembly; }
			set { FIsAssembly = value; }
		}
		
		private List<String> FEnvironments = new List<String>();
		public List<String> Environments { get { return FEnvironments; } }

		public override bool Equals(object AObject)
		{
			FileReference LObject = AObject as FileReference;
			return 
				(LObject != null) 
					&& String.Equals(FFileName, LObject.FileName, StringComparison.OrdinalIgnoreCase) 
					&& (FIsAssembly == LObject.IsAssembly);
		}
		
		public override int GetHashCode()
		{
			return FFileName.GetHashCode();
		}
		
		public object Clone()
		{
			return new FileReference(FFileName, FIsAssembly, FEnvironments.ToArray());
		}
	}
	
	#if USETYPEDLIST
	public class FileReferences : TypedList
	{
		public FileReferences() : base(typeof(FileReference)) {}
		
		public new FileReference this[int AIndex]
		{
			get { return (FileReference)base[AIndex]; }
			set { base[AIndex] = value; }
		}
		
		protected override void Validate(object AValue)
		{
			base.Validate(AValue);
			if (Contains(((FileReference)AValue).FileName))
				throw new SchemaException(SchemaException.Codes.DuplicateObjectName, ((FileReference)AValue).FileName);
		}
	#else
	public class FileReferences : ValidatingBaseList<FileReference>
	{
		protected override void Validate(FileReference AValue)
		{
			//base.Validate(AValue);
			if (Contains(AValue.FileName))
				throw new SchemaException(SchemaException.Codes.DuplicateObjectName, AValue.FileName);
		}
	#endif
		
		public int IndexOf(string AFileName)
		{
			for (int LIndex = 0; LIndex < Count; LIndex++)
				if (String.Equals(this[LIndex].FileName, AFileName, StringComparison.OrdinalIgnoreCase))
					return LIndex;
			return -1;
		}

		public bool Contains(string AFileName)
		{
			return IndexOf(AFileName) >= 0;
		}
		
		public FileReference this[string AFileName]
		{
			get { return this[IndexOf(AFileName)]; }
		}
	}
	
	public class LibraryInfo : System.Object, ICloneable
	{
		public LibraryInfo() : base() {}
		public LibraryInfo(string AName, bool AIsSuspect, string ASuspectReason)
		{
			FName = AName;
			FIsSuspect = AIsSuspect;
			FSuspectReason = ASuspectReason;
		}
		
		private string FName;
		public string Name
		{
			get { return FName; }
			set { FName = value; }
		}
		
		private bool FIsSuspect;
		public bool IsSuspect
		{
			get { return FIsSuspect; }
			set 
			{
				FIsSuspect = value; 
				if (!FIsSuspect)
					FSuspectReason = null;
			}
		}
		
		private string FSuspectReason;
		public string SuspectReason
		{
			get { return FSuspectReason; }
			set { FSuspectReason = value; }
		}
		
		#region ICloneable Members

		public object Clone()
		{
			return new LibraryInfo(FName, FIsSuspect, FSuspectReason);
		}

		#endregion

		public void SaveToStream(Stream AStream)
		{
			new Serializer().Serialize(AStream, this);
		}
		
		public static LibraryInfo LoadFromStream(Stream AStream)
		{
			return new Deserializer().Deserialize(AStream, null) as LibraryInfo;
		}
	}
	
	// Library
	// A library is either a directory in the LibraryDirectory of the DAE, 
	//	or a directory explicitly specified as part of the library definition.
	// This directory will have a file called <library name>.d4l which is a serialization of this structure
	// detailing the assemblies in the library and the required libraries
	// Registering a library ->
	//	Ensure that each required library is registered
	//	Copy all files into the DAE's bin (or the GAC)
	//  Register each assembly with the DAE
	//	run the register.d4 script if it exists in the library
	//		catalog objects created in this script are part of this library
	public class Library : Schema.Object, ICloneable
	{
		public Library() : base(String.Empty) {}
		public Library(string AName) : base(AName) {}
		public Library(string AName, VersionNumber AVersion) : base(AName)
		{
			FVersion = AVersion;
		}
		
		public Library(string AName, VersionNumber AVersion, string ADefaultDeviceName) : base(AName)
		{
			FVersion = AVersion;
			DefaultDeviceName = ADefaultDeviceName;
		}
		
		public Library(string AName, string ADirectory, VersionNumber AVersion, string ADefaultDeviceName) : base(AName)
		{
			Directory = ADirectory;
			FVersion = AVersion;
			DefaultDeviceName = ADefaultDeviceName;
		}
		
		private VersionNumber FVersion = new VersionNumber(-1, -1, -1, -1);
		public VersionNumber Version
		{
			get { return FVersion; }
			set { FVersion = value; }
		}
		
		private string FDefaultDeviceName = String.Empty;
		public string DefaultDeviceName
		{
			get { return FDefaultDeviceName; }
			set { FDefaultDeviceName = value == null ? String.Empty : value; }
		}
		
		private string FDirectory = String.Empty;
		[Publish(PublishMethod.None)]
		public string Directory
		{
			get { return FDirectory; }
			set 
			{ 
				if ((value == null) || (value == String.Empty))
					FDirectory = String.Empty; 
				else
					FDirectory = value;
			}
		}
		
		private bool FIsSuspect;
		[Publish(PublishMethod.None)]
		public bool IsSuspect
		{
			get { return FIsSuspect; }
			set 
			{
				FIsSuspect = value; 
				if (!FIsSuspect)
					FSuspectReason = null;
			}
		}
		
		private string FSuspectReason;
		[Publish(PublishMethod.None)]
		public string SuspectReason
		{
			get { return FSuspectReason; }
			set { FSuspectReason = value; }
		}
		
		private string ListToString(List AList)
		{
			StringBuilder LString = new StringBuilder();
			for (int LIndex = 0; LIndex < AList.Count; LIndex++)
			{
				if (LIndex > 0)
					LString.Append(";");
				LString.Append((string)AList[LIndex]);
			}
			return LString.ToString();
		}
		
		private void StringToList(string AString, List AList)
		{
			AList.Clear();
			if (AString.Length > 0)
			{
				string[] LStrings = AString.Split(';');
				for (int LIndex = 0; LIndex < LStrings.Length; LIndex++)
					AList.Add(LStrings[LIndex]);
			}
		}

		// A list of FileReference objects. These files will be copied into the DAE's startup directory. If IsAssembly is true, the file will be registered as an assembly with the DAE.
		private FileReferences FFiles = new FileReferences();
		public FileReferences Files { get { return FFiles; } }
		
		public string AssembliesAsString
		{
			get { return String.Empty; }
			set 
			{
				if ((value != null) && (value != String.Empty))
				{ 
					List LList = new List();
					StringToList(value, LList); 
					foreach (string LString in LList)
						FFiles.Add(new FileReference(LString, true));
				}
			}
		}
		
		// A list of LibraryReference objects.
		private LibraryReferences FLibraries = new LibraryReferences();
		public LibraryReferences Libraries { get { return FLibraries; } }
		
		public string LibrariesAsString
		{
			get { return String.Empty; }
			set 
			{
				if ((value != null) && (value != String.Empty))
				{
					List LList = new List();
					StringToList(value, LList);
					foreach (string LString in LList)
						FLibraries.Add(new LibraryReference(LString));
				}
			}
		}
		
		public string Settings
		{
			get 
			{
				if (MetaData == null)
					return String.Empty;
					
				StringBuilder LResult = new StringBuilder();
				#if USEHASHTABLEFORTAGS
				foreach (Tag LTag in MetaData.Tags)
				{
				#else
				Tag LTag;
				for (int LIndex = 0; LIndex < MetaData.Tags.Count; LIndex++)
				{
					LTag = MetaData.Tags[LIndex];
				#endif
					if (LResult.Length > 0)
						LResult.Append(";");
					LResult.AppendFormat(@"'{0}'='{1}'", LTag.Name.Replace("'", "''"), LTag.Value.Replace("'", "''"));
				}
				
				return LResult.ToString();
			}
			set
			{
				MetaData = new MetaData();

				Lexer FLexer = new Lexer(value);
				while (FLexer[1].Type != TokenType.EOF)
				{
					FLexer.NextToken();
					string LTagName = FLexer[0].AsString;
					FLexer.NextToken().CheckSymbol(Keywords.Equal);
					FLexer.NextToken();
					MetaData.Tags.Add(new Tag(LTagName, FLexer[0].AsString));
					if (FLexer.PeekTokenSymbol(1) == Keywords.StatementTerminator)
						FLexer.NextToken();
				}
			}
		}
		
		public void SaveToStream(Stream AStream)
		{
			new Serializer().Serialize(AStream, this);
		}
		
		public object Clone()
		{
			Library LLibrary = new Library(Name, FVersion, FDefaultDeviceName);
			LLibrary.Directory = FDirectory;
			LLibrary.MergeMetaData(MetaData);
			LLibrary.IsSuspect = FIsSuspect;
			LLibrary.SuspectReason = FSuspectReason;
			foreach (LibraryReference LLibraryReference in FLibraries)
				LLibrary.Libraries.Add((LibraryReference)LLibraryReference.Clone());
			foreach (FileReference LFileReference in FFiles)
				LLibrary.Files.Add((FileReference)LFileReference.Clone());
			return LLibrary;
		}
	}
	
	public delegate void LibraryNotifyEvent(Program AProgram, string ALibraryName);
	public delegate void LibraryRenameEvent(Program AProgram, string AOldLibraryName, string ANewLibraryName);
	
    /// <remarks> Libraries </remarks>
	public class Libraries : Objects
    {
		#if USEOBJECTVALIDATE
		protected override void Validate(Object AItem)
		{
			if (!(AItem is Library))
				throw new SchemaException(SchemaException.Codes.InvalidContainer, "Library");
			base.Validate(AItem);
		}
		#endif

		public new Library this[int AIndex]
		{
			get { return (Library)base[AIndex]; }
			set { base[AIndex] = value; }
		}

		public new Library this[string AName]
		{
			get { return (Library)base[AName]; }
			set { base[AName] = value; }
		}

		/// <summary>Occurs whenever a library is created in the DAE.</summary>		
		public event LibraryNotifyEvent OnLibraryCreated;
		public void DoLibraryCreated(Program AProgram, string ALibraryName)
		{
			if (OnLibraryCreated != null)
				OnLibraryCreated(AProgram, ALibraryName);
		}
		
		/// <summary>Occurs whenever a library is added to the list of available libraries in the DAE.</summary>
		public event LibraryNotifyEvent OnLibraryAdded;
		public void DoLibraryAdded(Program AProgram, string ALibraryName)
		{
			if (OnLibraryAdded != null)
				OnLibraryAdded(AProgram, ALibraryName);
		}
		
		/// <summary>Occurs whenever a library is removed from the list of available libraries in the DAE.</summary>
		public event LibraryNotifyEvent OnLibraryRemoved;
		public void DoLibraryRemoved(Program AProgram, string ALibraryName)
		{
			if (OnLibraryRemoved != null)
				OnLibraryRemoved(AProgram, ALibraryName);
		}
		
		/// <summary>Occurs whenever a library is renamed in the DAE.</summary>
		public event LibraryRenameEvent OnLibraryRenamed;
		public void DoLibraryRenamed(Program AProgram, string AOldLibraryName, string ANewLibraryName)
		{
			if (OnLibraryRenamed != null)
				OnLibraryRenamed(AProgram, AOldLibraryName, ANewLibraryName);
		}
		
		/// <summary>Occurs whenever a library is deleted in the DAE.</summary>
		public event LibraryNotifyEvent OnLibraryDeleted;
		public void DoLibraryDeleted(Program AProgram, string ALibraryName)
		{
			if (OnLibraryDeleted != null)
				OnLibraryDeleted(AProgram, ALibraryName);
		}

		/// <summary>Occurs whenever a library is registered or loaded in the DAE.</summary>
		public event LibraryNotifyEvent OnLibraryLoaded;
		public void DoLibraryLoaded(Program AProgram, string ALibraryName)
		{
			if (OnLibraryLoaded != null)
				OnLibraryLoaded(AProgram, ALibraryName);
		}

		/// <summary>Occurs whenever a library is unregistered or unloaded in the DAE.</summary>
		public event LibraryNotifyEvent OnLibraryUnloaded;
		public void DoLibraryUnloaded(Program AProgram, string ALibraryName)
		{
			if (OnLibraryUnloaded != null)
				OnLibraryUnloaded(AProgram, ALibraryName);
		}
    }

	public class LoadedLibrary : Schema.CatalogObject
	{
		public LoadedLibrary(string AName) : base(AName) {}
		
		private List FAssemblies = new List();
		public List Assemblies { get { return FAssemblies; } }

		[Reference]
		private LoadedLibraries FRequiredLibraries = new LoadedLibraries();
		public LoadedLibraries RequiredLibraries { get { return FRequiredLibraries; } }
		
		[Reference]
		private LoadedLibraries FRequiredByLibraries = new LoadedLibraries();		
		public LoadedLibraries RequiredByLibraries { get { return FRequiredByLibraries; } }
		
		private NameResolutionPath FNameResolutionPath;
		
		protected void GetNameResolutionPath(NameResolutionPath APath, int ALevel, Schema.LoadedLibrary ALibrary)
		{
			while (APath.Count <= ALevel)
				APath.Add(new Schema.LoadedLibraries());
				
			if (!APath.Contains(ALibrary))
				APath[ALevel].Add(ALibrary);
				
			foreach (LoadedLibrary LLibrary in ALibrary.RequiredLibraries)	
				GetNameResolutionPath(APath, ALevel + 1, LLibrary);
		}
		
		public NameResolutionPath GetNameResolutionPath(LoadedLibrary ASystemLibrary)
		{
			if (FNameResolutionPath == null)
			{
				/* Depth first traversal, tracking level, add every unique required library at the level corresponding to the depth of the library in the dependency graph */
				FNameResolutionPath = new NameResolutionPath();
				GetNameResolutionPath(FNameResolutionPath, 0, this);

				/* Remove all levels with no libraries */
				for (int LIndex = FNameResolutionPath.Count - 1; LIndex >= 0; LIndex--)
					if (FNameResolutionPath[LIndex].Count == 0)
						FNameResolutionPath.RemoveAt(LIndex);
						
				/* Ensure that system is in the path */
				if (!FNameResolutionPath.Contains(ASystemLibrary))
				{
					FNameResolutionPath.Add(new Schema.LoadedLibraries());
					FNameResolutionPath[FNameResolutionPath.Count - 1].Add(ASystemLibrary);
				}
			}
								
			return FNameResolutionPath;
		}
		
		public void ClearNameResolutionPath()
		{
			FNameResolutionPath = null;
		}
		
		/// <summary>Returns true if ALibrary is the same as this library, or is required by this library.</summary>
		public bool IsRequiredLibrary(LoadedLibrary ALibrary)
		{
			if (this.Equals(ALibrary))
				return true;
			foreach (LoadedLibrary LLibrary in FRequiredLibraries)
				if (LLibrary.IsRequiredLibrary(ALibrary))
					return true;
			return false;
		}
		
		public void AttachLibrary()
		{
			foreach (LoadedLibrary LLibrary in FRequiredLibraries)
				if (!LLibrary.RequiredByLibraries.Contains(this))
					LLibrary.RequiredByLibraries.Add(this);
		}
		
		public void DetachLibrary()
		{
			foreach (LoadedLibrary LLibrary in FRequiredLibraries)
				if (LLibrary.RequiredByLibraries.Contains(this))
					LLibrary.RequiredByLibraries.Remove(this);
		}
	}

    /// <remarks> LoadedLibraries </remarks>
	public class LoadedLibraries : Objects
    {
		#if USEOBJECTVALIDATE
		protected override void Validate(Object AItem)
		{
			if (!(AItem is LoadedLibrary))
				throw new SchemaException(SchemaException.Codes.InvalidContainer, "LoadedLibrary");
			base.Validate(AItem);
		}
		#endif

		public new LoadedLibrary this[int AIndex]
		{
			get { return (LoadedLibrary)base[AIndex]; }
			set { base[AIndex] = value; }
		}

		public new LoadedLibrary this[string AName]
		{
			get { return (LoadedLibrary)base[AName]; }
			set { base[AName] = value; }
		}
    }

	/// <summary>
	///	NameResolutionPath is a list of LoadedLibraries lists.  
	///	Each element is the unique set of library dependencies at the level given by the index in the list above the library.
	///	The first element contains only the library of the name resolution path.
	///	</summary>    
    public class NameResolutionPath : List
    {
		public new LoadedLibraries this[int AIndex]
		{
			get { return (LoadedLibraries)base[AIndex]; }
			set { base[AIndex] = value; }
		}
		
		public bool Contains(LoadedLibrary ALibrary)
		{
			for (int LIndex = 0; LIndex < Count; LIndex++)
				if (this[LIndex].Contains(ALibrary))
					return true;
			return false;
		}
    }
}

