namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using Hidistro.UI.SaleSystem.Tags;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VSignUp : VWeiXinOAuthTemplatedWebControl
    {
        private HiddenField hdID;
        private Literal litActivityDesc;
        private Literal litEndDate;
        private Common_PrizeNames litPrizeNames;
        private Literal litStartDate;
        private Panel pnlInfo;

        protected override void AttachChildControls()
        {
            int num;
            int.TryParse(HttpContext.Current.Request.QueryString.Get("id"), out num);
            LotteryTicketInfo lotteryTicket = VshopBrowser.GetLotteryTicket(num);
            if (lotteryTicket == null)
            {
                base.GotoResourceNotFound("");
            }
            if ((lotteryTicket != null) && VshopBrowser.HasSignUp(num, Globals.GetCurrentMemberUserId()))
            {
                HttpContext.Current.Response.Redirect(string.Format("~/vshop/ticket.aspx?id={0}", num));
            }
            if ((lotteryTicket.StartTime > DateTime.Now) || (DateTime.Now > lotteryTicket.EndTime))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "myscript", "<script>$(function(){hideSignUpBtn();alert_h(\"活动还未开始或者已经结束！\",function(){window.location.href=\"/vshop/default.aspx\";});});</script>");
            }
            this.hdID = (HiddenField) this.FindControl("hdID");
            this.pnlInfo = (Panel) this.FindControl("pnlInfo");
            this.litActivityDesc = (Literal) this.FindControl("litActivityDesc");
            this.litPrizeNames = (Common_PrizeNames) this.FindControl("litPrizeNames");
            this.litStartDate = (Literal) this.FindControl("litStartDate");
            this.litEndDate = (Literal) this.FindControl("litEndDate");
            this.hdID.Value = num.ToString();
            this.pnlInfo.Visible = !string.IsNullOrEmpty(lotteryTicket.InvitationCode);
            this.litActivityDesc.Text = lotteryTicket.ActivityDesc;
            this.litPrizeNames.Activity = lotteryTicket;
            this.litStartDate.Text = lotteryTicket.OpenTime.ToString("yyyy年MM月dd日 HH:mm:ss");
            this.litEndDate.Text = lotteryTicket.EndTime.ToString("yyyy年MM月dd日 HH:mm:ss");
            PageTitle.AddSiteNameTitle("抽奖报名");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vSignUp.html";
            }
            base.OnInit(e);
        }
    }
}

