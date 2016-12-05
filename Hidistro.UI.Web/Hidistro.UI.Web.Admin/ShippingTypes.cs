using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ShippingModes)]
	public class ShippingTypes : AdminPage
	{
		protected Grid grdShippingModes;
		protected Pager pager;
		public void BindData()
		{
			this.grdShippingModes.DataSource = SalesHelper.GetShippingModes();
			this.grdShippingModes.DataBind();
		}
		private void grdShippingModes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			if (e.CommandName != "Sort")
			{
				int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
				int modeId = (int)this.grdShippingModes.DataKeys[rowIndex].Value;
				int displaySequence = int.Parse((this.grdShippingModes.Rows[rowIndex].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
				int replaceModeId = 0;
				int replaceDisplaySequence = 0;
				string commandName = e.CommandName;
				if (commandName != null)
				{
					if (!(commandName == "Fall"))
					{
						if (commandName == "Rise" && rowIndex > 0)
						{
							replaceModeId = (int)this.grdShippingModes.DataKeys[rowIndex - 1].Value;
							replaceDisplaySequence = int.Parse((this.grdShippingModes.Rows[rowIndex - 1].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
						}
					}
					else
					{
						if (rowIndex < this.grdShippingModes.Rows.Count - 1)
						{
							replaceModeId = (int)this.grdShippingModes.DataKeys[rowIndex + 1].Value;
							replaceDisplaySequence = int.Parse((this.grdShippingModes.Rows[rowIndex + 1].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
						}
					}
				}
				if (replaceModeId > 0 && replaceDisplaySequence > 0)
				{
					SalesHelper.SwapShippingModeSequence(modeId, replaceModeId, displaySequence, replaceDisplaySequence);
					this.BindData();
				}
			}
		}
		private void grdShippingModes_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			if (SalesHelper.DeleteShippingMode((int)this.grdShippingModes.DataKeys[e.RowIndex].Value))
			{
				this.BindData();
				this.ShowMsg("删除成功", true);
			}
			else
			{
				this.ShowMsg("删除失败", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.grdShippingModes.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdShippingModes_RowCommand);
			this.grdShippingModes.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdShippingModes_RowDeleting);
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
	}
}
