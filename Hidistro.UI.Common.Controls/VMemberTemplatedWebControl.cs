using System;
using System.Data;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewLife.Log;

namespace Hidistro.UI.Common.Controls
{
    [ParseChildren(true), PersistChildren(false)]
    public abstract class VWeiXinOAuthTemplatedWebControl : VshopTemplatedWebControl
    {

        #region 注释
        //protected VMemberTemplatedWebControl()
        //{
        //    if (((MemberProcessor.GetCurrentMember() == null) || (this.Page.Session["userid"] == null)) || (this.Page.Session["userid"].ToString() != MemberProcessor.GetCurrentMember().UserId.ToString()))
        //    {
        //        SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
        //        if (masterSettings.IsValidationService)
        //        {
        //            string msg = this.Page.Request.QueryString["code"];
        //            this.WriteError(msg, "code值");
        //            if (!string.IsNullOrEmpty(msg))
        //            {
        //                string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + msg + "&grant_type=authorization_code");
        //                if (responseResult.Contains("access_token"))
        //                {
        //                    this.WriteError(responseResult, "access_token");
        //                    JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
        //                    string str3 = this.GetResponseResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");
        //                    if (str3.Contains("nickname"))
        //                    {
        //                        JObject obj3 = JsonConvert.DeserializeObject(str3) as JObject;
        //                        new MemberInfo();
        //                        string generateId = Globals.GetGenerateId();
        //                        MemberInfo member = new MemberInfo {
        //                            GradeId = MemberProcessor.GetDefaultMemberGrade(),
        //                            UserName = Globals.UrlDecode(obj3["nickname"].ToString()),
        //                            OpenId = obj3["openid"].ToString(),
        //                            CreateDate = DateTime.Now,
        //                            SessionId = generateId,
        //                            SessionEndTime = DateTime.Now.AddYears(10)
        //                        };
        //                        if ((MemberProcessor.GetCurrentMember() != null) && string.IsNullOrEmpty(MemberProcessor.GetCurrentMember().OpenId))
        //                        {
        //                            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
        //                            currentMember.OpenId = member.OpenId;
        //                            MemberProcessor.UpdateMember(currentMember);
        //                        }
        //                        MemberProcessor.CreateMember(member);
        //                        MemberProcessor.GetusernameMember(Globals.UrlDecode(obj3["nickname"].ToString()));
        //                        MemberInfo info3 = MemberProcessor.GetMember(generateId);
        //                        HttpCookie cookie = new HttpCookie("Vshop-Member") {
        //                            Value = info3.UserId.ToString(),
        //                            Expires = DateTime.Now.AddYears(10)
        //                        };
        //                        HttpContext.Current.Response.Cookies.Add(cookie);
        //                        this.Page.Session["userid"] = info3.UserId.ToString();
        //                        DistributorsInfo userIdDistributors = new DistributorsInfo();
        //                        userIdDistributors = DistributorsBrower.GetUserIdDistributors(info3.UserId);
        //                        if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
        //                        {
        //                            HttpCookie cookie2 = new HttpCookie("Vshop-ReferralId") {
        //                                Value = userIdDistributors.UserId.ToString(),
        //                                Expires = DateTime.Now.AddYears(1)
        //                            };
        //                            HttpContext.Current.Response.Cookies.Add(cookie2);
        //                        }
        //                        this.Page.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        //                    }
        //                    else
        //                    {
        //                        this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
        //                    }
        //                }
        //                else
        //                {
        //                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
        //                }
        //            }
        //            else if (!string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
        //            {
        //                this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
        //            }
        //            else
        //            {
        //                string str5 = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
        //                this.WriteError(str5, "用户授权的路径");
        //                this.Page.Response.Redirect(str5);
        //            }
        //        }
        //        else if (this.Page.Request.Cookies["Vshop-Member"] == null)
        //        {
        //            this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/UserLogin.aspx");
        //        }
        //    }
        //}

        //private string GetResponseResult(string url)
        //{
        //    using (HttpWebResponse response = (HttpWebResponse)WebRequest.Create(url).GetResponse())
        //    {
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //            {
        //                return reader.ReadToEnd();
        //            }
        //        }
        //    }
        //}

        //public void WriteError(string msg, string OpenId)
        //{
        //    DataTable table = new DataTable
        //    {
        //        TableName = "wxlogin"
        //    };
        //    table.Columns.Add("OperTime");
        //    table.Columns.Add("ErrorMsg");
        //    table.Columns.Add("OpenId");
        //    table.Columns.Add("PageUrl");
        //    DataRow row = table.NewRow();
        //    row["OperTime"] = DateTime.Now;
        //    row["ErrorMsg"] = msg;
        //    row["OpenId"] = OpenId;
        //    row["PageUrl"] = HttpContext.Current.Request.Url;
        //    table.Rows.Add(row);
        //    table.WriteXml(HttpContext.Current.Request.MapPath("/wxlogin.xml"));
        //}
        #endregion
        static readonly bool isWxLogger = false; //微信日志开关

        protected VWeiXinOAuthTemplatedWebControl()
        {

            WeiXinOAuthAttribute oAuth2Attr = Attribute.GetCustomAttribute(this.GetType(), typeof(WeiXinOAuthAttribute)) as WeiXinOAuthAttribute;

            WxLogger("*****************请求进入会员中心*****************");

            //WxLogger("Cookies:" + Globals.GetCurrentMemberUserId());

            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            //XTrace.WriteLine("当前登录的用户是否已成为会员：" + (null != currentMember));
            if (null != oAuth2Attr)
            {
                //XTrace.WriteLine("当前微信授权登录页面：" + oAuth2Attr.WeiXinOAuthPage.ToString());
            }
            else
            {
                //XTrace.WriteLine("当前微信授权登录页面：没有检测到." );
            }


            if (currentMember != null)// || (this.Page.Session["userid"] == null || this.Page.Session["userid"].ToString() != currentMember.UserId.ToString())
            {
                WxLogger(string.Format("        状态信息：**用户“{0}”已登录，中止请求微信**", currentMember.UserName));

                if (null != oAuth2Attr)
                {
                    switch (oAuth2Attr.WeiXinOAuthPage)
                    {
                        case WeiXinOAuthPage.VLogin:
                        case WeiXinOAuthPage.VRegister:
                        case WeiXinOAuthPage.VUserLogin:
                            {
                                Page.Response.Redirect("~/vshop/MemberCenter.aspx", true);
                                break;
                            }
                        case WeiXinOAuthPage.VMemberCenter:
                            {
                                break;
                            }
                        case WeiXinOAuthPage.VDistributorRegister:
                            Page.Response.Redirect("~/vshop/DistributorRegister.aspx", true);
                            break;
                        case WeiXinOAuthPage.VDistributorRequest:
                            Page.Response.Redirect("~/vshop/DistributorValid.aspx", true);
                            break;
                        default:
                            {
                                break;
                            }
                    }
                }

                return;
            }

            //读取配置信息
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);

            if (masterSettings.IsValidationService) //是否启用微信登录
            {

                string code = this.Page.Request.QueryString["code"];

                if (!string.IsNullOrEmpty(code))
                {

                    WxLogger("      状态信息：**从微信网关授权回来**");

                    WxLogger("      code：" + code);

                    #region 取到了code,说明用户同意了授权登录

                    string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + code + "&grant_type=authorization_code");

                    WxLogger("      获取令牌：" + responseResult);

                    if (responseResult.Contains("access_token"))
                    {

                        JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;

                        string openId = obj2["openid"].ToString();//微信用户ＯＰＥＮＩＤ
                        WxLogger("      openid：" + openId);
                        bool bOpenIdExists = this.CheckUserByOpenId(openId);// 判断openid是否存在
                        WxLogger("      openid：" + (bOpenIdExists ? "已绑定用户" : "未绑定用户 "));
                        if (!bOpenIdExists)
                        {
                            string wxUserInfoStr = this.GetResponseResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");

                            WxLogger("      用户信息：" + wxUserInfoStr);
                            if (wxUserInfoStr.Contains("nickname"))
                            {
                                JObject wxUserInfo = JsonConvert.DeserializeObject(wxUserInfoStr) as JObject;
                                if (this.SkipWinxinOpenId(Globals.UrlDecode(wxUserInfo["nickname"].ToString()), wxUserInfo["openid"].ToString(), wxUserInfo["headimgurl"].ToString(), Page.Request["state"]))
                                {
                                    WxLogger("      状态信息：**微信绑定登录成功**");
                                    //跳志到原来页面
                                    // this.Page.Response.Redirect("~/vshop/MemberCenter.aspx", true);
                                    this.Page.Response.Redirect(HttpContext.Current.Request.Url.ToString());
                                }
                                else
                                {
                                    WxLogger("      状态信息：**微信绑定登录失败**");
                                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");
                                }

                            }
                            else
                            {

                                WxLogger("      状态信息：**微信登录失败**");

                                this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");

                            }

                        }
                    }
                    else
                    {

                        WxLogger("      状态信息：**获取信息失败**");

                        this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");

                    }

                    #endregion


                }
                else if (!string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
                {
                    this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/Default.aspx");

                }
                else
                {

                    #region 跳转到微信登录

                    WxLogger("      状态信息：**到微信网关授权**");

                    string state = "";

                    if (System.Web.HttpContext.Current.Request.Cookies["Vshop-ReferralId"] != null)
                    {
                        state = System.Web.HttpContext.Current.Request.Cookies.Get("Vshop-ReferralId").Value;
                    }

                    WxLogger("      state：" + state);

                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&response_type=code&scope=snsapi_userinfo&state=" + (string.IsNullOrWhiteSpace(state) ? "STATE" : state) + "#wechat_redirect";

                    //这里是微信入口
                    this.Page.Response.Redirect(url, true);

                    #endregion

                }
            }
            else
            {

                //if (!string.IsNullOrEmpty(masterSettings.WeixinLoginUrl))
                //{
                //    WxLogger("      状态信息：**跳转到通用登陆接口" + masterSettings.WeixinLoginUrl + "**");
                //    this.Page.Response.Redirect(masterSettings.WeixinLoginUrl);
                //}
                //else
                //{

                WxLogger("      状态信息：**跳转到通用登陆接口" + masterSettings.WeixinLoginUrl + "**");

                #region 加上尾巴

                int ReferralUserId = (null == currentMember ? 0 : currentMember.ReferralUserId);// GetReferralUserId();

                //跳转过来的URL
                Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;

                string returnUrl = HttpContext.Current.Request.Url.ToString();

                if (ReferralUserId > 0 && returnUrl.Contains("?"))
                {
                    returnUrl += "&ReferralUserId=" + ReferralUserId.ToString();
                }
                //else
                //{
                //    returnUrl += "?ReferralUserId=" + ReferralUserId.ToString();
                //}

                #endregion

                // this.Page.Response.Redirect("Login.aspx?returnUrl=" + Globals.UrlEncode(returnUrl));

                if (null != oAuth2Attr)
                {
                    switch (oAuth2Attr.WeiXinOAuthPage)
                    {
                        case WeiXinOAuthPage.VLogin:
                        case WeiXinOAuthPage.VMemberCenter:
                            {
                                //XTrace.WriteLine("进入到VWeiXinOAuthTemplatedWebControl的登录页面");
                                this.Page.Response.Redirect(Globals.ApplicationPath + "/Vshop/UserLogin.aspx?returnUrl=" + Globals.UrlEncode(returnUrl));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

            }




            // }



        }

        /// <summary>
        /// 获取推荐人ID
        /// </summary>
        /// <returns></returns>
        string GetReferralUserId()
        {
            string ReferralUserId = "";

            try
            {
                //跳转过来的URL
                Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;

                if (null != urlReferrer && !string.IsNullOrWhiteSpace(urlReferrer.Query))
                {
                    string querystr = "";
                    if (urlReferrer.Query.StartsWith("?")) querystr = urlReferrer.Query.Substring(1);

                    foreach (string item in querystr.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (item.Contains("ReferralUserId"))
                        {
                            ReferralUserId = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1];
                            break;
                        }
                    }
                }
            }
            catch { }

            return ReferralUserId;
        }

        private string GetResponseResult(string url)
        {
            using (HttpWebResponse response = (HttpWebResponse)WebRequest.Create(url).GetResponse())
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

        /// <summary>
        /// 通过openid获取是否登录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool CheckUserByOpenId(string OpenId)
        {

            IList<MemberInfo> memberLst = Hidistro.ControlPanel.Members.MemberHelper.GetMemdersByOpenIds("'" + OpenId + "'");

            if (memberLst.Count > 0)
            {

                MemberInfo member = memberLst[0];

                if (member != null)
                {

                    HttpCookie cookie = new HttpCookie("Vshop-Member");
                    cookie.Value = member.UserId.ToString();
                    cookie.Expires = DateTime.Now.AddYears(10);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    //this.Page.Session["userid"] = member.UserId.ToString();
                    DistributorsInfo userIdDistributors = new DistributorsInfo();
                    userIdDistributors = DistributorsBrower.GetUserIdDistributors(member.UserId);
                    if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                    {
                        HttpCookie cookie2 = new HttpCookie("Vshop-ReferralId")
                        {
                            Value = userIdDistributors.UserId.ToString(),
                            Expires = DateTime.Now.AddYears(1)
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie2);
                    }

                    return true;

                }


            }

            return false;

        }

        bool SkipWinxinOpenId(string userName, string openId, string headimgurl, string state)
        {

            WxLogger("      状态信息：**　进入　SkipWinxinOpenId　函数体 **");

            bool flag = false;
            try
            {
                string generateId = Globals.GetGenerateId();
                MemberInfo member = new MemberInfo();
                member.GradeId = MemberProcessor.GetDefaultMemberGrade();
                member.UserName = userName;
                member.RealName = userName;
                member.OpenId = openId;
                member.CreateDate = DateTime.Now;
                member.SessionId = generateId;
                member.SessionEndTime = DateTime.Now.AddYears(10);
                member.Email = generateId + "@localhost.com";
                member.SessionId = generateId;
                member.Password = generateId;
                member.RealName = string.Empty;
                member.Address = string.Empty;
                member.UserHead = headimgurl;//用户头像
                #region 设置推荐人
                if (!string.IsNullOrWhiteSpace(state))
                {
                    int referralUserId = 0;
                    if (int.TryParse(state, out referralUserId))
                    {
                        MemberInfo referralMember = MemberProcessor.GetMember(referralUserId);
                        if (null != referralMember)
                        {
                            member.ReferralUserId = referralUserId;
                        }
                        else
                        {
                            member.ReferralUserId = 0;
                        }
                    }
                }

                //System.IO.File.AppendAllText(HiContext.Current.Context.Request.MapPath("~/ReferralUserId.txt"), "用户名：" + userName + ";ReferralUserId＝" + member.ReferralUserId + ";openid=" + openId + Environment.NewLine);

                #endregion

                if (MemberProcessor.CreateMember(member))
                {
                    //MemberProcessor.GetusernameMember(Globals.UrlDecode(userName));
                    //MemberProcessor.GetusernameMember(Globals.UrlDecode(obj3["nickname"].ToString()));
                    //System.IO.File.AppendAllText(Page.Request.MapPath("~/wx.txt"), "***用户创建成功了***" + Environment.NewLine);//获取到openid

                    IList<MemberInfo> memberLst = Hidistro.ControlPanel.Members.MemberHelper.GetMemdersByOpenIds("'" + openId + "'");

                    HttpCookie cookie = new HttpCookie("Vshop-Member");
                    cookie.Value = memberLst[0].UserId.ToString();
                    cookie.Expires = DateTime.Now.AddYears(10);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    //  this.Page.Session["userid"] = member.UserId.ToString();
                    DistributorsInfo userIdDistributors = new DistributorsInfo();
                    userIdDistributors = DistributorsBrower.GetUserIdDistributors(member.UserId);
                    if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                    {
                        HttpCookie cookie2 = new HttpCookie("Vshop-ReferralId")
                        {
                            Value = userIdDistributors.UserId.ToString(),
                            Expires = DateTime.Now.AddYears(1)
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie2);
                    }
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                WxLogger("      异常信息：** SkipWinxinOpenId()函数调用引发的异常：" + ex.Message + "**");
            }


            return flag;
        }

        /// <summary>
        /// 微信日志
        /// </summary>
        /// <param name="log"></param>
        void WxLogger(string log)
        {

            if (!isWxLogger) return;

            string logFile = Page.Request.MapPath("~/wx_login.txt");

            File.AppendAllText(logFile, string.Format("{0}:{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log));

        }

    }
}

