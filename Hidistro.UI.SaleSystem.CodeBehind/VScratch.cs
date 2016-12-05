namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.SaleSystem.Tags;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VScratch : VWeiXinOAuthTemplatedWebControl
    {
        private int activityid;
        private HtmlImage bgimg;
        private Literal litActivityDesc;
        private Literal litEndDate;
        private Common_PrizeNames litPrizeNames;
        private Common_PrizeUsers litPrizeUsers;
        private Literal litStartDate;

        protected override void AttachChildControls()
        {
            if (!int.TryParse(this.Page.Request.QueryString["activityid"], out this.activityid))
            {
                base.GotoResourceNotFound("");
            }
            this.bgimg = (HtmlImage) this.FindControl("bgimg");
            this.litActivityDesc = (Literal) this.FindControl("litActivityDesc");
            this.litPrizeNames = (Common_PrizeNames) this.FindControl("litPrizeNames");
            this.litPrizeUsers = (Common_PrizeUsers) this.FindControl("litPrizeUsers");
            this.litStartDate = (Literal) this.FindControl("litStartDate");
            this.litEndDate = (Literal) this.FindControl("litEndDate");
            PageTitle.AddSiteNameTitle("刮刮卡");
            LotteryActivityInfo lotteryActivity = VshopBrowser.GetLotteryActivity(this.activityid);
            if (lotteryActivity == null)
            {
                base.GotoResourceNotFound("");
            }
            this.litStartDate.Text = lotteryActivity.StartTime.ToString("yyyy年MM月dd日 HH:mm:ss");
            this.litEndDate.Text = lotteryActivity.EndTime.ToString("yyyy年MM月dd日 HH:mm:ss");
            if (VshopBrowser.GetUserPrizeCount(this.activityid) >= lotteryActivity.MaxNum)
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "myscript", "<script>$(function(){alert_h(\"亲，不好意思您的抽奖机会已经用完了哦，敬请期待下次活动吧！\",function(){window.location.href=\"/vshop/default.aspx\";});});</script>");
            }
            if ((lotteryActivity.StartTime < DateTime.Now) && (DateTime.Now < lotteryActivity.EndTime))
            {
                this.litActivityDesc.Text = lotteryActivity.ActivityDesc;
                this.litPrizeNames.Activity = lotteryActivity;
                this.litPrizeUsers.Activity = lotteryActivity;
                int userPrizeCount = VshopBrowser.GetUserPrizeCount(this.activityid);
                this.litActivityDesc.Text = this.litActivityDesc.Text + string.Format("您一共有{0}次参与机会，目前还剩{1}次。", lotteryActivity.MaxNum, lotteryActivity.MaxNum - userPrizeCount);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "scriptErro", "<script>$(function(){alert_h(\"活动还未开始或者已经结束！\",function(){window.location.href=\"/vshop/default.aspx\";});});</script>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VScratch.html";
            }
            base.OnInit(e);
        }
    }
}

