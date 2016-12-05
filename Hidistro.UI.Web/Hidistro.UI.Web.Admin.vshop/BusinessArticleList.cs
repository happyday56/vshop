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
    public class BusinessArticleList : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton Lksave;
		protected Pager pager;
        protected System.Web.UI.WebControls.Repeater rpBusinessArticle;
        protected void BindBusinessArticleList()
		{
            BusinessArticleQuery page = new BusinessArticleQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortBy = "DisplaySequence",
				SortOrder = SortAction.Asc
			};
            DbQueryResult businessArticleList = VShopHelper.GetBusinessArticleList(page);
            this.rpBusinessArticle.DataSource = businessArticleList.Data;
            this.rpBusinessArticle.DataBind();
            this.pager.TotalRecords = businessArticleList.TotalRecords;
		}
		protected void Lksave_Click(object sender, System.EventArgs e)
		{
            foreach (System.Web.UI.WebControls.RepeaterItem item in this.rpBusinessArticle.Items)
			{
				int result = 0;
				System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)item.FindControl("txtSequence");
				if (int.TryParse(box.Text.Trim(), out result))
				{
                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)item.FindControl("LbArticleId");
                    int articleId = System.Convert.ToInt32(label.Text);
                    if (VShopHelper.GetBusinessArticle(articleId).DisplaySequence != result)
					{
                        VShopHelper.SwapBusinessArticleSequence(articleId, result);
					}
				}
			}
            this.BindBusinessArticleList();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
                this.BindBusinessArticleList();
			}
		}
        protected void rpBusinessArticle_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "del")
			{
                int articleId = System.Convert.ToInt32(e.CommandArgument);
                if (VShopHelper.DeleteBusinessArticle(articleId))
				{
					this.ShowMsg("删除成功！", true);
                    this.BindBusinessArticleList();
				}
			}
		}
	}
}
