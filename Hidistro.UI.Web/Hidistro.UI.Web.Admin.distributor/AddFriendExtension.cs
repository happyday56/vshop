using Hidistro.ControlPanel.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class AddFriendExtension : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.HiddenField hidpic;
		protected System.Web.UI.WebControls.TextBox txtName;
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			FriendExtensionInfo query = new FriendExtensionInfo
			{
				ExensionImg = this.hidpic.Value,
				ExensiontRemark = this.txtName.Text.Trim()
			};
			if (ProductCommentHelper.InsertFriendExtension(query))
			{
				this.ShowMsg("保存成功", true);
				this.hidpic.Value = "";
				this.txtName.Text = "";
			}
			else
			{
				this.ShowMsg("保存失败", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
	}
}
