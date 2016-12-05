using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    public class VParamConfig : AdminPage
	{
		protected Button btnAdd;
        protected TextBox txtDefaultVirtualPoint;
        protected TextBox txtTempStorePoint;
        protected TextBox txtRecruitCnt;
        protected TextBox txtRecruitCntPoint;
        protected TextBox txtTempStoreSaleAmount;

		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            if (!string.IsNullOrEmpty(this.txtDefaultVirtualPoint.Text.Trim()))
            {
                masterSettings.DefaultVirtualPoint = decimal.Parse(this.txtDefaultVirtualPoint.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtTempStorePoint.Text.Trim()))
            {
                masterSettings.TempStorePoint = decimal.Parse(this.txtTempStorePoint.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtRecruitCnt.Text.Trim()))
            {
                masterSettings.RecruitCnt = int.Parse(this.txtRecruitCnt.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtRecruitCntPoint.Text.Trim()))
            {
                masterSettings.RecruitCntPoint = decimal.Parse(this.txtRecruitCntPoint.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtTempStoreSaleAmount.Text.Trim()))
            {
                masterSettings.TempStoreSaleAmount = decimal.Parse(this.txtTempStoreSaleAmount.Text.Trim());
            }
            
			SettingsManager.Save(masterSettings);

            // 更新配置对应数据库中表数据
            VShopHelper.UpdateParamConfig("DefaultVirtualPoint", masterSettings.DefaultVirtualPoint.ToString());
            VShopHelper.UpdateParamConfig("TempStorePoint", masterSettings.TempStorePoint.ToString());
            VShopHelper.UpdateParamConfig("TempStoreSaleAmount", masterSettings.TempStoreSaleAmount.ToString());

			this.ShowMsg("修改成功", true);
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

                this.txtDefaultVirtualPoint.Text = masterSettings.DefaultVirtualPoint.ToString();
                this.txtRecruitCnt.Text = masterSettings.RecruitCnt.ToString();
                this.txtRecruitCntPoint.Text = masterSettings.RecruitCntPoint.ToString();
                this.txtTempStorePoint.Text = masterSettings.TempStorePoint.ToString();
                this.txtTempStoreSaleAmount.Text = masterSettings.TempStoreSaleAmount.ToString();

			}
		}
	}
}
