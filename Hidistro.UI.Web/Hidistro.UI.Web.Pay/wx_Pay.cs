using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Messages;
using Hidistro.SaleSystem.Vshop;
using Hishop.Weixin.Pay;
using Hishop.Weixin.Pay.Notify;
using System;
using System.Web.UI;
using NewLife.Log;

namespace Hidistro.UI.Web.Pay
{
	public class wx_Pay : System.Web.UI.Page
	{
		protected OrderInfo Order;
		protected string OrderId;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			PayNotify payNotify = new NotifyClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).GetPayNotify(base.Request.InputStream);
            
			if (payNotify != null)
			{

				this.OrderId = payNotify.PayInfo.OutTradeNo;
				this.Order = ShoppingProcessor.GetOrderInfo(this.OrderId);
				if (this.Order == null)
				{
					base.Response.Write("success");
				}
				else
				{
					this.Order.GatewayOrderId = payNotify.PayInfo.TransactionId;
					this.UserPayOrder();
				}
			}
		}
		private void UserPayOrder()
		{
			if (this.Order.OrderStatus == OrderStatus.BuyerAlreadyPaid)
			{
				base.Response.Write("success");
			}
			else
			{
				if (this.Order.CheckAction(OrderActions.BUYER_PAY) && MemberProcessor.UserPayOrder(this.Order))
				{
                    //if (this.Order.UserId != 0 && this.Order.UserId != 1100)
                    //{
                    //    MemberInfo member = MemberProcessor.GetMember(this.Order.UserId);
                    //    if (member != null)
                    //    {
                    //        Messenger.OrderPayment(member, this.OrderId, this.Order.GetTotal());
                    //        XTrace.WriteLine("支付微信通知1");
                    //    }
                    //}
					this.Order.OnPayment();
					base.Response.Write("success");
				}
			}
		}
	}
}
