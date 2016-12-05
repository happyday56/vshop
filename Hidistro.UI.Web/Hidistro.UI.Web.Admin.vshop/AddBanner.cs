using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
    public class AddBanner : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnAddBanner;
        protected System.Web.UI.WebControls.DropDownList ddlSubType;
        protected System.Web.UI.WebControls.DropDownList ddlThridType;
        protected System.Web.UI.WebControls.DropDownList ddlType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden fmSrc;
        protected System.Web.UI.HtmlControls.HtmlGenericControl liParent;
        protected System.Web.UI.HtmlControls.HtmlInputHidden locationUrl;
        protected System.Web.UI.HtmlControls.HtmlGenericControl navigateDesc;
        protected System.Web.UI.WebControls.TextBox Tburl;
        protected System.Web.UI.WebControls.TextBox txtBannerDesc;
        protected void btnAddBanner_Click(object sender, System.EventArgs e)
        {
            TplCfgInfo info = new BannerInfo
            {
                IsDisable = false,
                ImageUrl = this.fmSrc.Value,
                ShortDesc = this.txtBannerDesc.Text,
                LocationType = (LocationType)System.Enum.Parse(typeof(LocationType), this.ddlType.SelectedValue),
                Url = this.locationUrl.Value
            };
            if (VShopHelper.SaveTplCfg(info))
            {
                this.CloseWindow();
            }
            else
            {
                this.ShowMsg("添加错误！", false);
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ddlType.BindEnum<LocationType>("VipCard");
            }
        }
        private void Reset()
        {
            this.txtBannerDesc.Text = string.Empty;
            this.Tburl.Text = string.Empty;
            this.ddlType.SelectedValue = LocationType.Link.ToString();
        }
    }
}
