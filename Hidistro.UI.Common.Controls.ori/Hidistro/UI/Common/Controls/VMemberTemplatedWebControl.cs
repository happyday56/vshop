namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    [PersistChildren(false), ParseChildren(true)]
    public abstract class VMemberTemplatedWebControl : VshopTemplatedWebControl
    {
        protected VMemberTemplatedWebControl()
        {
            if (((MemberProcessor.GetCurrentMember() == null) || (this.Page.Session["userid"] == null)) || (this.Page.Session["userid"].ToString() != MemberProcessor.GetCurrentMember().UserId.ToString()))
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
                if (masterSettings.IsValidationService)
                {
                    string msg = this.Page.Request.QueryString["code"];
                    this.WriteError(msg, "code值");
                    if (!string.IsNullOrEmpty(msg))
                    {
                        string str2 = this.AAZ0JeEma(2x58C7QQ)d9L4MH("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + msg + "&grant_type=authorization_code");
                        if (str2.Contains("access_token"))
                        {
                            this.WriteError(str2, "access_token");
                            JObject obj2 = JsonConvert.DeserializeObject(str2) as JObject;
                            if (!this.HasLogin(obj2["openid"].ToString()))
                            {
                                string str3 = this.AAZ0JeEma(2x58C7QQ)d9L4MH("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");
                                if (str3.Contains("nickname"))
                                {
                                    JObject obj3 = JsonConvert.DeserializeObject(str3) as JObject;
                                    string generateId = Globals.GetGenerateId();
                                    this.Page.Response.Redirect("UserLogin.aspx?openId=" + obj2["openid"].ToString() + "&nickname=" + obj3["nickname"].ToString() + "&sessionId=" + generateId + "&headimgurl=" + obj3["headimgurl"].ToString() + "&returnUrl=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()));
                                }
                                else
                                {
                                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
                                }
                            }
                            else
                            {
                                MemberInfo openIdMember = MemberProcessor.GetOpenIdMember(obj2["openid"].ToString());
                                HttpCookie cookie = new HttpCookie("Vshop-Member") {
                                    Value = openIdMember.UserId.ToString(),
                                    Expires = DateTime.Now.AddYears(10)
                                };
                                HttpContext.Current.Response.Cookies.Add(cookie);
                                this.Page.Session["userid"] = openIdMember.UserId.ToString();
                                DistributorsInfo userIdDistributors = new DistributorsInfo();
                                userIdDistributors = DistributorsBrower.GetUserIdDistributors(openIdMember.UserId);
                                if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                                {
                                    HttpCookie cookie2 = new HttpCookie("Vshop-ReferralId") {
                                        Value = userIdDistributors.UserId.ToString(),
                                        Expires = DateTime.Now.AddYears(1)
                                    };
                                    HttpContext.Current.Response.Cookies.Add(cookie2);
                                }
                                this.WriteError("会员OpenId已绑定过会员帐号已自动登陆！", obj2["openid"].ToString());
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
                        string str5 = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                        this.WriteError(str5, "用户授权的路径");
                        this.Page.Response.Redirect(str5);
                    }
                }
                else if (this.Page.Request.Cookies["Vshop-Member"] == null)
                {
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/UserLogin.aspx?returnUrl=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()));
                }
            }
        }

        private string AAZ0JeEma(2x58C7QQ)d9L4MH(string text1)
        {
            using (HttpWebResponse response = (HttpWebResponse) WebRequest.Create(text1).GetResponse())
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

        public bool HasLogin(string OpenId)
        {
            MemberInfo openIdMember = MemberProcessor.GetOpenIdMember(OpenId);
            if (openIdMember != null)
            {
                HttpCookie cookie = new HttpCookie(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VgBzAGgAbwBwAC0ATQBlAG0AYgBlAHIA")) {
                    Value = openIdMember.UserId.ToString(),
                    Expires = DateTime.Now.AddYears(10)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                this.Page.Session[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dQBzAGUAcgBpAGQA")] = openIdMember.UserId.ToString();
                return true;
            }
            return false;
        }

        public void WriteError(string msg, string OpenId)
        {
            DataTable table = new DataTable {
                TableName = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwB4AGwAbwBnAGkAbgA=")
            };
            table.Columns.Add(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwBwAGUAcgBUAGkAbQBlAA=="));
            table.Columns.Add(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RQByAHIAbwByAE0AcwBnAA=="));
            table.Columns.Add(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwBwAGUAbgBJAGQA"));
            table.Columns.Add(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UABhAGcAZQBVAHIAbAA="));
            DataRow row = table.NewRow();
            row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwBwAGUAcgBUAGkAbQBlAA==")] = DateTime.Now;
            row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RQByAHIAbwByAE0AcwBnAA==")] = msg;
            row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwBwAGUAbgBJAGQA")] = OpenId;
            row[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UABhAGcAZQBVAHIAbAA=")] = HttpContext.Current.Request.Url;
            table.Rows.Add(row);
            table.WriteXml(HttpContext.Current.Request.MapPath(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB3AHgAbABvAGcAaQBuAC4AeABtAGwA")));
        }
    }
}

