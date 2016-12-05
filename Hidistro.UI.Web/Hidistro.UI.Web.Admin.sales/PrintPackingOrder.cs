using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Entities.Orders;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class PrintPackingOrder : AdminPage
	{
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		protected Grid grdOrderItems;
		protected HeadContainer HeadContainer1;
		protected System.Web.UI.WebControls.Literal litAddress;
		protected System.Web.UI.WebControls.Literal litCellPhone;
		protected System.Web.UI.WebControls.Literal litOrderDate;
		protected System.Web.UI.WebControls.Literal litOrderId;
		protected System.Web.UI.WebControls.Literal litOrderStatus;
		protected System.Web.UI.WebControls.Literal litPayType;
		protected System.Web.UI.WebControls.Literal litRemark;
		protected System.Web.UI.WebControls.Literal litShipperMode;
		protected System.Web.UI.WebControls.Literal litShippNo;
		protected System.Web.UI.WebControls.Literal litSkipTo;
		protected System.Web.UI.WebControls.Literal litTelPhone;
		protected System.Web.UI.WebControls.Literal litZipCode;
		private string orderId = string.Empty;
		protected PageTitle PageTitle1;
		protected Script Script2;
		private void BindOrderInfo(OrderInfo order)
		{
			this.litAddress.Text = order.ShippingRegion + order.Address;
			this.litCellPhone.Text = order.CellPhone;
			this.litTelPhone.Text = order.TelPhone;
			this.litZipCode.Text = order.ZipCode;
			this.litOrderId.Text = order.OrderId;
			this.litOrderDate.Text = order.OrderDate.ToString();
			this.litPayType.Text = order.PaymentType;
			this.litRemark.Text = order.Remark;
			this.litShipperMode.Text = order.RealModeName;
			this.litShippNo.Text = order.ShipOrderNumber;
			this.litSkipTo.Text = order.ShipTo;
			switch (order.OrderStatus)
			{
			case OrderStatus.WaitBuyerPay:
				this.litOrderStatus.Text = "等待付款";
				break;
			case OrderStatus.BuyerAlreadyPaid:
				this.litOrderStatus.Text = "已付款等待发货";
				break;
			case OrderStatus.SellerAlreadySent:
				this.litOrderStatus.Text = "已发货";
				break;
			case OrderStatus.Closed:
				this.litOrderStatus.Text = "已关闭";
				break;
			case OrderStatus.Finished:
				this.litOrderStatus.Text = "已完成";
				break;
			}
		}
		private void BindOrderItems(OrderInfo order)
		{
			this.grdOrderItems.DataSource = order.LineItems.Values;
			this.grdOrderItems.DataBind();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.orderId = this.Page.Request.Params["OrderId"];
			if (!this.Page.IsPostBack && !string.IsNullOrEmpty(this.orderId))
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.orderId);
				this.BindOrderInfo(orderInfo);
				this.BindOrderItems(orderInfo);
			}
		}
	}
}
