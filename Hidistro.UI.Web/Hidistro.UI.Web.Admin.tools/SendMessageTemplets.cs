using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.tools
{
    public class SendMessageTemplets : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnSaveSendSetting;
        protected Grid grdEmailTemplets;
        protected TextBox txtManageOpenID;
        private void btnSaveSendSetting_Click(object sender, System.EventArgs e)
        {
            System.Collections.Generic.List<MessageTemplate> templates = new System.Collections.Generic.List<MessageTemplate>();
            foreach (System.Web.UI.WebControls.GridViewRow row in this.grdEmailTemplets.Rows)
            {
                MessageTemplate item = new MessageTemplate();
                System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkWeixinMessage");
                item.SendWeixin = box.Checked;
                item.MessageType = (string)this.grdEmailTemplets.DataKeys[row.RowIndex].Value;
                templates.Add(item);
            }
            VShopHelper.UpdateSettings(templates);
            this.ShowMsg("保存设置成功", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.ManageOpenID = this.txtManageOpenID.Text.Trim();
			SettingsManager.Save(masterSettings);
            this.ShowMsg("修改成功", true);
		}
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.btnSaveSendSetting.Click += new System.EventHandler(this.btnSaveSendSetting_Click);
            if (!this.Page.IsPostBack)
            {
                // 设置对应的管理员微信OpenID
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                this.txtManageOpenID.Text = masterSettings.ManageOpenID;

                // 绑定消息类型数据
                this.grdEmailTemplets.DataSource = VShopHelper.GetMessageTemplates();
                this.grdEmailTemplets.DataBind();
            }
        }
    }
}
