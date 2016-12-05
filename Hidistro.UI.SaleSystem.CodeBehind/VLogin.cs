using System;
using System.Web;
using System.Web.UI;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.Common.Controls;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VLogin)]
    public class VLogin : VWeiXinOAuthTemplatedWebControl// VshopTemplatedWebControl
    {
        private string sessionId;

        protected override void AttachChildControls()
        {
            this.sessionId = this.Page.Request.QueryString["sessionId"];
            if (string.IsNullOrEmpty(this.sessionId))
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }
            else
            {
                MemberInfo member = MemberProcessor.GetMember(this.sessionId);
                if ((member == null) || (member.SessionEndTime < DateTime.Now))
                {
                    this.Page.Response.Redirect("/Vshop/Default.aspx");
                }
                else
                {
                    MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                    if (((currentMember != null) && string.IsNullOrEmpty(currentMember.OpenId)) && (currentMember.UserId != member.UserId))
                    {
                        currentMember.OpenId = member.OpenId;
                        MemberProcessor.UpdateMember(currentMember);
                        MemberProcessor.Delete(member.UserId);
                        member = currentMember;
                    }
                    HttpCookie cookie = new HttpCookie("Vshop-Member")
                    {
                        Value = member.UserId.ToString(),
                        Expires = DateTime.Now.AddYears(10)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    MemberProcessor.SetMemberSessionId(member.SessionId, DateTime.Now, member.OpenId);
                    if (!string.IsNullOrEmpty(member.UserName) && (member.UserName != member.OpenId))
                    {
                        this.Page.Response.Redirect("/Vshop/Default.aspx");
                    }
                    else
                    {
                        PageTitle.AddSiteNameTitle("登录");
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VLogin.html";
            }
            base.OnInit(e);
        }
    }
}

