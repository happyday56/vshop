namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using NewLife.Log;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;

    [ParseChildren(true)]
    public class VRequestCommissions : VWeiXinOAuthTemplatedWebControl
    {
        private HtmlInputHidden hidmoney;
        private HtmlInputHidden txtmentionnowmoney;
        private Literal litmaxmoney;
        private HtmlAnchor requestcommission;
        private HtmlAnchor requestcommission1;
        private HtmlInputText txtaccount;
        private HtmlInputText txtmoney;
        private HtmlInputText txtmoneyweixin;
        private HtmlInputText txtbankname;
        private HtmlInputText txtbankaddress;
        private HtmlInputText txtaccountname;
        private Literal litPayDisplayStyle;
        private Literal litRegAddress;
        private Literal litRegId;
        

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("申请提现");
            this.litmaxmoney = (Literal) this.FindControl("litmaxmoney");
            this.txtaccount = (HtmlInputText) this.FindControl("txtaccount");
            this.txtbankname = (HtmlInputText)this.FindControl("txtbankname");
            this.txtbankaddress = (HtmlInputText)this.FindControl("txtbankaddress");
            this.txtaccountname = (HtmlInputText)this.FindControl("txtaccountname");
            this.txtmoney = (HtmlInputText) this.FindControl("txtmoney");
            this.txtmoneyweixin = (HtmlInputText) this.FindControl("txtmoneyweixin");
            this.hidmoney = (HtmlInputHidden) this.FindControl("hidmoney");
            this.requestcommission = (HtmlAnchor) this.FindControl("requestcommission");
            this.requestcommission1 = (HtmlAnchor) this.FindControl("requestcommission1");
            this.txtmentionnowmoney = (HtmlInputHidden)this.FindControl("txtmentionnowmoney");
            this.litPayDisplayStyle = (Literal)this.FindControl("litPayDisplayStyle");
            this.litRegAddress = (Literal)this.FindControl("litRegAddress");
            this.litRegId = (Literal)this.FindControl("litRegId");
            DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(Globals.GetCurrentMemberUserId());
            this.txtaccount.Value = userIdDistributors.RequestAccount;
            this.litmaxmoney.Text = userIdDistributors.ReferralBlance.ToString("F2");
            this.txtbankname.Value = userIdDistributors.BankName;
            this.txtbankaddress.Value = userIdDistributors.BankAddress;
            this.txtaccountname.Value = userIdDistributors.AccountName;
            this.txtmentionnowmoney.Value = SettingsManager.GetMasterSettings(false).MentionNowMoney;
            if (string.IsNullOrEmpty(userIdDistributors.RegionAddress))
            {
                this.litRegAddress.Text = "请选择省市区";
                this.litRegId.Text = "";
            }
            else
            {
                this.litRegId.Text = userIdDistributors.RegionAddress;
                this.litRegAddress.Text = RegionHelper.GetFullRegion(int.Parse(userIdDistributors.RegionAddress), " ");
            }
            //this.litRegId.Text = userIdDistributors.RegionAddress;
            //this.litRegAddress.Text = RegionHelper.GetFullRegion(int.Parse(userIdDistributors.RegionAddress), " ");
            
            decimal result = 0M;
            if (decimal.TryParse(SettingsManager.GetMasterSettings(false).MentionNowMoney, out result) && (result > 0M))
            {
                this.litmaxmoney.Text = ((userIdDistributors.ReferralBlance / result) * result).ToString("F2");
                this.txtmoney.Attributes["placeholder"] = "请输入大于等于" + result + "元的金额,并且是整数";
                this.txtmoneyweixin.Attributes["placeholder"] = "请输入大于等于" + result + "元的金额,并且是整数";
                this.hidmoney.Value = result.ToString();
            }
            if (DistributorsBrower.IsExitsCommionsRequest())
            {
                this.requestcommission.Disabled = true;
                this.requestcommission.InnerText = "您的申请正在审核当中";
                this.requestcommission1.Disabled = true;
                this.requestcommission1.InnerText = "您的申请正在审核当中";
            }
            
            SiteSettings settings = SettingsManager.GetMasterSettings(false);
            if (settings.SiteFlag.EqualIgnoreCase("ls"))
            {
                litPayDisplayStyle.Text = "display:block;";
            }
            else
            {
                litPayDisplayStyle.Text = "display:none;";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-RequestCommissions.html";
            }
            base.OnInit(e);
        }
    }
}

