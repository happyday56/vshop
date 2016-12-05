using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.SaleDetails)]
    public class DistributorSaleStatistics : AdminPage
    {
        protected LinkButton btnCreateReportOfDay;
        protected LinkButton btnCreateReportOfMonth;
        protected LinkButton btnCreateReportOfYear;
        protected Button btnQueryDaySaleTotal;
        protected Button btnQueryMonthSaleTotal;
        protected Button btnQueryYearSaleTotal;
        protected Button btnSearch;
        private int dayMonth = System.DateTime.Now.Month;
        private SaleStatisticsType dayType = SaleStatisticsType.SaleCounts;
        private int dayYear = System.DateTime.Now.Year;
        protected YearDropDownList dropDayForYear;
        protected YearDropDownList dropMonthForYaer;
        protected YearDropDownList dropYearForYear;
        protected MonthDropDownList dropMoth;
        protected GridView grdDaySaleTotalStatistics;
        protected Grid grdMonthSaleTotalStatistics;
        protected Grid grdYearSaleTotalStatistics;
        private SaleStatisticsType monthType = SaleStatisticsType.SaleCounts;
        private int monthYear = System.DateTime.Now.Year;
        private int year = DateTime.Now.Year;
        protected DistributorGradeDropDownList DrGrade;
        private int? Grade = 0;
        protected TextBox txtStoreName;
        protected TextBox txtUserName;
        private string storeName;
        private string userName;
        protected WebCalendar calendarEnd;
        protected WebCalendar calendarStart;
        private DateTime? startTime;
        private DateTime? endTime;


        public System.Data.DataTable TableOfDay
        {
            get
            {
                System.Data.DataTable result;
                if (this.ViewState["TableOfDay"] != null)
                {
                    result = (System.Data.DataTable)this.ViewState["TableOfDay"];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this.ViewState["TableOfDay"] = value;
            }
        }
        public System.Data.DataTable TableOfMonth
        {
            get
            {
                System.Data.DataTable result;
                if (this.ViewState["TableOfMonth"] != null)
                {
                    result = (System.Data.DataTable)this.ViewState["TableOfMonth"];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this.ViewState["TableOfMonth"] = value;
            }
        }
        public DataTable TableOfYear
        {
            get
            {
                DataTable result;
                if (null != this.ViewState["TableOfYear"])
                {
                    result = (DataTable)this.ViewState["TableOfYear"];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this.ViewState["TableOfYear"] = value;
            }
        }

        private void BindDaySaleTotalStatistics()
        {
            SearchStatisticsQuery query = new SearchStatisticsQuery
            {
                SearchType = 1,
                Year = this.dayYear,
                Month = this.dayMonth,
                GradeId = this.Grade,
                StoreName = this.storeName,
                UserName = this.userName,
                StartDate = this.startTime,
                EndDate = this.endTime
            };
            DataTable dbQueryResult = SalesHelper.SearchDistributorSaleStatisticsData(query);

            this.grdDaySaleTotalStatistics.DataSource = dbQueryResult;
            this.grdDaySaleTotalStatistics.DataBind();
            this.TableOfDay = dbQueryResult;

        }
        private void BindMonthSaleTotalStatistics()
        {
            SearchStatisticsQuery query = new SearchStatisticsQuery
            {
                SearchType = 2,
                Year = this.monthYear,
                GradeId = this.Grade,
                StoreName = this.storeName,
                UserName = this.userName,
                StartDate = this.startTime,
                EndDate = this.endTime
            };
            DataTable dbQueryResult = SalesHelper.SearchDistributorSaleStatisticsData(query);

            this.grdMonthSaleTotalStatistics.DataSource = dbQueryResult;
            this.grdMonthSaleTotalStatistics.DataBind();
            this.TableOfMonth = dbQueryResult;

        }

        private void BindYearSaleTotalStatistics()
        {
            SearchStatisticsQuery query = new SearchStatisticsQuery
            {
                SearchType = 3,
                Year = this.year,
                GradeId = this.Grade,
                StoreName = this.storeName,
                UserName = this.userName,
                StartDate = this.startTime,
                EndDate = this.endTime
            };
            DataTable dbQueryResult = SalesHelper.SearchDistributorSaleStatisticsData(query);

            this.grdYearSaleTotalStatistics.DataSource = dbQueryResult;
            this.grdYearSaleTotalStatistics.DataBind();
            this.TableOfYear = dbQueryResult;

        }

        private void btnCreateReportOfDay_Click(object sender, System.EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            DataTable exportData = this.TableOfDay;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>日期</td>");
                builder.AppendLine("        <td>店铺名称</td>");
                builder.AppendLine("        <td>店主名称</td>");
                builder.AppendLine("        <td>店主等级</td>");
                builder.AppendLine("        <td>销售金额</td>");
                builder.AppendLine("        <td>优惠金额</td>");
                builder.AppendLine("        <td>金币抵扣</td>");
                builder.AppendLine("        <td>实际付款金额</td>");
                builder.AppendLine("        <td>成本金额</td>");
                builder.AppendLine("        <td>利润</td>");
                builder.AppendLine("        <td>佣金</td>");
                builder.AppendLine("        <td>销售佣金</td>");
                builder.AppendLine("        <td>推荐佣金</td>");
                builder.AppendLine("        <td>毛利</td>");
                builder.AppendLine("        <td>新增店铺数</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["OrderDate"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DistributorGradeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderCostPrice"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CommTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["NormalIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RecommendIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["GrossProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreCnt"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleDataByDay" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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
        private void btnCreateReportOfMonth_Click(object sender, System.EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            DataTable exportData = this.TableOfMonth;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>年份</td>");
                builder.AppendLine("        <td>月份</td>");
                builder.AppendLine("        <td>店铺名称</td>");
                builder.AppendLine("        <td>店主名称</td>");
                builder.AppendLine("        <td>店主等级</td>");
                builder.AppendLine("        <td>销售金额</td>");
                builder.AppendLine("        <td>优惠金额</td>");
                builder.AppendLine("        <td>金币抵扣</td>");
                builder.AppendLine("        <td>实际付款金额</td>");
                builder.AppendLine("        <td>成本金额</td>");
                builder.AppendLine("        <td>利润</td>");
                builder.AppendLine("        <td>佣金</td>");
                builder.AppendLine("        <td>销售佣金</td>");
                builder.AppendLine("        <td>推荐佣金</td>");
                builder.AppendLine("        <td>毛利</td>");
                builder.AppendLine("        <td>新增店铺数</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["OrderYear"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderMonth"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DistributorGradeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderCostPrice"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CommTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["NormalIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RecommendIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["GrossProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreCnt"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleDataByMonth" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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

        private void btnCreateReportOfYear_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            DataTable exportData = this.TableOfYear;

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>年份</td>");
                builder.AppendLine("        <td>店铺名称</td>");
                builder.AppendLine("        <td>店主名称</td>");
                builder.AppendLine("        <td>店主等级</td>");
                builder.AppendLine("        <td>销售金额</td>");
                builder.AppendLine("        <td>优惠金额</td>");
                builder.AppendLine("        <td>金币抵扣</td>");
                builder.AppendLine("        <td>实际付款金额</td>");
                builder.AppendLine("        <td>成本金额</td>");
                builder.AppendLine("        <td>利润</td>");
                builder.AppendLine("        <td>佣金</td>");
                builder.AppendLine("        <td>销售佣金</td>");
                builder.AppendLine("        <td>推荐佣金</td>");
                builder.AppendLine("        <td>毛利</td>");
                builder.AppendLine("        <td>新增店铺数</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td>" + row["OrderYear"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["UserName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DistributorGradeName"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Amount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["DiscountAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RedPagerAmount"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderCostPrice"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["CommTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["NormalIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RecommendIncome"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["GrossProfit"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["StoreCnt"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleDataByYear" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
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

        private void btnQueryDaySaleTotal_Click(object sender, System.EventArgs e)
        {
            this.ReBind();
        }
        private void btnQueryMonthSaleTotal_Click(object sender, System.EventArgs e)
        {
            this.ReBind();
        }

        private void btnQueryYearSaleTotal_Click(object sender, EventArgs e)
        {
            this.ReBind();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.ReBind();
        }

        private void LoadParameters()
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Year"]))
                {
                    int.TryParse(this.Page.Request.QueryString["Year"], out this.year);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["monthYear"]))
                {
                    int.TryParse(this.Page.Request.QueryString["monthYear"], out this.monthYear);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["monthType"]))
                {
                    this.monthType = (SaleStatisticsType)System.Convert.ToInt32(this.Page.Request.QueryString["monthType"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["dayYear"]))
                {
                    int.TryParse(this.Page.Request.QueryString["dayYear"], out this.dayYear);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["dayMonth"]))
                {
                    int.TryParse(this.Page.Request.QueryString["dayMonth"], out this.dayMonth);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["dayType"]))
                {
                    this.dayType = (SaleStatisticsType)System.Convert.ToInt32(this.Page.Request.QueryString["dayType"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Grade"]))
                {
                    int numGrade = 0;
                    int.TryParse(this.Page.Request.QueryString["Grade"], out numGrade);
                    if (numGrade > 0)
                    {
                        this.Grade = numGrade;
                    }
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StoreName"]))
                {
                    this.storeName = base.Server.UrlDecode(this.Page.Request.QueryString["StoreName"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["UserName"]))
                {
                    this.userName = base.Server.UrlDecode(this.Page.Request.QueryString["UserName"]);
                }

                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["startTime"]))
                {
                    this.startTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["startTime"]));
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endTime"]))
                {
                    this.endTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["endTime"]));
                }

                this.dropYearForYear.SelectedValue = this.year;
                this.dropMonthForYaer.SelectedValue = this.monthYear;
                this.dropDayForYear.SelectedValue = this.dayYear;
                this.dropMoth.SelectedValue = this.dayMonth;

                this.DrGrade.DataBind();
                this.DrGrade.SelectedValue = this.Grade;
                this.txtStoreName.Text = this.storeName;
                this.txtUserName.Text = this.userName;
                this.calendarStart.SelectedDate = this.startTime;
                this.calendarEnd.SelectedDate = this.endTime;

            }
            else
            {
                this.year = this.dropYearForYear.SelectedValue;
                this.monthYear = this.dropMonthForYaer.SelectedValue;
                this.dayYear = this.dropDayForYear.SelectedValue;
                this.dayMonth = this.dropMoth.SelectedValue;

                this.storeName = this.txtStoreName.Text;
                this.Grade = this.DrGrade.SelectedValue;
                this.userName = this.txtUserName.Text;
                this.startTime = this.calendarStart.SelectedDate;
                this.endTime = this.calendarEnd.SelectedDate;
            }
        }
        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            this.btnQueryYearSaleTotal.Click += new EventHandler(this.btnQueryYearSaleTotal_Click);
            this.btnQueryMonthSaleTotal.Click += new System.EventHandler(this.btnQueryMonthSaleTotal_Click);
            this.btnCreateReportOfMonth.Click += new System.EventHandler(this.btnCreateReportOfMonth_Click);
            this.btnQueryDaySaleTotal.Click += new System.EventHandler(this.btnQueryDaySaleTotal_Click);
            this.btnCreateReportOfDay.Click += new System.EventHandler(this.btnCreateReportOfDay_Click);
            this.btnCreateReportOfYear.Click += new EventHandler(this.btnCreateReportOfYear_Click);
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.LoadParameters();
            if (!this.Page.IsPostBack)
            {
                this.BindYearSaleTotalStatistics();
                this.BindMonthSaleTotalStatistics();
                this.BindDaySaleTotalStatistics();
            }
        }
        private void ReBind()
        {
            base.ReloadPage(new NameValueCollection
			{
                {
                    "Year",
                    this.dropYearForYear.SelectedValue.ToString()
                },
				{
					"monthYear",
					this.dropMonthForYaer.SelectedValue.ToString()
				},
				{
					"dayYear",
					this.dropDayForYear.SelectedValue.ToString()
				},
				{
					"dayMonth",
					this.dropMoth.SelectedValue.ToString()
				},
                {
                    "Grade", 
                    this.DrGrade.Text
                },
                {
                    "StoreName",
                    this.txtStoreName.Text
                },
                {
                    "UserName", 
                    this.txtUserName.Text
                },
                {
                      "startTime",
                      this.calendarStart.SelectedDate.ToString()
                 },
                {
                    "endTime", 
                    this.calendarEnd.SelectedDate.ToString()
                 }
			});
        }
    }
}
