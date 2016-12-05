using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using Ionic.Zlib;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.SiteSettings)]
	public class SMSSettings : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSaveSMSSettings;
		protected System.Web.UI.WebControls.Button btnTestSend;
		protected System.Web.UI.WebControls.Label lbNum;
		protected Script Script1;
		protected System.Web.UI.WebControls.HiddenField txtConfigData;
		protected System.Web.UI.WebControls.HiddenField txtSelectedName;
		protected System.Web.UI.WebControls.TextBox txtTestCellPhone;
		protected System.Web.UI.WebControls.TextBox txtTestSubject;
		private void btnSaveSMSSettings_Click(object sender, System.EventArgs e)
		{
			string str;
			ConfigData data = this.LoadConfig(out str);
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (string.IsNullOrEmpty(str) || data == null)
			{
				masterSettings.SMSSender = string.Empty;
				masterSettings.SMSSettings = string.Empty;
			}
			else
			{
				if (!data.IsValid)
				{
					string msg = "";
					foreach (string str2 in data.ErrorMsgs)
					{
						msg += Formatter.FormatErrorMessage(str2);
					}
					this.ShowMsg(msg, false);
					return;
				}
				masterSettings.SMSSender = str;
				masterSettings.SMSSettings = HiCryptographer.Encrypt(data.SettingsXml);
			}
			SettingsManager.Save(masterSettings);
			this.Page.Response.Redirect(Globals.GetAdminAbsolutePath("tools/SendMessageTemplets.aspx"));
		}
		private void btnTestSend_Click(object sender, System.EventArgs e)
		{
			string str;
			ConfigData data = this.LoadConfig(out str);
			if (string.IsNullOrEmpty(str) || data == null)
			{
				this.ShowMsg("请先选择发送方式并填写配置信息", false);
			}
			else
			{
				if (!data.IsValid)
				{
					string msg = "";
					foreach (string str2 in data.ErrorMsgs)
					{
						msg += Formatter.FormatErrorMessage(str2);
					}
					this.ShowMsg(msg, false);
				}
				else
				{
					if (string.IsNullOrEmpty(this.txtTestCellPhone.Text) || string.IsNullOrEmpty(this.txtTestSubject.Text) || this.txtTestCellPhone.Text.Trim().Length == 0 || this.txtTestSubject.Text.Trim().Length == 0)
					{
						this.ShowMsg("接收手机号和发送内容不能为空", false);
					}
					else
					{
						if (!Regex.IsMatch(this.txtTestCellPhone.Text.Trim(), "^(13|14|15|17|18)\\d{9}$"))
						{
							this.ShowMsg("请填写正确的手机号码", false);
						}
						else
						{
							string str3;
							bool success = SMSSender.CreateInstance(str, data.SettingsXml).Send(this.txtTestCellPhone.Text.Trim(), this.txtTestSubject.Text.Trim(), out str3);
							this.ShowMsg(str3, success);
						}
					}
				}
			}
		}
		protected string GetAmount(SiteSettings settings)
		{
			string result;
			if (string.IsNullOrEmpty(settings.SMSSettings))
			{
				result = "";
			}
			else
			{
				string xml = HiCryptographer.Decrypt(settings.SMSSettings);
				XmlDocument document = new XmlDocument();
				document.LoadXml(xml);
				string innerText = document.SelectSingleNode("xml/Appkey").InnerText;
				string postData = "method=getAmount&Appkey=" + innerText;
				string s = this.PostData("http://sms.kuaidiantong.cn/getAmount.aspx", postData);
				int num;
				if (int.TryParse(s, out num))
				{
					result = "您的短信剩余条数为：" + s.ToString();
				}
				else
				{
					result = "获取短信条数发生错误，请检查Appkey是否输入正确!";
				}
			}
			return result;
		}
		private ConfigData LoadConfig(out string selectedName)
		{
			selectedName = base.Request.Form["ddlSms"];
            if (null == selectedName || string.IsNullOrEmpty(selectedName))
            {
                selectedName = "hishop.plugins.sms.huiyisms";
            }
			this.txtSelectedName.Value = selectedName;
			this.txtConfigData.Value = "";
			ConfigData result;
			if (string.IsNullOrEmpty(selectedName) || selectedName.Length == 0)
			{
				result = null;
			}
			else
			{
				ConfigablePlugin plugin = SMSSender.CreateInstance(selectedName);
				if (plugin == null)
				{
					result = null;
				}
				else
				{
					ConfigData configData = plugin.GetConfigData(base.Request.Form);
					if (configData != null)
					{
						this.txtConfigData.Value = configData.SettingsXml;
					}
					result = configData;
				}
			}
			return result;
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnSaveSMSSettings.Click += new System.EventHandler(this.btnSaveSMSSettings_Click);
			this.btnTestSend.Click += new System.EventHandler(this.btnTestSend_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				if (masterSettings.SMSEnabled)
				{
                    this.txtSelectedName.Value = masterSettings.SMSSender.ToLower();
					ConfigData data = new ConfigData(HiCryptographer.Decrypt(masterSettings.SMSSettings));
					this.txtConfigData.Value = data.SettingsXml;
				}
				//this.lbNum.Text = this.GetAmount(masterSettings);
                this.txtSelectedName.Value = "hishop.plugins.sms.huiyisms";
			}
		}
		public string PostData(string url, string postData)
		{
			string str = string.Empty;
			string result;
			try
			{
				Uri requestUri = new Uri(url);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = (long)bytes.Length;
				using (System.IO.Stream stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (System.IO.Stream stream2 = response.GetResponseStream())
					{
						System.Text.Encoding encoding = System.Text.Encoding.UTF8;
						System.IO.Stream stream3 = stream2;
						if (response.ContentEncoding.ToLower() == "gzip")
						{
							stream3 = new GZipStream(stream2, CompressionMode.Decompress);
						}
						else
						{
							if (response.ContentEncoding.ToLower() == "deflate")
							{
								stream3 = new DeflateStream(stream2, CompressionMode.Decompress);
							}
						}
						using (System.IO.StreamReader reader = new System.IO.StreamReader(stream3, encoding))
						{
							result = reader.ReadToEnd();
							return result;
						}
					}
				}
			}
			catch (System.Exception exception)
			{
				str = string.Format("获取信息错误：{0}", exception.Message);
			}
			result = str;
			return result;
		}
	}
}
