namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMy : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litBackground;
        private Literal litStoreName;
        private Literal litRealName;
        private Literal litMData;
        private Literal litTodayOrderNum;
        private Literal litRefId;

        private Literal litSJStore;
        private Literal litSJName;

        private Literal litStartDate;
        private Literal litEndDate;

        public int todayOrderNum = 0;

        protected override void AttachChildControls()
        {
            this.litBackground = (Literal)this.FindControl("litBackground");
            this.litStoreName = (Literal)this.FindControl("litStoreName");
            this.litRealName = (Literal)this.FindControl("litRealName");
            this.litMData = (Literal)this.FindControl("litMData");
            this.litTodayOrderNum = (Literal)FindControl("litTodayOrderNum");
            this.litRefId = (Literal)FindControl("litRefId");
            this.litSJStore = (Literal)FindControl("litSJStore");
            this.litSJName = (Literal)FindControl("litSJName");
            this.litStartDate = (Literal)FindControl("litStartDate");
            this.litEndDate = (Literal)FindControl("litEndDate");

            var currentMember = MemberProcessor.GetCurrentMember();
            int currentMemberUserId = Globals.GetCurrentMemberUserId();
            if (currentMember != null)
            {

                DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(currentMemberUserId);
                if (userIdDistributors != null)
                {
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    this.litStoreName.Text = userIdDistributors.StoreName;
                    //this.litRealName.Text = userIdDistributors.UserName;
                    this.litRealName.Text = masterSettings.SiteName;
                    string logo = "";
                    if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                    {
                        logo = userIdDistributors.Logo;
                    }
                    else if (!string.IsNullOrEmpty(currentMember.UserHead))
                    {
                        logo = currentMember.UserHead;
                    }
                    else if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
                    {
                        logo = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
                    }

                    this.litBackground.Text = string.Format("<img src=\"{0}\" />", logo);

                    OrderQuery query = new OrderQuery
                    {
                        UserId = userIdDistributors.UserId,
                        Status = OrderStatus.Today
                    };

                    todayOrderNum = DistributorsBrower.GetDistributorOrderCount(query);

                    string data = Newtonsoft.Json.JsonConvert.SerializeObject(
                       new
                       {
                           //累计收益
                           Leijishouyi = userIdDistributors.AccumulatedIncome.ToString("F2"),
                           //可提现
                           Tixian = userIdDistributors.ReferralBlance.ToString("F2"),
                           //我的金贝
                           Jinbei = currentMember.VirtualPoints,
                           //今日收益
                           Jinrishouyi = DistributorsBrower.GetUserCommissions(userIdDistributors.UserId, DateTime.Now),
                           //累计访问
                           Leijifangwen = userIdDistributors.VistiCounts, 
                           // 今日订单数
                           TodayOrderNum = todayOrderNum
                       });
                    this.litMData.Text = "<script type=\"text/javascript\">var mdata = " + data + ";\r\n$(function(){for(var i in mdata){$(\":contains({\"+i+\"}):last\").text(mdata[i])}});</script>";

                    // 今日订单数
                    this.litTodayOrderNum.Text = todayOrderNum.ToString();

                    this.litRefId.Text = "?ReferralId=" + userIdDistributors.UserId;

                    if (userIdDistributors.IsTempStore == 1)
                    {
                        int tmpProductId = 0;
                        ProductInfo product = DistributorsBrower.GetQRCodeDistProductByPTTypeId(1);
                        if (null != product)
                        {
                            tmpProductId = product.ProductId;
                        }
                        this.litSJStore.Text = "/vshop/ProductDetails.aspx?SJCode=1&PTTypeId=1&ReferralId=" + userIdDistributors.ReferralUserId + "&ReferralUserId=" + userIdDistributors.ReferralUserId + "&ProductId=" + tmpProductId;
                        this.litSJName.Text = "升级店主";
                    }
                    else
                    {
                        this.litSJStore.Text = "/vshop/invite.aspx";
                        this.litSJName.Text = "邀请开店";
                    }

                    this.litStartDate.Text = userIdDistributors.CreateTime.ToString("yyyy-MM-dd");
                    this.litEndDate.Text = userIdDistributors.DeadlineTime.ToString("yyyy-MM-dd");
                }             

            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            PageTitle.AddSiteNameTitle("我");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vmy.html";
            }
            base.OnInit(e);
        }
    }
}

