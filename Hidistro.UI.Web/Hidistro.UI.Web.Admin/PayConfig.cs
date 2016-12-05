using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;


namespace Hidistro.UI.Web.Admin
{
    /// <summary>
    /// 微信支付配置
    /// </summary>
    public partial class PayConfig : AdminPage
    {

        #region 字段
        private string m_CertPath;
        protected TextBox txtAppId;
        protected TextBox txtAppSecret;
        protected TextBox txtPartnerID;
        protected TextBox txtPartnerKey;
        protected TextBox txtPaySignKey;
        protected FileUpload fileUploader;
        protected HtmlGenericControl labfilename;
        protected TextBox txtCertPassword;
        protected YesNoRadioButtonList radEnableHtmRewrite;
        protected Button btnOK;
        #endregion

        

        protected void Page_Load(object sender, EventArgs e)
		{

			this.btnOK.Click += new EventHandler(this.btnOK_Click);

			this.m_CertPath = this.Page.Request.MapPath("~/Pay/Cert");

			if (!base.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.txtAppId.Text = masterSettings.WeixinAppId;
				this.txtAppSecret.Text = masterSettings.WeixinAppSecret;
				this.txtPartnerID.Text = masterSettings.WeixinPartnerID;
				this.txtPartnerKey.Text = masterSettings.WeixinPartnerKey;
				this.txtPaySignKey.Text = masterSettings.WeixinPaySignKey;
				this.radEnableHtmRewrite.SelectedValue = masterSettings.EnableWeiXinRequest;
				this.labfilename.InnerText = (masterSettings.WeixinCertPath != "" ? string.Concat("已上传：", masterSettings.WeixinCertPath.Substring(masterSettings.WeixinCertPath.LastIndexOf(@"\") + 1, masterSettings.WeixinCertPath.Length - masterSettings.WeixinCertPath.LastIndexOf(@"\") - 1)) : "");
				this.txtCertPassword.Text = masterSettings.WeixinCertPassword;
			}
		}

        protected void btnOK_Click(object sender, EventArgs e)
        {
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            masterSettings.WeixinAppId = this.txtAppId.Text;
            masterSettings.WeixinAppSecret = this.txtAppSecret.Text;
            masterSettings.WeixinPartnerID = this.txtPartnerID.Text;
            masterSettings.WeixinPartnerKey = this.txtPartnerKey.Text;
            masterSettings.WeixinPaySignKey = this.txtPaySignKey.Text;
            masterSettings.EnableWeiXinRequest = this.radEnableHtmRewrite.SelectedValue;

            if (this.fileUploader.PostedFile.FileName != "")
            {
                if (!this.IsAllowableFileType(this.fileUploader.PostedFile.FileName))
                {
                    this.ShowMsg("请上传正确的文件", false);
                    return;
                }
                DateTime now = DateTime.Now;
                string str = string.Concat(now.ToString("yyyyMMddhhmmss"), Path.GetFileName(this.fileUploader.PostedFile.FileName));
                this.fileUploader.PostedFile.SaveAs(Path.Combine(this.m_CertPath, str));
                masterSettings.WeixinCertPath = Path.Combine(this.m_CertPath, str);
            }
            masterSettings.WeixinCertPassword = this.txtCertPassword.Text;
            SettingsManager.Save(masterSettings);
            this.ShowMsg("设置成功", true);
        }

        protected bool IsAllowableFileType(string FileName)
        {
            if (".p12".IndexOf(Path.GetExtension(FileName).ToLower()) != -1)
            {
                return true;
            }
            return false;
        }


    }
}