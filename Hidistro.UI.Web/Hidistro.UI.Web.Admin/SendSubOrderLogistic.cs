using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.Messages;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using Hishop.Weixin.Pay;
using Hishop.Weixin.Pay.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.OrderSendGoods)]
    public class SendSubOrderLogistic : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnSendGoods;
        protected ExpressRadioButtonList expressRadioButtonList;
        //protected Order_ItemsList itemsList;
        protected System.Web.UI.WebControls.Label lblOrderId;
        protected FormatedTimeLabel lblOrderTime;
        //protected System.Web.UI.WebControls.Literal litReceivingInfo;
        //protected System.Web.UI.WebControls.Label litRemark;
        //protected System.Web.UI.WebControls.Literal litShippingModeName;
        //protected System.Web.UI.WebControls.Label litShipToDate;
        private string orderId;
        protected ShippingModeRadioButtonList radioShippingMode;
        protected System.Web.UI.WebControls.TextBox txtShipOrderNumber;
        protected System.Web.UI.HtmlControls.HtmlGenericControl txtShipOrderNumberTip;
        protected SubOrderDropDownList ddlSubOrder;

        private void BindSubOrder(string orderId)
        {
            DataTable dt = ShoppingProcessor.GetSubOrderCVTDTByOrderId(orderId, false);
            if (null != dt && dt.Rows.Count > 0)
            {
                this.ddlSubOrder.SubOrderList = dt;

                this.ddlSubOrder.DataBind();
            }            
        }

        private void BindExpressCompany(int modeId)
        {
            this.expressRadioButtonList.ExpressCompanies = SalesHelper.GetExpressCompanysByMode(modeId);
            this.expressRadioButtonList.DataBind();
        }
        private void BindOrderItems(OrderInfo order)
        {
            this.lblOrderId.Text = order.OrderId;
            this.lblOrderTime.Time = order.OrderDate;
            //this.itemsList.Order = order;
        }


        private void btnSendGoods_Click(object sender, System.EventArgs e)
        {
            string subOrderId = "";
            string subExpressCompanyAbb = "";
            string subExpressCompanyName = "";
            string subShipOrderNumber = "";

            OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.orderId);
            if (orderInfo != null)
            {
                ManagerInfo currentManager = ManagerHelper.GetCurrentManager();
                if (currentManager != null)
                {
                    if (string.IsNullOrEmpty(this.ddlSubOrder.SelectedValue))
                    {
                        this.ShowMsg("请选择要发货对应的品牌名称!", false);
                    }
                    else
                    {
                        if (!this.radioShippingMode.SelectedValue.HasValue)
                        {
                            this.ShowMsg("请选择配送方式", false);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(this.txtShipOrderNumber.Text.Trim()) || this.txtShipOrderNumber.Text.Trim().Length > 20)
                            {
                                this.ShowMsg("运单号码不能为空，在1至20个字符之间", false);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.expressRadioButtonList.SelectedValue))
                                {
                                    this.ShowMsg("请选择物流公司", false);
                                }
                                else
                                {
                                    ShippingModeInfo shippingMode = SalesHelper.GetShippingMode(this.radioShippingMode.SelectedValue.Value, true);
                                    orderInfo.RealShippingModeId = this.radioShippingMode.SelectedValue.Value;
                                    orderInfo.RealModeName = shippingMode.Name;
                                    ExpressCompanyInfo info4 = ExpressHelper.FindNode(this.expressRadioButtonList.SelectedValue);
                                    if (info4 != null)
                                    {
                                        subExpressCompanyAbb = info4.Kuaidi100Code;
                                        subExpressCompanyName = info4.Name;
                                    }
                                    subShipOrderNumber = this.txtShipOrderNumber.Text;

                                    if (ShoppingProcessor.UpdateSubOrderExpressNo(orderInfo.OrderId, this.ddlSubOrder.SelectedValue, subExpressCompanyAbb, subExpressCompanyName, subShipOrderNumber))
                                    {
                                        this.ShowMsg("发货成功", true);
                                    }
                                    else
                                    {
                                        this.ShowMsg("发货失败", false);
                                    }
                                }
                            }
                        }
                    }                    
                }
            }
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
                OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.orderId);
                this.BindOrderItems(orderInfo);
                this.btnSendGoods.Click += new System.EventHandler(this.btnSendGoods_Click);
                this.radioShippingMode.SelectedIndexChanged += new System.EventHandler(this.radioShippingMode_SelectedIndexChanged);
                if (!this.Page.IsPostBack)
                {
                    if (orderInfo == null)
                    {
                        base.GotoResourceNotFound();
                    }
                    else
                    {
                        this.radioShippingMode.DataBind();
                        this.radioShippingMode.SelectedValue = new int?(orderInfo.ShippingModeId);
                        this.BindExpressCompany(orderInfo.ShippingModeId);
                        this.expressRadioButtonList.SelectedValue = orderInfo.ExpressCompanyAbb;

                        //this.litShippingModeName.Text = orderInfo.ModeName;
                        //this.litShipToDate.Text = orderInfo.ShipToDate;
                        //this.litRemark.Text = orderInfo.Remark;
                        this.txtShipOrderNumber.Text = orderInfo.ShipOrderNumber;

                        this.BindSubOrder(orderInfo.OrderId);

                    }
                }
            }
        }
        private void radioShippingMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.radioShippingMode.SelectedValue.HasValue)
            {
                this.BindExpressCompany(this.radioShippingMode.SelectedValue.Value);
            }
        }
    }
}
