using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.SaleDetails)]
	public class SaleDetails : AdminPage
	{
		protected Button btnQuery;
        protected WebCalendar calendarOrderStartDate;
        protected WebCalendar calendarOrderEndDate;
        protected WebCalendar calendarPayStartDate;
        protected WebCalendar calendarPayEndDate;
        protected WebCalendar calendarShippingStartDate;
        protected WebCalendar calendarShippingEndDate;
        private DateTime? orderStartTime;
        private DateTime? orderEndTime;
        private DateTime? payStartTime;
        private DateTime? payEndTime;
        private DateTime? shippingStartTime;
        private DateTime? shippingEndTime;
		protected GridView grdOrderLineItem;
		protected Pager pager;
        protected DropDownList ddlOrderType;
        protected TextBox txtOrderId;
        private string OrderId = "";
        private int? orderTypeId;
        protected LinkButton btnCreateReport;

		private void BindList()
		{
			SaleStatisticsQuery query = new SaleStatisticsQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
                OrderId = this.OrderId,
                OrderTypeId = this.orderTypeId,
                OrderStartDate = this.orderStartTime,
                OrderEndDate = this.orderEndTime,
                PayStartDate = this.payStartTime,
                PayEndDate = this.payEndTime,
                ShippingStartDate = this.shippingStartTime,
                ShippingEndDate = this.shippingEndTime,
                SortBy = "OrderDate",
                SortOrder = Core.Enums.SortAction.Desc
			};
			DbQueryResult saleOrderLineItemsStatistics = SalesHelper.GetSaleOrderLineItemsStatistics(query);
			this.grdOrderLineItem.DataSource = saleOrderLineItemsStatistics.Data;
			this.grdOrderLineItem.DataBind();
			this.pager.TotalRecords = saleOrderLineItemsStatistics.TotalRecords;
			this.grdOrderLineItem.PageSize = query.PageSize;
		}
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.ReBind(true);
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderStartDate"]))
                {
                    this.orderStartTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["OrderStartDate"]));
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderEndDate"]))
                {
                    this.orderEndTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["OrderEndDate"]));
                }

                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["PayStartDate"]))
                {
                    this.payStartTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["PayStartDate"]));
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["PayEndDate"]))
                {
                    this.payEndTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["PayEndDate"]));
                }

                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ShippingStartDate"]))
                {
                    this.shippingStartTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["ShippingStartDate"]));
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ShippingEndDate"]))
                {
                    this.shippingEndTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["ShippingEndDate"]));
                }

                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
                {
                    this.OrderId = base.Server.UrlDecode(this.Page.Request.QueryString["OrderId"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderType"]))
                {
                    int num1 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["OrderType"], out num1))
                    {
                        this.orderTypeId = num1;
                    }
                }
                this.calendarOrderStartDate.SelectedDate = this.orderStartTime;
                this.calendarOrderEndDate.SelectedDate = this.orderEndTime;
                this.calendarPayStartDate.SelectedDate = this.payStartTime;
                this.calendarPayEndDate.SelectedDate = this.payEndTime;
                this.calendarShippingStartDate.SelectedDate = this.shippingStartTime;
                this.calendarShippingEndDate.SelectedDate = this.shippingEndTime;

                this.txtOrderId.Text = this.OrderId;

                if (this.orderTypeId.HasValue)
                {
                    this.ddlOrderType.SelectedValue = this.orderTypeId.Value.ToString();
                }
			}
			else
			{
                this.orderStartTime = this.calendarOrderStartDate.SelectedDate;
                this.orderEndTime = this.calendarOrderEndDate.SelectedDate;
                this.payStartTime = this.calendarPayStartDate.SelectedDate;
                this.payEndTime = this.calendarPayEndDate.SelectedDate;
                this.shippingStartTime = this.calendarShippingStartDate.SelectedDate;
                this.shippingEndTime = this.calendarShippingEndDate.SelectedDate;

                this.OrderId = this.txtOrderId.Text;
                if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
                {
                    this.orderTypeId = int.Parse(this.ddlOrderType.SelectedValue);

                }
			}

		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			this.LoadParameters();
			if (!this.Page.IsPostBack)
			{
                this.ddlOrderType.Items.Clear();
                this.ddlOrderType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlOrderType.Items.Add(new ListItem("普通订单", "1"));
                this.ddlOrderType.Items.Add(new ListItem("开店订单", "2"));

                if (this.orderTypeId.HasValue)
                {
                    this.ddlOrderType.SelectedValue = this.orderTypeId.Value.ToString();
                }

				this.BindList();
			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("OrderStartDate", this.calendarOrderStartDate.SelectedDate.ToString());
            queryStrings.Add("OrderEndDate", this.calendarOrderEndDate.SelectedDate.ToString());
            queryStrings.Add("PayStartDate", this.calendarPayStartDate.SelectedDate.ToString());
            queryStrings.Add("PayEndDate", this.calendarPayEndDate.SelectedDate.ToString());
            queryStrings.Add("ShippingStartDate", this.calendarShippingStartDate.SelectedDate.ToString());
            queryStrings.Add("ShippingEndDate", this.calendarShippingEndDate.SelectedDate.ToString());

            queryStrings.Add("OrderId", this.txtOrderId.Text);
            if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
            {
                queryStrings.Add("OrderType", this.ddlOrderType.SelectedValue);
            }
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			base.ReloadPage(queryStrings);
		}

        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {
            LoadParameters();

            StringBuilder builder = new StringBuilder();

            SaleStatisticsQuery query = new SaleStatisticsQuery
            {
                PageIndex = 1,
                PageSize = 60000,
                OrderId = this.OrderId,
                OrderTypeId = this.orderTypeId,
                OrderStartDate = this.orderStartTime,
                OrderEndDate = this.orderEndTime,
                PayStartDate = this.payStartTime,
                PayEndDate = this.payEndTime,
                ShippingStartDate = this.shippingStartTime,
                ShippingEndDate = this.shippingEndTime,
                SortBy = "OrderDate",
                SortOrder = Core.Enums.SortAction.Desc
            };
            DbQueryResult saleOrderLineItemsStatistics = SalesHelper.GetSaleOrderLineItemsStatistics(query);

            DataTable exportData = (DataTable)saleOrderLineItemsStatistics.Data;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>订单号</td>");
                builder.AppendLine("        <td>货号</td>");
                builder.AppendLine("        <td>商品名称</td>");
                builder.AppendLine("        <td>发货数量</td>");
                builder.AppendLine("        <td>商品单价</td>");
                builder.AppendLine("        <td>订单总价</td>");
                builder.AppendLine("        <td>优惠减免</td>");
                builder.AppendLine("        <td>红包抵扣</td>");
                builder.AppendLine("        <td>金贝抵扣</td>");
                builder.AppendLine("        <td>实收金额</td>");
                builder.AppendLine("        <td>成交时间</td>");
                builder.AppendLine("        <td>发货时间</td>");
                builder.AppendLine("        <td>订单类型</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["SKU"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ProductName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ShipmentQuantity"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ItemAdjustedPrice"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["VirtualPointAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["orderDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ShippingDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTypeName"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleDetailData_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                this.Page.Response.ContentType = "application/ms-excel";
                this.Page.EnableViewState = false;
                this.Page.Response.Write(builder.ToString());
                this.Page.Response.End();

            }
            else
            {
                this.ShowMsg("没有导出数据", true);
            }
        }
	}
}
