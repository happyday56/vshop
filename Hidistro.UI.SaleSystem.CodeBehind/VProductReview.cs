namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Comments;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VProductReview : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litProdcutName;
        private Literal litSalePrice;
        private Literal litShortDescription;
        private Literal litSoldCount;
        private int productId;
        private HtmlImage productImage;
        private HyperLink productLink;
        private VshopTemplatedRepeater rptProducts;
        private HtmlInputHidden txtTotal;

        protected override void AttachChildControls()
        {
            int num;
            int num2;
            if (!int.TryParse(this.Page.Request.QueryString["productId"], out this.productId))
            {
                base.GotoResourceNotFound("");
            }
            this.litProdcutName = (Literal) this.FindControl("litProdcutName");
            this.litSalePrice = (Literal) this.FindControl("litSalePrice");
            this.litShortDescription = (Literal) this.FindControl("litShortDescription");
            this.litSoldCount = (Literal) this.FindControl("litSoldCount");
            this.productImage = (HtmlImage) this.FindControl("productImage");
            this.productLink = (HyperLink) this.FindControl("productLink");
            this.txtTotal = (HtmlInputHidden) this.FindControl("txtTotal");
            string str = this.Page.Request["OrderId"];
            string str2 = "";
            if (!string.IsNullOrEmpty(str))
            {
                OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(str);
                if ((orderInfo != null) && (orderInfo.ReferralUserId > 0))
                {
                    str2 = "&&ReferralId=" + orderInfo.ReferralUserId;
                }
            }
            else if (Globals.GetCurrentDistributorId() > 0)
            {
                str2 = "&&ReferralId=" + Globals.GetCurrentDistributorId().ToString();
            }
            ProductInfo product = ProductBrowser.GetProduct(MemberProcessor.GetCurrentMember(), this.productId);
            this.litProdcutName.SetWhenIsNotNull(product.ProductName);
            this.litSalePrice.SetWhenIsNotNull(product.MinSalePrice.ToString("F2"));
            this.litShortDescription.SetWhenIsNotNull(product.ShortDescription);
            this.litSoldCount.SetWhenIsNotNull(product.ShowSaleCounts.ToString());
            this.productImage.Src = product.ThumbnailUrl180;
            this.productLink.NavigateUrl = "ProductDetails.aspx?ProductId=" + product.ProductId + str2;
            if (!int.TryParse(this.Page.Request.QueryString["page"], out num))
            {
                num = 1;
            }
            if (!int.TryParse(this.Page.Request.QueryString["size"], out num2))
            {
                num2 = 20;
            }
            ProductReviewQuery reviewQuery = new ProductReviewQuery {
                productId = this.productId,
                IsCount = true,
                PageIndex = num,
                PageSize = num2,
                SortBy = "ReviewId",
                SortOrder = SortAction.Desc
            };
            this.rptProducts = (VshopTemplatedRepeater) this.FindControl("rptProducts");
            DbQueryResult productReviews = ProductBrowser.GetProductReviews(reviewQuery);
            this.rptProducts.DataSource = productReviews.Data;
            this.rptProducts.DataBind();
            this.txtTotal.SetWhenIsNotNull(productReviews.TotalRecords.ToString());
            PageTitle.AddSiteNameTitle("商品评价");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VProductReview.html";
            }
            base.OnInit(e);
        }
    }
}

