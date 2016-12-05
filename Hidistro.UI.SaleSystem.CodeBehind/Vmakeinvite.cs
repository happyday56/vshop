namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using NewLife.Log;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMakeInvite : VWeiXinOAuthTemplatedWebControl
    {

        protected override void AttachChildControls()
        {
            string referralUserid = this.Page.Request.QueryString["ReferralUserId"] ?? "";
            string ptTypeId = this.Page.Request.QueryString["PTTypeId"] ?? "";
            string productId = this.Page.Request.QueryString["ProductId"] ?? "";

            XTrace.WriteLine("QRCode ReferralUserID:" + referralUserid + "------TypeId:" + ptTypeId + "------ProductId:" + productId);

            bool ret = false;
            string ShareUrl = "";

            if (!string.IsNullOrEmpty(referralUserid) 
                && !string.IsNullOrEmpty(ptTypeId) 
                && !string.IsNullOrEmpty(productId))
            {
                int itReferralUserId = 0;
                int.TryParse(referralUserid, out itReferralUserId);

                int itPTTypeId = 0;
                int.TryParse(ptTypeId, out itPTTypeId);

                int itProductId = 0;
                int.TryParse(productId, out itProductId);

                if (itReferralUserId != 0 && itPTTypeId != 0 && itProductId != 0)
                {
                    InviteCode model = new InviteCode()
                    {
                        UserId = itReferralUserId,
                        Code = "",
                        InviteStatus = 0,
                        ProductId = itProductId
                    };
                    string code = string.Empty;
                    ret = InviteBrowser.AddInviteCode2(model, out code);

                    if (ret)
                    {
                        // 跳转页面
                        string host = Globals.HostPath(this.Page.Request.Url);
                        //如果是本地调试或者非80端口的域名
                        if (this.Page.Request.Url.Port != 80 && this.Page.Request.Url.Port != 0)
                        {
                            host += ":" + this.Page.Request.Url.Port;
                        }

                        ShareUrl = host + "/vshop/invite_open.aspx?invitecode=" + code;

                        XTrace.WriteLine("QRCode InviteCode:" + code);

                        this.Page.Response.Redirect(ShareUrl, true);
                    }
                }
            }

            
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vmakeinvite.html";
            }
            base.OnInit(e);
        }
    }
}

