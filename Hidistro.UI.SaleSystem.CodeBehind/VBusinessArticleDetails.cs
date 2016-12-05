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
    public class VBusinessArticleDetails : VshopTemplatedWebControl
    {

        private int articelId;
        private Literal litArticleTitle;
        private Literal litPublishTime;
        private Literal litPublishName;
        private Literal litSummary;
        private HtmlImage articleImage;
        private Literal litArtContent;
        private Literal litReviewsCount;

        private Literal litItemParams;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("商学院");

            string str3 = "";

            if (!int.TryParse(this.Page.Request.QueryString["ArticelId"], out this.articelId))
            {
                base.GotoResourceNotFound("");
            }

            this.litArticleTitle = (Literal)this.FindControl("litArticleTitle");
            this.litPublishTime = (Literal)this.FindControl("litPublishTime");
            this.litPublishName = (Literal)this.FindControl("litPublishName");
            this.litSummary = (Literal)this.FindControl("litSummary");
            this.articleImage = (HtmlImage)this.FindControl("articleImage");
            this.litArtContent = (Literal)this.FindControl("litArtContent");
            this.litReviewsCount = (Literal)this.FindControl("litReviewsCount");
            this.litItemParams = (Literal)this.FindControl("litItemParams");

            // 获取商学院文章内容，根据文章ID
            BusinessArticleInfo businessArticle = VShopHelper.GetBusinessArticle(this.articelId);

            if (null != businessArticle)
            {
                this.litArticleTitle.SetWhenIsNotNull(businessArticle.Title);
                this.litPublishTime.SetWhenIsNotNull(businessArticle.AddedDate.ToString("yyyy-MM-dd"));
                this.litPublishName.SetWhenIsNotNull(businessArticle.PublishName);
                this.litSummary.SetWhenIsNotNull(businessArticle.Summary);
                this.litSummary.Visible = false;
                this.articleImage.Visible = false;
                if (!string.IsNullOrEmpty(businessArticle.IconUrl))
                {
                    this.articleImage.Src = businessArticle.IconUrl;
                    //this.articleImage.Visible = true;

                    str3 = Globals.HostPath(HttpContext.Current.Request.Url) + businessArticle.IconUrl;
                }
                this.litArtContent.SetWhenIsNotNull(businessArticle.ArtContent);
                this.litReviewsCount.SetWhenIsNotNull( businessArticle.ReviewCnt.ToString());

                PageTitle.AddSiteNameTitle(businessArticle.Title);

                // 更新访问次数
                VShopHelper.UpdateBusinessArticleVisitCounts(this.articelId);

                this.litItemParams.SetWhenIsNotNull( string.Concat(new object[] { str3, "|", this.litArticleTitle.Text,"|", this.litSummary.Text, "|", HttpContext.Current.Request.Url }));

            }

        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VBusinessArticleDetails.html";
            }
            base.OnInit(e);
        }
    }
}

