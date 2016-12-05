using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Orders;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class ManageSendNote : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearchButton;
		protected System.Web.UI.WebControls.DataList dlstSendNote;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderId;
		protected PageSize hrefPageSize;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected ImageLinkButton lkbtnDeleteCheck;
		protected Pager pager;
		protected Pager pager1;
		protected System.Web.UI.WebControls.TextBox txtOrderId;
        protected LinkButton btnCreateReport;
        protected WebCalendar txtEndTime;
        protected WebCalendar txtStartTime;
        protected TextBox txtExpressNo;
        protected TextBox txtShipTo;

		private void BindSendNote()
		{
			RefundApplyQuery refundApplyQuery = this.GetRefundApplyQuery();
			DbQueryResult allSendNote = OrderHelper.GetAllSendNote(refundApplyQuery);
			this.dlstSendNote.DataSource = allSendNote.Data;
			this.dlstSendNote.DataBind();
			this.pager.TotalRecords = allSendNote.TotalRecords;
			this.pager1.TotalRecords = allSendNote.TotalRecords;
			this.txtOrderId.Text = refundApplyQuery.OrderId;
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.ReloadSendNotes(true);
		}

		private RefundApplyQuery GetRefundApplyQuery(bool isExport = false)
		{
			RefundApplyQuery query = new RefundApplyQuery();
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
			{
				query.OrderId = Globals.UrlDecode(this.Page.Request.QueryString["OrderId"]);
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ExpressNo"]))
            {
                query.ExpressNo = Globals.UrlDecode(this.Page.Request.QueryString["ExpressNo"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StartTime"]))
            {
                query.StartTime = base.Server.UrlDecode(this.Page.Request.QueryString["StartTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
            {
                query.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["EndTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ShipTo"]))
            {
                query.ShipTo = base.Server.UrlDecode(this.Page.Request.QueryString["ShipTo"]);
            }

            if (isExport)
            {
                query.PageIndex = 1;
                query.PageSize = 20000;
            }
            else
            {
                query.PageIndex = this.pager.PageIndex;
                query.PageSize = this.pager.PageSize;
            }			
			query.SortBy = "ShippingDate";
			query.SortOrder = SortAction.Desc;

            this.txtOrderId.Text = query.OrderId;
            this.txtExpressNo.Text = query.ExpressNo;
            this.txtStartTime.Text = query.StartTime;
            this.txtEndTime.Text = query.EndTime;
            this.txtShipTo.Text = query.ShipTo;

			return query;
		}
		private void lkbtnDeleteCheck_Click(object sender, System.EventArgs e)
		{
			string str = "";
			if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
			{
				str = base.Request["CheckBoxGroup"];
			}
			if (str.Length <= 0)
			{
				this.ShowMsg("请选要删除的发货单", false);
			}
			else
			{
				int num;
				OrderHelper.DelSendNote(str.Split(new char[]
				{
					','
				}), out num);
				this.BindSendNote();
				this.ShowMsg(string.Format("成功删除了{0}个发货单", num), true);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			this.lkbtnDeleteCheck.Click += new System.EventHandler(this.lkbtnDeleteCheck_Click);
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			if (!base.IsPostBack)
			{
				this.BindSendNote();
			}
		}
        
		private void ReloadSendNotes(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("OrderId", this.txtOrderId.Text);
            queryStrings.Add("ExpressNo", this.txtExpressNo.Text);
            queryStrings.Add("StartTime", this.txtStartTime.Text);
            queryStrings.Add("EndTime", this.txtEndTime.Text);
            queryStrings.Add("ShipTo", this.txtShipTo.Text);
			queryStrings.Add("PageSize", this.pager.PageSize.ToString());
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["GroupBuyId"]))
			{
				queryStrings.Add("GroupBuyId", this.Page.Request.QueryString["GroupBuyId"]);
			}
			base.ReloadPage(queryStrings);
		}

        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {
            RefundApplyQuery refundApplyQuery = this.GetRefundApplyQuery(true);

            StringBuilder builder = new StringBuilder();

            DbQueryResult allSendNote = OrderHelper.GetAllSendNote(refundApplyQuery);
            DataTable exportData = null;

            if (null != allSendNote)
            {
                exportData = (DataTable)allSendNote.Data;
            }

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>订单号</td>");
                builder.AppendLine("        <td>订单金额(元)</td>");
                builder.AppendLine("        <td>发货时间</td>");
                builder.AppendLine("        <td>会员昵称</td>");
                builder.AppendLine("        <td>物流公司</td>");
                builder.AppendLine("        <td>物流单号</td>");
                builder.AppendLine("        <td>收货人</td>");
                builder.AppendLine("        <td>联系电话</td>");
                builder.AppendLine("        <td>收货地址</td>");
                builder.AppendLine("        <td>操作员</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ShippingDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Username"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ExpressCompanyName"].ToString() + "</td>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["ShipOrderNumber"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ShipTo"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CellPhone"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["ShippingRegion"].ToString() + "&nbsp;" + row["Address"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Operator"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SendNoteData_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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
