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
namespace Hidistro.UI.Web.Admin.distributor
{
    public class BalanceDrawApplyList : AdminPage
    {
        protected System.Web.UI.WebControls.Button btapply;
        protected System.Web.UI.WebControls.Button btnSearchButton;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdapplyid;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdreferralblance;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hduserid;
        protected Pager pager;
        protected System.Web.UI.WebControls.Repeater reBalanceDrawRequest;
        private string RequestEndTime = "";
        private string RequestStartTime = "";
        private string StoreName = "";
        protected System.Web.UI.HtmlControls.HtmlTextArea txtcontent;
        protected WebCalendar txtRequestEndTime;
        protected WebCalendar txtRequestStartTime;
        protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected LinkButton btnCreateReport;
        protected LinkButton btnBatchApply;

        private void BindData()
        {
            BalanceDrawRequestQuery entity = new BalanceDrawRequestQuery
            {
                RequestTime = "",
                CheckTime = "",
                StoreName = this.StoreName,
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortOrder = SortAction.Desc,
                SortBy = "SerialID",
                RequestEndTime = this.RequestEndTime,
                RequestStartTime = this.RequestStartTime,
                IsCheck = "0",
                UserId = ""
            };
            Globals.EntityCoding(entity, true);
            DbQueryResult balanceDrawRequest = DistributorsBrower.GetBalanceDrawRequest(entity);
            this.reBalanceDrawRequest.DataSource = balanceDrawRequest.Data;
            this.reBalanceDrawRequest.DataBind();
            this.pager.TotalRecords = balanceDrawRequest.TotalRecords;
        }
        private void btnApply_Click(object sender, System.EventArgs e)
        {
            int id = int.Parse(this.hdapplyid.Value);
            string remark = this.txtcontent.Value;
            int userId = int.Parse(this.hduserid.Value);
            decimal referralRequestBalance = decimal.Parse(this.hdreferralblance.Value);
            if (VShopHelper.UpdateBalanceDrawRequest(id, remark))
            {
                if (VShopHelper.UpdateBalanceDistributors(userId, referralRequestBalance))
                {
                    this.ShowMsg("结算成功", true);
                    this.BindData();
                }
                else
                {
                    this.ShowMsg("结算失败", false);
                }
            }
            else
            {
                this.ShowMsg("结算失败", false);
            }
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
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RequestEndTime"]))
                {
                    this.RequestEndTime = base.Server.UrlDecode(this.Page.Request.QueryString["RequestEndTime"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RequestStartTime"]))
                {
                    this.RequestStartTime = base.Server.UrlDecode(this.Page.Request.QueryString["RequestStartTime"]);
                }
                this.txtStoreName.Text = this.StoreName;
                this.txtRequestStartTime.Text = this.RequestStartTime;
                this.txtRequestEndTime.Text = this.RequestEndTime;
            }
            else
            {
                this.StoreName = this.txtStoreName.Text;
                this.RequestStartTime = this.txtRequestStartTime.Text;
                this.RequestEndTime = this.txtRequestEndTime.Text;
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.btapply.Click += new System.EventHandler(this.btnApply_Click);
            this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);
            this.btnBatchApply.Click += new EventHandler(this.btnBatchApply_Click);
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
            queryStrings.Add("RequestStartTime", this.txtRequestStartTime.Text);
            queryStrings.Add("RequestEndTime", this.txtRequestEndTime.Text);
            queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (!isSearch)
            {
                queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            base.ReloadPage(queryStrings);
        }


        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int num = 0;
            int.TryParse(e.CommandArgument.ToString(), out num);
            if (num > 0)
            {
                string commandName = e.CommandName;
                string str = commandName;
                if (commandName != null)
                {
                    if (str == "del")
                    {
                        bool deleteFlag = VShopHelper.DeleteBalanceDrawRequest(num);
                        if (deleteFlag)
                        {
                            this.ShowMsg("删除成功", true);
                            this.BindData();
                        }
                        else
                        {
                            this.ShowMsg("删除失败", false);
                        }
                    }
                    else
                    {
                        if (str != "sendredpack")
                        {
                            return;
                        }
                        string balanceDrawRequest = DistributorsBrower.SendRedPackToBalanceDrawRequest(num);
                        string str1 = balanceDrawRequest;
                        string str2 = str1;
                        if (str1 != null)
                        {
                            if (str2 == "-1")
                            {
                                base.Response.Redirect(string.Concat("SendRedpackRecord.aspx?serialid=", num));
                                base.Response.End();
                                return;
                            }
                            if (str2 == "1")
                            {
                                base.Response.Redirect(string.Concat("SendRedpackRecord.aspx?serialid=", num));
                                base.Response.End();
                                return;
                            }
                        }
                        this.ShowMsg(string.Concat("生成红包失败，原因是：", balanceDrawRequest), false);
                    }
                }
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                LinkButton linkButton = (LinkButton)e.Item.FindControl("lkBtnSendRedPack");
                int num = int.Parse(((DataRowView)e.Item.DataItem).Row["serialid"].ToString());
                if (int.Parse(((DataRowView)e.Item.DataItem).Row["RedpackRecordNum"].ToString()) > 0)
                {
                    linkButton.PostBackUrl = string.Concat("SendRedpackRecord.aspx?serialid=", num);
                    linkButton.Text = "查看微信红包";
                    return;
                }
                int num1 = int.Parse(((DataRowView)e.Item.DataItem).Row["RequestType"].ToString());
                linkButton.OnClientClick = "return confirm('提现金额将会拆分为最大金额为200元的微信红包，等待发送！\n确定生成微信红包吗？')";
                if (num1 == 0)
                {
                    linkButton.Style.Add("color", "red");
                }
            }
        }

        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {
            string requestTypeName = "";
            StringBuilder builder = new StringBuilder();

            DataTable exportData = DistributorsBrower.GetBalanceDrawRequestByExport(this.StoreName, this.RequestStartTime, this.RequestEndTime);

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>店铺ID</td>");
                builder.AppendLine("        <td>店铺名称</td>");
                builder.AppendLine("        <td>申请提现时间</td>");
                builder.AppendLine("        <td>申请提现金额</td>");
                builder.AppendLine("        <td>可提现金额</td>");
                builder.AppendLine("        <td>申请人手机号码</td>");
                builder.AppendLine("        <td>提现方式</td>");
                builder.AppendLine("        <td>提现账号</td>");
                builder.AppendLine("        <td>提现银行</td>");
                builder.AppendLine("        <td>提现银行开户地址</td>");
                builder.AppendLine("        <td>提现开户行</td>");
                builder.AppendLine("        <td>持卡人姓名</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["UserId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RequestTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["ReferralBlance"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CellPhone"].ToString() + "</td>");
                    if (row["RequestType"].ToString().EqualIgnoreCase("0"))
                    {
                        requestTypeName = "微信支付";
                    }
                    else
                    {
                        requestTypeName = "银行支付";
                    }
                    builder.AppendLine("        <td>" + requestTypeName + "</td>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["MerchantCode"].ToString() + "　" + "</td>");
                    builder.AppendLine("        <td>" + row["BankName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RegionAddress"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["BankAddress"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["AccountName"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=ReferralBlanceData_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                this.Page.Response.ContentType = "application/ms-excel";
                this.Page.EnableViewState = false;
                this.Page.Response.Write(builder.ToString());
                this.Page.Response.End();

            }
            else
            {
                this.ShowMsg("没有提现数据", true);
            }
        }
        
        private void btnBatchApply_Click(object sender, EventArgs e)
        {
            int num;
            string item = "";
            if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
            {
                item = base.Request["CheckBoxGroup"];
            }
            if (item.Length <= 0)
            {
                this.ShowMsg("请选择要批量线下打款的申请单", false);
                return;
            }

            int id = 0;
            string remark = "";
            int userId = 0;
            decimal referralRequestBalance = 0;
            string storeName = "";

            StringBuilder errStoreName = new StringBuilder();
            
            DataTable dt = DistributorsBrower.GetBalanceDrawRequestByBatch(item);
            if (null != dt && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    id = int.Parse(dr["SerialID"].ToString());
                    userId = int.Parse(dr["UserId"].ToString());
                    referralRequestBalance = decimal.Parse(dr["Amount"].ToString());
                    storeName = dr["StoreName"].ToString();

                    XTrace.WriteLine("提现结算申请后台处理：－－SerialID：" + id + "－－UserId：" + userId + "－－Amount：" + referralRequestBalance);

                    if (VShopHelper.UpdateBalanceDrawRequest(id, remark))
                    {
                        if (VShopHelper.UpdateBalanceDistributors(userId, referralRequestBalance))
                        {
                            // 
                        }
                        else
                        {
                            errStoreName.Append(StoreName + ",");
                        }
                    }
                    else
                    {
                        errStoreName.Append(StoreName + ",");
                    }
                }
                if (errStoreName.Length > 0)
                {
                    this.ShowMsg(string.Format("以下店铺结算失败：{0}", errStoreName), false);
                }
                else
                {
                    this.ShowMsg("结算成功", true);
                }
                this.BindData();
            }
            
        }
    
    }
}
