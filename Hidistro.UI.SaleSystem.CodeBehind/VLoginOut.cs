namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using NewLife.Log;

    [ParseChildren(true)]
    public class VLoginOut : VshopTemplatedWebControl
    {
        protected override void AttachChildControls()
        {
            //MemberInfo member = MemberProcessor.GetMember(Globals.GetCurrentMemberUserId());
            //member.OpenId = "";
            //member.UserHead = "";
            //MemberProcessor.UpdateOpenid(member);
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("Vshop-Member");
            string s = "";
            if (cookie != null)
            {
                s = cookie.Value;
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
            HttpCookie cookie2 = HttpContext.Current.Request.Cookies.Get("Vshop-ReferralId");
            if ((cookie2 != null) && (cookie2.Value == s))
            {
                cookie2.Value = null;
                cookie2.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Set(cookie2);
                DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(int.Parse(s));
                if ((userIdDistributors != null) && (userIdDistributors.ReferralUserId != userIdDistributors.UserId))
                {
                    HttpCookie cookie3 = new HttpCookie("Vshop-ReferralId") {
                        Value = userIdDistributors.ReferralUserId.ToString(),
                        Expires = DateTime.Now.AddYears(1)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie3);
                }
            }
            //XTrace.WriteLine("进入到VLoginOut的登录页面");
            this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/UserLogin.aspx?returnUrl=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()));
        }

        private string GetResponseResult(string url)
        {
            using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create(url).GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VLogout.html";
            }
            base.OnInit(e);
        }
    }
}

