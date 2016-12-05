using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using kindeditor.Net;
using System;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.PaymentModes)]
	public class EditPaymentType : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.CheckBox chkIsPercent;
		protected KindeditorControl fcContent;
		private int modeId;
		protected YesNoRadioButtonList radiIsUseInDistributor;
		protected YesNoRadioButtonList radiIsUseInpour;
		protected Script Script1;
		protected System.Web.UI.WebControls.TextBox txtCharge;
		protected System.Web.UI.WebControls.HiddenField txtConfigData;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.HiddenField txtSelectedName;
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			string str;
			ConfigData data;
			decimal num;
			if (this.ValidateValues(out str, out data, out num))
			{
				PaymentModeInfo paymentMode = new PaymentModeInfo
				{
					ModeId = this.modeId,
					Name = this.txtName.Text.Trim(),
					Description = this.fcContent.Text.Replace("\r\n", "").Replace("\r", "").Replace("\n", ""),
					Gateway = str,
					IsUseInpour = this.radiIsUseInpour.SelectedValue,
					IsUseInDistributor = this.radiIsUseInDistributor.SelectedValue,
					Charge = num,
					IsPercent = this.chkIsPercent.Checked,
					Settings = HiCryptographer.Encrypt(data.SettingsXml)
				};
				PaymentModeActionStatus paymentModeActionStatus = SalesHelper.UpdatePaymentMode(paymentMode);
				switch (paymentModeActionStatus)
				{
				case PaymentModeActionStatus.Success:
					base.Response.Redirect(Globals.GetAdminAbsolutePath("sales/PaymentTypes.aspx"));
					break;
				case PaymentModeActionStatus.DuplicateName:
					this.ShowMsg("已经存在一个相同的支付方式名称", false);
					break;
				case PaymentModeActionStatus.OutofNumber:
					this.ShowMsg("支付方式的数目已经超出系统设置的数目", false);
					break;
				default:
					if (paymentModeActionStatus == PaymentModeActionStatus.UnknowError)
					{
						this.ShowMsg("未知错误", false);
					}
					break;
				}
			}
		}
		private ConfigData LoadConfig(out string selectedName)
		{
			selectedName = base.Request.Form["ddlPayments"];
			this.txtSelectedName.Value = selectedName;
			this.txtConfigData.Value = "";
			ConfigData result;
			if (string.IsNullOrEmpty(selectedName) || selectedName.Length == 0)
			{
				result = null;
			}
			else
			{
				ConfigablePlugin plugin = PaymentRequest.CreateInstance(selectedName);
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
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["modeId"], out this.modeId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
				if (!this.Page.IsPostBack)
				{
					PaymentModeInfo paymentMode = SalesHelper.GetPaymentMode(this.modeId);
					if (paymentMode == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
						Globals.EntityCoding(paymentMode, false);
						this.txtSelectedName.Value = paymentMode.Gateway.ToLower();
						ConfigData data = new ConfigData(HiCryptographer.Decrypt(paymentMode.Settings));
						this.txtConfigData.Value = data.SettingsXml;
						this.txtName.Text = paymentMode.Name;
						this.fcContent.Text = paymentMode.Description;
						this.txtCharge.Text = paymentMode.Charge.ToString("F", System.Globalization.CultureInfo.InvariantCulture);
						this.chkIsPercent.Checked = paymentMode.IsPercent;
						this.radiIsUseInpour.SelectedValue = paymentMode.IsUseInpour;
						this.radiIsUseInDistributor.SelectedValue = paymentMode.IsUseInDistributor;
					}
				}
			}
		}
		private bool ValidateValues(out string selectedPlugin, out ConfigData data, out decimal payCharge)
		{
			string str = string.Empty;
			data = this.LoadConfig(out selectedPlugin);
			payCharge = 0m;
			bool result;
			if (string.IsNullOrEmpty(selectedPlugin))
			{
				this.ShowMsg("请先选择一个支付接口类型", false);
				result = false;
			}
			else
			{
				if (!data.IsValid)
				{
					foreach (string str2 in data.ErrorMsgs)
					{
						str += Formatter.FormatErrorMessage(str2);
					}
				}
				if (!decimal.TryParse(this.txtCharge.Text, out payCharge))
				{
					str += Formatter.FormatErrorMessage("支付手续费无效,大小在0-10000000之间");
				}
				if (payCharge < 0m || payCharge > 10000000m)
				{
					str += Formatter.FormatErrorMessage("支付手续费大小1-10000000之间");
				}
				if (string.IsNullOrEmpty(this.txtName.Text) || this.txtName.Text.Length > 60)
				{
					str += Formatter.FormatErrorMessage("支付方式名称不能为空，长度限制在1-60个字符之间");
				}
				if (!string.IsNullOrEmpty(str))
				{
					this.ShowMsg(str, false);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
	}
}
