/*
	Dataphor
	© Copyright 2000-2008 Alphora
	This file is licensed under a modified BSD-license which can be found here: http://dataphor.org/dataphor_license.txt
*/
using System; 
using System.Text;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

using Alphora.Dataphor.DAE.Language;
using Alphora.Dataphor.DAE.Language.D4;
using Alphora.Dataphor.DAE.Compiling;
using Alphora.Dataphor.DAE.Server;
using Alphora.Dataphor.DAE.Streams;
using Alphora.Dataphor.DAE.Runtime;
using Alphora.Dataphor.DAE.Runtime.Data;
using Alphora.Dataphor.DAE.Runtime.Instructions;
using Alphora.Dataphor.DAE.Device.Catalog;
using Schema = Alphora.Dataphor.DAE.Schema;

namespace Alphora.Dataphor.DAE.Runtime.Instructions
{
    /// <remarks>operator CreateRight(const ARightName : System.Name);</remarks>
    /// <remarks>operator CreateRight(const ARightName : System.Name, const AUserID : System.String);</remarks>
    public class SystemCreateRightNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			CreateRightNode.CreateRight(AProgram, (string)AArguments[0], (AArguments.Length == 2) ? (string)AArguments[1] : AProgram.Plan.User.ID);
			return null;
		}
    }
    
    /// <remarks>operator DropRight(const ARightName : String); </remarks>
    public class SystemDropRightNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			DropRightNode.DropRight(AProgram, (string)AArguments[0]);
			return null;
		}
    }
    
    /// <remarks>operator RightExists(ARightName : string) : boolean; </remarks>
    public class SystemRightExistsNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			return ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RightExists((string)AArguments[0]);
		}
    }

    /// <remarks>operator CreateGroup(const AGroupName : String, const AParentGroupName : String); </remarks>
    public class SystemCreateGroupNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			// Deprecated, stubbed for backwards compatibility
			return null;
		}
    }
    
	// operator CreateRole(const ARoleName : Name);    
    public class SystemCreateRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = new Schema.Role(Schema.Object.Qualify((string)AArguments[0], AProgram.Plan.CurrentLibrary.Name));
			LRole.Owner = AProgram.Plan.User;
			LRole.Library = AProgram.Plan.CurrentLibrary;
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertRole(LRole);
			return null;
		}
    }
    
    // operator DropRole(const ARoleName : Name);
    public class SystemDropRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[0], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);
				
			AProgram.Plan.CheckRight(LRole.GetRight(Schema.RightNames.Drop));
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeleteRole(LRole);
			return null;
		}
    }
    
    /// <remarks>operator RoleExists(ARoleName : string) : boolean; </remarks>
    public class SystemRoleExistsNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			lock (AProgram.Catalog)
			{
				Schema.Object LObject = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[0], false);
				return LObject is Schema.Role;
			}
		}
    }

	/// <remarks>operator RoleHasRight(ARoleName : String, ARightName : Name) : System.Boolean; </remarks>
    public class SystemRoleHasRightNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[0], true) as Schema.Role;
			return ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RoleHasRight(LRole, (string)AArguments[1]);
		}
    }

    /// <remarks>operator CreateUser(AUserID : String, AUserName : String, APassword : String); </remarks>
    public class SystemCreateUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			AProgram.Plan.CheckRight(Schema.RightNames.CreateUser);
			string LUserID = (string)AArguments[0];
			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserExists(LUserID))
				throw new Schema.SchemaException(Schema.SchemaException.Codes.DuplicateUserID, LUserID);
			Schema.User LUser = new Schema.User(LUserID, (string)AArguments[1], Schema.SecurityUtility.EncryptPassword((string)AArguments[2]));
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertUser(LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertUserRole(LUser.ID, AProgram.ServerProcess.ServerSession.Server.UserRole.ID);
			return null;
		}
    }
    
    /// <remarks>operator CreateUserWithEncryptedPassword(AUserID : string, AUserName : string, AEncryptedPassword : string); </remarks>
    /// <remarks>operator CreateUserWithEncryptedPassword(AUserID : string, AUserName : string, AEncryptedPassword : string, AGroupName : String); </remarks> // Deprecated
    public class SystemCreateUserWithEncryptedPasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			AProgram.Plan.CheckRight(Schema.RightNames.CreateUser);
			string LUserID = (string)AArguments[0];
			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserExists(LUserID))
				throw new Schema.SchemaException(Schema.SchemaException.Codes.DuplicateUserID, LUserID);
			Schema.User LUser = new Schema.User(LUserID, (string)AArguments[1], (string)AArguments[2]);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertUser(LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertUserRole(LUser.ID, AProgram.ServerProcess.ServerSession.Server.UserRole.ID);
			return null;
		}
    }
    
    /// <remarks>operator SetPassword(AUserID : string, APassword : string); </remarks>
    public class SystemSetPasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LUserID = (string)AArguments[0];
			AProgram.Plan.CheckAuthorized(LUserID);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetUserPassword(LUserID, Schema.SecurityUtility.EncryptPassword((string)AArguments[1]));
			return null;
		}
    }
    
    /// <remarks>operator SetEncryptedPassword(AUserID : string, AEncryptedPassword : string); </remarks>
    public class SystemSetEncryptedPasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LUserID = (string)AArguments[0];
			AProgram.Plan.CheckAuthorized(LUserID);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetUserPassword(LUserID, (string)AArguments[1]);
			return null;
		}
    }

    /// <remarks>operator ChangePassword(AOldPassword : string, ANewPassword : string); </remarks>
    public class SystemChangePasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = AProgram.ServerProcess.ServerSession.User;
			if (String.Compare((string)AArguments[0], Schema.SecurityUtility.DecryptPassword(LUser.Password), true) != 0)
				throw new ServerException(ServerException.Codes.InvalidPassword);

			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetUserPassword(LUser.ID, Schema.SecurityUtility.EncryptPassword((string)AArguments[1]));
			return null;
		}
    }
    
    /// <remarks>operator SetUserName(AUserID : string, AUserName : string); </remarks>
    public class SystemSetUserNameNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LUserID = (string)AArguments[0];
			if (String.Compare(AProgram.ServerProcess.ServerSession.User.ID, LUserID, true) != 0)
				AProgram.Plan.CheckAuthorized(LUserID);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetUserName(LUserID, (string)AArguments[1]);
			return null;
		}
    }
    
    /// <remarks>operator DropUser(AUserID : string); </remarks>
    public class SystemDropUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			
			if ((String.Compare(LUser.ID, Server.Engine.CSystemUserID, true) == 0) || (String.Compare(LUser.ID, Server.Engine.CAdminUserID, true) == 0))
				throw new ServerException(ServerException.Codes.CannotDropSystemUsers);
			else
				AProgram.Plan.CheckRight(Schema.RightNames.DropUser);
				
			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserOwnsObjects(LUser.ID))
				throw new ServerException(ServerException.Codes.UserOwnsObjects, LUser.ID);

			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserOwnsRights(LUser.ID))
				throw new ServerException(ServerException.Codes.UserOwnsRights, LUser.ID);
				
			foreach (ServerSession LSession in AProgram.ServerProcess.ServerSession.Server.Sessions)
				if (String.Compare(LSession.User.ID, LUser.ID, true) == 0)
					throw new ServerException(ServerException.Codes.UserHasOpenSessions, LUser.ID);

			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeleteUser(LUser);
			
			return null;
		}
    }

    /// <remarks>operator UserExists(AUserID : string) : boolean; </remarks>
    public class SystemUserExistsNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			return ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserExists((string)AArguments[0]);
		}
    }
    
    // operator AddUserToRole
    public class SystemAddUserToRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);
				
			AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LRole.GetRight(Schema.RightNames.Alter));
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertUserRole(LUser.ID, LRole.ID);
			return null;
		}
    }

    // operator RemoveUserFromRole
    public class SystemRemoveUserFromRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);
				
			AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LRole.GetRight(Schema.RightNames.Alter));
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeleteUserRole(LUser.ID, LRole.ID);
			return null;
		}
	}
	
    // operator AddGroupToRole(const AGroupName : String, const ARoleName : Name);
    // operator AddGroupToRole(const AGroupName : String, const ARoleName : Name, const AInherited : Boolean);
    public class SystemAddGroupToRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			// Deprecated, stubbed for backwards compatibility
			return null;
		}
    }
    
    /// <remarks>operator GrantRightToRole(ARightName : String, ARoleName : Name); </remarks>
    public class SystemGrantRightToRoleNode : InstructionNode
    {
		public static void GrantRight(Program AProgram, string ARightName, Schema.Role ARole)
		{
			AProgram.Plan.CheckRight(ARole.GetRight(Schema.RightNames.Alter));
			AProgram.Plan.CheckRight(ARightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).GrantRightToRole(ARightName, ARole.ID);
		}
		
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);
			GrantRight(AProgram, (string)AArguments[0], LRole);
			return null;
		}
    }

    /// <remarks>operator SafeGrantRightToRole(ARightName : String, ARoleName : Name); </remarks>
    public class SystemSafeGrantRightToRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], false) as Schema.Role;
			if (LRole != null)
				SystemGrantRightToRoleNode.GrantRight(AProgram, (string)AArguments[0], LRole);
			return null;
		}
    }

	/// <remarks>operator SafeGrantRightToGroup(ARightName : String, AGroupName : String, AInherited : Boolean, AApplyRecursively : Boolean, AIncludeUsers : Boolean); </remarks>
    public class SystemSafeGrantRightToGroupNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			// Deprecated, stubbed for backwards compatibility
			return null;
		}
    }

    /// <remarks>
    ///	operator GrantRightToUser(ARightName : String, AUserID : String); 
    ///	</remarks>
    public class SystemGrantRightToUserNode : InstructionNode
    {
		public static void GrantRight(Program AProgram, Schema.User AUser, string ARightName)
		{
			AProgram.Plan.CheckAuthorized(AUser.ID);
			AProgram.Plan.CheckRight(ARightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).GrantRightToUser(ARightName, AUser.ID);
		}
		
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1]);
			GrantRight(AProgram, LUser, (string)AArguments[0]);
			return null;
		}
    }

    /// <remarks>
    ///	operator SafeGrantRightToUser(ARightName : String, AUserID : String); 
    ///	</remarks>
    public class SystemSafeGrantRightToUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1], false);
			if (LUser != null)
				SystemGrantRightToUserNode.GrantRight(AProgram, LUser, (string)AArguments[0]);
			return null;
		}
    }

    /// <remarks>
    ///	operator RevokeRightFromRole(ARightName : String, AGroupName : String); 
    ///	</remarks>
    public class SystemRevokeRightFromRoleNode : InstructionNode
    {
		public static void RevokeRight(Program AProgram, string ARightName, Schema.Role ARole)
		{
			AProgram.Plan.CheckRight(ARole.GetRight(Schema.RightNames.Alter));
			AProgram.Plan.CheckRight(ARightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RevokeRightFromRole(ARightName, ARole.ID);
		}

		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);
			RevokeRight(AProgram, (string)AArguments[0], LRole);
			return null;
		}
    }

    /// <remarks>
    ///	operator SafeRevokeRightFromRole(ARightName : String, AGroupName : String, AInherited : Boolean); 
    ///	</remarks>
    public class SystemSafeRevokeRightFromRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], false) as Schema.Role;
			if (LRole != null)
				SystemRevokeRightFromRoleNode.RevokeRight(AProgram, (string)AArguments[0], LRole);
			return null;
		}
    }
    
    /// <remarks>
    ///	operator RevokeRightFromUser(ARightName : String, AUserID : String); 
    ///	</remarks>
    public class SystemRevokeRightFromUserNode : InstructionNode
    {
		public static void RevokeRight(Program AProgram, string ARightName, Schema.User AUser)
		{
			AProgram.Plan.CheckAuthorized(AUser.ID);
			AProgram.Plan.CheckRight(ARightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RevokeRightFromUser(ARightName, AUser.ID);
		}
		
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1]);
			RevokeRight(AProgram, (string)AArguments[0], LUser);
			return null;
		}
    }
    
    /// <remarks>
    ///	operator SafeRevokeRightFromUser(ARightName : String, AUserID : String); 
    ///	</remarks>
    public class SystemSafeRevokeRightFromUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1], false);
			if (LUser != null)
				SystemRevokeRightFromUserNode.RevokeRight(AProgram, (string)AArguments[0], LUser);
			return null;
		}
    }
    
    /// <remarks>operator RevertRightForRole(ARightName : Name, ARoleName : Name);</remarks>
    public class SystemRevertRightForRoleNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LRightName = (string)AArguments[0];
			Schema.Role LRole = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Role;
			if (LRole == null)
				throw new CompilerException(CompilerException.Codes.RoleIdentifierExpected);

			AProgram.Plan.CheckRight(LRole.GetRight(Schema.RightNames.Alter));
			AProgram.Plan.CheckRight(LRightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RevertRightForRole(LRightName, LRole.ID);
			return null;
		}
    }
    
    /// <remarks>operator RevertRightForUser(ARightName : String, AUserID : String);</remarks>
    public class SystemRevertRightForUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LRightName = (string)AArguments[0];
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1]);
			AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LRightName);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).RevertRightForUser(LRightName, LUser.ID);
			return null;
		}
    }
    
    /// <remarks>operator SetObjectOwner(AObjectName : Name, AUserID : String); </remarks>
    public class SystemSetObjectOwnerNode : InstructionNode
    {
		private void ChangeObjectOwner(Program AProgram, Schema.CatalogObject AObject, Schema.User AUser)
		{
			AObject.Owner = AUser;
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetCatalogObjectOwner(AObject.ID, AUser.ID);

			if (AObject is Schema.ScalarType)
			{
				Schema.ScalarType LScalarType = (Schema.ScalarType)AObject;
				
				if (LScalarType.EqualityOperator != null)
					ChangeObjectOwner(AProgram, LScalarType.EqualityOperator, AUser);
					
				if (LScalarType.ComparisonOperator != null)
					ChangeObjectOwner(AProgram, LScalarType.ComparisonOperator, AUser);
				
				if (LScalarType.IsSpecialOperator != null)
					ChangeObjectOwner(AProgram, LScalarType.IsSpecialOperator, AUser);
					
				foreach (Schema.Special LSpecial in LScalarType.Specials)
				{
					ChangeObjectOwner(AProgram, LSpecial.Selector, AUser);
					ChangeObjectOwner(AProgram, LSpecial.Comparer, AUser);
				}
				
				#if USETYPEINHERITANCE	
				foreach (Schema.Operator LOperator in LScalarType.ExplicitCastOperators)
					ChangeObjectOwner(AProgram, LOperator, AUser);
				#endif
					
				foreach (Schema.Representation LRepresentation in LScalarType.Representations)
				{
					ChangeObjectOwner(AProgram, LRepresentation.Selector, AUser);
					foreach (Schema.Property LProperty in LRepresentation.Properties)
					{
						ChangeObjectOwner(AProgram, LProperty.ReadAccessor, AUser);
						ChangeObjectOwner(AProgram, LProperty.WriteAccessor, AUser);
					}
				}
			}
		}
		
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.CatalogObject LObject = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[0], false) as Schema.CatalogObject;
			if (LObject == null) 
				throw new Schema.SchemaException(Schema.SchemaException.Codes.CatalogObjectExpected, LObject.Name);

			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1]);
			if (AProgram.Plan.User.ID != LUser.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			if (!LObject.IsOwner(AProgram.Plan.User))
				throw new ServerException(ServerException.Codes.UnauthorizedUser, ErrorSeverity.Environment, AProgram.Plan.User.ID);
			ChangeObjectOwner(AProgram, LObject, LUser);

			return null;
		}
    }

	/// <remarks>operator SetRightOwner(ARightName : Name, AUserID : String); </remarks>
    public class SystemSetRightOwnerNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			string LRightName = (string)AArguments[0];
			Schema.Right LRight = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveRight(LRightName);
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[1]);
			if (LRight.IsGenerated)
				throw new ServerException(ServerException.Codes.CannotDropGeneratedRight, LRight.Name);
				
			if (AProgram.Plan.User.ID != LUser.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			if (!LRight.IsOwner(AProgram.Plan.User.ID))
				throw new ServerException(ServerException.Codes.UnauthorizedUser, ErrorSeverity.Environment, AProgram.Plan.User.ID);

			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetRightOwner(LRightName, LUser.ID);
			return null;
		}
    }

	/// <remarks>operator UserHasRight(AUserID : String, ARightName : Name) : System.Boolean; </remarks>
    public class SystemUserHasRightNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			return ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UserHasRight(LUser.ID, (string)AArguments[1]);
		}
    }

	/// <remarks>operator CreateDeviceUser(AUserID : string, ADeviceName : System.Name, ADeviceUserID : string, ADevicePassword : string); </remarks>
	/// <remarks>operator CreateDeviceUser(AUserID : string, ADeviceName : System.Name, ADeviceUserID : string, ADevicePassword : string, AConnectionString : string); </remarks>
	public class SystemCreateDeviceUserNode : InstructionNode
	{
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeviceUserExists(LDevice, LUser))
				throw new Schema.SchemaException(Schema.SchemaException.Codes.DuplicateDeviceUser, LUser.ID, LDevice.Name);
				
			Schema.DeviceUser LDeviceUser = new Schema.DeviceUser(LUser, LDevice, (string)AArguments[2], Schema.SecurityUtility.EncryptPassword((string)AArguments[3]));
			if ((AArguments.Length == 5) && (AArguments[4] != null))
				LDeviceUser.ConnectionParameters = (string)AArguments[4];				
			
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertDeviceUser(LDeviceUser);	
			return null;
		}
	}

	/// <remarks>operator CreateDeviceUserWithEncryptedPassword(AUserID : String, ADeviceName : System.Name, ADeviceUserID : String, ADevicePassword : String); </remarks>
	/// <remarks>operator CreateDeviceUserWithEncryptedPassword(AUserID : String, ADeviceName : System.Name, ADeviceUserID : String, ADevicePassword : String, AConnectionString : String); </remarks>
	public class SystemCreateDeviceUserWithEncryptedPasswordNode : InstructionNode
	{
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			if (((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeviceUserExists(LDevice, LUser))
				throw new Schema.SchemaException(Schema.SchemaException.Codes.DuplicateDeviceUser, LUser.ID, LDevice.Name);

			Schema.DeviceUser LDeviceUser = new Schema.DeviceUser(LUser, LDevice, (string)AArguments[2], (string)AArguments[3]);
			if ((AArguments.Length == 5) && (AArguments[4] != null))
				LDeviceUser.ConnectionParameters = (string)AArguments[4];

			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).InsertDeviceUser(LDeviceUser);
			return null;
		}
	}

    /// <remarks>operator SetDeviceUserID(AUserID : string, ADeviceName : System.Name, ADeviceUserID : string); </remarks>
    public class SystemSetDeviceUserIDNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetDeviceUserID(LDeviceUser, (string)AArguments[2]);
			//LDeviceUser.DeviceUserID = (string)AArguments[2];
			//((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UpdateDeviceUser(LDeviceUser);
			return null;
		}
    }
    
    /// <remarks>operator SetDeviceUserConnectionParameters(AUserID : string, ADeviceName : System.Name, AConnectionParameters : String); </remarks>
    public class SystemSetDeviceUserConnectionParametersNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetDeviceUserConnectionParameters(LDeviceUser, (string)AArguments[2]);
			//LDeviceUser.ConnectionParameters = (string)AArguments[2];
			//((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UpdateDeviceUser(LDeviceUser);
			return null;
		}
    }
    
    /// <remarks>operator SetDeviceUserPassword(AUserID : string, ADeviceName : System.Name, ADevicePassword : string); </remarks>
    public class SystemSetDeviceUserPasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetDeviceUserPassword(LDeviceUser, Schema.SecurityUtility.EncryptPassword((string)AArguments[2]));
			//LDeviceUser.DevicePassword = Schema.SecurityUtility.EncryptPassword((string)AArguments[2]);
			//((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UpdateDeviceUser(LDeviceUser);
			return null;
		}
    }
    
    /// <remarks>operator ChangeDeviceUserPasswordNode(ADeviceName : System.Name, AOldPassword : string, ANewPassword : string); </remarks>
    public class SystemChangeDeviceUserPasswordNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[0], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			Schema.User LUser = AProgram.ServerProcess.ServerSession.User;
			if (LUser.ID != AProgram.Plan.User.ID)
				throw new ServerException(ServerException.Codes.UnauthorizedUser, ErrorSeverity.Environment, AProgram.Plan.User.ID);
				
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser);
			if (String.Compare((string)AArguments[1], Schema.SecurityUtility.DecryptPassword(LDeviceUser.DevicePassword), true) != 0)
				throw new ServerException(ServerException.Codes.InvalidPassword);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).SetDeviceUserPassword(LDeviceUser, Schema.SecurityUtility.EncryptPassword((string)AArguments[2]));
			//LDeviceUser.DevicePassword = Schema.SecurityUtility.EncryptPassword((string)AArguments[2]);
			//((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).UpdateDeviceUser(LDeviceUser);
			return null;
		}
    }
    
    /// <remarks>operator DropDeviceUser(AUserID : string, ADeviceName : System.Name); </remarks>
    public class SystemDropDeviceUserNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			if (LUser.IsSystemUser())
				AProgram.Plan.CheckRight(Schema.RightNames.MaintainSystemDeviceUsers);
			if (LUser.ID != AProgram.Plan.User.ID)
				AProgram.Plan.CheckAuthorized(LUser.ID);
			AProgram.Plan.CheckRight(LDevice.GetRight(Schema.RightNames.MaintainUsers));
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser);
			((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).DeleteDeviceUser(LDeviceUser);
			return null;
		}
    }

    /// <remarks>operator DeviceUserExists(AUserID : string, ADeviceName : System.Name) : boolean; </remarks>
    public class SystemDeviceUserExistsNode : InstructionNode
    {
		public override object InternalExecute(Program AProgram, object[] AArguments)
		{
			Schema.User LUser = ((ServerCatalogDeviceSession)AProgram.CatalogDeviceSession).ResolveUser((string)AArguments[0]);
			Schema.Device LDevice = Compiler.ResolveCatalogIdentifier(AProgram.Plan, (string)AArguments[1], true) as Schema.Device;
			if (LDevice == null)
				throw new CompilerException(CompilerException.Codes.DeviceIdentifierExpected);
			Schema.DeviceUser LDeviceUser = AProgram.CatalogDeviceSession.ResolveDeviceUser(LDevice, LUser, false);
			return LDeviceUser != null;
		}
    }
}