namespace Hidistro.UI.Common.Controls
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    [PersistChildren(false), ParseChildren(true)]
    public abstract class VActivityidTemplatedWebControl : VshopTemplatedWebControl
    {
        protected VActivityidTemplatedWebControl()
        {
            if (MemberProcessor.GetCurrentMember() == null)
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
                if (masterSettings.IsValidationService)
                {
                    string str = this.Page.Request.QueryString["code"];
                    if (!string.IsNullOrEmpty(str))
                    {
                        string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + str + "&grant_type=authorization_code");
                        if (responseResult.Contains("access_token"))
                        {
                            JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
                            string str3 = this.GetResponseResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");
                            if (str3.Contains("nickname"))
                            {
                                JObject obj3 = JsonConvert.DeserializeObject(str3) as JObject;
                                string generateId = Globals.GetGenerateId();
                                MemberInfo member = new MemberInfo {
                                    GradeId = MemberProcessor.GetDefaultMemberGrade(),
                                    UserName = Globals.UrlDecode(obj3["nickname"].ToString()),
                                    OpenId = obj3["openid"].ToString(),
                                    CreateDate = DateTime.Now,
                                    SessionId = generateId,
                                    SessionEndTime = DateTime.Now.AddYears(10)
                                };
                                MemberProcessor.CreateMember(member);
                                MemberInfo info2 = MemberProcessor.GetMember(generateId);
                                HttpCookie cookie = new HttpCookie("Vshop-Member") {
                                    Value = info2.UserId.ToString(),
                                    Expires = DateTime.Now.AddYears(10)
                                };
                                HttpContext.Current.Response.Cookies.Add(cookie);
                                this.Page.Response.Redirect(HttpContext.Current.Request.Url.ToString());
                            }
                            else
                            {
                                this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
                            }
                        }
                        else
                        {
                            this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
                    {
                        this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
                    }
                    else
                    {
                        string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                        this.Page.Response.Redirect(url);
                    }
                }
            }
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
    }
}

