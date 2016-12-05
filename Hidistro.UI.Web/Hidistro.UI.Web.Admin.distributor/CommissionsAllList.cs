using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class CommissionsAllList : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearchButton;
		private string EndTime = "";
		private string OrderId = "";
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater reCommissions;
		private string StartTime = "";
		private string StoreName = "";
		protected WebCalendar txtEndTime;
		protected System.Web.UI.WebControls.TextBox txtOrderId;
		protected WebCalendar txtStartTime;
		protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected LinkButton btnCreateReport;
        protected TextBox txtUserName;
        protected TextBox txtOneStoreName;
        private string UserNamme = "";
        private string OneStoreName = "";
        protected DropDownList ddlCommType;
        protected DropDownList ddlOrderType;
        protected DropDownList ddlIncomeType;
        private int? commTypeId;
        private int? orderTypeId;
        private int? incomeTypeId;

		private void BindData()
		{
			CommissionsQuery entity = new CommissionsQuery
			{
				EndTime = this.EndTime,
				StartTime = this.StartTime,
				StoreName = this.StoreName,
				OrderNum = this.OrderId,
                UserName = this.UserNamme,
                OneStoreName = this.OneStoreName,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "CommId",
                CommTypeId = this.commTypeId,
                OrderTypeId = this.orderTypeId,
                IncomeTypeId = this.incomeTypeId
			};
			Globals.EntityCoding(entity, true);
			DbQueryResult commissions = VShopHelper.GetCommissions(entity);
			this.reCommissions.DataSource = commissions.Data;
			this.reCommissions.DataBind();
			this.pager.TotalRecords = commissions.TotalRecords;
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.ReBind(true);
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StoreName"]))
				{
					this.StoreName = base.Server.UrlDecode(this.Page.Request.QueryString["StoreName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
				{
					this.OrderId = base.Server.UrlDecode(this.Page.Request.QueryString["OrderId"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StartTime"]))
				{
					this.StartTime = base.Server.UrlDecode(this.Page.Request.QueryString["StartTime"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
				{
					this.EndTime = base.Server.UrlDecode(this.Page.Request.QueryString["EndTime"]);
				}
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["UserName"]))
                {
                    this.UserNamme = base.Server.UrlDecode(this.Page.Request.QueryString["UserName"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OneStoreName"]))
                {
                    this.OneStoreName = base.Server.UrlDecode(this.Page.Request.QueryString["OneStoreName"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CommType"]))
                {
                    int num = 0;
                    if (int.TryParse(this.Page.Request.QueryString["CommType"], out num))
                    {
                        this.commTypeId = num;
                    }
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderType"]))
                {
                    int num1 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["OrderType"], out num1))
                    {
                        this.orderTypeId = num1;
                    }
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["IncomeType"]))
                {
                    int num2 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["IncomeType"], out num2))
                    {
                        this.incomeTypeId = num2;
                    }
                }
				this.txtStoreName.Text = this.StoreName;
				this.txtOrderId.Text = this.OrderId;
				this.txtStartTime.Text = this.StartTime;
				this.txtEndTime.Text = this.EndTime;
                this.txtUserName.Text = this.UserNamme;
                this.txtOneStoreName.Text = this.OneStoreName;
                
			}
			else
			{
				this.OrderId = this.txtOrderId.Text;
				this.StoreName = this.txtStoreName.Text;
				this.StartTime = this.txtStartTime.Text;
				this.EndTime = this.txtEndTime.Text;
                this.UserNamme = this.txtUserName.Text;
                this.OneStoreName = this.txtOneStoreName.Text;
                if (!string.IsNullOrEmpty(this.ddlCommType.SelectedValue))
                {
                    this.commTypeId = int.Parse( this.ddlCommType.SelectedValue );
                    
                }
                if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
                {
                    this.orderTypeId = int.Parse(this.ddlOrderType.SelectedValue);
                   
                }
                if (!string.IsNullOrEmpty(this.ddlIncomeType.SelectedValue))
                {
                    this.incomeTypeId = int.Parse(this.ddlIncomeType.SelectedValue);
                    
                }
			}

            if (this.commTypeId.HasValue)
            {
                this.ddlCommType.SelectedValue = this.commTypeId.Value.ToString();
            }
            if (this.orderTypeId.HasValue)
            {
                this.ddlOrderType.SelectedValue = this.orderTypeId.Value.ToString();
            }
            if (this.incomeTypeId.HasValue)
            {
                this.ddlIncomeType.SelectedValue = this.incomeTypeId.Value.ToString();
            }
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
			this.LoadParameters();
			if (!base.IsPostBack)
			{
                this.ddlOrderType.Items.Clear();
                this.ddlOrderType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlOrderType.Items.Add(new ListItem("普通订单", "1"));
                this.ddlOrderType.Items.Add(new ListItem("开店订单", "2"));

                this.ddlCommType.Items.Clear();
                this.ddlCommType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlCommType.Items.Add(new ListItem("销售佣金", "1"));
                this.ddlCommType.Items.Add(new ListItem("管理佣金", "2"));
                this.ddlCommType.Items.Add(new ListItem("推荐佣金", "2"));

                this.ddlIncomeType.Items.Clear();
                this.ddlIncomeType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlIncomeType.Items.Add(new ListItem("收入", "1"));
                this.ddlIncomeType.Items.Add(new ListItem("扣减", "0"));

                if (this.commTypeId.HasValue)
                {
                    this.ddlCommType.SelectedValue = this.commTypeId.Value.ToString();
                }
                if (this.orderTypeId.HasValue)
                {
                    this.ddlOrderType.SelectedValue = this.orderTypeId.Value.ToString();
                }
                if (this.incomeTypeId.HasValue)
                {
                    this.ddlIncomeType.SelectedValue = this.incomeTypeId.Value.ToString();
                }

				this.BindData();

			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("StoreName", this.txtStoreName.Text);
			queryStrings.Add("OrderId", this.txtOrderId.Text);
			queryStrings.Add("StartTime", this.txtStartTime.Text);
			queryStrings.Add("EndTime", this.txtEndTime.Text);
            queryStrings.Add("UserName", this.txtUserName.Text);
            queryStrings.Add("OneStoreName", this.txtOneStoreName.Text);
            if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
            {
                queryStrings.Add("OrderType", this.ddlOrderType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.ddlCommType.SelectedValue))
            {
                queryStrings.Add("CommType", this.ddlCommType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.ddlIncomeType.SelectedValue))
            {
                queryStrings.Add("IncomeType", this.ddlIncomeType.SelectedValue);
            }

			queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}

        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {
            LoadParameters();

            StringBuilder builder = new StringBuilder();

            CommissionsQuery entity = new CommissionsQuery
            {
                EndTime = this.EndTime,
                StartTime = this.StartTime,
                StoreName = this.StoreName,
                OrderNum = this.OrderId,
                UserName = this.UserNamme,
                OneStoreName = this.OneStoreName,
                PageIndex = 1,
                PageSize = 60000,
                SortOrder = SortAction.Desc,
                SortBy = "CommId",
                CommTypeId = this.commTypeId,
                OrderTypeId = this.orderTypeId,
                IncomeTypeId = this.incomeTypeId
            };
            Globals.EntityCoding(entity, true);
            DbQueryResult commissions = VShopHelper.GetCommissions(entity);

            //DataTable exportData = DistributorsBrower.GetCommissionByExport(this.StoreName, this.UserNamme, this.OneStoreName, this.OrderId, this.StartTime, this.EndTime);
            DataTable exportData = (DataTable)commissions.Data;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>订单号</td>");
                builder.AppendLine("        <td>佣金时间</td>");
                builder.AppendLine("        <td>来源店铺</td>");
                builder.AppendLine("        <td>分佣者ID</td>");
                builder.AppendLine("        <td>分佣者</td>");
                builder.AppendLine("        <td>分佣者店铺</td>");
                builder.AppendLine("        <td>分佣者等级</td>");
                builder.AppendLine("        <td>订单金额(元)</td>");
                builder.AppendLine("        <td>订单实收金额(元)</td>");
                builder.AppendLine("        <td>佣金比率</td>");
                builder.AppendLine("        <td>佣金(元)</td>");
                builder.AppendLine("        <td>佣金类型</td>");
                builder.AppendLine("        <td>订单类型</td>");
                builder.AppendLine("        <td>收支类型</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["TradeTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OneUserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OneStoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OneGradeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Rate"].ToString() + "%</td>");
                    builder.AppendLine("        <td>" + row["CommTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CommTypeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTypeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["IncomeTypeName"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=CommissionData_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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
