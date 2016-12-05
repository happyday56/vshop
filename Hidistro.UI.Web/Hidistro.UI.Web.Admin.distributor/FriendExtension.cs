using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class FriendExtension : AdminPage
	{
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater reFriend;
		private void BindData()
		{
			FriendExtensionQuery entity = new FriendExtensionQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "ExtensionId"
			};
			Globals.EntityCoding(entity, true);
			DbQueryResult result = ProductCommentHelper.FriendExtensionList(entity);
			this.reFriend.DataSource = result.Data;
			this.reFriend.DataBind();
			this.pager.TotalRecords = result.TotalRecords;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.reFriend.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.reFriend_ItemCommand);
			this.reFriend.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.reFriend_ItemDataBound);
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
		private void reFriend_ItemCommand(object sender, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Del")
			{
				if (ProductCommentHelper.DeleteFriendExtension(int.Parse(e.CommandArgument.ToString())) > 0)
				{
					this.BindData();
					this.ShowMsg("删除成功", true);
					System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)e.Item.FindControl("Literal1");
					string text = literal.Text;
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = text.Split(new char[]
						{
							'|'
						});
						for (int i = 0; i < array.Length; i++)
						{
							string str2 = array[i];
							string path = str2;
							path = base.Server.MapPath(path);
							if (System.IO.File.Exists(path))
							{
								System.IO.File.Delete(path);
							}
						}
					}
				}
				else
				{
					this.ShowMsg("删除失败", false);
				}
			}
		}
		private void reFriend_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
			{
				System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)e.Item.FindControl("ImgPic");
				if (!string.IsNullOrEmpty(System.Web.UI.DataBinder.Eval(e.Item.DataItem, "ExensionImg").ToString()))
				{
					string[] strArray = System.Web.UI.DataBinder.Eval(e.Item.DataItem, "ExensionImg").ToString().Split(new char[]
					{
						'|'
					});
					string str = "";
					string[] array = strArray;
					for (int i = 0; i < array.Length; i++)
					{
						string str2 = array[i];
						if (!string.IsNullOrEmpty(str2))
						{
							str = str + "<img src='" + str2 + "' width='60' height='60'/>";
						}
					}
					literal.Text = str;
				}
			}
		}
	}
}
