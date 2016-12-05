using Hidistro.Core.Jobs;
using System;
using System.Web;
using NewLife.Log;

namespace Hidistro.UI.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_AuthenticateRequest(object sender, System.EventArgs e)
		{
		}
		protected void Application_BeginRequest(object sender, System.EventArgs e)
		{
		}
		protected void Application_End(object sender, System.EventArgs e)
		{
			Hidistro.Core.Jobs.Jobs.Instance().Stop();
		}
		protected void Application_Error(object sender, System.EventArgs e)
		{
            Exception ex = HttpContext.Current.Server.GetLastError();
            XTrace.WriteLine("Application Error:" + ex.Message);
            XTrace.WriteLine("Application Error Detail: " + ex.ToString());
		}
		protected void Application_Start(object sender, System.EventArgs e)
		{
            Hidistro.Core.Jobs.Jobs.Instance().Start();
		}
		protected void Session_End(object sender, System.EventArgs e)
		{
		}
		protected void Session_Start(object sender, System.EventArgs e)
		{
		}
	}
}
