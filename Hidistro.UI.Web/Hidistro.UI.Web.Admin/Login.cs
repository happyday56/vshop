using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnAdminLogin;
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		protected HeadContainer HeadContainer1;
		protected SmallStatusMessage lblStatus;
		private readonly string licenseMsg = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <Hi:HeadContainer ID=\"HeadContainer1\" runat=\"server\" />\r\n    <Hi:PageTitle ID=\"PageTitle1\" runat=\"server\" />\r\n    <link rel=\"stylesheet\" href=\"css/login.css\" type=\"text/css\" media=\"screen\" />\r\n</head>\r\n<body>\r\n<div class=\"admin\">\r\n<div id=\"\" class=\"wrap\">\r\n<div class=\"main\" style=\"position:relative\">\r\n    <div class=\"LoginBack\">\r\n     <div>\r\n     <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n      <tr>\r\n        <td class=\"td1\"><img src=\"images/comeBack.gif\" width=\"56\" height=\"49\" /></td>\r\n        <td class=\"td2\">您正在使用的微分销系统未经官方授权，无法登录后台管理。请联系微分销官方购买软件使用权。感谢您的关注！</td>\r\n      </tr>\r\n      <tr>\r\n        <th colspan=\"2\"><a href=\"" + Globals.GetSiteUrls().Home + "Vshop/\">返回前台</a></th>\r\n        </tr>\r\n    </table>\r\n     </div>\r\n    </div>\r\n</div>\r\n</div><div class=\"footer\">Copyright 2015 全婴汇 all Rights Reserved. 本产品资源均为 全婴汇 版权所有</div>\r\n</div>\r\n</body>\r\n</html>";
        private readonly string noticeMsg = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <Hi:HeadContainer ID=\"HeadContainer1\" runat=\"server\" />\r\n    <Hi:PageTitle ID=\"PageTitle1\" runat=\"server\" />\r\n    <link rel=\"stylesheet\" href=\"css/login.css\" type=\"text/css\" media=\"screen\" />\r\n</head>\r\n<body>\r\n<div class=\"admin\">\r\n<div id=\"\" class=\"wrap\">\r\n<div class=\"main\" style=\"position:relative\">\r\n    <div class=\"LoginBack\">\r\n     <div>\r\n     <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n      <tr>\r\n        <td class=\"td1\"><img src=\"images/comeBack.gif\" width=\"56\" height=\"49\" /></td>\r\n        <td class=\"td2\">您正在使用的微分销系统已过授权有效期，无法登录后台管理。请续费。感谢您的关注！</td>\r\n      </tr>\r\n      <tr>\r\n        <th colspan=\"2\"><a href=\"" + Globals.GetSiteUrls().Home + "Vshop/\">返回前台</a></th>\r\n        </tr>\r\n    </table>\r\n     </div>\r\n    </div>\r\n</div>\r\n</div><div class=\"footer\">Copyright 2015 全婴汇 all Rights Reserved. 本产品资源均为 全婴汇 版权所有</div>\r\n</div>\r\n</body>\r\n</html>";
		protected PageTitle PageTitle1;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.TextBox txtAdminName;
		protected System.Web.UI.WebControls.TextBox txtAdminPassWord;
		protected System.Web.UI.WebControls.TextBox txtCode;
		private string verifyCodeKey = "VerifyCode";
		private string ReferralLink
		{
			get
			{
				return this.ViewState["ReferralLink"] as string;
			}
			set
			{
				this.ViewState["ReferralLink"] = value;
			}
		}
		private void btnAdminLogin_Click(object sender, System.EventArgs e)
		{
			if (!Globals.CheckVerifyCode(this.txtCode.Text.Trim()))
			{
				this.ShowMessage("验证码不正确");
			}
			else
			{
				ManagerInfo manager = ManagerHelper.GetManager(this.txtAdminName.Text);
				if (manager == null)
				{
					this.ShowMessage("无效的用户信息");
				}
				else
				{
					if (manager.Password != HiCryptographer.Md5Encrypt(this.txtAdminPassWord.Text))
					{
						this.ShowMessage("密码不正确");
					}
					else
					{
						System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-Manager")
						{
							Value = manager.UserId.ToString(),
							Expires = System.DateTime.Now.AddDays(1.0)
						};
						System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
						this.Page.Response.Redirect("Default.aspx", true);
					}
				}
			}
		}
		private bool CheckVerifyCode(string verifyCode)
		{
			return base.Request.Cookies[this.verifyCodeKey] != null && string.Compare(HiCryptographer.Decrypt(base.Request.Cookies[this.verifyCodeKey].Value), verifyCode, true, System.Globalization.CultureInfo.InvariantCulture) == 0;
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnAdminLogin.Click += new System.EventHandler(this.btnAdminLogin_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["isCallback"]) && base.Request["isCallback"] == "true")
			{
				string verifyCode = base.Request["code"];
				string str2;
				if (!this.CheckVerifyCode(verifyCode))
				{
					str2 = "0";
				}
				else
				{
					str2 = "1";
				}
				base.Response.Clear();
				base.Response.ContentType = "application/json";
				base.Response.Write("{ ");
				base.Response.Write(string.Format("\"flag\":\"{0}\"", str2));
				base.Response.Write("}");
				base.Response.End();
			}
			if (!this.Page.IsPostBack)
			{
				Uri urlReferrer = this.Context.Request.UrlReferrer;
				if (urlReferrer != null)
				{
					this.ReferralLink = urlReferrer.ToString();
				}
				this.txtAdminName.Focus();
				PageTitle.AddSiteNameTitle("后台登录");
			}
		}
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			string str = "1";
			if (!string.IsNullOrEmpty(str))
			{
				switch (System.Convert.ToInt32(str.Replace("{\"state\":\"", "").Replace("\"}", "")))
				{
				case -1:
					writer.Write(this.noticeMsg);
					return;
				case 0:
					writer.Write(this.licenseMsg);
					return;
				}
			}
			base.Render(writer);
		}
		private void ShowMessage(string msg)
		{
			this.lblStatus.Text = msg;
			this.lblStatus.Success = false;
			this.lblStatus.Visible = true;
		}
	}
}
