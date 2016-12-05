using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Hidistro.UI.Common.Controls;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VUserLogin)]
    public class VUserLogin : VWeiXinOAuthTemplatedWebControl//VshopTemplatedWebControl
    {
        private HtmlInputHidden litproductid;
        private HtmlInputHidden lituserstatus;

        protected override void AttachChildControls()
        {
            this.lituserstatus = (HtmlInputHidden)this.FindControl("lituserstatus");
            this.litproductid = (HtmlInputHidden)this.FindControl("litproductid");
            int result = -1;
            int num2 = 0;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["userstatus"]))
            {
                if (!int.TryParse(this.Page.Request.QueryString["userstatus"].ToString(), out result))
                {
                    this.Page.Response.Redirect("/Vshop/Default.aspx");
                }
                else if (this.Page.Request.QueryString["userstatus"].ToString() == "0")
                {
                    this.lituserstatus.Value = this.Page.Request.QueryString["userstatus"].ToString();
                    if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productid"]))
                    {
                        if (!int.TryParse(this.Page.Request.QueryString["productid"].ToString(), out num2))
                        {
                            this.Page.Response.Redirect("/Vshop/Default.aspx");
                        }
                        else
                        {
                            this.litproductid.Value = this.Page.Request.QueryString["productid"].ToString();
                        }
                    }
                }
            }
            PageTitle.AddSiteNameTitle("用户登录");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VUserLogin.html";
            }
            base.OnInit(e);
        }
    }
}

