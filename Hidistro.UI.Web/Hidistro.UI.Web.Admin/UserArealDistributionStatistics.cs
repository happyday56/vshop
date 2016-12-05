using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.MemberArealDistributionStatistics)]
	public class UserArealDistributionStatistics : AdminPage
	{
		protected Grid grdUserStatistics;
		private void BindUserStatistics()
		{
			int totalProductSaleVisits = 0;
			Pagination page = new Pagination
			{
				SortBy = this.grdUserStatistics.SortOrderBy
			};
			if (this.grdUserStatistics.SortOrder.ToLower() == "desc")
			{
				page.SortOrder = SortAction.Desc;
			}
			this.grdUserStatistics.DataSource = SalesHelper.GetUserStatistics(page, out totalProductSaleVisits);
			this.grdUserStatistics.DataBind();
		}
		private void grdUserStatistics_ReBindData(object sender)
		{
			this.BindUserStatistics();
		}
		private void grdUserStatistics_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
			{
				int currentRegionId = int.Parse(this.grdUserStatistics.DataKeys[e.Row.RowIndex].Value.ToString(), System.Globalization.NumberStyles.None);
				System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblReionName");
				if (currentRegionId != 0 && label != null)
				{
					label.Text = RegionHelper.GetFullRegion(currentRegionId, "");
				}
				if (currentRegionId == 0 && label != null)
				{
					label.Text = "其它";
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.grdUserStatistics.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdUserStatistics_RowDataBound);
			this.grdUserStatistics.ReBindData += new Grid.ReBindDataEventHandler(this.grdUserStatistics_ReBindData);
			this.grdUserStatistics.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdUserStatistics_RowDataBound);
			if (!base.IsPostBack)
			{
				this.BindUserStatistics();
			}
		}
	}
}
