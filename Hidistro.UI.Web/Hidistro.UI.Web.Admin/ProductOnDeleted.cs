using ASPNET.WebControls;
using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Products)]
	public class ProductOnDeleted : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton btnInStock;
		protected System.Web.UI.WebControls.LinkButton btnOffShelf;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.LinkButton btnUpShelf;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		private int? categoryId;
		protected System.Web.UI.WebControls.CheckBox chkDeleteImage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden currentProductId;
		protected BrandCategoriesDropDownList dropBrandList;
		protected ProductCategoriesDropDownList dropCategories;
		private System.DateTime? endDate;
		protected Grid grdProducts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdPenetrationStatus;
		protected PageSize hrefPageSize;
		protected Pager pager;
		protected Pager pager1;
		private string productCode;
		private string productName;
		private System.DateTime? startDate;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected System.Web.UI.WebControls.TextBox txtSKU;
		private void BindProducts()
		{
			this.LoadParameters();
			ProductQuery entity = new ProductQuery
			{
				Keywords = this.productName,
				ProductCode = this.productCode,
				CategoryId = this.categoryId,
				StartDate = this.startDate,
				EndDate = this.endDate,
				PageSize = this.pager.PageSize,
				PageIndex = this.pager.PageIndex,
				SaleStatus = ProductSaleStatus.Delete,
				SortOrder = SortAction.Desc,
				BrandId = this.dropBrandList.SelectedValue.HasValue ? this.dropBrandList.SelectedValue : null,
				SortBy = "DisplaySequence"
			};
			if (this.categoryId.HasValue)
			{
				entity.MaiCategoryPath = CatalogHelper.GetCategory(this.categoryId.Value).Path;
			}
			Globals.EntityCoding(entity, true);
			DbQueryResult products = ProductHelper.GetProducts(entity, true);
			this.grdProducts.DataSource = products.Data;
			this.grdProducts.DataBind();
			this.txtSearchText.Text = entity.Keywords;
			this.txtSKU.Text = entity.ProductCode;
			this.dropCategories.SelectedValue = entity.CategoryId;
			this.pager1.TotalRecords = (this.pager.TotalRecords = products.TotalRecords);
		}
		private void btnInStock_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要入库的商品", false);
			}
			else
			{
				if (ProductHelper.InStock(str) > 0)
				{
					this.ShowMsg("成功入库了选择的商品，您可以在仓库里的商品里面找到入库以后的商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("入库商品失败，未知错误", false);
				}
			}
		}
		private void btnOffShelf_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要下架的商品", false);
			}
			else
			{
				if (ProductHelper.OffShelf(str) > 0)
				{
					this.ShowMsg("成功下架了选择的商品，您可以在下架区的商品里面找到下架以后的商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("下架商品失败，未知错误", false);
				}
			}
		}
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			string str = this.currentProductId.Value;
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要删除的商品", false);
			}
			else
			{
				if (ProductHelper.DeleteProduct(str, this.hdPenetrationStatus.Value.Equals("1")) > 0)
				{
					this.ShowMsg("成功的删除了商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("删除商品失败，未知错误", false);
				}
			}
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReloadProductOnSales(true);
		}
		private void btnUpShelf_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要上架的商品", false);
			}
			else
			{
				if (ProductHelper.UpShelf(str) > 0)
				{
					this.ShowMsg("成功上架了选择的商品，您可以在出售中的商品里面找到上架以后的商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("上架商品失败，未知错误", false);
				}
			}
		}
		private void LoadParameters()
		{
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productName"]))
			{
				this.productName = Globals.UrlDecode(this.Page.Request.QueryString["productName"]);
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productCode"]))
			{
				this.productCode = Globals.UrlDecode(this.Page.Request.QueryString["productCode"]);
			}
			int result = 0;
			if (int.TryParse(this.Page.Request.QueryString["categoryId"], out result))
			{
				this.categoryId = new int?(result);
			}
			int num2 = 0;
			if (int.TryParse(this.Page.Request.QueryString["brandId"], out num2))
			{
				this.dropBrandList.SelectedValue = new int?(num2);
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["startDate"]))
			{
				this.startDate = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["startDate"]));
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endDate"]))
			{
				this.endDate = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["endDate"]));
			}
			this.txtSearchText.Text = this.productName;
			this.txtSKU.Text = this.productCode;
			this.dropCategories.DataBind();
			this.dropCategories.SelectedValue = this.categoryId;
			this.calendarStartDate.SelectedDate = this.startDate;
			this.calendarEndDate.SelectedDate = this.endDate;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnUpShelf.Click += new System.EventHandler(this.btnUpShelf_Click);
			this.btnOffShelf.Click += new System.EventHandler(this.btnOffShelf_Click);
			this.btnInStock.Click += new System.EventHandler(this.btnInStock_Click);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			if (!this.Page.IsPostBack)
			{
				this.dropBrandList.DataBind();
				this.dropCategories.DataBind();
				this.BindProducts();
			}
			CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
		}
		private void ReloadProductOnSales(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("productName", Globals.UrlEncode(this.txtSearchText.Text.Trim()));
			if (this.dropCategories.SelectedValue.HasValue)
			{
				queryStrings.Add("categoryId", this.dropCategories.SelectedValue.ToString());
			}
			queryStrings.Add("productCode", Globals.UrlEncode(Globals.HtmlEncode(this.txtSKU.Text.Trim())));
			queryStrings.Add("pageSize", this.pager.PageSize.ToString());
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			if (this.calendarStartDate.SelectedDate.HasValue)
			{
				queryStrings.Add("startDate", this.calendarStartDate.SelectedDate.Value.ToString());
			}
			if (this.calendarEndDate.SelectedDate.HasValue)
			{
				queryStrings.Add("endDate", this.calendarEndDate.SelectedDate.Value.ToString());
			}
			if (this.dropBrandList.SelectedValue.HasValue)
			{
				queryStrings.Add("brandId", this.dropBrandList.SelectedValue.ToString());
			}
			base.ReloadPage(queryStrings);
		}
	}
}
