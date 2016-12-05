using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Orders;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Orders)]
	public class OrderDetails : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnCloseOrder;
		protected System.Web.UI.WebControls.Button btnMondifyPay;
		protected System.Web.UI.WebControls.Button btnMondifyShip;
		protected System.Web.UI.WebControls.Button btnRemark;
		protected Order_ChargesList chargesList;
		protected CloseTranReasonDropDownList ddlCloseReason;
		protected PaymentDropDownList ddlpayment;
		protected ShippingModeDropDownList ddlshippingMode;
		protected System.Web.UI.WebControls.HyperLink hlkOrderGifts;
		protected Order_ItemsList itemsList;
		protected System.Web.UI.WebControls.Label lbCloseReason;
		protected FormatedTimeLabel lblorderDateForRemark;
		protected OrderStatusLabel lblOrderStatus;
		protected FormatedMoneyLabel lblorderTotalForRemark;
		protected System.Web.UI.WebControls.Label lbReason;
		protected System.Web.UI.HtmlControls.HtmlAnchor lbtnClocsOrder;
		protected System.Web.UI.WebControls.Literal litFinishTime;
		protected System.Web.UI.WebControls.Literal litOrderId;
		protected System.Web.UI.WebControls.Literal litPayTime;
		protected System.Web.UI.WebControls.Literal litRealName;
		protected System.Web.UI.WebControls.Literal litSendGoodTime;
		protected System.Web.UI.WebControls.Literal litUserEmail;
		protected System.Web.UI.WebControls.Literal litUserName;
		protected System.Web.UI.WebControls.Literal litUserTel;
		protected System.Web.UI.WebControls.HyperLink lkbtnEditPrice;
		protected System.Web.UI.WebControls.HyperLink lkbtnSendGoods;
		private OrderInfo order;
		private string orderId;
		protected OrderRemarkImageRadioButtonList orderRemarkImageForRemark;
		protected Order_ShippingAddress shippingAddress;
		protected System.Web.UI.WebControls.Literal spanOrderId;
		protected System.Web.UI.WebControls.TextBox txtRemark;
		private void BindRemark(OrderInfo order)
		{
			this.spanOrderId.Text = order.OrderId;
			this.lblorderDateForRemark.Time = order.OrderDate;
			this.lblorderTotalForRemark.Money = order.GetTotal();
			this.txtRemark.Text = Globals.HtmlDecode(order.ManagerRemark);
			this.orderRemarkImageForRemark.SelectedValue = order.ManagerMark;
		}
		private void btnCloseOrder_Click(object sender, System.EventArgs e)
		{
			this.order.CloseReason = this.ddlCloseReason.SelectedValue;
			if (OrderHelper.CloseTransaction(this.order))
			{
				this.order.OnClosed();
				this.ShowMsg("关闭订单成功", true);
			}
			else
			{
				this.ShowMsg("关闭订单失败", false);
			}
		}
		private void btnMondifyPay_Click(object sender, System.EventArgs e)
		{
			this.order = OrderHelper.GetOrderInfo(this.orderId);
			if (this.ddlpayment.SelectedValue.HasValue && this.ddlpayment.SelectedValue == -1)
			{
				this.order.PaymentTypeId = 0;
				this.order.PaymentType = "货到付款";
				this.order.Gateway = "hishop.plugins.payment.podrequest";
			}
			else
			{
				if (this.ddlpayment.SelectedValue.HasValue && this.ddlpayment.SelectedValue == 99)
				{
					this.order.PaymentTypeId = 99;
					this.order.PaymentType = "线下付款";
					this.order.Gateway = "hishop.plugins.payment.offlinerequest";
				}
				else
				{
					if (this.ddlpayment.SelectedValue.HasValue && this.ddlpayment.SelectedValue == 88)
					{
						this.order.PaymentTypeId = 88;
						this.order.PaymentType = "微信支付";
						this.order.Gateway = "hishop.plugins.payment.weixinrequest";
					}
					else
					{
						PaymentModeInfo paymentMode = SalesHelper.GetPaymentMode(this.ddlpayment.SelectedValue.Value);
						this.order.PaymentTypeId = paymentMode.ModeId;
						this.order.PaymentType = paymentMode.Name;
						this.order.Gateway = paymentMode.Gateway;
					}
				}
			}
			if (OrderHelper.UpdateOrderPaymentType(this.order))
			{
				this.chargesList.LoadControls();
				this.ShowMsg("修改支付方式成功", true);
			}
			else
			{
				this.ShowMsg("修改支付方式失败", false);
			}
		}
		private void btnMondifyShip_Click(object sender, System.EventArgs e)
		{
			this.order = OrderHelper.GetOrderInfo(this.orderId);
			ShippingModeInfo shippingMode = SalesHelper.GetShippingMode(this.ddlshippingMode.SelectedValue.Value, false);
			this.order.ShippingModeId = shippingMode.ModeId;
			this.order.ModeName = shippingMode.Name;
			if (OrderHelper.UpdateOrderShippingMode(this.order))
			{
				this.chargesList.LoadControls();
				this.shippingAddress.LoadControl();
				this.ShowMsg("修改配送方式成功", true);
			}
			else
			{
				this.ShowMsg("修改配送方式失败", false);
			}
		}
		private void btnRemark_Click(object sender, System.EventArgs e)
		{
			if (this.txtRemark.Text.Length > 300)
			{
				this.ShowMsg("备忘录长度限制在300个字符以内", false);
			}
			else
			{
				Regex regex = new Regex("^(?!_)(?!.*?_$)(?!-)(?!.*?-$)[a-zA-Z0-9_一-龥-]+$");
				if (!regex.IsMatch(this.txtRemark.Text))
				{
					this.ShowMsg("备忘录只能输入汉字,数字,英文,下划线,减号,不能以下划线、减号开头或结尾", false);
				}
				else
				{
					this.order.OrderId = this.spanOrderId.Text;
					if (this.orderRemarkImageForRemark.SelectedItem != null)
					{
						this.order.ManagerMark = this.orderRemarkImageForRemark.SelectedValue;
					}
					this.order.ManagerRemark = Globals.HtmlEncode(this.txtRemark.Text);
					if (OrderHelper.SaveRemark(this.order))
					{
						this.BindRemark(this.order);
						this.ShowMsg("保存备忘录成功", true);
					}
					else
					{
						this.ShowMsg("保存失败", false);
					}
				}
			}
		}
		private void LoadUserControl(OrderInfo order)
		{
			this.itemsList.Order = order;
			this.chargesList.Order = order;
			this.shippingAddress.Order = order;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.orderId = this.Page.Request.QueryString["OrderId"];
				this.btnMondifyPay.Click += new System.EventHandler(this.btnMondifyPay_Click);
				this.btnMondifyShip.Click += new System.EventHandler(this.btnMondifyShip_Click);
				this.btnCloseOrder.Click += new System.EventHandler(this.btnCloseOrder_Click);
				this.btnRemark.Click += new System.EventHandler(this.btnRemark_Click);
				this.order = OrderHelper.GetOrderInfo(this.orderId);
				this.LoadUserControl(this.order);
				if (!this.Page.IsPostBack)
				{
					this.lblOrderStatus.OrderStatusCode = this.order.OrderStatus;
					this.litOrderId.Text = this.order.OrderId;
					this.litUserName.Text = this.order.Username;
					this.litRealName.Text = this.order.RealName;
					this.litUserTel.Text = this.order.TelPhone;
					this.litUserEmail.Text = this.order.EmailAddress;
					if ((int)this.lblOrderStatus.OrderStatusCode != 4)
					{
						this.lbCloseReason.Visible = false;
					}
					else
					{
						this.lbReason.Text = this.order.CloseReason;
					}
					if (this.order.OrderStatus != OrderStatus.WaitBuyerPay && this.order.OrderStatus != OrderStatus.Closed && this.order.PayDate.HasValue)
					{
						this.litPayTime.Text = "付款时间：" + this.order.PayDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
					}
                    if (this.order.OrderStatus == OrderStatus.SellerAlreadySent || this.order.OrderStatus == OrderStatus.Finished || (this.order.OrderStatus == OrderStatus.Returned || this.order.OrderStatus == OrderStatus.ApplyForReturns) || this.order.OrderStatus == OrderStatus.ApplyForReplacement)
                    {
                        if (this.order.ShippingDate == null)
                        {
                            this.litSendGoodTime.Text = "发货时间：";
                        }
                        else
                        {
                            this.litSendGoodTime.Text = "发货时间：" + this.order.ShippingDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
					if (this.order.OrderStatus == OrderStatus.Finished)
					{
						this.litFinishTime.Text = "完成时间：" + this.order.FinishDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
					}
					if (this.order.OrderStatus == OrderStatus.BuyerAlreadyPaid || (this.order.OrderStatus == OrderStatus.WaitBuyerPay && this.order.Gateway == "hishop.plugins.payment.podrequest"))
					{
						this.lkbtnSendGoods.Visible = true;
					}
					else
					{
						this.lkbtnSendGoods.Visible = false;
					}
					if (this.order.OrderStatus == OrderStatus.WaitBuyerPay)
					{
						this.lbtnClocsOrder.Visible = true;
						this.lkbtnEditPrice.Visible = true;
					}
					else
					{
						this.lbtnClocsOrder.Visible = false;
						this.lkbtnEditPrice.Visible = false;
					}
					this.lkbtnEditPrice.NavigateUrl = string.Concat(new string[]
					{
						"javascript:DialogFrame('",
						Globals.ApplicationPath,
						"/Admin/sales/EditOrder.aspx?OrderId=",
						this.orderId,
						"','修改订单价格',null,null)"
					});
					this.BindRemark(this.order);
					this.ddlshippingMode.DataBind();
					this.ddlshippingMode.SelectedValue = new int?(this.order.ShippingModeId);
					this.ddlpayment.DataBind();
					this.ddlpayment.SelectedValue = new int?(this.order.PaymentTypeId);
				}
			}
		}
	}
}
