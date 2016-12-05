using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class SaleService : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected KindeditorControl fkContent;
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.SaleService = this.fkContent.Text;
			SettingsManager.Save(masterSettings);
			this.ShowMsg("修改成功", true);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.fkContent.Text = masterSettings.SaleService;
			}
		}
	}
}
