using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Configuration;
using Hidistro.Core.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.SiteSettings)]
	public class EmailSettings : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnChangeEmailSettings;
		protected System.Web.UI.WebControls.Button btnTestEmailSettings;
		protected Script Script1;
		protected System.Web.UI.WebControls.HiddenField txtConfigData;
		protected System.Web.UI.WebControls.HiddenField txtSelectedName;
		protected System.Web.UI.WebControls.TextBox txtTestEmail;
		private void btnChangeEmailSettings_Click(object sender, System.EventArgs e)
		{
			string str;
			ConfigData data = this.LoadConfig(out str);
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (string.IsNullOrEmpty(str) || data == null)
			{
				masterSettings.EmailSender = string.Empty;
				masterSettings.EmailSettings = string.Empty;
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
				masterSettings.EmailSender = str;
				masterSettings.EmailSettings = HiCryptographer.Encrypt(data.SettingsXml);
			}
			SettingsManager.Save(masterSettings);
			this.ShowMsg("配置成功", true);
		}
		private void btnTestEmailSettings_Click(object sender, System.EventArgs e)
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
					if (string.IsNullOrEmpty(this.txtTestEmail.Text) || this.txtTestEmail.Text.Trim().Length == 0)
					{
						this.ShowMsg("请填写接收测试邮件的邮箱地址", false);
					}
					else
					{
						if (!Regex.IsMatch(this.txtTestEmail.Text.Trim(), "([a-zA-Z\\.0-9_-])+@([a-zA-Z0-9_-])+((\\.[a-zA-Z0-9_-]{2,4}){1,2})"))
						{
							this.ShowMsg("请填写正确的邮箱地址", false);
						}
						else
						{
							MailMessage mail = new MailMessage
							{
								IsBodyHtml = true,
								Priority = MailPriority.High,
								Body = "Success",
								Subject = "This is a test mail"
							};
							mail.To.Add(this.txtTestEmail.Text.Trim());
							EmailSender sender2 = EmailSender.CreateInstance(str, data.SettingsXml);
							try
							{
								if (sender2.Send(mail, System.Text.Encoding.GetEncoding(HiConfiguration.GetConfig().EmailEncoding)))
								{
									this.ShowMsg("发送测试邮件成功", true);
								}
								else
								{
									this.ShowMsg("发送测试邮件失败", false);
								}
							}
							catch
							{
								this.ShowMsg("邮件配置错误", false);
							}
						}
					}
				}
			}
		}
		private ConfigData LoadConfig(out string selectedName)
		{
			selectedName = base.Request.Form["ddlEmails"];
			this.txtSelectedName.Value = selectedName;
			this.txtConfigData.Value = "";
			ConfigData result;
			if (string.IsNullOrEmpty(selectedName) || selectedName.Length == 0)
			{
				result = null;
			}
			else
			{
				ConfigablePlugin plugin = EmailSender.CreateInstance(selectedName);
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
			this.btnChangeEmailSettings.Click += new System.EventHandler(this.btnChangeEmailSettings_Click);
			this.btnTestEmailSettings.Click += new System.EventHandler(this.btnTestEmailSettings_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				if (masterSettings.EmailEnabled)
				{
					this.txtSelectedName.Value = masterSettings.EmailSender.ToLower();
					ConfigData data = new ConfigData(HiCryptographer.Decrypt(masterSettings.EmailSettings));
					this.txtConfigData.Value = data.SettingsXml;
				}
			}
		}
	}
}
