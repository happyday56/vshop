namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VTopics : VshopTemplatedWebControl
    {
        private HiImage imgUrl;
        private Literal litContent;
        private VshopTemplatedRepeater rptProducts;
        private int topicId;

        protected override void AttachChildControls()
        {
            if (!int.TryParse(this.Page.Request.QueryString["TopicId"], out this.topicId))
            {
                base.GotoResourceNotFound("");
            }
            this.imgUrl = (HiImage) this.FindControl("imgUrl");
            this.litContent = (Literal) this.FindControl("litContent");
            this.rptProducts = (VshopTemplatedRepeater) this.FindControl("rptProducts");
            TopicInfo topic = VshopBrowser.GetTopic(this.topicId);
            if (topic == null)
            {
                base.GotoResourceNotFound("");
            }
            this.imgUrl.ImageUrl = topic.IconUrl;
            this.litContent.Text = topic.Content;
            this.rptProducts.DataSource = ProductBrowser.GetTopicProducts(MemberProcessor.GetCurrentMember(), this.topicId, new VTemplateHelper().GetTopicProductMaxNum());
            this.rptProducts.DataBind();
            PageTitle.AddSiteNameTitle("专题详情");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VTopics.html";
            }
            base.OnInit(e);
        }
    }
}

