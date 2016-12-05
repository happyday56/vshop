namespace Hidistro.ControlPanel.Store
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Store;
    using Hidistro.SqlDal.Store;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    public static class ManagerHelper
    {
        public static void AddPrivilegeInRoles(int roleId, string strPermissions)
        {
            new RoleDao().AddPrivilegeInRoles(roleId, strPermissions);
        }

        public static bool AddRole(RoleInfo role)
        {
            return new RoleDao().AddRole(role);
        }

        public static void CheckPrivilege(Privilege privilege)
        {
            if (GetCurrentManager() == null)
            {
                HttpContext.Current.Response.Redirect(Globals.GetAdminAbsolutePath("/accessDenied.aspx?privilege=" + privilege.ToString()));
            }
        }

        public static void ClearRolePrivilege(int roleId)
        {
            new RoleDao().ClearRolePrivilege(roleId);
        }

        public static bool Create(ManagerInfo manager)
        {
            return new MessageDao().Create(manager);
        }

        public static bool Delete(int userId)
        {
            if (GetManager(userId).UserId == Globals.GetCurrentManagerUserId())
            {
                return false;
            }
            HiCache.Remove(string.Format("DataCache-Manager-{0}", userId));
            return new MessageDao().DeleteManager(userId);
        }

        public static bool DeleteRole(int roleId)
        {
            return new RoleDao().DeleteRole(roleId);
        }

        public static ManagerInfo GetCurrentManager()
        {
            return GetManager(Globals.GetCurrentManagerUserId());
        }

        public static ManagerInfo GetManager(int userId)
        {
            ManagerInfo manager = HiCache.Get(string.Format("DataCache-Manager-{0}", userId)) as ManagerInfo;
            if (manager == null)
            {
                manager = new MessageDao().GetManager(userId);
                HiCache.Insert(string.Format("DataCache-Manager-{0}", userId), manager, 360, CacheItemPriority.Normal);
            }
            return manager;
        }

        public static ManagerInfo GetManager(string userName)
        {
            return new MessageDao().GetManager(userName);
        }

        public static DbQueryResult GetManagers(ManagerQuery query)
        {
            return new MessageDao().GetManagers(query);
        }

        public static IList<int> GetPrivilegeByRoles(int roleId)
        {
            return new RoleDao().GetPrivilegeByRoles(roleId);
        }

        public static RoleInfo GetRole(int roleId)
        {
            return new RoleDao().GetRole(roleId);
        }

        public static IList<RoleInfo> GetRoles()
        {
            return new RoleDao().GetRoles();
        }

        public static bool RoleExists(string roleName)
        {
            return new RoleDao().RoleExists(roleName);
        }

        public static bool Update(ManagerInfo manager)
        {
            HiCache.Remove(string.Format("DataCache-Manager-{0}", manager.UserId));
            return new MessageDao().Update(manager);
        }

        public static bool UpdateRole(RoleInfo role)
        {
            return new RoleDao().UpdateRole(role);
        }
    }
}

