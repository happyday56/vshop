namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VGetSuccess : VshopTemplatedWebControl
    {
        private Panel divNoLogin;
        private Panel divNoNum;
        private Panel divSuccess;
        private HyperLink hlinkLogin;
        private Literal ltExpiryTime;
        private Literal ltGetTotal;
        private Literal ltOrderAmountCanUse;
        private Literal ltRedPagerActivityName;
        private Literal ltRedPagerActivityNameForOrders;

        protected override void AttachChildControls()
        {
            string s = HttpContext.Current.Request.QueryString.Get("m");
            string str2 = HttpContext.Current.Request.QueryString.Get("type");
            this.ltGetTotal = (Literal) this.FindControl("ltGetTotal");
            this.ltOrderAmountCanUse = (Literal) this.FindControl("ltOrderAmountCanUse");
            this.ltExpiryTime = (Literal) this.FindControl("ltExpiryTime");
            this.ltRedPagerActivityName = (Literal) this.FindControl("ltRedPagerActivityName");
            this.ltRedPagerActivityNameForOrders = (Literal) this.FindControl("ltRedPagerActivityNameForOrders");
            this.divNoLogin = (Panel) this.FindControl("divNoLogin");
            this.divNoNum = (Panel) this.FindControl("divNoNum");
            this.divSuccess = (Panel) this.FindControl("divSuccess");
            this.hlinkLogin = (HyperLink) this.FindControl("hlinkLogin");
            switch (str2)
            {
                case "1":
                case "5":
                {
                    int result = 0;
                    int.TryParse(s, out result);
                    if (result > 0)
                    {
                        string orderid = HttpContext.Current.Request.QueryString.Get("orderid");
                        UserRedPagerInfo userRedPagerByOrderIDAndUserID = UserRedPagerBrower.GetUserRedPagerByOrderIDAndUserID(result, orderid);
                        if (userRedPagerByOrderIDAndUserID != null)
                        {
                            this.ltGetTotal.Text = userRedPagerByOrderIDAndUserID.Amount.ToString().Split(new char[] { '.' })[0];
                            this.ltOrderAmountCanUse.Text = userRedPagerByOrderIDAndUserID.OrderAmountCanUse.ToString("F2").Replace(".00", "");
                            this.ltExpiryTime.Text = userRedPagerByOrderIDAndUserID.ExpiryTime.ToString("yyyy-M-d");
                            if (str2 == "5")
                            {
                                this.ltRedPagerActivityName.Text = "该券已经到你的钱包了</div><div class='get-red-explain'><a href='/Vshop/myredpager.aspx'>点击查看</a>";
                            }
                            else
                            {
                                this.ltRedPagerActivityName.Text = userRedPagerByOrderIDAndUserID.RedPagerActivityName ?? "";
                            }
                            this.divSuccess.Visible = true;
                        }
                    }
                    PageTitle.AddSiteNameTitle("成功获取代金券");
                    return;
                }
                default:
                {
                    string str6 = str2;
                    if (str6 != null)
                    {
                        if (!(str6 == "-1"))
                        {
                            if ((str6 == "-2") || (str6 == "-4"))
                            {
                                this.divNoLogin.Visible = true;
                                break;
                            }
                            if (str6 == "-3")
                            {
                                this.divNoNum.Visible = true;
                                break;
                            }
                        }
                        else
                        {
                            string str4 = HttpContext.Current.Request.QueryString.Get("orderid");
                            OrderRedPagerInfo orderRedPagerInfo = OrderRedPagerBrower.GetOrderRedPagerInfo(str4);
                            if (orderRedPagerInfo != null)
                            {
                                this.ltRedPagerActivityNameForOrders.Text = orderRedPagerInfo.RedPagerActivityName;
                                string str5 = "http://" + Globals.DomainName + Globals.ApplicationPath + "/Vshop/GetRedPager.aspx?orderid=" + str4;
                                this.hlinkLogin.NavigateUrl = "/Vshop/UserLogin.aspx?returnUrl=" + HttpContext.Current.Server.UrlEncode(str5 + "&" + this.getopenid());
                                this.divNoLogin.Visible = true;
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect("/Vshop/");
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                    break;
                }
            }
            PageTitle.AddSiteNameTitle("获取代金券");
        }

        public string getopenid()
        {
            string str = "";
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
            if (masterSettings.IsValidationService)
            {
                string str2 = this.Page.Request.QueryString["code"];
                if (!string.IsNullOrEmpty(str2))
                {
                    string responseResult = this.GetResponseResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + masterSettings.WeixinAppId + "&secret=" + masterSettings.WeixinAppSecret + "&code=" + str2 + "&grant_type=authorization_code");
                    if (responseResult.Contains("access_token"))
                    {
                        JObject obj2 = JsonConvert.DeserializeObject(responseResult) as JObject;
                        string str4 = this.GetResponseResult("https://api.weixin.qq.com/sns/userinfo?access_token=" + obj2["access_token"].ToString() + "&openid=" + obj2["openid"].ToString() + "&lang=zh_CN");
                        if (str4.Contains("nickname"))
                        {
                            JObject obj3 = JsonConvert.DeserializeObject(str4) as JObject;
                            string generateId = Globals.GetGenerateId();
                            str = "red=0&openId=" + obj2["openid"].ToString() + "&nickname=" + obj3["nickname"].ToString() + "&sessionId=" + generateId + "&headimgurl=" + obj3["headimgurl"].ToString();
                        }
                    }
                    return str;
                }
                if (string.IsNullOrEmpty(this.Page.Request.QueryString["state"]))
                {
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + masterSettings.WeixinAppId + "&redirect_uri=" + Globals.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                    this.Page.Response.Redirect(url);
                }
            }
            return str;
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
                this.SkinName = "skin-VGetSuccess.html";
            }
            base.OnInit(e);
        }
    }
}

