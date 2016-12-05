using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class ManageBanner : AdminPage
	{
		protected Grid grdBanner;
		private void BindData()
		{
			this.grdBanner.DataSource = VShopHelper.GetAllBanners();
			this.grdBanner.DataBind();
		}
		protected void grdBanner_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
			int bannerId = (int)this.grdBanner.DataKeys[rowIndex].Value;
			int replaceBannerId = 0;
			if (e.CommandName == "Fall")
			{
				if (rowIndex < this.grdBanner.Rows.Count - 1)
				{
					replaceBannerId = (int)this.grdBanner.DataKeys[rowIndex + 1].Value;
				}
			}
			else
			{
				if (e.CommandName == "Rise" && rowIndex > 0)
				{
					replaceBannerId = (int)this.grdBanner.DataKeys[rowIndex - 1].Value;
				}
			}
			if (replaceBannerId > 0)
			{
				VShopHelper.SwapTplCfgSequence(bannerId, replaceBannerId);
				base.ReloadPage(null);
			}
			if (e.CommandName == "DeleteBanner")
			{
				if (VShopHelper.DelTplCfg(bannerId))
				{
					this.ShowMsg("删除成功！", true);
					base.ReloadPage(null);
				}
				else
				{
					this.ShowMsg("删除失败！", false);
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
	}
}
