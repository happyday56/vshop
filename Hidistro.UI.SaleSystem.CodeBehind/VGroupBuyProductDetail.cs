namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Promotions;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.SaleSystem.Tags;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Linq;

    public class VGroupBuyProductDetail : VshopTemplatedWebControl
    {
        private HtmlInputHidden endTime;
        private Common_ExpandAttributes expandAttr;
        private int groupbuyId;
        private HtmlInputHidden groupBuyMinCount;
        private HtmlInputHidden groupBuySoldCount;
        private Literal leftCount;
        private HyperLink linkDescription;
        private Literal litConsultationsCount;
        private Literal litcontent;
        private Literal litDescription;
        private Literal litGroupbuyDescription;
        private HtmlInputControl litGroupBuyId;
        private Literal litLeftSeconds;
        private Literal litMaxCount;
        private Literal litminCount;
        private Literal litprice;
        private Literal litProdcutName;
        private Literal litReviewsCount;
        private Literal litShortDescription;
        private Literal litSoldCount;
        private Literal minSuccessCount;
        private HtmlInputHidden nowTime;
        private VshopTemplatedRepeater rptProductImages;
        private Literal salePrice;
        private Common_SKUSelector skuSelector;
        private HtmlInputHidden startTime;
        private HtmlInputControl txtProductId;

        protected override void AttachChildControls()
        {
            if (!this.CheckGroupbuyIdExist())
            {
                base.GotoResourceNotFound("");
            }
            this.FindControls();
            this.SetControlsValue(this.groupbuyId);
            PageTitle.AddSiteNameTitle("团购商品详情");
        }

        private bool CheckGroupbuyIdExist()
        {
            return int.TryParse(this.Page.Request.QueryString["GroupBuyId"], out this.groupbuyId);
        }

        private void FindControls()
        {
            this.rptProductImages = (VshopTemplatedRepeater) this.FindControl("rptProductImages");
            this.litProdcutName = (Literal) this.FindControl("litProdcutName");
            this.litShortDescription = (Literal) this.FindControl("litShortDescription");
            this.litDescription = (Literal) this.FindControl("litDescription");
            this.litSoldCount = (Literal) this.FindControl("soldCount");
            this.litprice = (Literal) this.FindControl("price");
            this.litcontent = (Literal) this.FindControl("content");
            this.litminCount = (Literal) this.FindControl("minCount");
            this.litGroupBuyId = (HtmlInputControl) this.FindControl("litGroupbuyId");
            this.litLeftSeconds = (Literal) this.FindControl("leftSeconds");
            this.skuSelector = (Common_SKUSelector) this.FindControl("skuSelector");
            this.linkDescription = (HyperLink) this.FindControl("linkDescription");
            this.expandAttr = (Common_ExpandAttributes) this.FindControl("ExpandAttributes");
            this.salePrice = (Literal) this.FindControl("salePrice");
            this.leftCount = (Literal) this.FindControl("leftCount");
            this.minSuccessCount = (Literal) this.FindControl("minSuccessCount");
            this.txtProductId = (HtmlInputControl) this.FindControl("txtProductId");
            this.litConsultationsCount = (Literal) this.FindControl("litConsultationsCount");
            this.litReviewsCount = (Literal) this.FindControl("litReviewsCount");
            this.litMaxCount = (Literal) this.FindControl("litMaxCount");
            this.startTime = (HtmlInputHidden) this.FindControl("startTime");
            this.endTime = (HtmlInputHidden) this.FindControl("endTime");
            this.groupBuySoldCount = (HtmlInputHidden) this.FindControl("groupBuySoldCount");
            this.groupBuyMinCount = (HtmlInputHidden) this.FindControl("groupBuyMinCount");
            this.litGroupbuyDescription = (Literal) this.FindControl("litGroupbuyDescription");
            this.nowTime = (HtmlInputHidden) this.FindControl("nowTime");
        }

        protected override void OnInit(EventArgs e)
        {
            this.SkinName = (this.SkinName == null) ? "Skin-VGroupBuyProductDetail.html" : this.SkinName;
            base.OnInit(e);
        }

        private void SetControlsValue(int groupbuyId)
        {
            GroupBuyInfo groupBuy = GroupBuyBrowser.GetGroupBuy(groupbuyId);
            ProductInfo product = ProductBrowser.GetProduct(MemberProcessor.GetCurrentMember(), groupBuy.ProductId);
            if (product == null)
            {
                base.GotoResourceNotFound("此商品已不存在");
            }
            if (product.SaleStatus != ProductSaleStatus.OnSale)
            {
                base.GotoResourceNotFound("此商品已下架");
            }
            if (this.rptProductImages != null)
            {
                string locationUrl = "javascript:;";
                SlideImage[] imageArray = new SlideImage[] { new SlideImage(product.ImageUrl1, locationUrl), new SlideImage(product.ImageUrl2, locationUrl), new SlideImage(product.ImageUrl3, locationUrl), new SlideImage(product.ImageUrl4, locationUrl), new SlideImage(product.ImageUrl5, locationUrl) };
                this.rptProductImages.DataSource = from item in imageArray
                    where !string.IsNullOrWhiteSpace(item.ImageUrl)
                    select item;
                this.rptProductImages.DataBind();
            }
            this.litProdcutName.SetWhenIsNotNull(product.ProductName);
            this.litSoldCount.SetWhenIsNotNull(groupBuy.SoldCount.ToString());
            this.litminCount.SetWhenIsNotNull(groupBuy.Count.ToString());
            this.litShortDescription.SetWhenIsNotNull(product.ShortDescription);
            this.litDescription.SetWhenIsNotNull(product.Description);
            this.litprice.SetWhenIsNotNull(groupBuy.Price.ToString("F2"));
            TimeSpan span = (TimeSpan) (groupBuy.EndDate - DateTime.Now);
            this.litLeftSeconds.SetWhenIsNotNull(Math.Ceiling(span.TotalSeconds).ToString());
            this.litcontent.SetWhenIsNotNull(groupBuy.Content);
            this.litGroupBuyId.SetWhenIsNotNull(groupBuy.GroupBuyId.ToString());
            this.skuSelector.ProductId = groupBuy.ProductId;
            this.expandAttr.ProductId = groupBuy.ProductId;
            this.salePrice.SetWhenIsNotNull(product.MaxSalePrice.ToString("F2"));
            this.leftCount.SetWhenIsNotNull((groupBuy.MaxCount - groupBuy.SoldCount).ToString());
            this.linkDescription.SetWhenIsNotNull("/Vshop/ProductDescription.aspx?productId=" + groupBuy.ProductId);
            int num = groupBuy.Count - groupBuy.SoldCount;
            this.minSuccessCount.SetWhenIsNotNull(((num > 0) ? num : 0).ToString());
            this.txtProductId.SetWhenIsNotNull(groupBuy.ProductId.ToString());
            this.groupBuySoldCount.SetWhenIsNotNull(groupBuy.SoldCount.ToString());
            this.litConsultationsCount.SetWhenIsNotNull(ProductBrowser.GetProductConsultationsCount(groupBuy.ProductId, false).ToString());
            this.groupBuyMinCount.SetWhenIsNotNull(groupBuy.Count.ToString());
            this.litReviewsCount.SetWhenIsNotNull(ProductBrowser.GetProductReviewsCount(groupBuy.ProductId).ToString());
            this.litGroupbuyDescription.SetWhenIsNotNull(groupBuy.Content);
            this.litMaxCount.SetWhenIsNotNull(groupBuy.MaxCount.ToString());
            this.nowTime.SetWhenIsNotNull(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            this.startTime.SetWhenIsNotNull(groupBuy.StartDate.ToString("yyyy/MM/dd HH:mm:ss"));
            this.endTime.SetWhenIsNotNull(groupBuy.EndDate.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
}

