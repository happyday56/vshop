using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Votes)]
	public class Votes : AdminPage
	{
        protected string LocalUrl = string.Empty;
		protected System.Web.UI.WebControls.DataList dlstVote;
		private void BindVote()
		{
			this.dlstVote.DataSource = StoreHelper.GetVotes();
			this.dlstVote.DataBind();
		}
		private void dlstVote_DeleteCommand(object sender, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			if (StoreHelper.DeleteVote(System.Convert.ToInt64(this.dlstVote.DataKeys[e.Item.ItemIndex])) > 0)
			{
				this.BindVote();
				this.ShowMsg("成功删除了选择的投票", true);
			}
			else
			{
				this.ShowMsg("删除投票失败", false);
			}
		}
		private void dlstVote_EditCommand(object sender, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			this.dlstVote.EditItemIndex = e.Item.ItemIndex;
			this.BindVote();
		}
		private void dlstVote_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			if (e.CommandName != "Sort" && e.CommandName == "IsBackup")
			{
				StoreHelper.SetVoteIsBackup(System.Convert.ToInt64(this.dlstVote.DataKeys[e.Item.ItemIndex]));
				this.BindVote();
			}
		}
		private void dlstVote_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
			{
				long voteId = System.Convert.ToInt64(this.dlstVote.DataKeys[e.Item.ItemIndex]);
				VoteInfo voteById = StoreHelper.GetVoteById(voteId);
				System.Collections.Generic.IList<VoteItemInfo> voteItems = StoreHelper.GetVoteItems(voteId);
				for (int i = 0; i < voteItems.Count; i++)
				{
					if (voteById.VoteCounts != 0)
					{
						voteItems[i].Percentage = decimal.Parse((voteItems[i].ItemCount / voteById.VoteCounts * 100m).ToString("F", System.Globalization.CultureInfo.InvariantCulture));
					}
					else
					{
						voteItems[i].Percentage = 0m;
					}
				}
				System.Web.UI.WebControls.GridView view = (System.Web.UI.WebControls.GridView)e.Item.FindControl("grdVoteItem");
				if (view != null)
				{
					view.DataSource = voteItems;
					view.DataBind();
				}
			}
		}
		public string GetUrl(object voteId)
		{
			return string.Concat(new object[]
			{
				"http://",
				Globals.DomainName,
				"/Vshop/Vote.aspx?voteId=",
				voteId
			});
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.LocalUrl = base.Server.UrlEncode(base.Request.Url.ToString());
			this.dlstVote.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dlstVote_ItemDataBound);
			this.dlstVote.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dlstVote_DeleteCommand);
			this.dlstVote.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dlstVote_ItemCommand);
			if (!this.Page.IsPostBack)
			{
				this.BindVote();
			}
		}
	}
}
