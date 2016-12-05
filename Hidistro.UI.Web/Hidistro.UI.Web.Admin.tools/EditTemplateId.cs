using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.tools
{
	public class EditTemplateId : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSaveEmailTemplet;
		protected System.Web.UI.WebControls.TextBox txtTemplateId;
		private void btnSaveEmailTemplet_Click(object sender, System.EventArgs e)
		{
			string text = this.txtTemplateId.Text;
			string messageType = base.Request["MessageType"];
			MessageTemplate messageTemplate = VShopHelper.GetMessageTemplate(messageType);
			messageTemplate.WeixinTemplateId = text;
			try
			{
				VShopHelper.UpdateTemplate(messageTemplate);
				this.ShowMsg("保存模板Id成功", true);
			}
			catch
			{
			}
		}
		private void InitShow()
		{
			string messageType = base.Request["MessageType"];
			MessageTemplate messageTemplate = VShopHelper.GetMessageTemplate(messageType);
			this.txtTemplateId.Text = messageTemplate.WeixinTemplateId;
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnSaveEmailTemplet.Click += new System.EventHandler(this.btnSaveEmailTemplet_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.InitShow();
			}
		}
	}
}
