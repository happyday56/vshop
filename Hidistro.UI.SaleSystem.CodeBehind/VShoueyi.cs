namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VShouyi : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litBackground;
        private Literal litStoreName;
        private Literal litRealName;
        private Literal litShouyeData;

        protected override void AttachChildControls()
        {
            this.litBackground = (Literal)this.FindControl("litBackground");
            this.litStoreName = (Literal)this.FindControl("litStoreName");
            this.litRealName = (Literal)this.FindControl("litRealName");
            //收益数据
            this.litShouyeData = (Literal)this.FindControl("litShouyeData");
            int currentMemberUserId = Globals.GetCurrentMemberUserId();
            var currentMember = MemberProcessor.GetCurrentMember();
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

                    string data = Newtonsoft.Json.JsonConvert.SerializeObject(
                   new
                   {
                       //累计收益
                       Leijishouyi = userIdDistributors.AccumulatedIncome.ToString("F2"),
                       //可提现
                       Tixian = userIdDistributors.ReferralBlance.ToString("F2"),
                       //待确认
                       Daiqueren = DistributorsBrower.GetUserCommissionsByCond(userIdDistributors.UserId, 0, new DateTime(2015, 1, 1), false).ToString("F2"),
                       //提现中
                       Tixianzhong = DistributorsBrower.GetUserBalanceDrawRequesByCond(userIdDistributors.UserId, 0, new DateTime(2015, 1, 1), false),
                       //已提现
                       Yitixian = userIdDistributors.ReferralRequestBalance.ToString("F2"),
                       //我的金贝
                       Jinbei = currentMember.VirtualPoints,
                       //我的积分
                       Jifen = currentMember.Points,
                       //今日收益
                       Jinrishouyi = DistributorsBrower.GetUserCommissions(userIdDistributors.UserId, DateTime.Now).ToString("F2"),
                       //本月收益
                       Benyueshouyi = DistributorsBrower.GetUserCommissionsByCond(userIdDistributors.UserId, -1, DateTime.Now, true).ToString("F2"),
                       //上月收益
                       Shangyueshouyi = DistributorsBrower.GetUserCommissionsByCond(userIdDistributors.UserId, -1, DateTime.Now.AddMonths(-1), true).ToString("F2")
                   });

                    this.litShouyeData.Text = "<script type=\"text/javascript\">var mdata = " + data + ";\r\n$(function(){for(var i in mdata){$(\":contains({\"+i+\"}):last\").text(mdata[i])}});</script>";


                }


            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            PageTitle.AddSiteNameTitle("发现");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vShouyi.html";
            }
            base.OnInit(e);
        }
    }
}

