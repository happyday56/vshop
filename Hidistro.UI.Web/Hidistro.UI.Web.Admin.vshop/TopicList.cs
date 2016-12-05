using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class TopicList : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton Lksave;
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater rpTopic;
		protected void BindTopicList()
		{
			TopicQuery page = new TopicQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortBy = "DisplaySequence",
				SortOrder = SortAction.Asc
			};
			DbQueryResult topicList = VShopHelper.GettopicList(page);
			this.rpTopic.DataSource = topicList.Data;
			this.rpTopic.DataBind();
			this.pager.TotalRecords = topicList.TotalRecords;
		}
		protected void Lksave_Click(object sender, System.EventArgs e)
		{
			foreach (System.Web.UI.WebControls.RepeaterItem item in this.rpTopic.Items)
			{
				int result = 0;
				System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)item.FindControl("txtSequence");
				if (int.TryParse(box.Text.Trim(), out result))
				{
					System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)item.FindControl("Lbtopicid");
					int topicId = System.Convert.ToInt32(label.Text);
					if (VShopHelper.Gettopic(topicId).DisplaySequence != result)
					{
						VShopHelper.SwapTopicSequence(topicId, result);
					}
				}
			}
			this.BindTopicList();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindTopicList();
			}
		}
		protected void rpTopic_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "del")
			{
				int topicId = System.Convert.ToInt32(e.CommandArgument);
				if (VShopHelper.Deletetopic(topicId))
				{
					VShopHelper.RemoveReleatesProductBytopicid(topicId);
					this.ShowMsg("删除成功！", true);
					this.BindTopicList();
				}
			}
		}
	}
}
