namespace Hishop.Weixin.Pay
{
    using Hishop.Weixin.Pay.Domain;
    using Hishop.Weixin.Pay.Notify;
    using Hishop.Weixin.Pay.Util;
    using System;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    public class NotifyClient
    {
        private PayAccount _payAccount;
        public static readonly string Update_Feedback_Url = "https://api.weixin.qq.com/payfeedback/update";

        public NotifyClient(PayAccount account) : this(account.AppId, account.AppSecret, account.PartnerId, account.PartnerKey, account.PaySignKey)
        {
        }

        public NotifyClient(string appId, string appSecret, string partnerId, string partnerKey, string paySignKey)
        {
            PayAccount account = new PayAccount {
                AppId = appId,
                AppSecret = appSecret,
                PartnerId = partnerId,
                PartnerKey = partnerKey,
                PaySignKey = paySignKey
            };
            this._payAccount = account;
        }

        public DataTable ErrorTable(string tabName = "Notify")
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("OperTime"));
            table.Columns.Add(new DataColumn("Error"));
            table.Columns.Add(new DataColumn("Param"));
            table.Columns.Add(new DataColumn("PayInfo"));
            table.TableName = tabName;
            return table;
        }

        public AlarmNotify GetAlarmNotify(Stream inStream)
        {
            string xml = this.ReadString(inStream);
            return this.GetAlarmNotify(xml);
        }

        public AlarmNotify GetAlarmNotify(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            AlarmNotify notifyObject = Utils.GetNotifyObject<AlarmNotify>(xml);
            if (!((notifyObject != null) && this.ValidAlarmSign(notifyObject)))
            {
                return null;
            }
            return notifyObject;
        }

        public FeedBackNotify GetFeedBackNotify(Stream inStream)
        {
            string xml = this.ReadString(inStream);
            return this.GetFeedBackNotify(xml);
        }

        public FeedBackNotify GetFeedBackNotify(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            FeedBackNotify notifyObject = Utils.GetNotifyObject<FeedBackNotify>(xml);
            if (!((notifyObject != null) && this.ValidFeedbackSign(notifyObject)))
            {
                return null;
            }
            return notifyObject;
        }

        public PayNotify GetPayNotify(Stream inStream)
        {
            string xml = this.ReadString(inStream);
            return this.GetPayNotify(xml);
        }

        public PayNotify GetPayNotify(string xml)
        {
            DataTable dt = this.ErrorTable("Notify");
            DataRow row = dt.NewRow();
            row["OperTime"] = DateTime.Now;
            try
            {
                if (string.IsNullOrEmpty(xml))
                {
                    return null;
                }
                PayNotify notifyObject = Utils.GetNotifyObject<PayNotify>(xml);
                string servicesign = "";
                if (!((notifyObject != null) && this.ValidPaySign(notifyObject, out servicesign)))
                {
                    row["Error"] = (notifyObject == null) ? "Notify Null" : "Valid pay Sign Error";
                    row["Param"] = xml;
                    row["PayInfo"] = notifyObject.out_trade_no + "-" + servicesign + "-" + this._payAccount.PartnerKey;
                    dt.Rows.Add(row);
                    this.WriteLog(dt);
                    return null;
                }
                PayInfo info = new PayInfo {
                    SignType = "MD5",
                    Sign = notifyObject.sign,
                    TradeMode = 0,
                    BankType = notifyObject.bank_type,
                    BankBillNo = "",
                    TotalFee = notifyObject.total_fee / 100M,
                    FeeType = (notifyObject.fee_type == "CNY") ? 1 : 0,
                    NotifyId = "",
                    TransactionId = notifyObject.transaction_id,
                    OutTradeNo = notifyObject.out_trade_no,
                    TransportFee = 0M,
                    ProductFee = 0M,
                    Discount = 1M,
                    BuyerAlias = ""
                };
                notifyObject.PayInfo = info;
                return notifyObject;
            }
            catch (Exception exception)
            {
                row["Error"] = exception.Message;
                row["Param"] = xml;
                dt.Rows.Add(row);
                this.WriteLog(dt);
                return null;
            }
        }

        private string ReadString(Stream inStream)
        {
            if (inStream == null)
            {
                return null;
            }
            byte[] buffer = new byte[inStream.Length];
            inStream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        public bool UpdateFeedback(string feedbackid, string openid)
        {
            string token = Utils.GetToken(this._payAccount.AppId, this._payAccount.AppSecret);
            return this.UpdateFeedback(feedbackid, openid, token);
        }

        public bool UpdateFeedback(string feedbackid, string openid, string token)
        {
            string url = string.Format("{0}?access_token={1}&openid={2}&feedbackid={3}", new object[] { Update_Feedback_Url, token, openid, feedbackid });
            string str2 = new WebUtils().DoGet(url);
            if (!(!string.IsNullOrEmpty(str2) && str2.Contains("ok")))
            {
                return false;
            }
            return true;
        }

        private bool ValidAlarmSign(AlarmNotify notify)
        {
            return true;
        }

        private bool ValidFeedbackSign(FeedBackNotify notify)
        {
            PayDictionary parameters = new PayDictionary();
            parameters.Add("appid", this._payAccount.AppId);
            parameters.Add("timestamp", notify.TimeStamp);
            parameters.Add("openid", notify.OpenId);
            return (notify.AppSignature == SignHelper.SignPay(parameters, ""));
        }

        private bool ValidPaySign(PayNotify notify, out string servicesign)
        {
            PayDictionary parameters = new PayDictionary();
            parameters.Add("appid", notify.appid);
            parameters.Add("bank_type", notify.bank_type);
            parameters.Add("cash_fee", notify.cash_fee);
            parameters.Add("fee_type", notify.fee_type);
            parameters.Add("is_subscribe", notify.is_subscribe);
            parameters.Add("mch_id", notify.mch_id);
            parameters.Add("nonce_str", notify.nonce_str);
            parameters.Add("openid", notify.openid);
            parameters.Add("out_trade_no", notify.out_trade_no);
            parameters.Add("result_code", notify.result_code);
            parameters.Add("return_code", notify.return_code);
            parameters.Add("sub_mch_id", notify.sub_mch_id);
            parameters.Add("time_end", notify.time_end);
            parameters.Add("total_fee", notify.total_fee);
            parameters.Add("trade_type", notify.trade_type);
            parameters.Add("transaction_id", notify.transaction_id);
            servicesign = SignHelper.SignPay(parameters, this._payAccount.PartnerKey);
            bool flag = notify.sign == servicesign;
            servicesign = servicesign + "-" + SignHelper.BuildQuery(parameters, false);
            return flag;
        }

        public void WriteLog(DataTable dt)
        {
            dt.WriteXml(HttpContext.Current.Request.MapPath("/NotifyError.xml"));
        }
    }
}

