using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
    public class AddNavigate : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnAddBanner;
        protected System.Web.UI.WebControls.DropDownList ddlSubType;
        protected System.Web.UI.WebControls.DropDownList ddlThridType;
        protected System.Web.UI.WebControls.DropDownList ddlType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden fmSrc;
        protected System.Web.UI.WebControls.Repeater FontIcon;
        protected System.Web.UI.HtmlControls.HtmlGenericControl liParent;
        protected System.Web.UI.HtmlControls.HtmlImage littlepic;
        protected System.Web.UI.HtmlControls.HtmlInputHidden locationUrl;
        protected System.Web.UI.HtmlControls.HtmlGenericControl navigateDesc;
        protected System.Web.UI.WebControls.TextBox Tburl;
        protected string tplpath = Globals.GetVshopSkinPath(null) + "/images/deskicon/";
        protected System.Web.UI.WebControls.TextBox txtNavigateDesc;
        protected void BindIcons()
        {
            string str2;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(base.Server.MapPath("/Utility/icomoon") + "/icomoon.font"))
            {
                str2 = reader.ReadToEnd();
            }
            this.FontIcon.DataSource = str2.Split(new char[]
			{
				','
			});
            this.FontIcon.DataBind();
        }
        protected void btnAddBanner_Click(object sender, System.EventArgs e)
        {
            TplCfgInfo info = new NavigateInfo
            {
                IsDisable = false,
                ImageUrl = this.fmSrc.Value,
                ShortDesc = this.txtNavigateDesc.Text,
                LocationType = (LocationType)System.Enum.Parse(typeof(LocationType), this.ddlType.SelectedValue),
                Url = this.locationUrl.Value
            };
            if (string.IsNullOrWhiteSpace(this.locationUrl.Value))
            {
                this.ShowMsg("跳转页不能为空！", false);
            }
            else
            {
                if (VShopHelper.SaveTplCfg(info))
                {
                    this.CloseWindow();
                }
                else
                {
                    this.ShowMsg("添加错误！", false);
                }
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ddlType.BindEnum<LocationType>("VipCard");
                this.BindIcons();
            }
        }
        private void Reset()
        {
            this.txtNavigateDesc.Text = string.Empty;
            this.Tburl.Text = string.Empty;
            this.ddlType.SelectedValue = LocationType.Link.ToString();
        }
    }
}
