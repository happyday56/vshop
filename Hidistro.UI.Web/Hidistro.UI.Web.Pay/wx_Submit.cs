using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.SaleSystem.Vshop;
using Hishop.Weixin.Pay;
using Hishop.Weixin.Pay.Domain;
using System;
using System.Web.UI;
using NewLife.Log;

namespace Hidistro.UI.Web.Pay
{
	public class wx_Submit : System.Web.UI.Page
	{
		public string pay_json = "";
        public string isPromotionBuy = "";
        public string orderId = "";

		public string ConvertPayJson(PayRequestInfo req)
		{
			string packageValue = "{";
			packageValue = packageValue + " \"appId\": \"" + req.appId + "\", ";
			packageValue = packageValue + " \"timeStamp\": \"" + req.timeStamp + "\", ";
			packageValue = packageValue + " \"nonceStr\": \"" + req.nonceStr + "\", ";
			packageValue = packageValue + " \"package\": \"" + req.package + "\", ";
			packageValue += " \"signType\": \"MD5\", ";
			return packageValue + " \"paySign\": \"" + req.paySign + "\"}";
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string str = base.Request.QueryString.Get("orderId");
			if (!string.IsNullOrEmpty(str))
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(str);
				if (orderInfo != null)
				{
                    // 如果订单中有分销商默认的推广商品时，不计算佣金
                    if (DistributorsBrower.CheckIsDistributoBuyProduct(orderInfo.OrderId))
                    {
                        this.isPromotionBuy = "1";
                    }
                    else
                    {
                        this.isPromotionBuy = "0";
                    }

                    XTrace.WriteLine("订单类型：0->正常订单;1->开店订单----" + this.isPromotionBuy);

					PackageInfo package = new PackageInfo
					{
						Body = orderInfo.OrderId,
						NotifyUrl = string.Format("http://{0}/pay/wx_Pay.aspx", base.Request.Url.Host),
						OutTradeNo = orderInfo.OrderId,
						TotalFee = (int)(orderInfo.GetTotal() * 100m)
					};
					if (package.TotalFee < 1m)
					{
						package.TotalFee = 1m;
					}
					string openId = "";
					MemberInfo currentMember = MemberProcessor.GetCurrentMember();
					if (currentMember != null)
					{
						openId = currentMember.OpenId;
					}
					package.OpenId = openId;
					SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
					PayRequestInfo req = new PayClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).BuildPayRequest(package);
					this.pay_json = this.ConvertPayJson(req);
                    orderId = orderInfo.OrderId.ToString();
                    XTrace.WriteLine("支付时的订单ID：" + orderId);
				}
			}
		}
	}
}
