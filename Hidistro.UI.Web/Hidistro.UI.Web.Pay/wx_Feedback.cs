using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.VShop;
using Hishop.Weixin.Pay;
using Hishop.Weixin.Pay.Notify;
using System;
using System.Web.UI;
namespace Hidistro.UI.Web.Pay
{
	public class wx_Feedback : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Write("success");
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			FeedBackNotify feedBackNotify = new NotifyClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).GetFeedBackNotify(base.Request.InputStream);
			if (feedBackNotify != null)
			{
				string msgType = feedBackNotify.MsgType;
				if (msgType != null)
				{
					if (!(msgType == "request"))
					{
						if (msgType == "confirm")
						{
							feedBackNotify.MsgType = "已完成";
						}
					}
					else
					{
						feedBackNotify.MsgType = "未处理";
					}
				}
				if (VShopHelper.GetFeedBack(feedBackNotify.FeedBackId) != null)
				{
					VShopHelper.UpdateFeedBackMsgType(feedBackNotify.FeedBackId, feedBackNotify.MsgType);
				}
				else
				{
					FeedBackInfo info = new FeedBackInfo
					{
						AppId = feedBackNotify.AppId,
						ExtInfo = feedBackNotify.ExtInfo,
						FeedBackId = feedBackNotify.FeedBackId,
						MsgType = feedBackNotify.MsgType,
						OpenId = feedBackNotify.OpenId,
						Reason = feedBackNotify.Reason,
						Solution = feedBackNotify.Solution,
						TransId = feedBackNotify.TransId
					};
					VShopHelper.SaveFeedBack(info);
				}
			}
		}
	}
}
