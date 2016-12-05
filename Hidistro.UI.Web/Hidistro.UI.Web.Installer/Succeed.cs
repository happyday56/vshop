using Hidistro.Core;
using Hidistro.Core.Entities;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Installer
{
	public class Succeed : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		protected System.Web.UI.WebControls.Literal txtToken;
		protected System.Web.UI.WebControls.Literal txtUrl;
		private string CreateKey(int len)
		{
			byte[] data = new byte[len];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(data);
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				builder.Append(string.Format("{0:X2}", data[i]));
			}
			return builder.ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request.QueryString["callback"]))
			{
				try
				{
					Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
					configuration.AppSettings.Settings.Remove("Installer");
					configuration.Save();
					base.Response.Write("true");
				}
				catch (System.Exception exception)
				{
					base.Response.Write(exception.Message);
				}
				base.Response.End();
			}
			else
			{
				if (base.Request.UrlReferrer == null || base.Request.UrlReferrer.OriginalString.IndexOf("Install.aspx") < 0)
				{
					base.Response.Redirect("default.aspx");
				}
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				if (string.IsNullOrEmpty(masterSettings.WeixinToken))
				{
					masterSettings.WeixinToken = this.CreateKey(8);
					SettingsManager.Save(masterSettings);
				}
				this.txtToken.Text = masterSettings.WeixinToken;
				this.txtUrl.Text = string.Format("http://{0}/api/wx.ashx", base.Request.Url.Host, this.txtToken.Text);
			}
		}
	}
}
