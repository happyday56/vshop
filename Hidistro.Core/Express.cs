namespace Hidistro.Core
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Xml;

    public static class Express
    {
        public static string GetDataByKuaidi100(string computer, string expressNo, int show = 0)
        {
            HttpWebResponse response;
            string str = "29833628d495d7a5";
            string str2 = null;
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str2 = current.Request.MapPath("~/Express.xml");
            }
            XmlDocument document = new XmlDocument();
            if (!string.IsNullOrEmpty(str2))
            {
                document.Load(str2);
                XmlNode node = document.SelectSingleNode("companys");
                if (node != null)
                {
                    str = node.Attributes["Kuaidi100NewKey"].Value;
                }
            }
            string apiUrl = string.Format("http://api.kuaidi100.com/api?id={0}&com={1}&nu={2}&show={3}&muti=1&order=desc", str, computer, expressNo, show);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(string.Format("http://www.kuaidi100.com/query?type={0}&postid={1}", computer, expressNo));
            request.Timeout = 0x1f40;
            string str3 = "暂时没有此快递单号的信息";
            try
            {
                response = (HttpWebResponse) request.GetResponse();
            }
            catch
            {
                return str3;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                str3 = reader.ReadToEnd().Replace("&amp;", "").Replace("&nbsp;", "").Replace("&", "");
            }
            return str3;
        }

        public static string GetExpressDataByKuaidi100(string computer, string expressNo, bool isRtnHref = true)
        {
            string detail = "";

            try
            {
                WebResponse response = null;
                WebRequest request;
                HttpContext current;
                string apiKey = "42536ba9f7efe6e6";
                string filename = "";
                string computerName = "";

                current = HttpContext.Current;
                if (null != current)
                {
                    filename = current.Request.MapPath("~/Express.xml");
                }

                XmlDocument document = new XmlDocument();
                if (!string.IsNullOrEmpty(filename))
                {
                    document.Load(filename);
                    XmlNode node = document.SelectSingleNode("companys");
                    if (null != node)
                    {
                        apiKey = node.Attributes["Kuaidi100NewKey"].Value;

                        XmlNodeList companyNodeList = node.SelectNodes("company");
                        if (null != companyNodeList)
                        {
                            foreach (XmlNode xmlnode in companyNodeList)
                            {
                                if (xmlnode.Attributes["Kuaidi100Code"].Value.Equals(computer))
                                {
                                    computerName = xmlnode.Attributes["name"].Value;
                                    break;
                                }
                            }
                        }

                    }
                }

                string apiUrl = string.Format("http://www.kuaidi100.com/applyurl?key={0}&com={1}&nu={2}", apiKey, computer, expressNo);

                request = WebRequest.Create(apiUrl);
                response = request.GetResponse();

                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                StreamReader reader = new StreamReader(stream, encode);
                detail = reader.ReadToEnd();

                if (isRtnHref)
                {
                    if (string.IsNullOrEmpty(computerName))
                    {
                        detail = "<a href=\"" + detail + "\">" + expressNo + "</a>";
                    }
                    else
                    {
                        detail = "<a href=\"" + detail + "\">" + computerName + ":" + expressNo + "</a>";
                    }
                }
            }
            catch (Exception ex)
            {
                detail = "None";
            }            

            return detail;

        }

        public static string GetDataByTaobaoTop(string computer, string expressNo)
        {
            return "暂时没有此快递单号的信息";
        }

        public static string GetExpressData(string computer, string expressNo, int show = 0, bool isRtnHref = true)
        {
            if (GetExpressType() == "kuaidi100")
            {
                // 2015-12-07 替换原有的查询方式，以防被禁用
                //return GetDataByKuaidi100(computer, expressNo, show);
                return GetExpressDataByKuaidi100(computer, expressNo, isRtnHref);
            }
            return GetDataByTaobaoTop(computer, expressNo);
        }

        public static string GetExpressType()
        {
            string innerText = "kuaidi100";
            string str2 = null;
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                str2 = current.Request.MapPath("~/Express.xml");
            }
            XmlDocument document = new XmlDocument();
            if (!string.IsNullOrEmpty(str2))
            {
                document.Load(str2);
                XmlNode node = document.SelectSingleNode("expressapi");
                if (node != null)
                {
                    innerText = node.Attributes["usetype"].InnerText;
                }
            }
            return innerText;
        }
    }
}

