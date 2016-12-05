using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.UserIncreaseStatistics)]
	public class UserIncreaseStatistics : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnOfMonth;
		protected System.Web.UI.WebControls.Button btnOfYear;
		protected MonthDropDownList drpMonthOfMonth;
		protected YearDropDownList drpYearOfMonth;
		protected YearDropDownList drpYearOfYear;
		protected System.Web.UI.HtmlControls.HtmlImage imgChartOfMonth;
		protected System.Web.UI.HtmlControls.HtmlImage imgChartOfSevenDay;
		protected System.Web.UI.HtmlControls.HtmlImage imgChartOfYear;
		protected System.Web.UI.WebControls.Literal litlOfMonth;
		protected System.Web.UI.WebControls.Literal litlOfYear;
		private void BindDaysAddUser()
		{
			System.Collections.Generic.IList<UserStatisticsForDate> list = SalesHelper.GetUserAdd(null, null, new int?(7));
			string str = string.Empty;
			string str2 = string.Empty;
			foreach (UserStatisticsForDate date in list)
			{
				if (string.IsNullOrEmpty(str))
				{
					if (System.DateTime.Now.Date.Day < 7 && date.TimePoint > 7)
					{
						str += ((System.DateTime.Now.Month > 9) ? (System.DateTime.Now.Month - 1).ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + (System.DateTime.Now.Month - 1).ToString(System.Globalization.CultureInfo.InvariantCulture) + "-" + ((date.TimePoint > 9) ? date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture)))));
					}
					else
					{
						str += ((System.DateTime.Now.Month > 9) ? System.DateTime.Now.Month.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + System.DateTime.Now.Month.ToString(System.Globalization.CultureInfo.InvariantCulture) + "-" + ((date.TimePoint > 9) ? date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture)))));
					}
				}
				else
				{
					if (System.DateTime.Now.Date.Day < 7 && date.TimePoint > 7)
					{
						string str3 = str;
						str = string.Concat(new string[]
						{
							str3,
							"|",
							(System.DateTime.Now.Month > 10) ? (System.DateTime.Now.Month - 1).ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + (System.DateTime.Now.Month - 1).ToString(System.Globalization.CultureInfo.InvariantCulture)),
							"-",
							(date.TimePoint > 9) ? date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture))
						});
					}
					else
					{
						string str4 = str;
						str = string.Concat(new string[]
						{
							str4,
							"|",
							(System.DateTime.Now.Month > 10) ? System.DateTime.Now.Month.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + System.DateTime.Now.Month.ToString(System.Globalization.CultureInfo.InvariantCulture)),
							"-",
							(date.TimePoint > 9) ? date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture) : ("0" + date.TimePoint.ToString(System.Globalization.CultureInfo.InvariantCulture))
						});
					}
				}
				if (string.IsNullOrEmpty(str2))
				{
					str2 += date.UserCounts;
				}
				else
				{
					str2 = str2 + "|" + date.UserCounts;
				}
			}
			this.imgChartOfSevenDay.Src = Globals.ApplicationPath + string.Format("/UserStatisticeChart.aspx?ChartType={0}&XValues={1}&YValues={2}", "bar", str, str2);
		}
		private void BindMonthAddUser()
		{
			System.Collections.Generic.IList<UserStatisticsForDate> list = SalesHelper.GetUserAdd(new int?(this.drpYearOfMonth.SelectedValue), new int?(this.drpMonthOfMonth.SelectedValue), null);
			string str = string.Empty;
			string str2 = string.Empty;
			foreach (UserStatisticsForDate date in list)
			{
				if (string.IsNullOrEmpty(str))
				{
					str += date.TimePoint;
				}
				else
				{
					str = str + "|" + date.TimePoint;
				}
				if (string.IsNullOrEmpty(str2))
				{
					str2 += date.UserCounts;
				}
				else
				{
					str2 = str2 + "|" + date.UserCounts;
				}
			}
			this.imgChartOfMonth.Src = Globals.ApplicationPath + string.Format("/UserStatisticeChart.aspx?ChartType={0}&XValues={1}&YValues={2}", "bar", str, str2);
			this.litlOfMonth.Text = this.drpYearOfMonth.SelectedValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + "年" + this.drpMonthOfMonth.SelectedValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + "月";
		}
		private void BindYearAddUser()
		{
			System.Collections.Generic.IList<UserStatisticsForDate> list = SalesHelper.GetUserAdd(new int?(this.drpYearOfYear.SelectedValue), null, null);
			string str = string.Empty;
			string str2 = string.Empty;
			foreach (UserStatisticsForDate date in list)
			{
				if (string.IsNullOrEmpty(str))
				{
					str += date.TimePoint;
				}
				else
				{
					str = str + "|" + date.TimePoint;
				}
				if (string.IsNullOrEmpty(str2))
				{
					str2 += date.UserCounts;
				}
				else
				{
					str2 = str2 + "|" + date.UserCounts;
				}
			}
			this.imgChartOfYear.Src = Globals.ApplicationPath + string.Format("/UserStatisticeChart.aspx?ChartType={0}&XValues={1}&YValues={2}", "bar", str, str2);
			this.litlOfYear.Text = this.drpYearOfYear.SelectedValue.ToString(System.Globalization.CultureInfo.InvariantCulture) + "年";
		}
		private void btnOfMonth_Click(object sender, System.EventArgs e)
		{
			this.BindMonthAddUser();
		}
		private void btnOfYear_Click(object sender, System.EventArgs e)
		{
			this.BindYearAddUser();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnOfMonth.Click += new System.EventHandler(this.btnOfMonth_Click);
			this.btnOfYear.Click += new System.EventHandler(this.btnOfYear_Click);
			if (!this.Page.IsPostBack)
			{
				this.BindDaysAddUser();
				this.BindMonthAddUser();
				this.BindYearAddUser();
			}
		}
	}
}
