using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class VIPcard : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button BtnUploadBG;
		protected System.Web.UI.WebControls.Button BtnUploadQR;
		protected System.Web.UI.WebControls.CheckBox ChkAddres;
		protected System.Web.UI.WebControls.CheckBox ChkCoupon;
		protected System.Web.UI.WebControls.CheckBox ChkVipMobile;
		protected System.Web.UI.WebControls.CheckBox ChkVipName;
		protected System.Web.UI.WebControls.CheckBox ChkVipQQ;
		protected System.Web.UI.WebControls.FileUpload FileUploadBG;
		protected System.Web.UI.WebControls.FileUpload FileUploadQR;
		protected System.Web.UI.HtmlControls.HtmlImage imgbg;
		protected System.Web.UI.HtmlControls.HtmlImage imgqrcode;
		protected System.Web.UI.WebControls.LinkButton Lkdel;
		protected System.Web.UI.WebControls.TextBox TbCardName;
		protected System.Web.UI.WebControls.TextBox Tblogo;
		protected System.Web.UI.WebControls.TextBox TBPrefix;
		protected System.Web.UI.WebControls.TextBox txtVipRemark;
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.VipCardName = this.TbCardName.Text;
			if (this.TBPrefix.Text.StartsWith("0"))
			{
				this.ShowMsg("会员卡前缀不能以0开始", false);
			}
			else
			{
				masterSettings.VipCardPrefix = this.TBPrefix.Text;
				masterSettings.VipEnableCoupon = this.ChkCoupon.Checked;
				masterSettings.VipRequireAdress = this.ChkAddres.Checked;
				masterSettings.VipRequireMobile = this.ChkVipMobile.Checked;
				masterSettings.VipRequireName = this.ChkVipName.Checked;
				masterSettings.VipRequireQQ = this.ChkVipQQ.Checked;
				masterSettings.VipCardLogo = this.Tblogo.Text;
				masterSettings.VipRemark = this.txtVipRemark.Text;
				SettingsManager.Save(masterSettings);
				this.ShowMsg("成功修改了会员卡信息设置", true);
				this.LoadSiteContent(masterSettings);
			}
		}
		protected void BtnUploadBG_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (this.FileUploadBG.HasFile)
			{
				try
				{
					string str = VShopHelper.UploadVipBGImage(this.FileUploadBG.PostedFile);
					if (!string.IsNullOrEmpty(str))
					{
						masterSettings.VipCardBG = str;
						SettingsManager.Save(masterSettings);
						this.LoadSiteContent(masterSettings);
					}
					else
					{
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件!", false);
					}
				}
				catch
				{
					this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
				}
			}
			else
			{
				this.ShowMsg("你还没有选择背景图片", false);
			}
		}
		protected void BtnUploadQR_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (this.FileUploadQR.HasFile)
			{
				try
				{
					string str = VShopHelper.UploadVipQRImage(this.FileUploadQR.PostedFile);
					if (!string.IsNullOrEmpty(str))
					{
						masterSettings.VipCardQR = str;
						SettingsManager.Save(masterSettings);
						this.LoadSiteContent(masterSettings);
						this.imgqrcode.Visible = true;
					}
					else
					{
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件!", false);
					}
				}
				catch
				{
					this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
				}
			}
			else
			{
				this.ShowMsg("你还没有选择二维码图片", false);
			}
		}
		protected void Lkdel_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			string src = this.imgqrcode.Src;
			try
			{
				StoreHelper.DeleteImage(src);
			}
			catch
			{
			}
			masterSettings.VipCardQR = string.Empty;
			SettingsManager.Save(masterSettings);
			this.LoadSiteContent(masterSettings);
		}
		private void LoadSiteContent(SiteSettings siteSettings)
		{
			this.TbCardName.Text = siteSettings.VipCardName;
			this.TBPrefix.Text = siteSettings.VipCardPrefix;
			if (!string.IsNullOrEmpty(siteSettings.VipCardBG))
			{
				this.imgbg.Src = siteSettings.VipCardBG;
			}
			if (!string.IsNullOrEmpty(siteSettings.VipCardQR))
			{
				this.imgqrcode.Src = siteSettings.VipCardQR;
				this.Lkdel.Visible = true;
			}
			else
			{
				this.imgqrcode.Visible = false;
				this.Lkdel.Visible = false;
			}
			this.ChkAddres.Checked = siteSettings.VipRequireAdress;
			this.ChkVipMobile.Checked = siteSettings.VipRequireMobile;
			this.ChkVipName.Checked = siteSettings.VipRequireName;
			this.ChkVipQQ.Checked = siteSettings.VipRequireQQ;
			this.ChkCoupon.Checked = siteSettings.VipEnableCoupon;
			this.Tblogo.Text = siteSettings.VipCardLogo;
			this.txtVipRemark.Text = siteSettings.VipRemark;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.LoadSiteContent(masterSettings);
			}
		}
	}
}
