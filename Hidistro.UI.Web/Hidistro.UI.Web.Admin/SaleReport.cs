using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
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
	[PrivilegeCheck(Privilege.SaleReport)]
	public class SaleReport : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton btnCreateReportOfDay;
		protected System.Web.UI.WebControls.LinkButton btnCreateReportOfMonth;
		protected System.Web.UI.WebControls.Button btnQueryDaySaleTotal;
		protected System.Web.UI.WebControls.Button btnQueryMonthSaleTotal;
		private int dayMonth = System.DateTime.Now.Month;
		private SaleStatisticsType dayType = SaleStatisticsType.SaleCounts;
		private int dayYear = System.DateTime.Now.Year;
		protected YearDropDownList dropDayForYear;
		protected YearDropDownList dropMonthForYaer;
		protected MonthDropDownList dropMoth;
		protected System.Web.UI.WebControls.GridView grdDaySaleTotalStatistics;
		protected Grid grdMonthSaleTotalStatistics;
		protected System.Web.UI.WebControls.Label lblDayAllTotal;
		protected System.Web.UI.WebControls.Label lblDayMaxTotal;
		protected System.Web.UI.WebControls.Label lblMonthAllTotal;
		protected System.Web.UI.WebControls.Label lblMonthMaxTotal;
		protected System.Web.UI.WebControls.Literal litDayAllTotal;
		protected System.Web.UI.WebControls.Literal litDayMaxTotal;
		protected System.Web.UI.WebControls.Literal litMonthAllTotal;
		protected System.Web.UI.WebControls.Literal litMonthMaxTotal;
		private SaleStatisticsType monthType = SaleStatisticsType.SaleCounts;
		private int monthYear = System.DateTime.Now.Year;
		protected SaleStatisticsTypeRadioButtonList radioDayForSaleType;
		protected SaleStatisticsTypeRadioButtonList radioMonthForSaleType;
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
		private void BindDaySaleTotalStatistics()
		{
			System.Data.DataTable table = SalesHelper.GetDaySaleTotal(this.dayYear, this.dayMonth, this.dayType);
			if (this.radioDayForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.grdDaySaleTotalStatistics.Columns[1].Visible = true;
				this.grdDaySaleTotalStatistics.Columns[1].HeaderText = this.radioDayForSaleType.SelectedItem.Text;
				this.grdDaySaleTotalStatistics.Columns[2].Visible = false;
			}
			else
			{
				this.grdDaySaleTotalStatistics.Columns[1].Visible = false;
				this.grdDaySaleTotalStatistics.Columns[2].Visible = true;
				this.grdDaySaleTotalStatistics.Columns[2].HeaderText = this.radioDayForSaleType.SelectedItem.Text;
			}
			this.grdDaySaleTotalStatistics.DataSource = table;
			this.grdDaySaleTotalStatistics.DataBind();
			this.TableOfDay = table;
			this.lblDayAllTotal.Text = string.Format("总{0}：", this.radioDayForSaleType.SelectedItem.Text);
			decimal money = SalesHelper.GetMonthSaleTotal(this.dayYear, this.dayMonth, this.dayType);
			if (this.radioDayForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.litDayAllTotal.Text = money.ToString();
			}
			else
			{
				this.litDayAllTotal.Text = Globals.FormatMoney(money);
			}
			this.lblDayMaxTotal.Text = string.Format("最高峰{0}：", this.radioDayForSaleType.SelectedItem.Text);
			decimal num2 = 0m;
			foreach (System.Data.DataRow row in table.Rows)
			{
				if ((decimal)row["SaleTotal"] > num2)
				{
					num2 = (decimal)row["SaleTotal"];
				}
			}
			if (this.radioDayForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.litDayMaxTotal.Text = num2.ToString();
			}
			else
			{
				this.litDayMaxTotal.Text = Globals.FormatMoney(num2);
			}
		}
		private void BindMonthSaleTotalStatistics()
		{
			System.Data.DataTable monthSaleTotal = SalesHelper.GetMonthSaleTotal(this.monthYear, this.monthType);
			if (this.radioMonthForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.grdMonthSaleTotalStatistics.Columns[1].Visible = true;
				this.grdMonthSaleTotalStatistics.Columns[1].HeaderText = this.radioMonthForSaleType.SelectedItem.Text;
				this.grdMonthSaleTotalStatistics.Columns[2].Visible = false;
			}
			else
			{
				this.grdMonthSaleTotalStatistics.Columns[1].Visible = false;
				this.grdMonthSaleTotalStatistics.Columns[2].Visible = true;
				this.grdMonthSaleTotalStatistics.Columns[2].HeaderText = this.radioMonthForSaleType.SelectedItem.Text;
			}
			this.grdMonthSaleTotalStatistics.DataSource = monthSaleTotal;
			this.grdMonthSaleTotalStatistics.DataBind();
			this.TableOfMonth = monthSaleTotal;
			this.lblMonthAllTotal.Text = string.Format("总{0}：", this.radioMonthForSaleType.SelectedItem.Text);
			decimal yearSaleTotal = SalesHelper.GetYearSaleTotal(this.monthYear, this.monthType);
			if (this.radioMonthForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.litMonthAllTotal.Text = yearSaleTotal.ToString();
			}
			else
			{
				this.litMonthAllTotal.Text = Globals.FormatMoney(yearSaleTotal);
			}
			this.lblMonthMaxTotal.Text = string.Format("最高峰{0}：", this.radioMonthForSaleType.SelectedItem.Text);
			decimal money = 0m;
			foreach (System.Data.DataRow row in monthSaleTotal.Rows)
			{
				if ((decimal)row["SaleTotal"] > money)
				{
					money = (decimal)row["SaleTotal"];
				}
			}
			if (this.radioMonthForSaleType.SelectedValue == SaleStatisticsType.SaleCounts)
			{
				this.litMonthMaxTotal.Text = money.ToString();
			}
			else
			{
				this.litMonthMaxTotal.Text = Globals.FormatMoney(money);
			}
		}
		private void btnCreateReportOfDay_Click(object sender, System.EventArgs e)
		{
			string s = string.Concat(new string[]
			{
				string.Empty,
				string.Format("总{0}：", this.radioDayForSaleType.SelectedItem.Text),
				",",
				this.litDayAllTotal.Text,
				",\"\",",
				string.Format("最高峰{0}：", this.radioDayForSaleType.SelectedItem.Text),
				",",
				this.litDayMaxTotal.Text,
				"\r\n\r\n日期,",
				this.radioDayForSaleType.SelectedItem.Text,
				",比例\r\n"
			});
			foreach (System.Data.DataRow row in this.TableOfDay.Rows)
			{
				s += row["Date"].ToString();
				s = s + "," + row["SaleTotal"].ToString();
				s = s + "," + row["Percentage"].ToString() + "%\r\n";
			}
			this.Page.Response.Clear();
			this.Page.Response.Buffer = false;
            this.Page.Response.Charset = "UTF-8";
			this.Page.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleTotalStatistics.csv");
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
			this.Page.Response.ContentType = "application/octet-stream";
			this.Page.EnableViewState = false;
			this.Page.Response.Write(s);
			this.Page.Response.End();
		}
		private void btnCreateReportOfMonth_Click(object sender, System.EventArgs e)
		{
			string s = string.Concat(new string[]
			{
				string.Empty,
				string.Format("总{0}：", this.radioMonthForSaleType.SelectedItem.Text),
				",",
				this.litMonthAllTotal.Text,
				",\"\",",
				string.Format("最高峰{0}：", this.radioMonthForSaleType.SelectedItem.Text),
				",",
				this.litMonthMaxTotal.Text,
				"\r\n\r\n月份,",
				this.radioMonthForSaleType.SelectedItem.Text,
				",比例\r\n"
			});
			foreach (System.Data.DataRow row in this.TableOfMonth.Rows)
			{
				s += row["Date"].ToString();
				s = s + "," + row["SaleTotal"].ToString();
				s = s + "," + row["Percentage"].ToString() + "%\r\n";
			}
			this.Page.Response.Clear();
			this.Page.Response.Buffer = false;
            this.Page.Response.Charset = "UTF-8";
			this.Page.Response.AppendHeader("Content-Disposition", "attachment;filename=SaleTotalStatistics.csv");
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
			this.Page.Response.ContentType = "application/octet-stream";
			this.Page.EnableViewState = false;
			this.Page.Response.Write(s);
			this.Page.Response.End();
		}
		private void btnQueryDaySaleTotal_Click(object sender, System.EventArgs e)
		{
			this.ReBind();
		}
		private void btnQueryMonthSaleTotal_Click(object sender, System.EventArgs e)
		{
			this.ReBind();
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
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
				this.dropMonthForYaer.SelectedValue = this.monthYear;
				this.radioMonthForSaleType.SelectedValue = this.monthType;
				this.dropDayForYear.SelectedValue = this.dayYear;
				this.dropMoth.SelectedValue = this.dayMonth;
				this.radioDayForSaleType.SelectedValue = this.dayType;
			}
			else
			{
				this.monthYear = this.dropMonthForYaer.SelectedValue;
				this.monthType = this.radioMonthForSaleType.SelectedValue;
				this.dayYear = this.dropDayForYear.SelectedValue;
				this.dayMonth = this.dropMoth.SelectedValue;
				this.dayType = this.radioDayForSaleType.SelectedValue;
			}
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnQueryMonthSaleTotal.Click += new System.EventHandler(this.btnQueryMonthSaleTotal_Click);
			this.btnCreateReportOfMonth.Click += new System.EventHandler(this.btnCreateReportOfMonth_Click);
			this.btnQueryDaySaleTotal.Click += new System.EventHandler(this.btnQueryDaySaleTotal_Click);
			this.btnCreateReportOfDay.Click += new System.EventHandler(this.btnCreateReportOfDay_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LoadParameters();
			if (!this.Page.IsPostBack)
			{
				this.BindMonthSaleTotalStatistics();
				this.BindDaySaleTotalStatistics();
			}
		}
		private void ReBind()
		{
			base.ReloadPage(new NameValueCollection
			{

				{
					"monthYear",
					this.dropMonthForYaer.SelectedValue.ToString()
				},

				{
					"monthType",
					((int)this.radioMonthForSaleType.SelectedValue).ToString()
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
					"dayType",
					((int)this.radioDayForSaleType.SelectedValue).ToString()
				}
			});
		}
	}
}
