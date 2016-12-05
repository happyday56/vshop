using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hishop.Weixin.MP.Api;
using Newtonsoft.Json;
using NewLife.Log;
using System.Web.Caching;

namespace Hidistro.UI.Common.Controls
{
    public class WeixinSet : Literal
    {
        public string htmlAppID = string.Empty;
        public string htmlToken = string.Empty;
        public string htmlNonceStr = "QoN4FvGbxdTi7mnffL";
        public string htmlTimeStamp = string.Empty;
        public string htmlSignature = string.Empty;
        public string htmlstring1 = string.Empty;
        public string htmlTicket = string.Empty;
        protected override void Render(HtmlTextWriter writer)
        {
            base.Text = "";
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            this.htmlAppID = masterSettings.WeixinAppId;
            string weixinAppSecret = masterSettings.WeixinAppSecret;
            try
            {
                this.htmlToken = this.GetToken(this.htmlAppID, weixinAppSecret);
            }
            catch (Exception)
            {
            }
            this.htmlTimeStamp = WeixinSet.ConvertDateTimeInt(DateTime.Now).ToString();
            this.htmlSignature = this.GetSignature(this.htmlToken, this.htmlTimeStamp, this.htmlNonceStr, out this.htmlstring1);
            //string token = TokenApi.GetToken(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret);
            string token = this.GetToken(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret);
            MenuApi.GetMenus(token);
            base.Text = string.Concat(new string[]
			{
				"<script>wx.config({ debug: " + masterSettings.WXDebug + ",appId: '",
				this.htmlAppID,
				"',timestamp: '",
				this.htmlTimeStamp,
				"', nonceStr: '",
				this.htmlNonceStr,
				"',signature: '",
				this.htmlSignature,
				"',jsApiList: ['checkJsApi','onMenuShareTimeline','onMenuShareAppMessage','onMenuShareQQ','onMenuShareWeibo']});</script>"
			});

            //XTrace.WriteLine("-------微信接口配置相关信息--------微信APPID：" + this.htmlAppID + "-----微信APPSECRET：" + weixinAppSecret + "-----微信TOKEN：" + this.htmlToken + "-----微信时间：" + this.htmlTimeStamp + "----微信签名：" + this.htmlSignature + "-----TOKEN2：" + token + "-----微信内容：" + base.Text);

            base.Render(writer);
        }
        public string GetSignature(string token, string timestamp, string nonce, out string str)
        {
            string str2 = this.Page.Request.Url.ToString();
            string jsApi_ticket = this.GetJsApi_ticket(token);
            //XTrace.WriteLine("-------微信接口配置相关信息--------jsapi_ticket：" + jsApi_ticket + "  微信Nonce：" + nonce);
            this.htmlTicket = jsApi_ticket;
            string text = "jsapi_ticket=" + jsApi_ticket;
            string text2 = "noncestr=" + nonce;
            string text3 = "timestamp=" + timestamp;
            string text4 = "url=" + str2;
            string[] value = new string[]
			{
				text,
				text2,
				text3,
				text4
			};
            str = string.Join("&", value);
            string text5 = str;
            text5 = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return text5.ToLower();
        }
        public string GetJsApi_ticket(string token)
        {
            bool isExist = false;
            string resultJsApiTicket = string.Empty;

            JsApiTicketBag jat = HiCache.Get("DataCache-JsApiTicket") as JsApiTicketBag;
            if (null == jat || string.IsNullOrEmpty(jat.JsApiTicket))
            {
                jat = new JsApiTicketBag();

                isExist = true;
            }
            else
            {
                if (jat.JsApiTicketExpireTime <= DateTime.Now)
                {
                    // 已过期，重新获取
                    isExist = true;
                }
                else
                {
                    resultJsApiTicket = jat.JsApiTicket;
                }
            }

            if (isExist)
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token);
                string value = this.DoGet(url);
                XTrace.WriteLine("-------微信接口配置相关信息GetJsApi_ticket--------jsapi_ticket：" + value + "  微信Token：" + token);
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                if (dictionary != null && dictionary.ContainsKey("ticket"))
                {
                    XTrace.WriteLine("-------微信接口配置相关信息GetJsApi_ticket--------ticket：" + dictionary["ticket"] + "  微信Token：" + token);
                    resultJsApiTicket = dictionary["ticket"];
                }
                jat.JsApiTicket = resultJsApiTicket;
                jat.JsApiTicketExpireTime = DateTime.Now.AddSeconds(7200);

                HiCache.Remove("DataCache-JsApiTicket");
                HiCache.Insert("DataCache-JsApiTicket", jat, 7200, CacheItemPriority.Normal);
            }
                        
            return resultJsApiTicket ;
        }
        public string GetToken(string appid, string secret)
        {
            bool isExist = false;
            string resultToken = string.Empty;

            AccessTokenBag atb = HiCache.Get("DataCache-AccessToken") as AccessTokenBag;
            if (null == atb || string.IsNullOrEmpty(atb.AccessToken))
            {
                atb = new AccessTokenBag();
                atb.AppId = appid;
                atb.AppSecret = secret;

                isExist = true;
            }
            else
            {
                if (atb.AccessTokenExpireTime <= DateTime.Now)
                {
                    // 已过期，重新获取
                    isExist = true;
                }
                else
                {
                    resultToken = atb.AccessToken;
                }
            }

            if (isExist)
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
                string value = this.DoGet(url);
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                if (dictionary != null && dictionary.ContainsKey("access_token"))
                {
                    resultToken = dictionary["access_token"];
                }
                atb.AccessToken = resultToken;
                atb.AccessTokenExpireTime = DateTime.Now.AddSeconds(7200);

                HiCache.Remove("DataCache-AccessToken");
                HiCache.Insert("DataCache-AccessToken", atb, 7200, CacheItemPriority.Normal);

            }

            return resultToken;
        }
        public string DoGet(string url)
        {
            HttpWebRequest webRequest = this.GetWebRequest(url, "GET");
            webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
            Encoding uTF = Encoding.UTF8;
            return this.GetResponseAsString(rsp, uTF);
        }
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader streamReader = null;
            string result;
            try
            {
                stream = rsp.GetResponseStream();
                streamReader = new StreamReader(stream, encoding);
                result = streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return result;
        }
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public HttpWebRequest GetWebRequest(string url, string method)
        {
            int timeout = 100000;
            HttpWebRequest httpWebRequest;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Method = method;
            httpWebRequest.KeepAlive = true;
            //httpWebRequest.UserAgent = "Hishop";
            httpWebRequest.UserAgent = "LAS";
            httpWebRequest.Timeout = timeout;
            return httpWebRequest;
        }
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ticks = long.Parse(timeStamp + "0000000");
            TimeSpan value = new TimeSpan(ticks);
            return dateTime.Add(value);
        }
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - d).TotalSeconds;
        }
        static WeixinSet()
        {
        }
    }
}
