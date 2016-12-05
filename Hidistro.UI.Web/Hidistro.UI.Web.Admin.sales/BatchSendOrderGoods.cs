using ASPNET.WebControls;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities;
using Hidistro.Entities.Orders;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Sales;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using Hishop.Weixin.Pay;
using Hishop.Weixin.Pay.Domain;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class BatchSendOrderGoods : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnBatchSendGoods;
		protected System.Web.UI.WebControls.Button btnSetShipOrderNumber;
		protected System.Web.UI.WebControls.Button btnSetShippingMode;
		protected DropdownColumn dropExpress;
		protected System.Web.UI.WebControls.DropDownList dropExpressComputerpe;
		protected DropdownColumn dropShippId;
		protected ShippingModeDropDownList dropShippingMode;
		protected Grid grdOrderGoods;
		private string strIds;
		protected System.Web.UI.WebControls.TextBox txtStartShipOrderNumber;
		private void BindData()
		{
			DropdownColumn column = (DropdownColumn)this.grdOrderGoods.Columns[4];
			column.DataSource = SalesHelper.GetShippingModes();
			DropdownColumn column2 = (DropdownColumn)this.grdOrderGoods.Columns[5];
			column2.DataSource = ExpressHelper.GetAllExpress();
			string orderIds = "'" + this.strIds.Replace(",", "','") + "'";
			this.grdOrderGoods.DataSource = OrderHelper.GetSendGoodsOrders(orderIds);
			this.grdOrderGoods.DataBind();
		}
		private void btnSendGoods_Click(object sender, System.EventArgs e)
		{
			if (this.grdOrderGoods.Rows.Count <= 0)
			{
				this.ShowMsg("没有要进行发货的订单。", false);
			}
			else
			{
				DropdownColumn column = (DropdownColumn)this.grdOrderGoods.Columns[4];
				System.Web.UI.WebControls.ListItemCollection selectedItems = column.SelectedItems;
				DropdownColumn column2 = (DropdownColumn)this.grdOrderGoods.Columns[5];
				System.Web.UI.WebControls.ListItemCollection items2 = column2.SelectedItems;
				int num = 0;
				for (int i = 0; i < selectedItems.Count; i++)
				{
					string orderId = (string)this.grdOrderGoods.DataKeys[this.grdOrderGoods.Rows[i].RowIndex].Value;
					System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)this.grdOrderGoods.Rows[i].FindControl("txtShippOrderNumber");
					System.Web.UI.WebControls.ListItem item = selectedItems[i];
					System.Web.UI.WebControls.ListItem item2 = items2[i];
					int result = 0;
					int.TryParse(item.Value, out result);
					if (!string.IsNullOrEmpty(box.Text.Trim()) && !string.IsNullOrEmpty(item.Value) && int.Parse(item.Value) > 0 && !string.IsNullOrEmpty(item2.Value))
					{
						OrderInfo orderInfo = OrderHelper.GetOrderInfo(orderId);
						if ((orderInfo.GroupBuyId <= 0 || orderInfo.GroupBuyStatus == GroupBuyStatus.Success) && ((orderInfo.OrderStatus == OrderStatus.WaitBuyerPay && orderInfo.Gateway == "hishop.plugins.payment.podrequest") || orderInfo.OrderStatus == OrderStatus.BuyerAlreadyPaid) && result > 0 && !string.IsNullOrEmpty(box.Text.Trim()) && box.Text.Trim().Length <= 20)
						{
							ShippingModeInfo shippingMode = SalesHelper.GetShippingMode(result, true);
							orderInfo.RealShippingModeId = shippingMode.ModeId;
							orderInfo.RealModeName = shippingMode.Name;
							orderInfo.ExpressCompanyAbb = item2.Value;
							orderInfo.ExpressCompanyName = item2.Text;
							orderInfo.ShipOrderNumber = box.Text;
							if (OrderHelper.SendGoods(orderInfo))
							{
								SendNoteInfo info3 = new SendNoteInfo();
								info3.NoteId = Globals.GetGenerateId() + num;
								info3.OrderId = orderId;
								info3.Operator = ManagerHelper.GetCurrentManager().UserName;
								info3.Remark = "后台" + info3.Operator + "发货成功";
								OrderHelper.SaveSendNote(info3);
								if (!string.IsNullOrEmpty(orderInfo.GatewayOrderId) && orderInfo.GatewayOrderId.Trim().Length > 0)
								{
									if (orderInfo.Gateway == "hishop.plugins.payment.ws_wappay.wswappayrequest")
									{
										PaymentModeInfo paymentMode = SalesHelper.GetPaymentMode(orderInfo.PaymentTypeId);
										if (paymentMode != null)
										{
											PaymentRequest.CreateInstance(paymentMode.Gateway, HiCryptographer.Decrypt(paymentMode.Settings), orderInfo.OrderId, orderInfo.GetTotal(), "订单发货", "订单号-" + orderInfo.OrderId, orderInfo.EmailAddress, orderInfo.OrderDate, Globals.FullPath(Globals.GetSiteUrls().Home), Globals.FullPath(Globals.GetSiteUrls().UrlData.FormatUrl("PaymentReturn_url", new object[]
											{
												paymentMode.Gateway
											})), Globals.FullPath(Globals.GetSiteUrls().UrlData.FormatUrl("PaymentNotify_url", new object[]
											{
												paymentMode.Gateway
											})), "").SendGoods(orderInfo.GatewayOrderId, orderInfo.RealModeName, orderInfo.ShipOrderNumber, "EXPRESS");
										}
									}
									if (orderInfo.Gateway == "hishop.plugins.payment.weixinrequest")
									{
										SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
										PayClient client = new PayClient(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey);
										DeliverInfo deliver = new DeliverInfo
										{
											TransId = orderInfo.GatewayOrderId,
											OutTradeNo = orderInfo.OrderId,
											OpenId = MemberHelper.GetMember(orderInfo.UserId).OpenId
										};
										client.DeliverNotify(deliver);
									}
								}
								orderInfo.OnDeliver();
								num++;
							}
						}
					}
				}
				if (num == 0)
				{
					this.ShowMsg("批量发货失败！", false);
				}
				else
				{
					if (num > 0)
					{
						this.BindData();
						this.ShowMsg(string.Format("批量发货成功！发货数量{0}个", num), true);
					}
				}
			}
		}
		private void btnSetShipOrderNumber_Click(object sender, System.EventArgs e)
		{
			string orderIds = this.GetOrderIds();
			string[] strArray = orderIds.Split(new char[]
			{
				','
			});
			long result = 0L;
			string purchaseOrderIds = "'" + orderIds.Replace(",", "','") + "'";
			if (!string.IsNullOrEmpty(this.dropExpressComputerpe.SelectedValue))
			{
				OrderHelper.SetOrderExpressComputerpe(purchaseOrderIds, this.dropExpressComputerpe.SelectedItem.Text, this.dropExpressComputerpe.SelectedValue);
			}
			if (!string.IsNullOrEmpty(this.txtStartShipOrderNumber.Text.Trim()) && long.TryParse(this.txtStartShipOrderNumber.Text.Trim(), out result))
			{
				try
				{
					OrderHelper.SetOrderShipNumber(strArray, this.txtStartShipOrderNumber.Text.Trim(), this.dropExpressComputerpe.SelectedValue);
					this.BindData();
				}
				catch (System.Exception)
				{
					this.ShowMsg("你输入的起始单号不正确", false);
				}
			}
			else
			{
				this.ShowMsg("起始发货单号不允许为空且必须为正整数", false);
			}
		}
		private void btnSetShippingMode_Click(object sender, System.EventArgs e)
		{
			string orderIds = "'" + this.strIds.Replace(",", "','") + "'";
			if (this.dropShippingMode.SelectedValue.HasValue)
			{
				OrderHelper.SetOrderShippingMode(orderIds, this.dropShippingMode.SelectedValue.Value, this.dropShippingMode.SelectedItem.Text);
			}
			this.BindData();
		}
		protected string GetOrderIds()
		{
			string str = string.Empty;
			for (int i = 0; i < this.grdOrderGoods.Rows.Count; i++)
			{
				System.Web.UI.WebControls.GridViewRow row = this.grdOrderGoods.Rows[i];
				str = str + this.grdOrderGoods.DataKeys[i].Value.ToString() + ",";
			}
			return str.TrimEnd(new char[]
			{
				','
			});
		}
		private void grdOrderGoods_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
			{
				string orderId = (string)this.grdOrderGoods.DataKeys[e.Row.RowIndex].Value;
				System.Web.UI.WebControls.DropDownList list = e.Row.FindControl("dropExpress") as System.Web.UI.WebControls.DropDownList;
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(orderId);
				if (orderInfo != null && orderInfo.OrderStatus == OrderStatus.BuyerAlreadyPaid)
				{
					ExpressCompanyInfo info2 = ExpressHelper.FindNode(orderInfo.ExpressCompanyName);
					if (info2 != null)
					{
						list.SelectedValue = info2.Kuaidi100Code;
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strIds = base.Request.QueryString["OrderIds"];
			this.btnSetShippingMode.Click += new System.EventHandler(this.btnSetShippingMode_Click);
			this.btnSetShipOrderNumber.Click += new System.EventHandler(this.btnSetShipOrderNumber_Click);
			this.grdOrderGoods.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdOrderGoods_RowDataBound);
			this.btnBatchSendGoods.Click += new System.EventHandler(this.btnSendGoods_Click);
			if (!this.Page.IsPostBack)
			{
				this.dropShippingMode.DataBind();
				this.dropExpressComputerpe.DataSource = ExpressHelper.GetAllExpress();
				this.dropExpressComputerpe.DataTextField = "name";
				this.dropExpressComputerpe.DataValueField = "Kuaidi100Code";
				this.dropExpressComputerpe.DataBind();
				this.dropExpressComputerpe.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
				this.BindData();
			}
		}
	}
}
