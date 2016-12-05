using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Store;
using System;
using System.Collections.Generic;
using System.Web;
namespace Hidistro.UI.Web.Admin
{
	public class LoginUser : System.Web.IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
        public void ProcessRequest(System.Web.HttpContext context)
        {
            string s = "";
            string username = "";
            string str2 = context.Request.QueryString["action"];
            if (!string.IsNullOrEmpty(str2) && str2 == "login")
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            
                var returnModel = new
                {
                    sitename = masterSettings.SiteName,
                    username = username,
                    privileges = new List<PrivilegeModule>()
                };

                ManagerInfo manager = ManagerHelper.GetCurrentManager();
                if (null != manager)
                {
                    username = manager.UserName;

                    var privilegeCtx = PrivilegeContext.GetPrivilegeModule();
                    var currentPrivilege = ManagerHelper.GetPrivilegeByRoles(manager.RoleId);

                    foreach (var r in privilegeCtx)
                    {
                        if (null != currentPrivilege)
                        {
                            if (currentPrivilege.Contains(r.Privilege))
                            {
                                returnModel.privileges.Add(r);
                            }
                        }
                    }
                }
                s = LitJson.JsonMapper.ToJson(returnModel);

                //s = string.Concat(new string[]
                //{
                //    "{\"sitename\":\"",
                //    masterSettings.SiteName,
                //    "\",\"username\":\"",
                //    username,
                //    "\"}"
                //});
            }
            context.Response.ContentType = "text/json";
            context.Response.Write(s);
        }
	}
}
