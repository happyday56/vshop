using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Orders;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Hidistro.ControlPanel.Sales;
using System.Text;
using NewLife.Log;
using System.Collections;
using System.Collections.Generic;
using Hidistro.Entities.Members;
namespace Hidistro.UI.Web.Admin.distributor
{
    public class DistributorIncomeList : AdminPage
    {
        
        protected System.Web.UI.WebControls.Button btnSearchButton;
        protected Pager pager;
        protected System.Web.UI.WebControls.Repeater reDistIncome;
        private string StoreName = "";
        protected System.Web.UI.WebControls.TextBox txtStoreName;
        private string CellPhone = "";
        protected TextBox txtCellPhone;
        protected LinkButton btnCreateReport;

        private void BindData()
        {
            DistributorIncomeQuery entity = new DistributorIncomeQuery
            {
                StoreName = this.StoreName,
                CellPhone = this.CellPhone,
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortOrder = SortAction.Asc,
                SortBy = "UserId"
            };
            Globals.EntityCoding(entity, true);
            DbQueryResult balanceDrawRequest = DistributorsBrower.GetDistributorIncome(entity);
            this.reDistIncome.DataSource = balanceDrawRequest.Data;
            this.reDistIncome.DataBind();
            this.pager.TotalRecords = balanceDrawRequest.TotalRecords;
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
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CellPhone"]))
                {
                    this.CellPhone = base.Server.UrlDecode(this.Page.Request.QueryString["CellPhone"]);
                }
                this.txtStoreName.Text = this.StoreName;
                this.txtCellPhone.Text = this.CellPhone;
            }
            else
            {
                this.StoreName = this.txtStoreName.Text;
                this.CellPhone = this.txtCellPhone.Text;
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
            this.LoadParameters();
            if (!base.IsPostBack)
            {
                this.BindData();
            }
        }
        private void ReBind(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();
            queryStrings.Add("StoreName", this.txtStoreName.Text);
            queryStrings.Add("CellPhone", this.txtCellPhone.Text);
            queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (!isSearch)
            {
                queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            base.ReloadPage(queryStrings);
        }
               

        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {

            StringBuilder builder = new StringBuilder();

            DistributorIncomeQuery entity = new DistributorIncomeQuery
            {
                StoreName = this.StoreName,
                CellPhone = this.CellPhone,
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortOrder = SortAction.Desc,
                SortBy = "UserId"
            };
            Globals.EntityCoding(entity, true);

            DataTable exportData = DistributorsBrower.GetDistributorIncomeByExport(entity);

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>店铺名称</td>");
                builder.AppendLine("        <td>店主名称</td>");
                builder.AppendLine("        <td>真实姓名</td>");
                builder.AppendLine("        <td>店主ID</td>");
                builder.AppendLine("        <td>电话号码</td>");
                builder.AppendLine("        <td>累计收益</td>");
                builder.AppendLine("        <td>不可提现</td>");
                builder.AppendLine("        <td>可提现</td>");
                builder.AppendLine("        <td>可提现未提现</td>");
                builder.AppendLine("        <td>已提现待发放</td>");
                builder.AppendLine("        <td>已发放收益</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RealName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CellPhone"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["LJSY"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["BKTX"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["KTX"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["KTXWTX"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["YTXDFF"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["YFFSY"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=DistributorIncome" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                this.Page.Response.ContentType = "application/ms-excel";
                this.Page.EnableViewState = false;
                this.Page.Response.Write(builder.ToString());
                this.Page.Response.End();

            }
            else
            {
                this.ShowMsg("没有佣金数据", true);
            }
        }
        
    
    }
}
