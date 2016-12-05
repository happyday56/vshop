namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMemberCard : VWeiXinOAuthTemplatedWebControl
    {
        private HiImage imgBG;
        private HiImage imgQR;
        private Literal litCouponsCount;
        private Literal litExpenditure;
        private Literal litGrade;
        private Literal litLogo;
        private Literal litPoints;
        private Literal litVipCardPrefix;
        private Literal litVipRemark;
        private Panel pnlCoupons;
        private Panel pnlInfo;

        protected override void AttachChildControls()
        {
            this.imgBG = (HiImage) this.FindControl("imgBG");
            this.imgQR = (HiImage) this.FindControl("imgQR");
            this.litLogo = (Literal) this.FindControl("litLogo");
            this.litVipCardPrefix = (Literal) this.FindControl("litVipCardPrefix");
            this.litExpenditure = (Literal) this.FindControl("litExpenditure");
            this.litPoints = (Literal) this.FindControl("litPoints");
            this.litCouponsCount = (Literal) this.FindControl("litCouponsCount");
            this.litGrade = (Literal) this.FindControl("litGrade");
            this.pnlInfo = (Panel) this.FindControl("pnlInfo");
            this.pnlCoupons = (Panel) this.FindControl("pnlCoupons");
            this.litVipCardPrefix = (Literal) this.FindControl("litVipCardPrefix");
            this.litVipRemark = (Literal) this.FindControl("litVipRemark");
            this.pnlCoupons.Visible = false;
            this.pnlInfo.Visible = true;
            decimal expenditure = 0M;
            int userHistoryPoint = 0;
            int count = 0;
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                userHistoryPoint = MemberProcessor.GetUserHistoryPoint(currentMember.UserId);
                expenditure = currentMember.Expenditure;
                DataTable userCoupons = MemberProcessor.GetUserCoupons(currentMember.UserId, 0);
                if (userCoupons != null)
                {
                    count = userCoupons.Rows.Count;
                }
            }
            if ((currentMember != null) && !string.IsNullOrEmpty(currentMember.VipCardNumber))
            {
                this.pnlCoupons.Visible = true;
                this.pnlInfo.Visible = false;
                this.litVipCardPrefix.Text = currentMember.VipCardNumber;
                this.litGrade.Text = MemberProcessor.GetMemberGrade(currentMember.GradeId).Name;
            }
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
            this.imgBG.ImageUrl = masterSettings.VipCardBG;
            this.litLogo.Text = masterSettings.VipCardLogo;
            this.imgQR.ImageUrl = masterSettings.VipCardQR;
            this.litExpenditure.Text = expenditure.ToString("F2");
            this.litPoints.Text = userHistoryPoint.ToString();
            this.litCouponsCount.Text = count.ToString();
            this.litVipRemark.Text = masterSettings.VipRemark;
            PageTitle.AddSiteNameTitle("会员卡");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VMemberCard.html";
            }
            base.OnInit(e);
        }
    }
}

