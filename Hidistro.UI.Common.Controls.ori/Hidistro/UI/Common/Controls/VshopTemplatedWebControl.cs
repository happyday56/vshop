namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;

    [PersistChildren(false), ParseChildren(true)]
    public abstract class VshopTemplatedWebControl : TemplatedWebControl
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "Vshop-ReferralId";
        private string ABOZyE4bfGcbd5Z8N5SO7b2;
        protected int referralId;

        protected VshopTemplatedWebControl()
        {
        }

        private string AAZ0JeEma(2x58C7QQ)d9L4MH()
        {
            if (!this.get_SkinFileExists())
            {
                return null;
            }
            StringBuilder builder = new StringBuilder(File.ReadAllText(this.Page.Request.MapPath(this.SkinPath), Encoding.UTF8));
            if (builder.Length == 0)
            {
                return null;
            }
            builder.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAA=="), "").Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JQA+AA=="), "");
            string vshopSkinPath = Globals.GetVshopSkinPath(null);
            builder.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), vshopSkinPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="));
            builder.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBzAGMAcgBpAHAAdAAvAA=="), vshopSkinPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBzAGMAcgBpAHAAdAAvAA=="));
            builder.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBzAHQAeQBsAGUALwA="), vshopSkinPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBzAHQAeQBsAGUALwA="));
            builder.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8A"), Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8A"));
            builder.Insert(0, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAEAAIABSAGUAZwBpAHMAdABlAHIAIABUAGEAZwBQAHIAZQBmAGkAeAA9ACIAVQBJACIAIABOAGEAbQBlAHMAcABhAGMAZQA9ACIAQQBTAFAATgBFAFQALgBXAGUAYgBDAG8AbgB0AHIAbwBsAHMAIgAgAEEAcwBzAGUAbQBiAGwAeQA9ACIAQQBTAFAATgBFAFQALgBXAGUAYgBDAG8AbgB0AHIAbwBsAHMAIgAgACUAPgA=") + Environment.NewLine);
            builder.Insert(0, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAEAAIABSAGUAZwBpAHMAdABlAHIAIABUAGEAZwBQAHIAZQBmAGkAeAA9ACIASwBpAG4AZABlAGQAaQB0AG8AcgAiACAATgBhAG0AZQBzAHAAYQBjAGUAPQAiAGsAaQBuAGQAZQBkAGkAdABvAHIALgBOAGUAdAAiACAAQQBzAHMAZQBtAGIAbAB5AD0AIgBrAGkAbgBkAGUAZABpAHQAbwByAC4ATgBlAHQAIgAgACUAPgA=") + Environment.NewLine);
            builder.Insert(0, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAEAAIABSAGUAZwBpAHMAdABlAHIAIABUAGEAZwBQAHIAZQBmAGkAeAA9ACIASABpACIAIABOAGEAbQBlAHMAcABhAGMAZQA9ACIASABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMAIgAgAEEAcwBzAGUAbQBiAGwAeQA9ACIASABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMAIgAgACUAPgA=") + Environment.NewLine);
            builder.Insert(0, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAEAAIABSAGUAZwBpAHMAdABlAHIAIABUAGEAZwBQAHIAZQBmAGkAeAA9ACIASABpACIAIABOAGEAbQBlAHMAcABhAGMAZQA9ACIASABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AUwBhAGwAZQBTAHkAcwB0AGUAbQAuAFQAYQBnAHMAIgAgAEEAcwBzAGUAbQBiAGwAeQA9ACIASABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AUwBhAGwAZQBTAHkAcwB0AGUAbQAuAFQAYQBnAHMAIgAgACUAPgA=") + Environment.NewLine);
            builder.Insert(0, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAlAEAAIABDAG8AbgB0AHIAbwBsACAATABhAG4AZwB1AGEAZwBlAD0AIgBDACMAIgAgACUAPgA=") + Environment.NewLine);
            MatchCollection matchs = Regex.Matches(builder.ToString(), AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAByAGUAZgAoAFwAcwArACkAPwA9ACgAXABzACsAKQA/ACIAdQByAGwAOgAoAD8APABVAHIAbABOAGEAbQBlAD4ALgAqAD8AKQAoAFwAKAAoAD8APABQAGEAcgBhAG0APgAuACoAPwApAFwAKQApAD8AIgA="), RegexOptions.Multiline | RegexOptions.IgnoreCase);
            for (int i = matchs.Count - 1; i >= 0; i--)
            {
                int startIndex = matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQByAGwATgBhAG0AZQA=")].Index - 4;
                int length = matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQByAGwATgBhAG0AZQA=")].Length + 4;
                if (matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UABhAHIAYQBtAA==")].Length > 0)
                {
                    length += matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UABhAHIAYQBtAA==")].Length + 2;
                }
                builder.Remove(startIndex, length);
                builder.Insert(startIndex, Globals.GetSiteUrls().UrlData.FormatUrl(matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQByAGwATgBhAG0AZQA=")].Value.Trim(), new object[] { matchs[i].Groups[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UABhAHIAYQBtAA==")].Value }));
            }
            return builder.ToString();
        }

        private string ABOZyE4bfGcbd5Z8N5SO7b2(NameValueCollection collection1)
        {
            if ((collection1 == null) || (collection1.Count == 0))
            {
                return this.Page.Request.Url.AbsolutePath;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(this.Page.Request.Url.AbsolutePath).Append(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PwA="));
            foreach (string str2 in collection1.Keys)
            {
                if (collection1[str2] != null)
                {
                    string str = collection1[str2].Trim();
                    if (!string.IsNullOrEmpty(str) && (str.Length > 0))
                    {
                        builder.Append(str2).Append(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PQA=")).Append(this.Page.Server.UrlEncode(str)).Append(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JgA="));
                    }
                }
            }
            collection1.Clear();
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            if ((!string.IsNullOrEmpty(this.Page.Request.QueryString[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGYAZQByAHIAYQBsAEkAZAA=")]) && (int.Parse(this.Page.Request.QueryString[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGYAZQByAHIAYQBsAEkAZAA=")]) != 0)) && (int.TryParse(this.Page.Request.QueryString[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGYAZQByAHIAYQBsAEkAZAA=")], out this.referralId) && (this.referralId != 0)))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VgBzAGgAbwBwAC0ATQBlAG0AYgBlAHIA")];
                if (((cookie != null) && !string.IsNullOrEmpty(cookie.Value)) && (this.referralId != Convert.ToInt32(cookie.Value)))
                {
                    DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(Convert.ToInt32(cookie.Value));
                    if ((currentDistributors != null) && (currentDistributors.UserId > 0))
                    {
                        cookie.Value = null;
                        cookie.Expires = DateTime.Now.AddYears(-1);
                        HttpContext.Current.Response.Cookies.Set(cookie);
                    }
                }
                HttpCookie cookie2 = new HttpCookie(this.AAZ0JeEma(2x58C7QQ)d9L4MH) {
                    Value = null,
                    Expires = DateTime.Now.AddYears(-1)
                };
                HttpContext.Current.Response.Cookies.Set(cookie2);
                cookie2.Value = this.referralId.ToString();
                cookie2.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie2);
            }
            HttpCookie cookie3 = HttpContext.Current.Request.Cookies[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VgBzAGgAbwBwAC0AUgBlAGYAZQByAHIAYQBsAEkAZAA=")];
            if (((cookie3 != null) && !string.IsNullOrEmpty(cookie3.Value)) && (int.Parse(cookie3.Value) != 0))
            {
                DistributorsInfo info2 = DistributorsBrower.GetCurrentDistributors(Convert.ToInt32(cookie3.Value));
                if ((info2 == null) || (info2.UserId <= 0))
                {
                    cookie3.Value = null;
                    cookie3.Expires = DateTime.Now.AddYears(-1);
                    HttpContext.Current.Response.Cookies.Set(cookie3);
                    HttpCookie cookie4 = HttpContext.Current.Request.Cookies[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VgBzAGgAbwBwAC0ATQBlAG0AYgBlAHIA")];
                    cookie4.Value = null;
                    cookie4.Expires = DateTime.Now.AddYears(-1);
                    HttpContext.Current.Response.Cookies.Set(cookie4);
                    HttpContext.Current.Response.Redirect(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBWAHMAaABvAHAALwBVAHMAZQByAEwAbwBnAGkAbgAuAGEAcwBwAHgA"));
                }
            }
            if (!this.LoadHtmlThemedControl())
            {
                throw new SkinNotFoundException(this.SkinPath);
            }
            this.AttachChildControls();
        }

        public string getUrl()
        {
            string str = HttpContext.Current.Request.Url.PathAndQuery.ToString();
            int startIndex = str.LastIndexOf(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=")) + 1;
            int length = (str.IndexOf(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBhAHMAcAB4AA==")) - str.LastIndexOf(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA="))) - 1;
            return str.Substring(startIndex, length);
        }

        protected void GotoResourceNotFound(string errorMsg = "")
        {
            this.Page.Response.Redirect(Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBSAGUAcwBvAHUAcgBjAGUATgBvAHQARgBvAHUAbgBkAC4AYQBzAHAAeAA/AGUAcgByAG8AcgBNAHMAZwA9AA==") + errorMsg);
        }

        protected bool LoadHtmlThemedControl()
        {
            string str = this.AAZ0JeEma(2x58C7QQ)d9L4MH();
            if (!string.IsNullOrEmpty(str))
            {
                Control child = this.Page.ParseControl(str);
                child.ID = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwA=");
                this.Controls.Add(child);
                return true;
            }
            return false;
        }

        public void RegisterShareScript(string ImgUrl, string lineLink, string descContent, string shareTitle)
        {
            string weixinAppId = SettingsManager.GetMasterSettings(true).WeixinAppId;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAGwAYQBuAGcAdQBhAGcAZQA9ACIAagBhAHYAYQBzAGMAcgBpAHAAdAAiACAAdAB5AHAAZQA9ACIAdABlAHgAdAAvAGoAYQB2AGEAcwBjAHIAaQBwAHQAIgA+AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAEkAbQBnAFUAcgBsACAAPQAgACcA") + ImgUrl + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAGwAaQBuAGUATABpAG4AawAgAD0AIAAnAA==") + lineLink + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAGQAZQBzAGMAQwBvAG4AdABlAG4AdAAgAD0AIAAnAA==") + (string.IsNullOrEmpty(descContent) ? "" : descContent.Replace(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA="), "")) + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAHMAaABhAHIAZQBUAGkAdABsAGUAIAA9ACAAJwA=") + shareTitle + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAYQBwAHAAaQBkACAAPQAgACcA") + weixinAppId + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAEkAbQBnAFcAaQBkAHQAaAAgAD0AIAAnADIAMgAwACcAOwA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAHIAIAB3AHgAUwBoAGEAcgBlAEkAbQBnAEgAZQBpAGcAaAB0ACAAPQAgACcAMgAyADAAJwA7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZgB1AG4AYwB0AGkAbwBuACAAdwBlAGkAeABpAG4AUwBoAGEAcgBlAFQAaQBtAGUAbABpAG4AZQAoACkAewA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VwBlAGkAeABpAG4ASgBTAEIAcgBpAGQAZwBlAC4AaQBuAHYAbwBrAGUAKAAnAHMAaABhAHIAZQBUAGkAbQBlAGwAaQBuAGUAJwAsAHsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAHUAcgBsACIAOgB3AHgAUwBoAGEAcgBlAEkAbQBnAFUAcgBsACwA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAHcAaQBkAHQAaAAiADoAdwB4AFMAaABhAHIAZQBJAG0AZwBXAGkAZAB0AGgALAA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAGgAZQBpAGcAaAB0ACIAOgAgAHcAeABTAGgAYQByAGUASQBtAGcASABlAGkAZwBoAHQALAA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBsAGkAbgBrACIAOgAgAHcAeABTAGgAYQByAGUAbABpAG4AZQBMAGkAbgBrACwA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBkAGUAcwBjACIAOgAgAHcAeABTAGgAYQByAGUAZABlAHMAYwBDAG8AbgB0AGUAbgB0ACwA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgB0AGkAdABsAGUAIgA6ACAAdwB4AFMAaABhAHIAZQBzAGgAYQByAGUAVABpAHQAbABlAA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQApADsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZgB1AG4AYwB0AGkAbwBuACAAdwB4AHMAaABhAHIAZQBGAHIAaQBlAG4AZAAoACkAIAB7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VwBlAGkAeABpAG4ASgBTAEIAcgBpAGQAZwBlAC4AaQBuAHYAbwBrAGUAKAAnAHMAZQBuAGQAQQBwAHAATQBlAHMAcwBhAGcAZQAnACwAIAB7AA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBhAHAAcABpAGQAIgA6ACAAdwB4AGEAcABwAGkAZAAsAA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAHUAcgBsACIAOgAgAHcAeABTAGgAYQByAGUASQBtAGcAVQByAGwALAA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAHcAaQBkAHQAaAAiADoAIAB3AHgAUwBoAGEAcgBlAEkAbQBnAFcAaQBkAHQAaAAsAA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBpAG0AZwBfAGgAZQBpAGcAaAB0ACIAOgAgAHcAeABTAGgAYQByAGUASQBtAGcASABlAGkAZwBoAHQALAA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBsAGkAbgBrACIAOgAgAHcAeABTAGgAYQByAGUAbABpAG4AZQBMAGkAbgBrACwA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgBkAGUAcwBjACIAOgAgAHcAeABTAGgAYQByAGUAZABlAHMAYwBDAG8AbgB0AGUAbgB0ACwA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgB0AGkAdABsAGUAIgA6ACAAdwB4AFMAaABhAHIAZQBzAGgAYQByAGUAVABpAHQAbABlAA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQAsACAAZgB1AG4AYwB0AGkAbwBuACAAKAByAGUAcwApACAAewA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQApAA=="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABvAGMAdQBtAGUAbgB0AC4AYQBkAGQARQB2AGUAbgB0AEwAaQBzAHQAZQBuAGUAcgAoACcAVwBlAGkAeABpAG4ASgBTAEIAcgBpAGQAZwBlAFIAZQBhAGQAeQAnACwAIABmAHUAbgBjAHQAaQBvAG4AIABvAG4AQgByAGkAZABnAGUAUgBlAGEAZAB5ACgAKQAgAHsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VwBlAGkAeABpAG4ASgBTAEIAcgBpAGQAZwBlAC4AbwBuACgAJwBtAGUAbgB1ADoAcwBoAGEAcgBlADoAYQBwAHAAbQBlAHMAcwBhAGcAZQAnACwAIABmAHUAbgBjAHQAaQBvAG4AIAAoAGEAcgBnAHYAKQAgAHsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwB4AHMAaABhAHIAZQBGAHIAaQBlAG4AZAAoACkAOwA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQApADsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VwBlAGkAeABpAG4ASgBTAEIAcgBpAGQAZwBlAC4AbwBuACgAJwBtAGUAbgB1ADoAcwBoAGEAcgBlADoAdABpAG0AZQBsAGkAbgBlACcALAAgAGYAdQBuAGMAdABpAG8AbgAgACgAYQByAGcAdgApACAAewA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwBlAGkAeABpAG4AUwBoAGEAcgBlAFQAaQBtAGUAbABpAG4AZQAoACkAOwA="));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQApADsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQAsAGYAYQBsAHMAZQApADsA"));
            builder.AppendLine(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAvAHMAYwByAGkAcAB0AD4A"));
            HttpContext.Current.Response.Write(builder.ToString());
        }

        public void ReloadPage(NameValueCollection queryStrings)
        {
            this.Page.Response.Redirect(this.ABOZyE4bfGcbd5Z8N5SO7b2(queryStrings));
        }

        public void ReloadPage(NameValueCollection queryStrings, bool endResponse)
        {
            this.Page.Response.Redirect(this.ABOZyE4bfGcbd5Z8N5SO7b2(queryStrings), endResponse);
        }

        private bool AAZ0JeEma(2x58C7QQ)d9L4MH
        {
            get
            {
                return !string.IsNullOrEmpty(this.SkinName);
            }
        }

        public virtual string SkinName
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.ToLower(CultureInfo.InvariantCulture);
                    if (value.EndsWith(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBoAHQAbQBsAA==")))
                    {
                        this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
                    }
                }
            }
        }

        protected virtual string SkinPath
        {
            get
            {
                string vshopSkinPath = Globals.GetVshopSkinPath(null);
                if (this.SkinName.StartsWith(vshopSkinPath))
                {
                    return this.SkinName;
                }
                if (this.SkinName.StartsWith(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=")))
                {
                    return (vshopSkinPath + this.SkinName);
                }
                return (vshopSkinPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.SkinName);
            }
        }
    }
}

