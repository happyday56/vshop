namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using NewLife.Log;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VInvite_success : VWeiXinOAuthTemplatedWebControl
    {
        public string InviteCode = "";
        public string ShareTitle = "XX商城的分享注册";
        public string ShareDesc = "XXXX商城的分享注册描述";
        public string ShareLink = "";
        public string ShareImgUrl = "";
        public string ReferralUserId = "";

        private Literal litShareurl;
        private Literal liturl;

        protected override void AttachChildControls()
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            this.litShareurl = (Literal)this.FindControl("litShareurl");
            this.liturl = (Literal)this.FindControl("liturl");

            InviteCode = this.Page.Request.QueryString["invitecode"];
            if (string.IsNullOrEmpty(InviteCode))
            {
                if (null != currentMember)
                {
                    XTrace.WriteLine("此邀请已被使用时的当前登录会员VInvite_success：" + currentMember.UserId + "------" + currentMember.UserName + "------" + currentMember.RealName + "------" + currentMember.CellPhone);
                }
                base.GotoResourceNotFound("此邀请已被使用");
            }
            //分享邀请注册链接
            string host = Globals.HostPath( this.Page.Request.Url );
            //如果是本地调试或者非80端口的域名
            if (this.Page.Request.Url.Port != 80 && this.Page.Request.Url.Port != 0)
            {
                host += ":" + this.Page.Request.Url.Port;
            }

            ShareLink = host + "/vshop/invite_open.aspx?invitecode=" + InviteCode;
            
            this.litShareurl.Text = ShareLink;
            this.liturl.Text = ShareLink;

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);
            this.ShareTitle = siteSettings.SiteName + "邀请注册";
            this.ShareDesc = siteSettings.SiteName + "邀请注册";

            this.Page.Response.Write(
                string.Format(
@"<script>var inviteCode = '{0}';var shareTitle = '{1}';var shareDesc = '{2}';var shareLink = '{3}';var shareImgUrl = '{4}';</script>"
                , this.InviteCode, this.ShareTitle, this.ShareDesc, this.ShareLink, this.ShareImgUrl)
                );

            PageTitle.AddSiteNameTitle("邀请分享");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vInvite_success.html";
            }
            base.OnInit(e);
        }
    }
}

