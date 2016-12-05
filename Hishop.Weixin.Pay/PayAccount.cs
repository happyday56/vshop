namespace Hishop.Weixin.Pay
{
    using System;
    using System.Runtime.CompilerServices;

    public class PayAccount
    {
        public PayAccount()
        {
        }

        public PayAccount(string appId, string appSecret, string partnerId, string partnerKey, string paySignKey)
        {
            this.AppId = appId;
            this.AppSecret = appSecret;
            this.PartnerId = partnerId;
            this.PartnerKey = partnerKey;
            this.PaySignKey = paySignKey;
        }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string PartnerId { get; set; }

        public string PartnerKey { get; set; }

        public string PaySignKey { get; set; }
    }
}

