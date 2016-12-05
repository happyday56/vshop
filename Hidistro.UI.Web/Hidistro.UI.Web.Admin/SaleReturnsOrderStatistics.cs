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
    public class SaleReturnsOrderStatistics : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected WebCalendar calendarEnd;
		protected WebCalendar calendarStart;
		private System.DateTime? endTime;
		protected System.Web.UI.WebControls.GridView grdOrderLineItem;
		protected Pager pager;
		private System.DateTime? startTime;
        protected DropDownList ddlOrderType;
        protected DropDownList ddlOrderStatus;
        protected System.Web.UI.WebControls.TextBox txtOrderId;
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
				StartDate = this.startTime,
				EndDate = this.endTime,
                OrderId = this.OrderId,
                OrderTypeId = this.orderTypeId,
                ReturnStatus = 1,
                OrderStatus = this.orderStatusId
                
			};
            DbQueryResult saleOrderLineItemsStatistics = SalesHelper.SearchSaleReturnsOrderStatisticsData(query);
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
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["startTime"]))
				{
					this.startTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["startTime"]));
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endTime"]))
				{
					this.endTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["endTime"]));
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
				this.calendarStart.SelectedDate = this.startTime;
				this.calendarEnd.SelectedDate = this.endTime;
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
				this.startTime = this.calendarStart.SelectedDate;
				this.endTime = this.calendarEnd.SelectedDate;
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
			queryStrings.Add("startTime", this.calendarStart.SelectedDate.ToString());
			queryStrings.Add("endTime", this.calendarEnd.SelectedDate.ToString());
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
                StartDate = this.startTime,
                EndDate = this.endTime,
                OrderId = this.OrderId,
                OrderTypeId = this.orderTypeId,
                ReturnStatus = 1,
                OrderStatus = this.orderStatusId
            };
            DbQueryResult saleOrderLineItemsStatistics = SalesHelper.SearchSaleReturnsOrderStatisticsData(query);

            DataTable exportData = (DataTable)saleOrderLineItemsStatistics.Data;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>退货时间</td>");
                builder.AppendLine("        <td>订单号</td>");
                builder.AppendLine("        <td>订单总额</td>");
                builder.AppendLine("        <td>优惠减免</td>");
                builder.AppendLine("        <td>红包抵扣</td>");
                builder.AppendLine("        <td>金贝抵扣</td>");
                builder.AppendLine("        <td>实收金额</td>");
                builder.AppendLine("        <td>实退金额</td>");
                builder.AppendLine("        <td>退货审核人</td>");
                builder.AppendLine("        <td>退货验收状态</td>");
                builder.AppendLine("        <td>退货验收人</td>");
                builder.AppendLine("        <td>退货验收时间</td>");
                builder.AppendLine("        <td>订单类型</td>");
                builder.AppendLine("        <td>订单状态</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["ApplyForTime"].ToString() + "</td>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["VirtualPointAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RefundMoney"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OperatorName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["HandleStatusName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OperatorName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["AuditTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTypeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderStatusName"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleReturnsOrderStatistics_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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
