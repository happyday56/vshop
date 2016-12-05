using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class OffLinePay : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected KindeditorControl fkContent;
		protected YesNoRadioButtonList radEnableOffLinePay;
		protected YesNoRadioButtonList radEnablePro;
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.OffLinePayContent = this.fkContent.Text;
			masterSettings.EnableOffLineRequest = this.radEnableOffLinePay.SelectedValue;
			masterSettings.EnablePodRequest = this.radEnablePro.SelectedValue;
			SettingsManager.Save(masterSettings);
			this.ShowMsg("修改成功", true);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.fkContent.Text = masterSettings.OffLinePayContent;
				this.radEnableOffLinePay.SelectedValue = masterSettings.EnableOffLineRequest;
				this.radEnablePro.SelectedValue = masterSettings.EnablePodRequest;
			}
		}
	}
}
