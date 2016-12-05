using System;
using System.Web;
using System.Web.UI;
namespace Hidistro.UI.Web.Admin
{
	public class LoginExit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-Manager")
			{
				Expires = System.DateTime.Now
			};
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
			base.Response.Redirect("Login.aspx", true);
		}
	}
}
