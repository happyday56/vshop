namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Members;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VWinningResults : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litPrizeLevel;
        private HtmlInputText txtName;
        private HtmlInputText txtPhone;

        protected override void AttachChildControls()
        {
            int num;
            int.TryParse(HttpContext.Current.Request.QueryString.Get("activityid"), out num);
            if (VshopBrowser.GetUserPrizeRecord(num) == null)
            {
                this.Context.Response.Redirect(this.Context.Request.Url.ToString().ToLower().Replace("winningresults.aspx?activityid=", "signup.aspx?id="));
                this.Context.Response.End();
            }
            PrizeRecordInfo userPrizeRecord = VshopBrowser.GetUserPrizeRecord(num);
            this.litPrizeLevel = (Literal) this.FindControl("litPrizeLevel");
            if (userPrizeRecord != null)
            {
                this.litPrizeLevel.Text = userPrizeRecord.Prizelevel;
            }
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                this.txtName = (HtmlInputText) this.FindControl("txtName");
                this.txtPhone = (HtmlInputText) this.FindControl("txtPhone");
                this.txtName.Value = currentMember.RealName;
                this.txtPhone.Value = currentMember.CellPhone;
            }
            PageTitle.AddSiteNameTitle("中奖记录");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vWinningResults.html";
            }
            base.OnInit(e);
        }
    }
}

