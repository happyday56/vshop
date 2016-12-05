using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public class CnzzStatisticTotal : AdminPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl framcnz;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
			if (!string.IsNullOrEmpty(masterSettings.CnzzPassword) && !string.IsNullOrEmpty(masterSettings.CnzzUsername))
			{
				this.framcnz.Attributes["src"] = "http://wss.cnzz.com/user/companion/92hi_login.php?site_id=" + masterSettings.CnzzUsername + "&password=" + masterSettings.CnzzPassword;
			}
			else
			{
				this.Page.Response.Redirect("cnzzstatisticsset.aspx");
			}
		}
	}
}
