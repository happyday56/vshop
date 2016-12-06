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
    public class SaleOrderStatistics : AdminPage
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
        protected DropDownList ddlOrderStatus;
        protected TextBox txtOrderId;
        private string OrderId = "";
        private int? orderTypeId;
        private int? orderStatusId;
        protected LinkButton btnCreateReport;

		private void BindList()
		{
			SaleStatisticsQuery query = new SaleStatisticsQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
                OrderId = this.OrderId,
                OrderTypeId = this.orderTypeId,
                ReturnStatus = 1,
                OrderStatus = this.orderStatusId,
                OrderStartDate = this.orderStartTime,
                OrderEndDate = this.orderEndTime,
                PayStartDate = this.payStartTime,
                PayEndDate = this.payEndTime,
                ShippingStartDate = this.shippingStartTime,
                ShippingEndDate = this.shippingEndTime,
                SortBy = "OrderDate",
                SortOrder = Core.Enums.SortAction.Desc
                
			};
            DbQueryResult saleOrderLineItemsStatistics = SalesHelper.SearchSaleOrderStatisticsData(query);
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
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderStatus"]))
                {
                    int num2 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["OrderStatus"], out num2))
                    {
                        this.orderStatusId = num2;
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
                if (this.orderStatusId.HasValue)
                {
                    this.ddlOrderStatus.SelectedValue = this.orderStatusId.Value.ToString();
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
                if (!string.IsNullOrEmpty(this.ddlOrderStatus.SelectedValue))
                {
                    this.orderStatusId = int.Parse(this.ddlOrderStatus.SelectedValue);
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

                this.ddlOrderStatus.Items.Clear();
                this.ddlOrderStatus.Items.Add(new ListItem("全部", string.Empty));
                this.ddlOrderStatus.Items.Add(new ListItem("待付款", "1"));
                this.ddlOrderStatus.Items.Add(new ListItem("待发货", "2"));
                this.ddlOrderStatus.Items.Add(new ListItem("已发货", "3"));
                this.ddlOrderStatus.Items.Add(new ListItem("已关闭", "4"));
                this.ddlOrderStatus.Items.Add(new ListItem("订单已完成", "5"));
                //this.ddlOrderStatus.Items.Add(new ListItem("申请退款", "6"));
                this.ddlOrderStatus.Items.Add(new ListItem("申请退货", "7"));
                //this.ddlOrderStatus.Items.Add(new ListItem("申请换货", "8"));
                //this.ddlOrderStatus.Items.Add(new ListItem("已退款", "9"));
                //this.ddlOrderStatus.Items.Add(new ListItem("已退货", "10"));

                if (this.orderTypeId.HasValue)
                {
                    this.ddlOrderType.SelectedValue = this.orderTypeId.Value.ToString();
                }
                if (this.orderStatusId.HasValue)
                {
                    this.ddlOrderStatus.SelectedValue = this.orderStatusId.Value.ToString();
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
            if (!string.IsNullOrEmpty(this.ddlOrderStatus.SelectedValue))
            {
                queryStrings.Add("OrderStatus", this.ddlOrderStatus.SelectedValue);
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
                ReturnStatus = 1,
                OrderStatus = this.orderStatusId,
                OrderStartDate = this.orderStartTime,
                OrderEndDate = this.orderEndTime,
                PayStartDate = this.payStartTime,
                PayEndDate = this.payEndTime,
                ShippingStartDate = this.shippingStartTime,
                ShippingEndDate = this.shippingEndTime,
                SortBy = "OrderDate",
                SortOrder = Core.Enums.SortAction.Desc
            };
            DbQueryResult saleOrderLineItemsStatistics = SalesHelper.SearchSaleOrderStatisticsData(query);

            DataTable exportData = (DataTable)saleOrderLineItemsStatistics.Data;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>订单号</td>");
                builder.AppendLine("        <td>成交时间</td>");
                builder.AppendLine("        <td>付款时间</td>");
                builder.AppendLine("        <td>发货时间</td>");
                builder.AppendLine("        <td>来源店铺</td>");
                builder.AppendLine("        <td>来源店主</td>");
                builder.AppendLine("        <td>购买者</td>");
                builder.AppendLine("        <td>订单总额</td>");
                builder.AppendLine("        <td>优惠减免</td>");
                builder.AppendLine("        <td>红包抵扣</td>");
                builder.AppendLine("        <td>金贝抵扣</td>");
                builder.AppendLine("        <td>赠送店主金贝</td>");
                builder.AppendLine("        <td>赠送会员金贝</td>");
                builder.AppendLine("        <td>实收金额</td>");
                builder.AppendLine("        <td>订单类型</td>");
                builder.AppendLine("        <td>订单状态</td>");
                builder.AppendLine("        <td>成本金额</td>");
                builder.AppendLine("        <td>佣金</td>");
                builder.AppendLine("        <td>利润</td>");
                builder.AppendLine("        <td>毛利</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    decimal commission = (Decimal.Parse(row["FirstCommission"].ToString()) + Decimal.Parse(row["SecondCommission"].ToString()) + Decimal.Parse(row["ThirdCommission"].ToString()));
                    decimal amount = Decimal.Parse(row["Amount"].ToString());
                    decimal costPrice = Decimal.Parse(row["OrderCostPrice"].ToString());
                    decimal profit = amount - costPrice;
                    decimal maoProfit = profit - commission;

                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["PayDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ShippingDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreUserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["MemberUserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + amount + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["VirtualPointAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreGiftMoney"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["MemberGiftMoney"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTypeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderStatusName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + costPrice + "</td>");
                    builder.AppendLine("        <td>" + commission + "</td>");
                    builder.AppendLine("        <td>" + profit + "</td>");
                    builder.AppendLine("        <td>" + maoProfit + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleOrderStatistics_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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
