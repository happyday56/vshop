using Hidistro.ControlPanel.Sales;
using Hidistro.Entities.Orders;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class ShippAddress : AdminPage
	{
		private string action = "";
		protected System.Web.UI.WebControls.Button btnMondifyAddress;
		protected RegionSelector dropRegions;
		private string orderId;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.TextBox txtShipTo;
		protected System.Web.UI.WebControls.TextBox txtTelPhone;
		protected System.Web.UI.WebControls.TextBox txtZipcode;
		private void BindUpdateSippingAddress(OrderInfo order)
		{
			this.txtShipTo.Text = order.ShipTo;
			this.dropRegions.SetSelectedRegionId(new int?(order.RegionId));
			this.txtAddress.Text = order.Address;
			this.txtZipcode.Text = order.ZipCode;
			this.txtTelPhone.Text = order.TelPhone;
			this.txtCellPhone.Text = order.CellPhone;
		}
		private void btnMondifyAddress_Click(object sender, System.EventArgs e)
		{
			OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.orderId);
			orderInfo.ShipTo = this.txtShipTo.Text.Trim();
			orderInfo.RegionId = this.dropRegions.GetSelectedRegionId().Value;
			orderInfo.Address = this.txtAddress.Text.Trim();
			orderInfo.TelPhone = this.txtTelPhone.Text.Trim();
			orderInfo.CellPhone = this.txtCellPhone.Text.Trim();
			orderInfo.ZipCode = this.txtZipcode.Text.Trim();
			orderInfo.ShippingRegion = this.dropRegions.SelectedRegions;
			if (string.IsNullOrEmpty(this.txtTelPhone.Text.Trim()) && string.IsNullOrEmpty(this.txtCellPhone.Text.Trim()))
			{
				this.ShowMsg("电话号码和手机号码必填其一", false);
			}
			else
			{
				if (this.action == "update")
				{
					orderInfo.OrderId = this.orderId;
					if (OrderHelper.MondifyAddress(orderInfo))
					{
						OrderHelper.GetOrderInfo(this.orderId);
						this.ShowMsg("修改成功", true);
					}
					else
					{
						this.ShowMsg("修改失败", false);
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Page.Request.QueryString["action"]))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.action = this.Page.Request.QueryString["action"];
				if (this.action == "update")
				{
					if (string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
					{
						base.GotoResourceNotFound();
						return;
					}
					this.orderId = this.Page.Request.QueryString["OrderId"];
					if (!base.IsPostBack)
					{
						OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.orderId);
						this.BindUpdateSippingAddress(orderInfo);
					}
				}
				this.btnMondifyAddress.Click += new System.EventHandler(this.btnMondifyAddress_Click);
			}
		}
	}
}
