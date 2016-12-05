using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Entities.Orders;
using Hidistro.SaleSystem.Vshop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class Order_ShippingAddress : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Button btnupdatepost;
		protected string edit = "";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdtagId;
		protected System.Web.UI.WebControls.Literal lblShipAddress;
		protected System.Web.UI.WebControls.Literal litCompanyName;
		protected System.Web.UI.WebControls.Literal litModeName;
		protected System.Web.UI.WebControls.Label litRemark;
		protected System.Web.UI.WebControls.Literal litShipToDate;
		protected System.Web.UI.WebControls.Label lkBtnEditShippingAddress;
		private OrderInfo order;
		protected System.Web.UI.HtmlControls.HtmlInputHidden OrderId;
		protected System.Web.UI.WebControls.Panel plExpress;
		protected System.Web.UI.HtmlControls.HtmlAnchor power;
		protected System.Web.UI.HtmlControls.HtmlTableRow tr_company;
		protected System.Web.UI.WebControls.TextBox txtpost;
        protected Literal litExpressData;

		public OrderInfo Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}
		private void btnupdatepost_Click(object sender, System.EventArgs e)
		{
			this.order.ShipOrderNumber = this.txtpost.Text;
			OrderHelper.SetOrderShipNumber(this.order.OrderId, this.order.ShipOrderNumber);
			this.ShowMsg("修改发货单号成功", true);
			this.LoadControl();
		}
		public void LoadControl()
		{
			string str = string.Empty;
			if (!string.IsNullOrEmpty(this.order.ShippingRegion))
			{
				str = this.order.ShippingRegion.Replace('，', ' ');
			}
			if (!string.IsNullOrEmpty(this.order.Address))
			{
				str += this.order.Address;
			}
			if (!string.IsNullOrEmpty(this.order.ShipTo))
			{
				str = str + "   " + this.order.ShipTo;
			}
			if (!string.IsNullOrEmpty(this.order.ZipCode))
			{
				str = str + "   " + this.order.ZipCode;
			}
			if (!string.IsNullOrEmpty(this.order.TelPhone))
			{
				str = str + "   " + this.order.TelPhone;
			}
			if (!string.IsNullOrEmpty(this.order.CellPhone))
			{
				str = str + "   " + this.order.CellPhone;
			}
			this.lblShipAddress.Text = str;
			if (this.order.OrderStatus == OrderStatus.WaitBuyerPay || this.order.OrderStatus == OrderStatus.BuyerAlreadyPaid)
			{
				this.lkBtnEditShippingAddress.Visible = true;
			}
			else
			{
				this.lkBtnEditShippingAddress.Visible = false;
			}
			this.litShipToDate.Text = this.order.ShipToDate;
			if (this.order.OrderStatus == OrderStatus.Finished || this.order.OrderStatus == OrderStatus.SellerAlreadySent)
			{
				this.litModeName.Text = this.order.RealModeName + " 发货单号：" + this.order.ShipOrderNumber;
				this.txtpost.Text = this.order.ShipOrderNumber;
				this.OrderId.Value = this.order.OrderId;
			}
			else
			{
				this.litModeName.Text = this.order.ModeName;
			}
			if (!string.IsNullOrEmpty(this.order.ExpressCompanyName))
			{
				this.litCompanyName.Text = this.order.ExpressCompanyName;
				this.tr_company.Visible = true;
			}
			this.litRemark.Text = this.order.Remark;

            // 处理物流信息
            StringBuilder expressHref = new StringBuilder();
            string currExpress = "";

            if (!string.IsNullOrEmpty(order.ShipOrderNumber))
            {
                currExpress = Express.GetExpressData(order.ExpressCompanyAbb, order.ShipOrderNumber, 0, false);

                if (!string.IsNullOrEmpty(currExpress))
                {
                    expressHref.AppendLine("<a href=\"javascript:DialogFrame('" + currExpress + "','查看快递单号',600,500);\">" + this.order.ExpressCompanyName + ":" + this.order.ShipOrderNumber + "</a>&nbsp;&nbsp;");
                }
            }            

            IList<OrderSubLogisticInfo> subOrderLogisticList = ShoppingProcessor.GetSubOrderLogisticByOrderId(this.order.OrderId, false);
            if (null != subOrderLogisticList && subOrderLogisticList.Count > 0)
            {
                foreach (OrderSubLogisticInfo osli in subOrderLogisticList)
                {
                    if (order.ExpressCompanyAbb.EqualIgnoreCase(osli.SubExpressCompanyAbb) && order.ShipOrderNumber.EqualIgnoreCase(osli.SubShipOrderNumber))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(osli.SubShipOrderNumber))
                    {
                        currExpress = Express.GetExpressData(osli.SubExpressCompanyAbb, osli.SubShipOrderNumber, 0, false);
                        if (!string.IsNullOrEmpty(currExpress))
                        {
                            expressHref.AppendLine("<a href=\"javascript:DialogFrame('" + currExpress + "','查看快递单号',600,500);\">" + osli.SubExpressCompanyName + ":" + osli.SubShipOrderNumber + "</a>&nbsp;&nbsp;");
                        }
                    }                    
                }
            }

            this.litExpressData.Text = expressHref.ToString(); 
		}
		protected override void OnLoad(System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.LoadControl();
			}
			if (this.order.OrderStatus == OrderStatus.SellerAlreadySent)
			{
				this.edit = "&nbsp;<input type=\"button\" class=\"submit_btnbianji\" id=\"btnedit\" value=\"修改发货单号\" onclick=\"ShowPurchaseOrder();\"/>";
			}
            //if ((this.order.OrderStatus == OrderStatus.SellerAlreadySent || this.order.OrderStatus == OrderStatus.Finished) && !string.IsNullOrEmpty(this.order.ExpressCompanyAbb))
            //{
            //    if (this.plExpress != null)
            //    {
            //        this.plExpress.Visible = true;
            //    }
            //    if (Express.GetExpressType() == "kuaidi100" && this.power != null)
            //    {
            //        this.power.Visible = true;
            //    }
            //}
            this.plExpress.Visible = true;
			this.btnupdatepost.Click += new System.EventHandler(this.btnupdatepost_Click);
		}
		protected virtual void ShowMsg(string msg, bool success)
		{
			string str = string.Format("ShowMsg(\"{0}\", {1})", msg, success ? "true" : "false");
			if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ServerMessageScript"))
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ServerMessageScript", "<script language='JavaScript' defer='defer'>setTimeout(function(){" + str + "},300);</script>");
			}
		}
	}
}
