using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Net;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public class CnzzStatisticsSet : AdminPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl div_pan1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl div_pan2;
		protected System.Web.UI.WebControls.LinkButton hlinkCreate;
		protected System.Web.UI.WebControls.LinkButton hplinkSet;
		protected System.Web.UI.WebControls.Literal litThemeName;
		protected void hlinkCreate_Click(object sender, System.EventArgs e)
		{
			string host = this.Page.Request.Url.Host;
			string str2 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(host + "A9jkLUxm", "MD5").ToLower();
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://wss.cnzz.com/user/companion/92hi.php?domain=" + host + "&key=" + str2);
			System.IO.Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
			responseStream.ReadTimeout = 100;
			System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);
			string str3 = reader.ReadToEnd().Trim();
			reader.Close();
			if (str3.IndexOf("@") == -1)
			{
				this.ShowMsg("创建账号失败", false);
			}
			else
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
				string[] strArray = str3.Split(new char[]
				{
					'@'
				});
				masterSettings.CnzzUsername = strArray[0];
				masterSettings.CnzzPassword = strArray[1];
				masterSettings.EnabledCnzz = false;
				this.div_pan1.Visible = false;
				this.div_pan2.Visible = true;
				this.hplinkSet.Text = "开启统计功能";
				SettingsManager.Save(masterSettings);
				this.ShowMsg("创建账号成功", true);
			}
		}
		protected void hplinkSet_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
			this.div_pan1.Visible = false;
			this.div_pan2.Visible = true;
			if (masterSettings.EnabledCnzz)
			{
				masterSettings.EnabledCnzz = false;
				this.hplinkSet.Text = "开启统计功能";
				SettingsManager.Save(masterSettings);
				this.ShowMsg("关闭统计功能成功", true);
			}
			else
			{
				masterSettings.EnabledCnzz = true;
				this.hplinkSet.Text = "关闭统计功能";
				SettingsManager.Save(masterSettings);
				this.ShowMsg("开启统计功能成功", true);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.hlinkCreate.Click += new System.EventHandler(this.hlinkCreate_Click);
			this.hplinkSet.Click += new System.EventHandler(this.hplinkSet_Click);
			if (!base.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
				if (string.IsNullOrEmpty(masterSettings.CnzzPassword) || string.IsNullOrEmpty(masterSettings.CnzzUsername))
				{
					this.div_pan1.Visible = true;
					this.div_pan2.Visible = false;
				}
				else
				{
					this.div_pan1.Visible = false;
					this.div_pan2.Visible = true;
					if (masterSettings.EnabledCnzz)
					{
						this.hplinkSet.Text = "关闭统计功能";
					}
					else
					{
						this.hplinkSet.Text = "开启统计功能";
					}
				}
			}
		}
	}
}
