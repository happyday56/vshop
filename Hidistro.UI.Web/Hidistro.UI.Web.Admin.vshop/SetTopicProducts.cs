using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class SetTopicProducts : AdminPage
	{
		protected ImageLinkButton btnDeleteAll;
		protected System.Web.UI.WebControls.LinkButton btnFinish;
		protected Grid grdTopicProducts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdtopic;
		protected System.Web.UI.WebControls.Literal litPromotionName;
		private int topicId;
		private void BindTopicProducts()
		{
			this.grdTopicProducts.DataSource = VShopHelper.GetRelatedTopicProducts(this.topicId);
			this.grdTopicProducts.DataBind();
		}
		private void btnDeleteAll_Click(object sender, System.EventArgs e)
		{
			if (VShopHelper.RemoveReleatesProductBytopicid(this.topicId))
			{
				base.Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString(), true);
			}
		}
		protected void btnFinish_Click(object sender, System.EventArgs e)
		{
			foreach (System.Web.UI.WebControls.GridViewRow row in this.grdTopicProducts.Rows)
			{
				int result = 0;
				System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)row.FindControl("txtSequence");
				if (int.TryParse(box.Text.Trim(), out result))
				{
					int relatedProductId = System.Convert.ToInt32(this.grdTopicProducts.DataKeys[row.DataItemIndex].Value);
					VShopHelper.UpdateRelateProductSequence(this.topicId, relatedProductId, result);
				}
			}
			this.BindTopicProducts();
		}
		private void grdTopicProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			if (VShopHelper.RemoveReleatesProductBytopicid(this.topicId, (int)this.grdTopicProducts.DataKeys[e.RowIndex].Value))
			{
				base.Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString(), true);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["topicid"], out this.topicId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.hdtopic.Value = this.topicId.ToString();
				this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
				this.grdTopicProducts.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdTopicProducts_RowDeleting);
				if (!this.Page.IsPostBack)
				{
					TopicInfo topic = VShopHelper.Gettopic(this.topicId);
					if (topic == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
						this.litPromotionName.Text = topic.Title;
						this.BindTopicProducts();
					}
				}
			}
		}
	}
}
