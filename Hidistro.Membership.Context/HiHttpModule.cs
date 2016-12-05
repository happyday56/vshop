using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
namespace Hidistro.Membership.Context
{
	public class HiHttpModule : IHttpModule
	{
		//private ApplicationType currentApplicationType;
		private bool applicationInstalled;
		private static readonly Regex urlReg = new Regex("(loginentry.aspx|login.aspx|logout.aspx|resourcenotfound.aspx|verifycodeimage.aspx|SendPayment.aspx|PaymentReturn_url|PaymentNotify_url|InpourReturn_url|InpourNotify_url)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
		public string ModuleName
		{
			get
			{
				return "HiHttpModule";
			}
		}
		public void Init(HttpApplication application)
		{
			if (null != application)
			{
				application.BeginRequest += new System.EventHandler(this.Application_BeginRequest);
				application.AuthorizeRequest += new System.EventHandler(this.Application_AuthorizeRequest);
				this.applicationInstalled = (ConfigurationManager.AppSettings["Installer"] == null);
				this.currentApplicationType = HiConfiguration.GetConfig().AppLocation.CurrentApplicationType;
				this.CheckInstall(application.Context);
				if (this.currentApplicationType != ApplicationType.Installer)
				{
					Jobs.Instance().Start();
					ExtensionContainer.LoadExtensions();
				}
			}
		}
		public void Dispose()
		{
			if (this.currentApplicationType != ApplicationType.Installer)
			{
				Jobs.Instance().Stop();
			}
		}
		private void CheckInstall(HttpContext context)
		{
			if (this.currentApplicationType == ApplicationType.Installer && this.applicationInstalled)
			{
				context.Response.Redirect(Globals.GetSiteUrls().Home, false);
			}
			else
			{
				if (!this.applicationInstalled && this.currentApplicationType != ApplicationType.Installer)
				{
					context.Response.Redirect(Globals.ApplicationPath + "/installer/default.aspx", false);
				}
			}
		}
		private void Application_BeginRequest(object source, System.EventArgs e)
		{
			this.currentApplicationType = HiConfiguration.GetConfig().AppLocation.CurrentApplicationType;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (context.Request.RawUrl.IndexOfAny(new char[]
			{
				'<',
				'>',
				'\'',
				'"'
			}) != -1)
			{
				context.Response.Redirect(context.Request.RawUrl.Replace("<", "%3c").Replace(">", "%3e").Replace("'", "%27").Replace("\"", "%22"), false);
			}
			else
			{
				this.CheckInstall(context);
				if (this.currentApplicationType != ApplicationType.Installer)
				{
					HiHttpModule.CheckSSL(HiConfiguration.GetConfig().SSL, context);
					HiContext.Create(context, new UrlReWriterDelegate(HiHttpModule.ReWriteUrl));
				}
			}
		}
		private void Application_AuthorizeRequest(object source, System.EventArgs e)
		{
			if (this.currentApplicationType != ApplicationType.Installer)
			{
				HttpApplication httpApplication = (HttpApplication)source;
				HttpContext context = httpApplication.Context;
				HiContext current = HiContext.Current;
				if (context.Request.IsAuthenticated)
				{
					string name = context.User.Identity.Name;
					if (name != null)
					{
						string[] rolesForUser = Roles.GetRolesForUser(name);
						if (rolesForUser != null && rolesForUser.Length > 0)
						{
							current.RolesCacheKey = string.Join(",", rolesForUser);
						}
					}
				}
			}
		}
		private static void CheckSSL(SSLSettings ssl, HttpContext context)
		{
			if (ssl == SSLSettings.All)
			{
				Globals.RedirectToSSL(context);
			}
		}
		private static bool ReWriteUrl(HttpContext context)
		{
			string path = context.Request.Path;
			string text = UrlReWriteProvider.Instance().RewriteUrl(path, context.Request.Url.Query);
			if (text != null)
			{
				string queryString = null;
				int num = text.IndexOf('?');
				if (num >= 0)
				{
					queryString = ((num < text.Length - 1) ? text.Substring(num + 1) : string.Empty);
					text = text.Substring(0, num);
				}
				context.RewritePath(text, null, queryString);
			}
			return text != null;
		}
	}
}
