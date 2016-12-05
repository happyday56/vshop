using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Store;
using Hidistro.Messages;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;

namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.Orders)]
    public class ManageOrder : AdminPage
    {
        #region 属性
        protected string Reurl = string.Empty;

        protected HyperLink hlinkAllOrder;

        protected HyperLink hlinkNotPay;

        protected HyperLink hlinkYetPay;

        protected HyperLink hlinkSendGoods;

        protected HyperLink hlinkTradeFinished;

        protected HyperLink hlinkClose;

        protected HyperLink hlinkHistory;

        protected WebCalendar calendarStartDate;

        protected WebCalendar calendarEndDate;

        protected TextBox txtUserName;

        protected TextBox txtOrderId;

        protected Label lblStatus;

        protected TextBox txtProductName;

        protected TextBox txtShopTo;

        protected DropDownList ddlIsPrinted;

        protected DropDownList ddlIsCrossOrder;

        protected DropDownList ddlOrderType;

        protected ShippingModeDropDownList shippingModeDropDownList;

        protected RegionSelector dropRegion;

        protected DropDownList OrderFromList;

        protected Button btnSearchButton;

        protected PageSize hrefPageSize;

        protected Pager pager1;

        protected ImageLinkButton lkbtnDeleteCheck;

        protected ImageLinkButton lkbtnExportSubOrder;

        protected HtmlInputHidden hidOrderId;

        protected DataList dlstOrders;

        protected Pager pager;

        protected CloseTranReasonDropDownList ddlCloseReason;

        protected FormatedMoneyLabel lblOrderTotalForRemark;

        protected OrderRemarkImageRadioButtonList orderRemarkImageForRemark;

        protected TextBox txtRemark;

        protected Label lblOrderId;

        protected Label lblOrderTotal;

        protected Label lblRefundType;

        protected Label lblRefundRemark;

        protected Label lblContacts;

        protected Label lblEmail;

        protected Label lblTelephone;

        protected Label lblAddress;

        protected TextBox txtAdminRemark;

        protected Label return_lblOrderId;

        protected Label return_lblOrderTotal;

        protected Label return_lblRefundType;

        protected Label return_lblReturnRemark;

        protected Label return_lblContacts;

        protected Label return_lblEmail;

        protected Label return_lblTelephone;

        protected Label return_lblAddress;

        protected TextBox return_txtRefundMoney;

        protected TextBox return_txtAdminRemark;

        protected Label replace_lblOrderId;

        protected Label replace_lblOrderTotal;

        protected Label replace_lblComments;

        protected Label replace_lblContacts;

        protected Label replace_lblEmail;

        protected Label replace_lblTelephone;

        protected Label replace_lblAddress;

        protected Label replace_lblPostCode;

        protected TextBox replace_txtAdminRemark;

        protected HtmlInputHidden hidOrderTotal;

        protected HtmlInputHidden hidRefundType;

        protected HtmlInputHidden hidRefundMoney;

        protected HtmlInputHidden hidAdminRemark;

        protected Button btnCloseOrder;

        protected Button btnAcceptRefund;

        protected Button btnRefuseRefund;

        protected Button btnAcceptReturn;

        protected Button btnRefuseReturn;

        protected Button btnAcceptReplace;

        protected Button btnRefuseReplace;

        protected Button btnRemark;

        protected Button btnOrderGoods;

        protected Button btnProductGoods;

        protected BrandCategoriesDropDownList dropBrandId;

        protected DropDownList ddlOrderStatusId;

        #endregion

        private void bindOrderType()
        {
            int result = 0;
            int.TryParse(base.Request.QueryString["orderType"], out result);
            this.OrderFromList.SelectedIndex = result;
        }

        private void btnRefuseRefund_Click(object obj, EventArgs eventArg)
        {
            string userName = ManagerHelper.GetCurrentManager().UserName;
            OrderInfo orderInfo = OrderHelper.GetOrderInfo(hidOrderId.Value);
            RefundHelper.EnsureRefund(orderInfo.OrderId, userName, this.hidAdminRemark.Value, int.Parse(this.hidRefundType.Value), false);
            this.BindOrders();
            this.ShowMsg("成功的拒绝了订单退款", true);
        }

        private void btnCloseOrder_Click(object sender, System.EventArgs e)
        {
            OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
            orderInfo.CloseReason = this.ddlCloseReason.SelectedValue;
            if (OrderHelper.CloseTransaction(orderInfo))
            {
                orderInfo.OnClosed();
                this.BindOrders();
                Messenger.OrderClosed(MemberHelper.GetMember(orderInfo.UserId), orderInfo, orderInfo.CloseReason);
                this.ShowMsg("关闭订单成功", true);
            }
            else
            {
                this.ShowMsg("关闭订单失败", false);
            }
        }

        private void btnOrderGoods_Click(object sender, System.EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选要下载配货表的订单", false);
            }
            else
            {
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                string[] array = str.Split(new char[]
                {
                    ','
                });
                for (int i = 0; i < array.Length; i++)
                {
                    string str2 = array[i];
                    list.Add("'" + str2 + "'");
                }
                System.Data.DataSet orderGoods = OrderHelper.GetOrderGoods(string.Join(",", list.ToArray()));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.AppendLine("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=gb2312\"></head><body>");
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("<caption style='text-align:center;'>配货单(仓库拣货表)</caption>");
                builder.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("<td>订单单号</td>");
                builder.AppendLine("<td>商品名称</td>");
                builder.AppendLine("<td>货号</td>");
                builder.AppendLine("<td>规格</td>");
                builder.AppendLine("<td>拣货数量</td>");
                builder.AppendLine("<td>现库存数</td>");
                builder.AppendLine("<td>备注</td>");
                builder.AppendLine("</tr>");
                foreach (System.Data.DataRow row in orderGoods.Tables[0].Rows)
                {
                    builder.AppendLine("<tr>");
                    builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"] + "</td>");
                    builder.AppendLine("<td>" + row["ProductName"] + "</td>");
                    builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["SKU"] + "</td>");
                    builder.AppendLine("<td>" + row["SKUContent"] + "</td>");
                    builder.AppendLine("<td>" + row["ShipmentQuantity"] + "</td>");
                    builder.AppendLine("<td>" + row["Stock"] + "</td>");
                    builder.AppendLine("<td>" + row["Remark"] + "</td>");
                    builder.AppendLine("</tr>");
                }
                builder.AppendLine("</table>");
                builder.AppendLine("</body></html>");
                base.Response.Clear();
                base.Response.Buffer = false;
                base.Response.Charset = "GB2312";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=ordergoods_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                base.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                base.Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                base.Response.Write(builder.ToString());
                base.Response.End();
            }
        }

        private void btnProductGoods_Click(object sender, System.EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选要下载配货表的订单", false);
            }
            else
            {
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                string[] array = str.Split(new char[]
                {
                    ','
                });
                for (int i = 0; i < array.Length; i++)
                {
                    string str2 = array[i];
                    list.Add("'" + str2 + "'");
                }
                System.Data.DataSet productGoods = OrderHelper.GetProductGoods(string.Join(",", list.ToArray()));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.AppendLine("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=gb2312\"></head><body>");
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("<caption style='text-align:center;'>配货单(仓库拣货表)</caption>");
                builder.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("<td>商品名称</td>");
                builder.AppendLine("<td>货号</td>");
                builder.AppendLine("<td>规格</td>");
                builder.AppendLine("<td>拣货数量</td>");
                builder.AppendLine("<td>现库存数</td>");
                builder.AppendLine("</tr>");
                foreach (System.Data.DataRow row in productGoods.Tables[0].Rows)
                {
                    builder.AppendLine("<tr>");
                    builder.AppendLine("<td>" + row["ProductName"] + "</td>");
                    builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["SKU"] + "</td>");
                    builder.AppendLine("<td>" + row["SKUContent"] + "</td>");
                    builder.AppendLine("<td>" + row["ShipmentQuantity"] + "</td>");
                    builder.AppendLine("<td>" + row["Stock"] + "</td>");
                    builder.AppendLine("</tr>");
                }
                builder.AppendLine("</table>");
                builder.AppendLine("</body></html>");
                base.Response.Clear();
                base.Response.Buffer = false;
                base.Response.Charset = "GB2312";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=productgoods_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                base.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                base.Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                base.Response.Write(builder.ToString());
                base.Response.End();
            }
        }

        private void dlstOrders_ItemDataBound(object obj, DataListItemEventArgs dataListItemEventArg)
        {
            bool flag;
            if (dataListItemEventArg.Item.ItemType == ListItemType.Item || dataListItemEventArg.Item.ItemType == ListItemType.AlternatingItem)
            {
                OrderStatus orderStatu = (OrderStatus)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "OrderStatus");
                string str = "";
                if (!(DataBinder.Eval(dataListItemEventArg.Item.DataItem, "Gateway") is DBNull))
                {
                    str = (string)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "Gateway");
                }
                int num = (DataBinder.Eval(dataListItemEventArg.Item.DataItem, "GroupBuyId")) != DBNull.Value ? (int)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "GroupBuyId") : 0;
                HyperLink hyperLink = (HyperLink)dataListItemEventArg.Item.FindControl("lkbtnEditPrice");
                Label label = (Label)dataListItemEventArg.Item.FindControl("lkbtnSendGoods");
                Label logisticNo = (Label)dataListItemEventArg.Item.FindControl("lkbtnSendLogistic");
                ImageLinkButton imageLinkButton = (ImageLinkButton)dataListItemEventArg.Item.FindControl("lkbtnPayOrder");
                ImageLinkButton imageLinkButton1 = (ImageLinkButton)dataListItemEventArg.Item.FindControl("lkbtnConfirmOrder");
                Literal literal = (Literal)dataListItemEventArg.Item.FindControl("litCloseOrder");
                Literal litHandleStatus = (Literal)dataListItemEventArg.Item.FindControl("litHandleStatus");
                HtmlAnchor htmlAnchor = (HtmlAnchor)dataListItemEventArg.Item.FindControl("lkbtnCheckRefund");
                HtmlAnchor htmlAnchor1 = (HtmlAnchor)dataListItemEventArg.Item.FindControl("lkbtnCheckReturn");
                HtmlAnchor htmlAnchor2 = (HtmlAnchor)dataListItemEventArg.Item.FindControl("lkbtnCheckReplace");
                if (orderStatu == OrderStatus.WaitBuyerPay)
                {
                    hyperLink.Visible = true;
                    literal.Visible = true;
                    if (str != "hishop.plugins.payment.podrequest") imageLinkButton.Visible = true;
                }
                if (orderStatu == OrderStatus.ApplyForRefund)
                {
                    //       htmlAnchor.Visible = true;

                    //  string orderId = DataBinder.Eval(dataListItemEventArg.Item.DataItem, "OrderId") == DBNull.Value ? "" : DataBinder.Eval(dataListItemEventArg.Item.DataItem, "OrderId").ToString();
                    //  string realname = DataBinder.Eval(dataListItemEventArg.Item.DataItem, "RealName") == DBNull.Value ? "" : DataBinder.Eval(dataListItemEventArg.Item.DataItem, "RealName").ToString();
                    ////  string shippingRegion = DataBinder.Eval(dataListItemEventArg.Item.DataItem, "ShoppingRegion") == DBNull.Value ? "" : DataBinder.Eval(dataListItemEventArg.Item.DataItem, "ShoppingRegion").ToString();
                    //  string cellphone = DataBinder.Eval(dataListItemEventArg.Item.DataItem, "CellPhone") == DBNull.Value ? "" : DataBinder.Eval(dataListItemEventArg.Item.DataItem, "CellPhone").ToString();
                    //  string orderTotal = DataBinder.Eval(dataListItemEventArg.Item.DataItem, "OrderTotal") == DBNull.Value ? "" : DataBinder.Eval(dataListItemEventArg.Item.DataItem, "OrderTotal").ToString();
                    //  //(ReturnsId, RefundMoney, Comments, ProductId, OrderId,Status)
                    //  if (!string.IsNullOrEmpty(orderId))
                    //  {
                    //      var refundinfo = RefundHelper.GetRefundInfoByOrderId(orderId);
                    //      htmlAnchor.Attributes["onclick"] =
                    //          string.Format("CheckRefund('{0}','{1}','{2}','{3}','{4}','{5}')", refundinfo.RefundId, orderTotal, refundinfo.RefundRemark, 0, orderId, (int)orderStatu);
                    //  }

                }
                //   if (orderStatu == OrderStatus.ApplyForReturns) htmlAnchor1.Visible = true;
                //   if (orderStatu == OrderStatus.ApplyForReplacement) htmlAnchor2.Visible = true;
                if (num <= 0)
                {
                    Label label1 = label;
                    if (orderStatu == OrderStatus.BuyerAlreadyPaid)
                    {
                        flag = true; 
                    }
                    else 
                    { 
                        flag = (orderStatu != OrderStatus.WaitBuyerPay ? false : str == "hishop.plugins.payment.podrequest"); 
                    }
                    label1.Visible = flag;
                    //SellerAlreadySent
                    if (orderStatu == OrderStatus.BuyerAlreadyPaid || orderStatu == OrderStatus.SellerAlreadySent)
                    {
                        logisticNo.Visible = true;
                    }
                    else
                    {
                        logisticNo.Visible = false;
                    }
                }
                else
                {
                    GroupBuyStatus groupBuyStatu = (GroupBuyStatus)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "GroupBuyStatus");
                    label.Visible = (orderStatu != OrderStatus.BuyerAlreadyPaid ? false : groupBuyStatu == GroupBuyStatus.Success);
                    //logisticNo.Visible = (orderStatu != OrderStatus.BuyerAlreadyPaid ? false : groupBuyStatu == GroupBuyStatus.Success);
                    logisticNo.Visible = ((orderStatu != OrderStatus.BuyerAlreadyPaid || orderStatu != OrderStatus.SellerAlreadySent) ? false : groupBuyStatu == GroupBuyStatus.Success);
                }
                imageLinkButton1.Visible = orderStatu == OrderStatus.SellerAlreadySent;
                if (orderStatu == OrderStatus.ApplyForReturns)
                {
                    // 申请退货状态
                    // litHandleStatus
                    int handleStatus = (int)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "HandleStatus");
                    string landleStatusName = "";

                    if (handleStatus == 4)
                    {
                        landleStatusName = "(退货待审核)";
                    }
                    else if (handleStatus == 5)
                    {
                        landleStatusName = "(退货审核通过)";
                    }
                    else if (handleStatus == 7)
                    {
                        landleStatusName = "(退货审核未通过)";
                    }
                    else if (handleStatus == 6)
                    {
                        landleStatusName = "(确认退货,未退款)";
                    }
                    else if (handleStatus == 8)
                    {
                        landleStatusName = "(拒绝退款)";
                    }
                    else if (handleStatus == 2)
                    {
                        landleStatusName = "(已退款)";
                    }

                    litHandleStatus.Text = landleStatusName;
                }
                else if (orderStatu == OrderStatus.Closed)
                {
                    string closeReason = (string)DataBinder.Eval(dataListItemEventArg.Item.DataItem, "CloseReason");
                    if (!string.IsNullOrEmpty(closeReason))
                    {
                        litHandleStatus.Text = "(" + closeReason + ")";
                    }
                    else
                    {
                        litHandleStatus.Text = "";
                    }
                }
            }
        }

        private void btnSendGoods_Click(object sender, System.EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选要发货的订单", false);
            }
            else
            {
                this.Page.Response.Redirect(Globals.GetAdminAbsolutePath("/Sales/BatchSendOrderGoods.aspx?OrderIds=" + str));
            }
        }

        private void BindOrders()
        {
            OrderQuery orderQuery = this.GetOrderQuery();
            DbQueryResult orders = OrderHelper.GetOrders(orderQuery);
            this.dlstOrders.DataSource = orders.Data;
            this.dlstOrders.DataBind();
            this.pager.TotalRecords = orders.TotalRecords;
            this.pager1.TotalRecords = orders.TotalRecords;
            this.txtUserName.Text = orderQuery.UserName;
            this.txtOrderId.Text = orderQuery.OrderId;
            this.txtProductName.Text = orderQuery.ProductName;
            this.txtShopTo.Text = orderQuery.ShipTo;
            this.calendarStartDate.SelectedDate = orderQuery.StartDate;
            this.calendarEndDate.SelectedDate = orderQuery.EndDate;
            this.lblStatus.Text = orderQuery.Status.ToString();
            this.shippingModeDropDownList.SelectedValue = orderQuery.ShippingModeId;
            if (orderQuery.IsPrinted.HasValue)
            {
                this.ddlIsPrinted.SelectedValue = orderQuery.IsPrinted.Value.ToString();
            }
            if (orderQuery.IsCrossOrder.HasValue)
            {
                this.ddlIsCrossOrder.SelectedValue = orderQuery.IsCrossOrder.Value.ToString();
            }
            if (orderQuery.OrderCategory.HasValue)
            {
                this.ddlOrderType.SelectedValue = orderQuery.OrderCategory.Value.ToString();
            }
            if (orderQuery.RegionId.HasValue)
            {
                this.dropRegion.SetSelectedRegionId(orderQuery.RegionId);
            }
            if (orderQuery.BrandId.HasValue)
            {
                this.dropBrandId.SelectedValue = orderQuery.BrandId.Value;
            }
            if (orderQuery.OrderStatus.HasValue)
            {
                this.ddlOrderStatusId.SelectedValue = orderQuery.OrderStatus.Value.ToString();
            }
        }

        private OrderQuery GetOrderQuery()
        {
            OrderQuery query = new OrderQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
            {
                query.OrderId = Globals.UrlDecode(this.Page.Request.QueryString["OrderId"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ProductName"]))
            {
                query.ProductName = Globals.UrlDecode(this.Page.Request.QueryString["ProductName"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ShipTo"]))
            {
                query.ShipTo = Globals.UrlDecode(this.Page.Request.QueryString["ShipTo"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["UserName"]))
            {
                query.UserName = Globals.UrlDecode(this.Page.Request.QueryString["UserName"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StartDate"]))
            {
                query.StartDate = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["StartDate"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["GroupBuyId"]))
            {
                query.GroupBuyId = new int?(int.Parse(this.Page.Request.QueryString["GroupBuyId"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndDate"]))
            {
                query.EndDate = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["EndDate"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderStatus"]))
            {
                int num = 0;
                if (int.TryParse(this.Page.Request.QueryString["OrderStatus"], out num))
                {
                    query.Status = (OrderStatus)num;
                }
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["IsPrinted"]))
            {
                int num2 = 0;
                if (int.TryParse(this.Page.Request.QueryString["IsPrinted"], out num2))
                {
                    query.IsPrinted = new int?(num2);
                }
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["IsCrossOrder"]))
            {
                int num6 = 0;
                if (int.TryParse(this.Page.Request.QueryString["IsCrossOrder"], out num6))
                {
                    query.IsCrossOrder = new int?(num6);
                }
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderCategory"]))
            {
                int num9 = 0;
                if (int.TryParse(this.Page.Request.QueryString["OrderCategory"], out num9))
                {
                    query.OrderCategory = new int?(num9);
                }
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ModeId"]))
            {
                int num3 = 0;
                if (int.TryParse(this.Page.Request.QueryString["ModeId"], out num3))
                {
                    query.ShippingModeId = new int?(num3);
                }
            }
            int num4;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["region"]) && int.TryParse(this.Page.Request.QueryString["region"], out num4))
            {
                query.RegionId = new int?(num4);
            }
            int num5;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["UserId"]) && int.TryParse(this.Page.Request.QueryString["UserId"], out num5))
            {
                query.UserId = new int?(num5);
            }
            int result = 0;
            if (int.TryParse(base.Request.QueryString["orderType"], out result) && result > 0)
            {
                query.Type = new OrderQuery.OrderType?((OrderQuery.OrderType)result);
            }
            query.PageIndex = this.pager.PageIndex;
            query.PageSize = this.pager.PageSize;
            query.SortBy = "OrderDate";
            query.SortOrder = SortAction.Desc;
            int num15;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["BrandId"]) && int.TryParse(this.Page.Request.QueryString["BrandId"], out num15))
            {
                query.BrandId = new int?(num15);
            }
            int num16;
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderStatusId"]) && int.TryParse(this.Page.Request.QueryString["OrderStatusId"], out num16))
            {
                query.OrderStatus = new int?(num16);
            }
            return query;
        }

        private void ReloadOrders(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("UserName", this.txtUserName.Text);
            queryStrings.Add("OrderId", this.txtOrderId.Text);
            queryStrings.Add("ProductName", this.txtProductName.Text);
            queryStrings.Add("ShipTo", this.txtShopTo.Text);
            queryStrings.Add("PageSize", this.pager.PageSize.ToString());
            queryStrings.Add("OrderType", this.OrderFromList.SelectedValue);
            queryStrings.Add("OrderStatus", this.lblStatus.Text);
            queryStrings.Add("OrderStatusId", this.ddlOrderStatusId.SelectedValue);
            if (this.calendarStartDate.SelectedDate.HasValue)
            {
                queryStrings.Add("StartDate", this.calendarStartDate.SelectedDate.Value.ToString());
            }
            if (this.calendarEndDate.SelectedDate.HasValue)
            {
                queryStrings.Add("EndDate", this.calendarEndDate.SelectedDate.Value.ToString());
            }
            if (!isSearch)
            {
                queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["GroupBuyId"]))
            {
                queryStrings.Add("GroupBuyId", this.Page.Request.QueryString["GroupBuyId"]);
            }
            if (this.shippingModeDropDownList.SelectedValue.HasValue)
            {
                queryStrings.Add("ModeId", this.shippingModeDropDownList.SelectedValue.Value.ToString());
            }
            if (!string.IsNullOrEmpty(this.ddlIsPrinted.SelectedValue))
            {
                queryStrings.Add("IsPrinted", this.ddlIsPrinted.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.ddlIsCrossOrder.SelectedValue))
            {
                queryStrings.Add("IsCrossOrder", this.ddlIsCrossOrder.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
            {
                queryStrings.Add("OrderCategory", this.ddlOrderType.SelectedValue);
            }
            if (this.dropRegion.GetSelectedRegionId().HasValue)
            {
                queryStrings.Add("region", this.dropRegion.GetSelectedRegionId().Value.ToString());
            }
            if (this.dropBrandId.SelectedValue.HasValue)
            {
                queryStrings.Add("BrandId", this.dropBrandId.SelectedValue.ToString());
            }
            
            base.ReloadPage(queryStrings);
        }

        private void SetOrderStatusLink()
        {
            string format = Globals.ApplicationPath + "/Admin/sales/ManageOrder.aspx?orderStatus={0}";
            this.hlinkAllOrder.NavigateUrl = string.Format(format, 0);
            this.hlinkNotPay.NavigateUrl = string.Format(format, 1);
            this.hlinkYetPay.NavigateUrl = string.Format(format, 2);
            this.hlinkSendGoods.NavigateUrl = string.Format(format, 3);
            this.hlinkClose.NavigateUrl = string.Format(format, 4);
            this.hlinkTradeFinished.NavigateUrl = string.Format(format, 5);
            this.hlinkHistory.NavigateUrl = string.Format(format, 99);
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
                    OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
                    orderInfo.OrderId = this.hidOrderId.Value;
                    if (this.orderRemarkImageForRemark.SelectedItem != null)
                    {
                        orderInfo.ManagerMark = this.orderRemarkImageForRemark.SelectedValue;
                    }
                    orderInfo.ManagerRemark = Globals.HtmlEncode(this.txtRemark.Text);
                    if (OrderHelper.SaveRemark(orderInfo))
                    {
                        this.BindOrders();
                        this.ShowMsg("保存备忘录成功", true);
                    }
                    else
                    {
                        this.ShowMsg("保存失败", false);
                    }
                }
            }
        }

        protected void btnAcceptRefund_Click(object sender, EventArgs e)
        {
            string userName = ManagerHelper.GetCurrentManager().UserName;
            if (RefundHelper.EnsureRefund(OrderHelper.GetOrderInfo(this.hidOrderId.Value).OrderId, userName, this.hidAdminRemark.Value, int.Parse(this.hidRefundType.Value == string.Empty ? "1" : this.hidRefundType.Value), true))
            {
                this.BindOrders();
                this.ShowMsg("成功的确认了订单退款", true);
            }
        }

        protected void btnSearchButton_Click(object sender, EventArgs e)
        {
            this.ReloadOrders(true);
        }

        protected void dlstOrders_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            bool flag = false;
            OrderInfo orderInfo = OrderHelper.GetOrderInfo(e.CommandArgument.ToString());
            if (orderInfo != null)
            {
                if (e.CommandName == "CONFIRM_PAY" && orderInfo.CheckAction(OrderActions.SELLER_CONFIRM_PAY))
                {
                    int groupBuyId = orderInfo.GroupBuyId;
                    if (!OrderHelper.ConfirmPay(orderInfo))
                    {
                        this.ShowMsg("确认订单收款失败", false);
                        return;
                    }
                    DebitNoteInfo debitNoteInfo = new DebitNoteInfo();

                    debitNoteInfo.NoteId = Globals.GetGenerateId();
                    debitNoteInfo.OrderId = e.CommandArgument.ToString();
                    debitNoteInfo.Operator = ManagerHelper.GetCurrentManager().UserName;
                    debitNoteInfo.Remark = string.Concat("后台", debitNoteInfo.Operator, "收款成功");

                    OrderHelper.SaveDebitNote(debitNoteInfo);
                    if (orderInfo.GroupBuyId > 0)
                    { }
                    // 订单付款完成后计算提成
                    //DistributorsBrower.CalcCommissionByBuy(orderInfo);
                    DistributorsBrower.UpdateCalculationCommissionNew(orderInfo);

                    this.BindOrders();
                    orderInfo.OnPayment();
                    this.ShowMsg("成功的确认了订单收款", true);
                    return;
                }
                if (e.CommandName == "FINISH_TRADE" && orderInfo.CheckAction(OrderActions.SELLER_FINISH_TRADE)) //完成订单
                {
                    Dictionary<string, LineItemInfo> lineItems = orderInfo.LineItems;
                    LineItemInfo lineItemInfo = new LineItemInfo();
                    foreach (KeyValuePair<string, LineItemInfo> lineItem in lineItems)
                    {
                        lineItemInfo = lineItem.Value;
                        if (lineItemInfo.OrderItemsStatus != OrderStatus.ApplyForRefund && lineItemInfo.OrderItemsStatus != OrderStatus.ApplyForReturns) continue;
                        flag = true;
                    }
                    if (!flag)
                    {
                        if (!OrderHelper.ConfirmOrderFinish(orderInfo))
                        {
                            this.ShowMsg("完成订单失败", false);
                            return;
                        }
                        this.BindOrders();
                        // 订单完成时不再计算佣金，只做订单相关数据状态的更新
                        //DistributorsBrower.UpdateCalculationCommission(orderInfo);
                        // 更新订单佣金的结算状态（1：表示已确认收货结算）
                        DistributorsBrower.UpdateCommissionOrderStatus(orderInfo.OrderId, 1);

                        foreach (LineItemInfo value in orderInfo.LineItems.Values)
                        {
                            if (value.OrderItemsStatus.ToString() != OrderStatus.SellerAlreadySent.ToString()) continue;
                            RefundHelper.UpdateOrderGoodStatu(orderInfo.OrderId, value.SkuId, (int)OrderStatus.Finished);
                        }
                        // 判断是否有购买默认的分销商购买商品，有，则用户可以申请开店
                        if (DistributorsBrower.CheckIsDistributoBuyProduct(orderInfo.OrderId))
                        {
                            bool isStoreFlag = MemberProcessor.UpdateUserIsStore(orderInfo.UserId, 1);
                        }

                        this.ShowMsg("成功的完成了该订单", true);
                        return;
                    }
                    this.ShowMsg("订单中商品有退货(款)不允许完成!", false);
                }
            }
        }

        protected void lkbtnExportSubOrder_Click(object sender, System.EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选择要导出的订单", false);
            }
            else
            {
                string currOrderId = "";
                string currSubOrderId = "";
                string currBrandId = "";

                DataTable subOrderData = OrderHelper.GetExportOrderSubLogistic("'" + str.Replace(",", "','") + "'");
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=gb2312\"></head><body>");
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"15\" rules=\"all\" border=\"1\">");
                builder.AppendLine("<caption style='text-align:center;'>品牌商发货单</caption>");
                builder.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("<td>主订单编号</td>");
                builder.AppendLine("<td>子订单编号</td>");
                builder.AppendLine("<td>品牌名称</td>");
                builder.AppendLine("<td>收货人</td>");
                builder.AppendLine("<td>收货人区域</td>");
                builder.AppendLine("<td>收货人地址</td>");
                builder.AppendLine("<td>收货人邮编</td>");
                builder.AppendLine("<td>收货人电话</td>");
                builder.AppendLine("<td>收货人身份证号码</td>");
                builder.AppendLine("<td>物流公司</td>");
                builder.AppendLine("<td>物流单号</td>");
                builder.AppendLine("<td>商品名称</td>");
                builder.AppendLine("<td>货号</td>");
                builder.AppendLine("<td>规格</td>");
                builder.AppendLine("<td>发货数量</td>");
                builder.AppendLine("</tr>");
                foreach (DataRow row in subOrderData.Rows)
                {

                    builder.AppendLine("<tr>");

                    if (currBrandId.EqualIgnoreCase(row["ProductBrandId"].ToString())
                        && currOrderId.EqualIgnoreCase(row["OrderId"].ToString())
                        && currSubOrderId.EqualIgnoreCase(row["SubOrderId"].ToString()))
                    {
                        builder.AppendLine("<td colspan=\"11\"></td>");
                    }
                    else
                    {
                        builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"] + "</td>");
                        builder.AppendLine("<td>" + row["SubOrderId"] + "</td>");
                        builder.AppendLine("<td>" + row["BrandName"] + "</td>");
                        builder.AppendLine("<td>" + row["ShipTo"] + "</td>");
                        builder.AppendLine("<td>" + row["ShippingRegion"] + "</td>");
                        builder.AppendLine("<td>" + row["Address"] + "</td>");
                        builder.AppendLine("<td>" + row["ZipCode"] + "</td>");
                        builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["CellPhone"] + "</td>");
                        builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["IdCard"] + "</td>");
                        builder.AppendLine("<td>" + row["SubExpressCompanyName"] + "</td>");
                        builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["SubShipOrderNumber"] + "</td>");
                    }

                    builder.AppendLine("<td>" + row["ProductName"] + "</td>");
                    builder.AppendLine("<td style=\"vnd.ms-excel.numberformat:@\">" + row["SKU"] + "</td>");
                    builder.AppendLine("<td>" + row["SKUContent"] + "</td>");
                    builder.AppendLine("<td>" + row["ShipmentQuantity"] + "</td>");
                    builder.AppendLine("</tr>");

                    currBrandId = row["ProductBrandId"].ToString();
                    currOrderId = row["OrderId"].ToString();
                    currSubOrderId = row["SubOrderId"].ToString();

                }
                builder.AppendLine("</table>");
                builder.AppendLine("</body></html>");
                base.Response.Clear();
                base.Response.Buffer = false;
                base.Response.Charset = "GB2312";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=OrderLogistic_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                base.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                base.Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                base.Response.Write(builder.ToString());
                base.Response.End();

                //this.BindOrders();
                //this.ShowMsg("成功导出数据", true);
            }
        }

        protected void lkbtnDeleteCheck_Click(object sender, System.EventArgs e)
        {
            string str = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                str = base.Request["CheckBoxGroup"];
            }
            if (str.Length <= 0)
            {
                this.ShowMsg("请选择要删除的订单", false);
            }
            else
            {
                int num = OrderHelper.DeleteOrders("'" + str.Replace(",", "','") + "'");
                this.BindOrders();
                this.ShowMsg(string.Format("成功删除了{0}个订单", num), true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int num;
            string str;
            string str1;
            this.Reurl = base.Request.Url.ToString();
            if (!this.Reurl.Contains("?"))
            {
                ManageOrder manageOrder = this;
                manageOrder.Reurl = string.Concat(manageOrder.Reurl, "?pageindex=1");
            }
            this.Reurl = Regex.Replace(this.Reurl, @"&t=(\d+)", "");
            this.Reurl = Regex.Replace(this.Reurl, @"(\?)t=(\d+)", "?");
            if ((string.IsNullOrEmpty(base.Request["isCallback"]) ? false : base.Request["isCallback"] == "true"))
            {
                if (string.IsNullOrEmpty(base.Request["ReturnsId"]))
                {
                    base.Response.Write("{\"Status\":\"0\"}");
                    base.Response.End();
                    return;
                }
                OrderInfo orderInfo = OrderHelper.GetOrderInfo(base.Request["orderId"]);
                StringBuilder stringBuilder = new StringBuilder();
                if (base.Request["type"] != "refund")
                {
                    num = 0;
                    str = "";
                }
                else
                {
                    RefundHelper.GetRefundType(base.Request["orderId"], out num, out str);
                }
                str1 = (num != 1 ? "银行转帐" : "退到预存款");
                stringBuilder.AppendFormat(",\"OrderTotal\":\"{0}\"", Globals.FormatMoney(orderInfo.GetTotal()));
                if (base.Request["type"] != "replace")
                {
                    stringBuilder.AppendFormat(",\"RefundType\":\"{0}\"", num);
                    stringBuilder.AppendFormat(",\"RefundTypeStr\":\"{0}\"", str1);
                }
                stringBuilder.AppendFormat(",\"Contacts\":\"{0}\"", orderInfo.ShipTo);
                stringBuilder.AppendFormat(",\"Email\":\"{0}\"", orderInfo.EmailAddress);
                stringBuilder.AppendFormat(",\"Telephone\":\"{0}\"", string.Concat(orderInfo.TelPhone, " "), orderInfo.CellPhone.Trim());
                stringBuilder.AppendFormat(",\"Address\":\"{0}\"", orderInfo.Address);
                stringBuilder.AppendFormat(",\"Remark\":\"{0}\"", str.Replace(",", ""));
                stringBuilder.AppendFormat(",\"PostCode\":\"{0}\"", orderInfo.ZipCode);
                stringBuilder.AppendFormat(",\"GroupBuyId\":\"{0}\"", (orderInfo.GroupBuyId > 0 ? orderInfo.GroupBuyId : 0));
                base.Response.Clear();
                base.Response.ContentType = "application/json";
                base.Response.Write(string.Concat("{\"Status\":\"1\"", stringBuilder.ToString(), "}"));
                base.Response.End();
            }
            this.btnAcceptRefund.Click += new EventHandler(this.btnAcceptRefund_Click);
            this.btnRefuseRefund.Click += new EventHandler(this.btnRefuseRefund_Click);
            this.dlstOrders.ItemDataBound += new DataListItemEventHandler(this.dlstOrders_ItemDataBound);
            this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click);
            this.dlstOrders.ItemCommand += new DataListCommandEventHandler(this.dlstOrders_ItemCommand);
            this.btnRemark.Click += new EventHandler(this.btnRemark_Click);
            this.btnCloseOrder.Click += new EventHandler(this.btnCloseOrder_Click);
            this.lkbtnDeleteCheck.Click += new EventHandler(this.lkbtnDeleteCheck_Click);
            this.lkbtnExportSubOrder.Click += new EventHandler(this.lkbtnExportSubOrder_Click);
            this.btnProductGoods.Click += new EventHandler(this.btnProductGoods_Click);
            this.btnOrderGoods.Click += new EventHandler(this.btnOrderGoods_Click);
            if (!this.Page.IsPostBack)
            {
                this.shippingModeDropDownList.DataBind();
                this.ddlIsPrinted.Items.Clear();
                this.ddlIsPrinted.Items.Add(new ListItem("全部", string.Empty));
                this.ddlIsPrinted.Items.Add(new ListItem("已打印", "1"));
                this.ddlIsPrinted.Items.Add(new ListItem("未打印", "0"));
                this.ddlIsCrossOrder.Items.Clear();
                this.ddlIsCrossOrder.Items.Add(new ListItem("全部", string.Empty));
                this.ddlIsCrossOrder.Items.Add(new ListItem("是", "1"));
                this.ddlIsCrossOrder.Items.Add(new ListItem("否", "0"));
                this.ddlOrderType.Items.Clear();
                this.ddlOrderType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlOrderType.Items.Add(new ListItem("普通订单", "1"));
                this.ddlOrderType.Items.Add(new ListItem("开店订单", "2"));
                this.SetOrderStatusLink();
                this.bindOrderType();
                this.BindOrders();
                this.dropBrandId.DataBind();
            }
            CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
        }
    }
}