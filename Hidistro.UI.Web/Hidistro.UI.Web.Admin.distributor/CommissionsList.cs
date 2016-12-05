using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class CommissionsList : AdminPage
	{
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater reCommissions;
		private int userid;
		private void BindData()
		{
			CommissionsQuery entity = new CommissionsQuery
			{
				UserId = int.Parse(this.Page.Request.QueryString["UserId"]),
				EndTime = "",
				StartTime = "",
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "CommId"
			};
			Globals.EntityCoding(entity, true);
			DbQueryResult commissions = VShopHelper.GetCommissions(entity);
			this.reCommissions.DataSource = commissions.Data;
			this.reCommissions.DataBind();
			this.pager.TotalRecords = commissions.TotalRecords;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (int.TryParse(this.Page.Request.QueryString["UserId"], out this.userid))
			{
				this.BindData();
			}
			else
			{
				this.Page.Response.Redirect("DistributorList.aspx");
			}
		}
	}
}
