namespace Hishop.Weixin.Pay
{
    using Hishop.Weixin.Pay.Domain;
    using Hishop.Weixin.Pay.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;
    using NewLife.Log;

    public class RedPackClient
    {
        public static readonly string SendRedPack_Url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return (errors == SslPolicyErrors.None);
        }

        public static string PostData(string url, string postData)
        {
            Exception exception;
            string xml = string.Empty;
            try
            {
                Uri requestUri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.ContentLength = postData.Length;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postData);
                }
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        Encoding encoding = Encoding.UTF8;
                        xml = new StreamReader(stream, encoding).ReadToEnd();
                        XmlDocument document = new XmlDocument();
                        try
                        {
                            document.LoadXml(xml);
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            xml = string.Format("获取信息错误doc.load：{0}", exception.Message) + xml;
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
                                return document.InnerXml;
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

        public static string Send(string cert, string password, string data, string url)
        {
            return Send(cert, password, Encoding.GetEncoding("UTF-8").GetBytes(data), url);
        }

        public static string Send(string cert, string password, byte[] data, string url)
        {
            Stream responseStream;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RedPackClient.CheckValidationResult);
            
            X509Certificate2 certificate = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            
            // 2015-09-30 注销此处代码，否则引起微信红包支付不成功，不需要这些信息。
            //X509Certificate2 certificate2 = new X509Certificate2(cert, password);
            //X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadWrite);
            //store.Remove(certificate2);
            //store.Add(certificate2);
            //store.Close();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                throw new ApplicationException(string.Format("Invalid url string: {0}", url));
            }
            request.UserAgent = "Hishop";
            request.ContentType = "text/xml";
            request.ClientCertificates.Add(certificate);
            request.Method = "POST";
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            try
            {
                responseStream = request.GetResponse().GetResponseStream();
            }
            catch (Exception exception)
            {
                //XTrace.WriteLine("Send Error = " + exception.ToString());
                throw exception;
            }
            string str = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
            {
                str = reader.ReadToEnd();
            }
            responseStream.Close();
            return str;
        }

        public string SendRedpack(SendRedPackInfo sendredpack)
        {
            string str = string.Empty;
            PayDictionary parameters = new PayDictionary();
            parameters.Add("nonce_str", Utils.CreateNoncestr());
            if (sendredpack.SendRedpackRecordID > 0)
            {
                parameters.Add("mch_billno", sendredpack.Mch_Id + DateTime.Now.ToString("yyyymmdd") + sendredpack.SendRedpackRecordID.ToString().PadLeft(10, '0'));
            }
            else
            {
                parameters.Add("mch_billno", sendredpack.Mch_Id + DateTime.Now.ToString("yyyymmdd") + DateTime.Now.ToString("MMddHHmmss"));
            }
            parameters.Add("mch_id", sendredpack.Mch_Id);
            if (!string.IsNullOrEmpty(sendredpack.Sub_Mch_Id))
            {
                parameters.Add("sub_mch_id", sendredpack.Sub_Mch_Id);
            }
            parameters.Add("wxappid", sendredpack.WXAppid);
            parameters.Add("nick_name", sendredpack.Nick_Name);
            parameters.Add("send_name", sendredpack.Send_Name);
            parameters.Add("re_openid", sendredpack.Re_Openid);
            parameters.Add("total_amount", sendredpack.Total_Amount);
            parameters.Add("min_value", sendredpack.Total_Amount);
            parameters.Add("max_value", sendredpack.Total_Amount);
            parameters.Add("total_num", sendredpack.Total_Num);
            parameters.Add("wishing", sendredpack.Wishing);
            parameters.Add("client_ip", sendredpack.Client_IP);
            parameters.Add("act_name", sendredpack.Act_Name);
            parameters.Add("remark", sendredpack.Remark);
            string str2 = SignHelper.SignPackage(parameters, sendredpack.PartnerKey);
            parameters.Add("sign", str2);
            string data = SignHelper.BuildXml(parameters, false);
            string msg = Send(sendredpack.WeixinCertPath, sendredpack.WeixinCertPassword, data, SendRedPack_Url);
            writeLog(parameters, str2, SendRedPack_Url, msg);
            if (!(string.IsNullOrEmpty(msg) || !msg.Contains("SUCCESS")))
            {
                return "1";
            }
            Match match = new Regex(@"<return_msg><!\[CDATA\[(?<code>(.*))\]\]></return_msg>").Match(msg);
            if (match.Success)
            {
                str = match.Groups["code"].Value;
            }
            return str;
        }

        public string SendRedpack(string appId, string mch_id, string sub_mch_id, string nick_name, string send_name, string re_openid, string wishing, string client_ip, string act_name, string remark, int amount, string partnerkey, string weixincertpath, string weixincertpassword, int sendredpackrecordid)
        {
            if (client_ip == "::1")
            {
                client_ip = "120.25.125.177";
            }

            SendRedPackInfo sendredpack = new SendRedPackInfo {
                WXAppid = appId,
                Mch_Id = mch_id,
                Sub_Mch_Id = sub_mch_id,
                Nick_Name = nick_name,
                Send_Name = send_name,
                Re_Openid = re_openid,
                Wishing = wishing,
                Client_IP = client_ip,
                Act_Name = act_name,
                Remark = remark,
                Total_Amount = amount,
                PartnerKey = partnerkey,
                WeixinCertPath = weixincertpath,
                WeixinCertPassword = weixincertpassword,
                SendRedpackRecordID = sendredpackrecordid
            };
            return this.SendRedpack(sendredpack);
        }

        public static void writeLog(IDictionary<string, string> param, string sign, string url, string msg)
        {
            DataTable table = new DataTable {
                TableName = "log"
            };
            table.Columns.Add(new DataColumn("OperTime"));
            foreach (KeyValuePair<string, string> pair in param)
            {
                table.Columns.Add(new DataColumn(pair.Key));
            }
            table.Columns.Add(new DataColumn("Msg"));
            table.Columns.Add(new DataColumn("Sign"));
            table.Columns.Add(new DataColumn("Url"));
            DataRow row = table.NewRow();
            row["OperTime"] = DateTime.Now;
            foreach (KeyValuePair<string, string> pair in param)
            {
                row[pair.Key] = pair.Value;
            }
            row["Msg"] = msg;
            row["Sign"] = sign;
            row["Url"] = url;
            table.Rows.Add(row);
            table.WriteXml(HttpContext.Current.Server.MapPath("~/wxpay.xml"));
        }
    }
}

