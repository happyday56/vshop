using ASPNET.WebControls;
using Hidistro.ControlPanel.Commodities;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Commodities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class SearchGroupbuyProduct : AdminPage
	{
		private int? brandId;
		protected System.Web.UI.WebControls.Button btnSearch;
		private int? categoryId;
		protected BrandCategoriesDropDownList dropBrandList;
		protected ProductCategoriesDropDownList dropCategories;
		protected Grid grdproducts;
		protected PageSize hrefPageSize;
		protected Pager pager;
		protected Pager pager1;
		private string productName;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请选择一件商品！", false);
			}
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReloadProductOnSales(true);
		}
		protected void DoCallback()
		{
			this.LoadParameters();
			ProductQuery query = new ProductQuery
			{
				PageSize = this.pager.PageSize,
				PageIndex = this.pager.PageIndex,
				SaleStatus = ProductSaleStatus.OnSale,
				IsIncludePromotionProduct = new bool?(false),
				Keywords = this.productName
			};
			if (this.brandId.HasValue)
			{
				query.BrandId = new int?(this.brandId.Value);
			}
			query.CategoryId = this.categoryId;
			if (this.categoryId.HasValue)
			{
				query.MaiCategoryPath = CatalogHelper.GetCategory(this.categoryId.Value).Path;
			}
			DbQueryResult products = ProductHelper.GetProducts(query);
			System.Data.DataTable data = (System.Data.DataTable)products.Data;
			this.pager1.TotalRecords = (this.pager.TotalRecords = products.TotalRecords);
			this.grdproducts.DataSource = data;
			this.grdproducts.DataBind();
		}
		private void LoadParameters()
		{
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productName"]))
			{
				this.productName = Globals.UrlDecode(this.Page.Request.QueryString["productName"]);
			}
			int result = 0;
			if (int.TryParse(this.Page.Request.QueryString["categoryId"], out result))
			{
				this.categoryId = new int?(result);
			}
			int num2 = 0;
			if (int.TryParse(this.Page.Request.QueryString["brandId"], out num2))
			{
				this.brandId = new int?(num2);
			}
			this.txtSearchText.Text = this.productName;
			this.dropCategories.DataBind();
			this.dropCategories.SelectedValue = this.categoryId;
			this.dropBrandList.DataBind();
			this.dropBrandList.SelectedValue = new int?(num2);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.DoCallback();
			}
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
		}
		private void ReloadProductOnSales(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("productName", Globals.UrlEncode(this.txtSearchText.Text.Trim()));
			if (this.dropCategories.SelectedValue.HasValue)
			{
				queryStrings.Add("categoryId", this.dropCategories.SelectedValue.ToString());
			}
			queryStrings.Add("pageSize", this.pager.PageSize.ToString());
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			if (this.dropBrandList.SelectedValue.HasValue)
			{
				queryStrings.Add("brandId", this.dropBrandList.SelectedValue.ToString());
			}
			base.ReloadPage(queryStrings);
		}
	}
}
