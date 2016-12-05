namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.SaleSystem.Tags;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Linq;
    using Hidistro.Entities.VShop;
    using Hidistro.ControlPanel.Store;

    [ParseChildren(true)]
    public class VPLDetails : VshopTemplatedWebControl
    {

        private int pId;
        private Literal litArticleTitle;
        private Literal litSummary;
        private HtmlImage articleImage1;
        private HtmlImage articleImage2;
        private HtmlImage articleImage3;
        private HtmlImage articleImage4;
        private HtmlImage articleImage5;

        private Literal litItemParams;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("商品文案");

            string str3 = "";

            if (!int.TryParse(this.Page.Request.QueryString["ProductId"], out this.pId))
            {
                base.GotoResourceNotFound("");
            }

            this.litArticleTitle = (Literal)this.FindControl("litArticleTitle");
            this.litSummary = (Literal)this.FindControl("litSummary");
            this.articleImage1 = (HtmlImage)this.FindControl("articleImage1");
            this.articleImage2 = (HtmlImage)this.FindControl("articleImage2");
            this.articleImage3 = (HtmlImage)this.FindControl("articleImage3");
            this.articleImage4 = (HtmlImage)this.FindControl("articleImage4");
            this.articleImage5 = (HtmlImage)this.FindControl("articleImage5");
            this.litItemParams = (Literal)this.FindControl("litItemParams");

            // 获取商品文案信息，根据商品ID
            ProductInfo productInfo = ProductBrowser.GetProductLetterInfo(this.pId);

            if (null != productInfo)
            {
                this.litArticleTitle.SetWhenIsNotNull(productInfo.ProductName);
                this.litSummary.SetWhenIsNotNull(productInfo.ProductShortLetter);
                //this.litSummary.Visible = false;
                this.articleImage1.Visible = false;
                this.articleImage2.Visible = false;
                this.articleImage3.Visible = false;
                this.articleImage4.Visible = false;
                this.articleImage5.Visible = false;
                if (!string.IsNullOrEmpty(productInfo.LetterImgUrl1))
                {
                    this.articleImage1.Src = productInfo.LetterImgUrl1;
                    this.articleImage1.Visible = true;

                    str3 = Globals.HostPath(HttpContext.Current.Request.Url) + productInfo.LetterTbUrl60;
                }
                if (!string.IsNullOrEmpty(productInfo.LetterImgUrl2))
                {
                    this.articleImage2.Src = productInfo.LetterImgUrl2;
                    this.articleImage2.Visible = true;
                }
                if (!string.IsNullOrEmpty(productInfo.LetterImgUrl3))
                {
                    this.articleImage3.Src = productInfo.LetterImgUrl3;
                    this.articleImage3.Visible = true;
                }
                if (!string.IsNullOrEmpty(productInfo.LetterImgUrl4))
                {
                    this.articleImage4.Src = productInfo.LetterImgUrl4;
                    this.articleImage4.Visible = true;
                }
                if (!string.IsNullOrEmpty(productInfo.LetterImgUrl5))
                {
                    this.articleImage5.Src = productInfo.LetterImgUrl5;
                    this.articleImage5.Visible = true;
                }

                PageTitle.AddSiteNameTitle(productInfo.ProductName);


                this.litItemParams.SetWhenIsNotNull( string.Concat(new object[] { str3, "|", this.litArticleTitle.Text,"|", this.litSummary.Text, "|", HttpContext.Current.Request.Url.Host + "/Vshop/ProductDetails.aspx?ProductId=" + productInfo.ProductId }));

            }

        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VPLDetails.html";
            }
            base.OnInit(e);
        }
    }
}

