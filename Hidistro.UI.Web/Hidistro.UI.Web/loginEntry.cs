using Hidistro.Core;
using System;
using System.Web.UI;
namespace Hidistro.UI.Web
{
	public class loginEntry : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string str = this.Page.Request.QueryString["returnUrl"].ToLower();
			if (!string.IsNullOrEmpty(str) && str.StartsWith(Globals.GetSiteUrls().Locations["admin"].ToLower()))
			{
				if (str.EndsWith("/admin") || str.EndsWith("/admin/"))
				{
					str = "/admin/default.aspx";
				}
				base.Response.Redirect(Globals.GetAdminAbsolutePath("/login.aspx?returnUrl=" + str), true);
			}
			else
			{
				base.Response.Redirect(Globals.GetSiteUrls().Login, true);
			}
		}
	}
}
