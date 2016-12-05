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
	public class wx_Alarm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Write("success");
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			AlarmNotify alarmNotify = new NotifyClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey).GetAlarmNotify(base.Request.InputStream);
			if (alarmNotify != null)
			{
				AlarmInfo info = new AlarmInfo
				{
					AlarmContent = alarmNotify.AlarmContent,
					AppId = alarmNotify.AppId,
					Description = alarmNotify.Description
				};
				VShopHelper.SaveAlarm(info);
			}
		}
	}
}
