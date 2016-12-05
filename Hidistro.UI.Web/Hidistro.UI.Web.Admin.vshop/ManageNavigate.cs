using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class ManageNavigate : AdminPage
	{
		protected Grid grdNavigate;
		private void BindData()
		{
			this.grdNavigate.DataSource = VShopHelper.GetAllNavigate();
			this.grdNavigate.DataBind();
		}
		protected string GetImageUrl(string url)
		{
			string str = url;
			string result;
			if (string.IsNullOrWhiteSpace(str))
			{
				result = "/utility/pics/none.gif";
			}
			else
			{
				if (!url.ToLower().Contains("storage/master/navigate") && !url.ToLower().Contains("templates"))
				{
					str = Globals.GetVshopSkinPath(null) + "/images/deskicon/" + url;
				}
				result = str;
			}
			return result;
		}
		protected void grdNavigate_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
			int bannerId = (int)this.grdNavigate.DataKeys[rowIndex].Value;
			int replaceBannerId = 0;
			if (e.CommandName == "Fall")
			{
				if (rowIndex < this.grdNavigate.Rows.Count - 1)
				{
					replaceBannerId = (int)this.grdNavigate.DataKeys[rowIndex + 1].Value;
				}
			}
			else
			{
				if (e.CommandName == "Rise" && rowIndex > 0)
				{
					replaceBannerId = (int)this.grdNavigate.DataKeys[rowIndex - 1].Value;
				}
			}
			if (replaceBannerId > 0)
			{
				VShopHelper.SwapTplCfgSequence(bannerId, replaceBannerId);
				base.ReloadPage(null);
			}
			if (e.CommandName == "Delete")
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
