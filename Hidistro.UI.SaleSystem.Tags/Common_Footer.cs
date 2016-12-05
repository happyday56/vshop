namespace Hidistro.UI.SaleSystem.Tags
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Common_Footer : VshopTemplatedWebControl
    {
        private HyperLink hyperindex;
        private Literal lblStyle;
        private Literal litDistrbutorTitle;
        private Literal litDistrbutorUrl;
        private Panel paneldistributor;

        protected override void AttachChildControls()
        {
            this.hyperindex = (HyperLink) this.FindControl("hyperindex");
            this.litDistrbutorTitle = (Literal) this.FindControl("litDistrbutorTitle");
            this.litDistrbutorUrl = (Literal) this.FindControl("litDistrbutorUrl");
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            this.litDistrbutorUrl.Text = "ApplicationDescription.aspx";
            this.lblStyle = (Literal) this.FindControl("lblStyle");
            this.paneldistributor = (Panel) this.FindControl("paneldistributor");
            if (this.Page.Session["stylestatus"] != null)
            {
                this.lblStyle.Text = this.Page.Session["stylestatus"].ToString();
            }
            if (currentMember != null)
            {
                DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(currentMember.UserId);
                if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                {
                    this.litDistrbutorTitle.Text = "店铺管理";
                    this.litDistrbutorUrl.Text = "DistributorValid.aspx";
                }
            }
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            MemberInfo info3 = MemberProcessor.GetCurrentMember();
            decimal expenditure = 0M;
            DistributorsInfo info4 = null;
            if ((info3 != null) && (info3.UserId > 0))
            {
                info4 = DistributorsBrower.GetUserIdDistributors(info3.UserId);
                expenditure = info3.Expenditure;
            }
            this.paneldistributor.Visible = (masterSettings.IsRequestDistributor && (expenditure >= masterSettings.FinishedOrderMoney)) || ((info4 != null) && (info4.UserId > 0));
            int currentDistributorId = Globals.GetCurrentDistributorId();
            if ((this.hyperindex != null) && (currentDistributorId > 0))
            {
                this.hyperindex.NavigateUrl = "Default.aspx?ReferralId=" + currentDistributorId;
            }

            // 2015-10-21 全婴汇取消申请开店和店铺管理
            if (masterSettings.SiteFlag.Equals("las"))
            {
                this.paneldistributor.Visible = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "tags/skin-Common_Footer.html";
            }
            base.OnInit(e);
        }
    }
}

