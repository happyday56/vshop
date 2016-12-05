using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class VServerConfig : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected ImageLinkButton btnPicDelete;
		protected System.Web.UI.WebControls.Button btnUpoad;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chk_manyService;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkIsValidationService;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected HiImage imgPic;
		protected System.Web.UI.WebControls.Literal litKeycode;
		protected System.Web.UI.HtmlControls.HtmlGenericControl P1;
		protected System.Web.UI.WebControls.TextBox txtAppId;
		protected System.Web.UI.WebControls.TextBox txtAppSecret;
		protected System.Web.UI.WebControls.TextBox txtShopIntroduction;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtShopIntroductionTip;
		protected System.Web.UI.WebControls.TextBox txtSiteName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtSiteNameTip;
		protected System.Web.UI.WebControls.Literal txtToken;
		protected System.Web.UI.WebControls.Literal txtUrl;
		protected System.Web.UI.WebControls.TextBox txtWeixinLoginUrl;
		protected System.Web.UI.WebControls.TextBox txtWeixinNumber;
        protected System.Web.UI.WebControls.TextBox txtGuidePageSet;

		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.WeixinAppId = this.txtAppId.Text;
			masterSettings.WeixinAppSecret = this.txtAppSecret.Text;
			masterSettings.IsValidationService = this.chkIsValidationService.Checked;
			masterSettings.WeixinNumber = this.txtWeixinNumber.Text;
			masterSettings.WeixinLoginUrl = this.txtWeixinLoginUrl.Text;
			masterSettings.SiteName = this.txtSiteName.Text;
			masterSettings.OpenManyService = this.chk_manyService.Checked;
			masterSettings.ShopIntroduction = this.txtShopIntroduction.Text.Trim();
            masterSettings.GuidePageSet = this.txtGuidePageSet.Text.Trim();
			SettingsManager.Save(masterSettings);
			this.ShowMsg("修改成功", true);
		}
		private void btnPicDelete_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (!string.IsNullOrEmpty(masterSettings.WeiXinCodeImageUrl))
			{
				ResourcesHelper.DeleteImage(masterSettings.WeiXinCodeImageUrl);
				this.btnPicDelete.Visible = false;
				masterSettings.WeiXinCodeImageUrl = (this.imgPic.ImageUrl = string.Empty);
				SettingsManager.Save(masterSettings);
			}
		}
		private void btnUpoad_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (this.fileUpload.HasFile)
			{
				try
				{
					if (!string.IsNullOrEmpty(masterSettings.WeiXinCodeImageUrl))
					{
						ResourcesHelper.DeleteImage(masterSettings.WeiXinCodeImageUrl);
					}
					this.imgPic.ImageUrl = (masterSettings.WeiXinCodeImageUrl = VShopHelper.UploadWeiXinCodeImage(this.fileUpload.PostedFile));
					this.btnPicDelete.Visible = true;
					SettingsManager.Save(masterSettings);
				}
				catch
				{
					this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
				}
			}
		}
		private string CreateKey(int len)
		{
			byte[] data = new byte[len];
			new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(data);
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				builder.Append(string.Format("{0:X2}", data[i]));
			}
			return builder.ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnUpoad.Click += new System.EventHandler(this.btnUpoad_Click);
			this.btnPicDelete.Click += new System.EventHandler(this.btnPicDelete_Click);
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				if (string.IsNullOrEmpty(masterSettings.WeixinToken))
				{
					masterSettings.WeixinToken = this.CreateKey(8);
					SettingsManager.Save(masterSettings);
				}
				if (string.IsNullOrWhiteSpace(masterSettings.CheckCode))
				{
					masterSettings.CheckCode = this.CreateKey(20);
					SettingsManager.Save(masterSettings);
				}
				this.txtSiteName.Text = masterSettings.SiteName;
				this.txtAppId.Text = masterSettings.WeixinAppId;
				this.txtAppSecret.Text = masterSettings.WeixinAppSecret;
				this.txtToken.Text = masterSettings.WeixinToken;
				this.chkIsValidationService.Checked = masterSettings.IsValidationService;
				this.imgPic.ImageUrl = masterSettings.WeiXinCodeImageUrl;
				this.txtWeixinNumber.Text = masterSettings.WeixinNumber;
				this.txtWeixinLoginUrl.Text = masterSettings.WeixinLoginUrl;
				this.btnPicDelete.Visible = !string.IsNullOrEmpty(masterSettings.WeiXinCodeImageUrl);
				this.litKeycode.Text = masterSettings.CheckCode;
				this.txtUrl.Text = string.Format("http://{0}/api/wx.ashx", base.Request.Url.Host, this.txtToken.Text);
				this.chk_manyService.Checked = masterSettings.OpenManyService;
				this.txtShopIntroduction.Text = masterSettings.ShopIntroduction;
                this.txtGuidePageSet.Text = masterSettings.GuidePageSet;
			}
		}
	}
}
