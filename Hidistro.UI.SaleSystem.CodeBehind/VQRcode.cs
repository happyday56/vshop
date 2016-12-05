namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VQRcode : VshopTemplatedWebControl
    {
        //private Literal litgotourl;
        //private Literal litimage;
        //private Literal litItemParams;
        //private Literal litstorename;        
        //private Literal litcurtime;

        public string ShareTitle = "";
        public string ShareDesc = "";
        public string ShareLink = "";
        public string ShareImgUrl = "";
        private Literal litShareurl;
        private Literal liturl;
        private Literal litShareName;



        protected override void AttachChildControls()
        {
            //this.litimage = (Literal)this.FindControl("litimage");
            //this.litgotourl = (Literal)this.FindControl("litgotourl");            
            //this.litstorename = (Literal)this.FindControl("litstorename");
            //this.litItemParams = (Literal)this.FindControl("litItemParams");

            this.litShareurl = (Literal)this.FindControl("litShareurl");
            this.liturl = (Literal)this.FindControl("liturl");
            this.litShareName = (Literal)FindControl("litShareName");

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            string strTitle = "";
            string userName = "";
            string pushUrl = "";
            // 推广用户
            string ReferralUserId = "";
            // 推广类型(1：正常店主 2：钻石会员)
            string PTTypeId = "";
            // 推广商品
            string ProductId = "";

            int tmpPTTypeId = 1;
            int tmpReferralUserId = 0;
            int tmpProductId = 0;

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ReferralUserId"]))
            {
                tmpReferralUserId = int.Parse(this.Page.Request.QueryString["ReferralUserId"]);
            }
            ReferralUserId = "&ReferralUserId=" + tmpReferralUserId;

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["PTTypeId"]))
            {
                tmpPTTypeId = int.Parse(this.Page.Request.QueryString["PTTypeId"]);
            }
            PTTypeId = "&PTTypeId=" + tmpPTTypeId;

            ProductInfo product = DistributorsBrower.GetQRCodeDistProductByPTTypeId(tmpPTTypeId);
            if (null != product)
            {
                tmpProductId = product.ProductId;
            }
            ProductId = "&ProductId=" + tmpProductId;

            long lCurrTime = DateTime.Now.Ticks;            

            // 生成推广链接
            pushUrl = Globals.HostPath(HttpContext.Current.Request.Url) + "/Vshop/makeinvite.aspx?ct=" + lCurrTime + ReferralUserId + PTTypeId + ProductId;

            //if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ReferralUserId"]))
            //{
            //    DistributorsInfo distributorInfo = DistributorsBrower.GetCurrentDistributors(int.Parse(this.Page.Request.QueryString["ReferralUserId"]));
            //    this.liturl.Text = pushUrl;
            //    this.litgotourl.Text = this.liturl.Text;
            //    if (null != distributorInfo)
            //    {
            //        this.litstorename.Text = distributorInfo.StoreName;
            //    }
            //    else
            //    {
            //        this.litstorename.Text = "";
            //    }
            //    MemberInfo memberInfo = DistributorsBrower.GetReferralMember(int.Parse(this.Page.Request.QueryString["ReferralUserId"]));
            //    if (null != memberInfo)
            //    {
            //        userName = memberInfo.RealName;
            //        if (string.IsNullOrEmpty(memberInfo.RealName))
            //        {
            //            userName = memberInfo.UserName;
            //        }
            //    }
            //}
            //this.litimage.Text = Globals.HostPath(HttpContext.Current.Request.Url) + "/Storage/master/QRcord" + masterSettings.SiteFlag + ".jpg";
            ////PageTitle.AddSiteNameTitle(this.litstorename.Text + "店铺二维码");

            //string str = "";
            //if (!string.IsNullOrEmpty(masterSettings.ShopSpreadingCodePic))
            //{
            //    str = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.ShopSpreadingCodePic;
            //}
            //this.litItemParams.Text = str + "|" + masterSettings.ShopSpreadingCodeName + "|" + masterSettings.ShopSpreadingCodeDescription;



            ShareLink = Globals.HostPath(HttpContext.Current.Request.Url) + "/Vshop/QRcode.aspx?ct=" + lCurrTime + ReferralUserId + PTTypeId + ProductId; ;

            this.litShareurl.Text = ShareLink;
            this.liturl.Text = pushUrl;

            if (tmpPTTypeId == 1)
            {
                strTitle = "【店主】";
            }
            else
            {
                strTitle = "【钻石会员】";
            }

            this.litShareName.Text = "邀请" + strTitle + "注册";

            this.ShareTitle = masterSettings.SiteName + strTitle + "邀请注册";
            this.ShareDesc = masterSettings.SiteName + strTitle + "邀请注册";

            this.ShareImgUrl = Globals.HostPath(HttpContext.Current.Request.Url) + "/Storage/master/shareimg" + masterSettings.SiteFlag + ".jpg";

            this.Page.Response.Write(
                string.Format(
@"<script>var shareTitle = '{0}';var shareDesc = '{2}';var shareLink = '{2}';var shareImgUrl = '{3}';</script>"
                , this.ShareTitle, this.ShareDesc, this.ShareLink, this.ShareImgUrl)
                );

            PageTitle.AddSiteNameTitle(masterSettings.SiteName + strTitle + "邀请分享");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VQRcode.html";
            }
            base.OnInit(e);
        }
    }
}

