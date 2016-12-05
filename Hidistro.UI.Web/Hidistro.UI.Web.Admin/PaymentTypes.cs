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
	[PrivilegeCheck(Privilege.PaymentModes)]
	public class PaymentTypes : AdminPage
	{
		protected System.Web.UI.WebControls.GridView grdPaymentMode;
		protected Pager pager1;
		private void BindData()
		{
			this.grdPaymentMode.DataSource = SalesHelper.GetPaymentModes();
			this.grdPaymentMode.DataBind();
		}
		private void grdPaymentMode_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			if (e.CommandName != "Sort")
			{
				int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
				int modeId = (int)this.grdPaymentMode.DataKeys[rowIndex].Value;
				int displaySequence = System.Convert.ToInt32((this.grdPaymentMode.Rows[rowIndex].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
				int replaceModeId = 0;
				int replaceDisplaySequence = 0;
				if (e.CommandName == "Fall")
				{
					if (rowIndex < this.grdPaymentMode.Rows.Count - 1)
					{
						replaceModeId = (int)this.grdPaymentMode.DataKeys[rowIndex + 1].Value;
						replaceDisplaySequence = System.Convert.ToInt32((this.grdPaymentMode.Rows[rowIndex + 1].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
					}
				}
				else
				{
					if (e.CommandName == "Rise" && rowIndex > 0)
					{
						replaceModeId = (int)this.grdPaymentMode.DataKeys[rowIndex - 1].Value;
						replaceDisplaySequence = System.Convert.ToInt32((this.grdPaymentMode.Rows[rowIndex - 1].FindControl("lblDisplaySequence") as System.Web.UI.WebControls.Literal).Text);
					}
				}
				if (replaceModeId > 0 && replaceDisplaySequence > 0)
				{
					SalesHelper.SwapPaymentModeSequence(modeId, replaceModeId, displaySequence, replaceDisplaySequence);
					this.BindData();
				}
			}
		}
		private void grdPaymentMode_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			if (SalesHelper.DeletePaymentMode((int)this.grdPaymentMode.DataKeys[e.RowIndex].Value))
			{
				this.BindData();
				this.ShowMsg("成功删除了一个支付方式", true);
			}
			else
			{
				this.ShowMsg("未知错误", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.grdPaymentMode.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdPaymentMode_RowDeleting);
			this.grdPaymentMode.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdPaymentMode_RowCommand);
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
	}
}
