namespace Hidistro.SqlDal.Store
{
    using Hidistro.Entities;
    using Hidistro.Entities.Store;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class RoleDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public void AddPrivilegeInRoles(int roleId, string strPermissions)
        {
            string[] strArray = strPermissions.Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder(" ");
            int privilegeId = 0;
            if ((strArray != null) && (strArray.Length > 0))
            {
                foreach (string str in strArray)
                {
                    int.TryParse(str, out privilegeId);
                    if (privilegeId > 0)
                        builder.AppendFormat("INSERT INTO Hishop_PrivilegeInRoles (RoleId, Privilege) VALUES (@RoleId, {0}); ", str);
                }
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.String, roleId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool AddRole(RoleInfo role)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO aspnet_Roles (RoleName, Description) VALUES (@RoleName, @Description)");
            this.database.AddInParameter(sqlStringCommand, "RoleName", DbType.String, role.RoleName);
            this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, role.Description);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public void ClearRolePrivilege(int roleId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_PrivilegeInRoles WHERE RoleId = @RoleId");
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.Int32, roleId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool DeleteRole(int roleId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("if( select count(*) from aspnet_Managers where RoleId = @RoleId ) = 0 DELETE FROM aspnet_Roles WHERE RoleId = @RoleId");
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.Int32, roleId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<int> GetPrivilegeByRoles(int roleId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT RoleId,Privilege FROM Hishop_PrivilegeInRoles  WHERE RoleId = @RoleId");
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.Int32, roleId);
            IList<int> list = new List<int>();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add((int)reader["Privilege"]);
                }
            }
            return list;
        }

        public RoleInfo GetRole(int roleId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Roles WHERE RoleId = @RoleId");
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.Int32, roleId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<RoleInfo>(reader);
            }
        }

        public IList<RoleInfo> GetRoles()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Roles");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<RoleInfo>(reader);
            }
        }

        public bool RoleExists(string roleName)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT COUNT(*) FROM aspnet_Roles WHERE RoleName = @RoleName");
            this.database.AddInParameter(sqlStringCommand, "RoleName", DbType.String, roleName);
            return (((int) this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public bool UpdateRole(RoleInfo role)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Roles SET RoleName = @RoleName, Description = @Description WHERE RoleId = @RoleId");
            this.database.AddInParameter(sqlStringCommand, "RoleId", DbType.Int32, role.RoleId);
            this.database.AddInParameter(sqlStringCommand, "RoleName", DbType.String, role.RoleName);
            this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, role.Description);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

