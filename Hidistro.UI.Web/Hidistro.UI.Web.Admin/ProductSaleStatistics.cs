using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Enums;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ProductSaleStatistics)]
	public class ProductSaleStatistics : AdminPage
	{
		protected Grid grdProductSaleStatistics;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPageIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPageSize;
		protected Pager pager;
		private void BindProductSaleStatistics()
		{
			SaleStatisticsQuery query = new SaleStatisticsQuery
			{
				PageSize = this.pager.PageSize,
				PageIndex = this.pager.PageIndex,
				SortBy = "BuyPercentage",
				SortOrder = SortAction.Desc
			};
			int totalProductSales = 0;
			System.Data.DataTable productVisitAndBuyStatistics = SalesHelper.GetProductVisitAndBuyStatistics(query, out totalProductSales);
			this.grdProductSaleStatistics.DataSource = productVisitAndBuyStatistics;
			this.grdProductSaleStatistics.DataBind();
			this.pager.TotalRecords = totalProductSales;
			this.hidPageSize.Value = this.pager.PageSize.ToString();
			this.hidPageIndex.Value = this.pager.PageIndex.ToString();
		}
		private void grdProductSaleStatistics_ReBindData(object sender)
		{
			this.ReBind(false);
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.grdProductSaleStatistics.ReBindData += new Grid.ReBindDataEventHandler(this.grdProductSaleStatistics_ReBindData);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindProductSaleStatistics();
			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}
	}
}
