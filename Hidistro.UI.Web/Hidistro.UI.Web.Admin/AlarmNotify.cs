using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class AlarmNotify : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DataList dlstPtReviews;
		protected ProductCategoriesDropDownList dropCategories;
		protected PageSize hrefPageSize;
		protected Pager pager;
		protected Pager pager1;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected System.Web.UI.WebControls.TextBox txtSKU;
		private void BindPtReview()
		{
			DbQueryResult alarms = VShopHelper.GetAlarms(this.pager.PageIndex, this.pager.PageSize);
			this.dlstPtReviews.DataSource = alarms.Data;
			this.dlstPtReviews.DataBind();
			this.pager.TotalRecords = alarms.TotalRecords;
			this.pager1.TotalRecords = alarms.TotalRecords;
		}
		private void dlstPtReviews_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			if (VShopHelper.DeleteAlarm(System.Convert.ToInt32(e.CommandArgument, System.Globalization.CultureInfo.InvariantCulture)))
			{
				this.ShowMsg("删除成功", true);
				this.BindPtReview();
			}
			else
			{
				this.ShowMsg("删除失败", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.dlstPtReviews.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dlstPtReviews_DeleteCommand);
			if (!base.IsPostBack)
			{
				this.BindPtReview();
			}
		}
	}
}
