using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Entities.Comments;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class ReplyProductReviews : AdminPage
	{
        protected System.Web.UI.WebControls.Button btnReplyProductReview;
		private int reviewId;
		protected KindeditorControl fckReplyText;
		protected FormatedTimeLabel lblTime;
        protected System.Web.UI.WebControls.Literal litReviewText;
		protected System.Web.UI.WebControls.Literal litUserName;
        protected void btnReplyProductReview_Click(object sender, System.EventArgs e)
		{
            
            ProductReviewInfo productReview = ProductCommentHelper.GetProductReview(this.reviewId);
			if (string.IsNullOrEmpty(this.fckReplyText.Text))
			{
                productReview.ReplyText = null;
			}
			else
			{
                productReview.ReplyText = this.fckReplyText.Text;
			}
            productReview.ReplyUserId = new int?(Globals.GetCurrentManagerUserId());
            productReview.ReplyDate = new System.DateTime?(System.DateTime.Now);
            ValidationResults results = Validation.Validate<ProductReviewInfo>(productReview, new string[]
			{
				"Reply"
			});
			string msg = string.Empty;
			if (!results.IsValid)
			{
				foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
				{
					msg += Formatter.FormatErrorMessage(result.Message);
				}
				this.ShowMsg(msg, false);
			}
			else
			{
                if (ProductCommentHelper.ReplyProductReview(productReview))
				{
					this.fckReplyText.Text = string.Empty;
					this.CloseWindow();
				}
				else
				{
					this.ShowMsg("回复商品评价失败", false);
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["ReviewId"], out this.reviewId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
                this.btnReplyProductReview.Click += new System.EventHandler(this.btnReplyProductReview_Click);
				if (!this.Page.IsPostBack)
				{
                    ProductReviewInfo productReview = ProductCommentHelper.GetProductReview(this.reviewId);
                    if (productReview == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
                        this.litUserName.Text = productReview.UserName;
                        this.litReviewText.Text = productReview.ReviewText;
                        this.lblTime.Time = productReview.ReviewDate;
                        this.fckReplyText.Text = productReview.ReplyText;
					}
				}
			}
		}
	}
}
