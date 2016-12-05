using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Core.Enums;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class EditFriendExtension : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSave;
		private int ExtensionId;
		protected System.Web.UI.WebControls.HiddenField hidpic;
		protected System.Web.UI.WebControls.HiddenField hidpicdel;
		protected System.Web.UI.WebControls.TextBox txtName;
		private void BindData()
		{
			FriendExtensionQuery entity = new FriendExtensionQuery
			{
				PageIndex = 1,
				PageSize = 1,
				SortOrder = SortAction.Desc,
				SortBy = "ExtensionId"
			};
			Globals.EntityCoding(entity, true);
			entity.ExtensionId = this.ExtensionId;
			System.Data.DataTable data = new System.Data.DataTable();
			data = (System.Data.DataTable)ProductCommentHelper.FriendExtensionList(entity).Data;
			if (data.Rows.Count > 0)
			{
				this.txtName.Text = data.Rows[0]["ExensiontRemark"].ToString();
				this.hidpic.Value = data.Rows[0]["ExensionImg"].ToString();
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			FriendExtensionInfo query = new FriendExtensionInfo
			{
				ExensionImg = this.hidpic.Value,
				ExensiontRemark = this.txtName.Text.Trim(),
				ExtensionId = int.Parse(base.Request.QueryString["ExtensionId"])
			};
			if (ProductCommentHelper.UpdateFriendExtension(query))
			{
				this.ShowMsg("修改成功", true);
				if (!string.IsNullOrEmpty(this.hidpicdel.Value))
				{
					string[] array = this.hidpicdel.Value.Split(new char[]
					{
						'|'
					});
					for (int i = 0; i < array.Length; i++)
					{
						string str = array[i];
						string path = str;
						path = base.Server.MapPath(path);
						if (System.IO.File.Exists(path))
						{
							System.IO.File.Delete(path);
						}
					}
				}
				this.hidpicdel.Value = "";
			}
			else
			{
				this.ShowMsg("修改失败", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			if (!base.IsPostBack && int.TryParse(base.Request.QueryString["ExtensionId"], out this.ExtensionId))
			{
				this.BindData();
			}
		}
	}
}
