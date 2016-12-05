using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Hidistro.Core.Entities
{
    public class WXSettings
    {

        public static WXSettings FromXml(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("Settings");

            return new WXSettings()
            {
                AccessToken = node.SelectSingleNode("AccessToken").InnerText,
                AccessTokenExpireTime = node.SelectSingleNode("AccessTokenExpireTime").InnerText,
                JsApiTicket = node.SelectSingleNode("JsApiTicket").InnerText,
                JsApiTicketExpireTime = node.SelectSingleNode("JsApiTicketExpireTime").InnerText
            };
        }

        public void WriteToXml(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("Settings");

            SetNodeValue(doc, root, "AccessToken", this.AccessToken);
            SetNodeValue(doc, root, "AccessTokenExpireTime", this.AccessTokenExpireTime);
            SetNodeValue(doc, root, "JsApiTicket", this.JsApiTicket);
            SetNodeValue(doc, root, "JsApiTicketExpireTime", this.JsApiTicketExpireTime);
        }

        private static void SetNodeValue(XmlDocument doc, XmlNode root, string nodeName, string nodeValue)
        {
            XmlNode newChild = root.SelectSingleNode(nodeName);
            if (null == newChild)
            {
                newChild = doc.CreateElement(nodeName);
                root.AppendChild(newChild);
            }
            newChild.InnerText = nodeValue;
        }

        public string AccessToken { get; set; }

        public string AccessTokenExpireTime { get; set; }

        public string JsApiTicket { get; set; }

        public string JsApiTicketExpireTime { get; set; }
    }
}
