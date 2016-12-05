namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.ControlPanel.Store;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Linq;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VActivitieDetails : VshopTemplatedWebControl
    {
        private Literal litActivitieDetail;
        private Literal litTitle;
        private Literal litStartTime;
        private Literal litEndTIme;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("活动详情");
            this.litTitle = (Literal)this.FindControl("litTitle");
            this.litActivitieDetail = (Literal)this.FindControl("litActivitieDetail");
            this.litStartTime = (Literal)this.FindControl("litStartTime");
            this.litEndTIme = (Literal)this.FindControl("litEndTIme");
            var dataList = VShopHelper.GetActivitiesInfo(this.Page.Request.QueryString["id"] ?? "0");
            if (dataList.Count > 0)
            {
                this.litActivitieDetail.Text = dataList.FirstOrDefault().ActivitiesDescription;
                this.litTitle.Text = dataList.FirstOrDefault().ActivitiesName;
                this.litStartTime.Text = dataList.FirstOrDefault().StartTime.ToString("yyyy-MM-dd");
                this.litEndTIme.Text = dataList.FirstOrDefault().EndTIme.ToString("yyyy-MM-dd");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VActivitieDetails.html";
            }
            base.OnInit(e);
        }
    }
}

