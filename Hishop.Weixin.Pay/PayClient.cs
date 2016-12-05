#region 注释
//namespace Hishop.Weixin.Pay
//{
//    using Hishop.Weixin.Pay.Domain;
//    using Hishop.Weixin.Pay.Util;
//    using Newtonsoft.Json;
//    using System;
//    using System.Collections.Generic;
//    using System.Data;
//    using System.IO;
//    using System.Net;
//    using System.Text;
//    using System.Web;
//    using System.Xml;

//    public class PayClient
//    {
//        private PayAccount _payAccount;
//        public static readonly string Deliver_Notify_Url = "https://api.weixin.qq.com/pay/delivernotify";
//        public static readonly string prepay_id_Url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

//        public PayClient(PayAccount account) : this(account.AppId, account.AppSecret, account.PartnerId, account.PartnerKey, account.PaySignKey)
//        {
//        }

//        public PayClient(string appId, string appSecret, string partnerId, string partnerKey, string paySignKey)
//        {
//            PayAccount account = new PayAccount {
//                AppId = appId,
//                AppSecret = appSecret,
//                PartnerId = partnerId,
//                PartnerKey = partnerKey,
//                PaySignKey = paySignKey
//            };
//            this._payAccount = account;
//        }

//        internal string BuildPackage(PackageInfo package)
//        {
//            PayDictionary parameters = new PayDictionary();
//            parameters.Add("appid", this._payAccount.AppId);
//            parameters.Add("mch_id", this._payAccount.PartnerId);
//            parameters.Add("device_info", "");
//            parameters.Add("nonce_str", Utils.CreateNoncestr());
//            parameters.Add("body", package.Body);
//            parameters.Add("attach", "");
//            parameters.Add("out_trade_no", package.OutTradeNo);
//            parameters.Add("total_fee", (int) package.TotalFee);
//            parameters.Add("spbill_create_ip", package.SpbillCreateIp);
//            parameters.Add("time_start", package.TimeExpire);
//            parameters.Add("time_expire", "");
//            parameters.Add("goods_tag", package.GoodsTag);
//            parameters.Add("notify_url", package.NotifyUrl);
//            parameters.Add("trade_type", "JSAPI");
//            parameters.Add("openid", package.OpenId);
//            parameters.Add("product_id", "");
//            string sign = SignHelper.SignPackage(parameters, this._payAccount.PartnerKey);
//            writeLog(parameters, sign, "", "");
//            string str2 = this.GetPrepay_id(parameters, sign);
//            if (str2.Length > 0x40)
//            {
//                str2 = "";
//            }
//            return string.Format("prepay_id=" + str2, new object[0]);
//        }

//        public PayRequestInfo BuildPayRequest(PackageInfo package)
//        {
//            PayRequestInfo info = new PayRequestInfo {
//                appId = this._payAccount.AppId,
//                package = this.BuildPackage(package),
//                timeStamp = Utils.GetCurrentTimeSeconds().ToString(),
//                nonceStr = Utils.CreateNoncestr()
//            };
//            PayDictionary parameters = new PayDictionary();
//            parameters.Add("appId", this._payAccount.AppId);
//            parameters.Add("timeStamp", info.timeStamp);
//            parameters.Add("package", info.package);
//            parameters.Add("nonceStr", info.nonceStr);
//            parameters.Add("signType", "MD5");
//            info.paySign = SignHelper.SignPay(parameters, this._payAccount.PartnerKey);
//            return info;
//        }

//        public bool DeliverNotify(DeliverInfo deliver)
//        {
//            string token = Utils.GetToken(this._payAccount.AppId, this._payAccount.AppSecret);
//            return this.DeliverNotify(deliver, token);
//        }

//        public bool DeliverNotify(DeliverInfo deliver, string token)
//        {
//            PayDictionary parameters = new PayDictionary();
//            parameters.Add("appid", this._payAccount.AppId);
//            parameters.Add("openid", deliver.OpenId);
//            parameters.Add("transid", deliver.TransId);
//            parameters.Add("out_trade_no", deliver.OutTradeNo);
//            parameters.Add("deliver_timestamp", Utils.GetTimeSeconds(deliver.TimeStamp));
//            parameters.Add("deliver_status", deliver.Status ? 1 : 0);
//            parameters.Add("deliver_msg", deliver.Message);
//            deliver.AppId = this._payAccount.AppId;
//            deliver.AppSignature = SignHelper.SignPay(parameters, "");
//            parameters.Add("app_signature", deliver.AppSignature);
//            parameters.Add("sign_method", deliver.SignMethod);
//            string data = JsonConvert.SerializeObject(parameters);
//            string url = string.Format("{0}?access_token={1}", Deliver_Notify_Url, token);
//            string str3 = new WebUtils().DoPost(url, data);
//            if (!(!string.IsNullOrEmpty(str3) && str3.Contains("ok")))
//            {
//                return false;
//            }
//            return true;
//        }

//        internal string GetPrepay_id(PayDictionary dict, string sign)
//        {
//            dict.Add("sign", sign);
//            string str = SignHelper.BuildQuery(dict, false);
//            string postData = SignHelper.BuildXml(dict, false);
//            string str3 = "";
//            str3 = PostData(prepay_id_Url, postData);
//            try
//            {
//                DataTable table = new DataTable {
//                    TableName = "log"
//                };
//                table.Columns.Add(new DataColumn("OperTime"));
//                table.Columns.Add(new DataColumn("Info"));
//                table.Columns.Add(new DataColumn("param"));
//                table.Columns.Add(new DataColumn("query"));
//                DataRow row = table.NewRow();
//                row["OperTime"] = DateTime.Now.ToString();
//                row["Info"] = str3;
//                row["param"] = postData;
//                row["query"] = str;
//                table.Rows.Add(row);
//                table.WriteXml(HttpContext.Current.Request.MapPath("/PrepayID.xml"));
//            }
//            catch (Exception exception)
//            {
//                writeLog(dict, sign, "", exception.Message + "-PrepayId获取错误");
//            }
//            return str3;
//        }

//        public static string PostData(string url, string postData)
//        {
//            Exception exception;
//            string xml = string.Empty;
//            try
//            {
//                Uri requestUri = new Uri(url);
//                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
//                byte[] bytes = Encoding.UTF8.GetBytes(postData);
//                request.Method = "POST";
//                request.ContentType = "text/xml";
//                request.ContentLength = postData.Length;
//                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
//                {
//                    writer.Write(postData);
//                }
//                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
//                {
//                    using (Stream stream = response.GetResponseStream())
//                    {
//                        Encoding encoding = Encoding.UTF8;
//                        xml = new StreamReader(stream, encoding).ReadToEnd();
//                        XmlDocument document = new XmlDocument();
//                        try
//                        {
//                            document.LoadXml(xml);
//                        }
//                        catch (Exception exception1)
//                        {
//                            exception = exception1;
//                            xml = string.Format("获取信息错误doc.load：{0}", exception.Message) + xml;
//                        }
//                        try
//                        {
//                            if (document == null)
//                            {
//                                return xml;
//                            }
//                            XmlNode node = document.SelectSingleNode("xml/return_code");
//                            if (node == null)
//                            {
//                                return xml;
//                            }
//                            if (node.InnerText == "SUCCESS")
//                            {
//                                XmlNode node2 = document.SelectSingleNode("xml/prepay_id");
//                                if (node2 != null)
//                                {
//                                    return node2.InnerText;
//                                }
//                            }
//                            else
//                            {
//                                return document.InnerXml;
//                            }
//                        }
//                        catch (Exception exception2)
//                        {
//                            exception = exception2;
//                            xml = string.Format("获取信息错误node.load：{0}", exception.Message) + xml;
//                        }
//                        return xml;
//                    }
//                }
//            }
//            catch (Exception exception3)
//            {
//                exception = exception3;
//                xml = string.Format("获取信息错误post error：{0}", exception.Message) + xml;
//            }
//            return xml;
//        }

//        public static void writeLog(IDictionary<string, string> param, string sign, string url, string msg)
//        {
//            DataTable table = new DataTable {
//                TableName = "log"
//            };
//            table.Columns.Add(new DataColumn("OperTime"));
//            foreach (KeyValuePair<string, string> pair in param)
//            {
//                table.Columns.Add(new DataColumn(pair.Key));
//            }
//            table.Columns.Add(new DataColumn("Msg"));
//            table.Columns.Add(new DataColumn("Sign"));
//            table.Columns.Add(new DataColumn("Url"));
//            DataRow row = table.NewRow();
//            row["OperTime"] = DateTime.Now;
//            foreach (KeyValuePair<string, string> pair in param)
//            {
//                row[pair.Key] = pair.Value;
//            }
//            row["Msg"] = msg;
//            row["Sign"] = sign;
//            row["Url"] = url;
//            table.Rows.Add(row);
//            table.WriteXml(HttpContext.Current.Server.MapPath("/wxpay.xml"));
//        }
//    }
//}

#endregion


using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Hishop.Weixin.Pay.Domain;
using Hishop.Weixin.Pay.Util;
using Newtonsoft.Json;

namespace Hishop.Weixin.Pay
{
    public class PayClient
    {
        private PayAccount m_payAccount;
        public static readonly string Deliver_Notify_Url = "https://api.weixin.qq.com/pay/delivernotify";
        public static readonly string prepay_id_Url = "https://api.mch.weixin.qq.com/pay/unifiedorder";


        // string nonceStr ="";// Utils.CreateNoncestr();
        public PayClient(PayAccount account)
            : this(account.AppId, account.AppSecret, account.PartnerId, account.PartnerKey, account.PaySignKey)
        {
        }

        public PayClient(string appId, string appSecret, string partnerId, string partnerKey, string paySignKey)
        {

            PayAccount account = new PayAccount();

            account.AppId = appId;

            account.AppSecret = appSecret;

            account.PartnerId = partnerId;

            account.PartnerKey = partnerKey;

            account.PaySignKey = paySignKey;

            this.m_payAccount = account;

            //this.nonceStr = Utils.CreateNoncestr();

        }

        internal string BuildPackage(PackageInfo package)
        {
            PayDictionary parameters = new PayDictionary();
            parameters.Add("appid", this.m_payAccount.AppId);
            parameters.Add("mch_id", this.m_payAccount.PartnerId);
            parameters.Add("device_info", "");
            parameters.Add("nonce_str", Utils.CreateNoncestr());
            parameters.Add("body", package.Body);
            parameters.Add("attach", "");
            parameters.Add("out_trade_no", package.OutTradeNo);
            parameters.Add("total_fee", (int)package.TotalFee);
            parameters.Add("spbill_create_ip", package.SpbillCreateIp);
            parameters.Add("time_start", package.TimeExpire);
            parameters.Add("time_expire", "");
            parameters.Add("goods_tag", package.GoodsTag);
            parameters.Add("notify_url", package.NotifyUrl);
            parameters.Add("trade_type", "JSAPI");
            parameters.Add("openid", package.OpenId);
            parameters.Add("product_id", "");
            string sign = SignHelper.SignPackage(parameters, this.m_payAccount.PartnerKey);
            string str2 = this.GetPrepay_id(parameters, sign);
            if (str2.Length > 0x40)
            {
                str2 = "";
            }
            return string.Format("prepay_id=" + str2, new object[0]);
        }

        public PayRequestInfo BuildPayRequest(PackageInfo package)
        {

            PayRequestInfo info = new PayRequestInfo();

            info.appId = this.m_payAccount.AppId;

            info.timeStamp = Utils.GetCurrentTimeSeconds().ToString();

            info.package = this.BuildPackage(package);//生成prepay_id

            info.nonceStr = Utils.CreateNoncestr();



            PayDictionary parameters = new PayDictionary();

            parameters.Add("appId", info.appId);

            parameters.Add("timeStamp", info.timeStamp);

            parameters.Add("package", info.package);

            parameters.Add("nonceStr", info.nonceStr);

            parameters.Add("signType", info.signType);



            info.paySign = SignHelper.SignPay(parameters, this.m_payAccount.PartnerKey);

            return info;
        }

        public bool DeliverNotify(DeliverInfo deliver)
        {
            string token = Utils.GetToken(this.m_payAccount.AppId, this.m_payAccount.AppSecret);
            return this.DeliverNotify(deliver, token);
        }

        public bool DeliverNotify(DeliverInfo deliver, string token)
        {
            PayDictionary parameters = new PayDictionary();
            parameters.Add("appid", this.m_payAccount.AppId);
            parameters.Add("openid", deliver.OpenId);
            parameters.Add("transid", deliver.TransId);
            parameters.Add("out_trade_no", deliver.OutTradeNo);
            parameters.Add("deliver_timestamp", Utils.GetTimeSeconds(deliver.TimeStamp));
            parameters.Add("deliver_status", deliver.Status ? 1 : 0);
            parameters.Add("deliver_msg", deliver.Message);
            deliver.AppId = this.m_payAccount.AppId;
            deliver.AppSignature = SignHelper.SignPay(parameters, "");
            parameters.Add("app_signature", deliver.AppSignature);
            parameters.Add("sign_method", deliver.SignMethod);
            string data = JsonConvert.SerializeObject(parameters);
            string url = string.Format("{0}?access_token={1}", Deliver_Notify_Url, token);
            string str3 = new WebUtils().DoPost(url, data);
            if (!(!string.IsNullOrEmpty(str3) && str3.Contains("ok")))
            {
                return false;
            }
            return true;
        }

        internal string GetPrepay_id(PayDictionary dict, string sign)
        {
            dict.Add("sign", sign);
            string str = SignHelper.BuildQuery(dict, false);
            string postData = SignHelper.BuildXml(dict, false);
            string prepay_id = "";
            prepay_id = PostData(prepay_id_Url, postData);
            DataTable table = new DataTable
            {
                TableName = "log"
            };
            table.Columns.Add(new DataColumn("OperTime"));
            table.Columns.Add(new DataColumn("prepay_id"));
            table.Columns.Add(new DataColumn("param"));
            table.Columns.Add(new DataColumn("query"));
            DataRow row = table.NewRow();
            row["OperTime"] = DateTime.Now.ToString();
            row["prepay_id"] = prepay_id;
            row["param"] = postData;
            row["query"] = str;
            table.Rows.Add(row);
            table.WriteXml(HttpContext.Current.Request.MapPath("/PrepayID.xml"));
            return prepay_id;
        }

        public static string PostData(string url, string postData)
        {
            Exception exception;
            string xml = string.Empty;
            try
            {
                Uri requestUri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.ContentLength = postData.Length;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postData);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        Encoding encoding = Encoding.UTF8;
                        xml = new StreamReader(stream, encoding).ReadToEnd();

                       // System.IO.File.AppendAllText(HttpContext.Current.Request.MapPath("/pre.txt"), xml + Environment.NewLine);

                        XmlDocument document = new XmlDocument();
                        try
                        {
                            document.LoadXml(xml);
                        }
                        catch (Exception exception1)
                        {
                            // exception = exception1;
                            xml = string.Format("获取信息错误doc.load：{0}", exception1.Message) + xml;
                        }
                        try
                        {
                            if (document == null)
                            {
                                return xml;
                            }
                            XmlNode node = document.SelectSingleNode("xml/return_code");
                            if (node == null)
                            {
                                return xml;
                            }
                            if (node.InnerText == "SUCCESS")
                            {
                                XmlNode node2 = document.SelectSingleNode("xml/prepay_id");
                                if (node2 != null)
                                {
                                    return node2.InnerText;
                                }
                            }
                            else
                            {
                                return node.InnerText;
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            xml = string.Format("获取信息错误node.load：{0}", exception.Message) + xml;
                        }
                        return xml;
                    }
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                xml = string.Format("获取信息错误post error：{0}", exception.Message) + xml;
            }
            return xml;
        }
    }
}

