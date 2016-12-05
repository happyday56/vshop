using ASPNET.WebControls;
using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.TransferManager;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.product
{
	[PrivilegeCheck(Privilege.ProductBatchExport)]
	public class ExportToPP : AdminPage
	{
		private int? _categoryId;
		private System.DateTime? _endDate;
		private bool _includeInStock;
		private bool _includeOnSales;
		private bool _includeUnSales;
		private string _productCode;
		private string _productName;
		private System.DateTime? _startDate;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		protected System.Web.UI.WebControls.CheckBox chkExportStock;
		protected System.Web.UI.WebControls.CheckBox chkInStock;
		protected System.Web.UI.WebControls.CheckBox chkOnSales;
		protected System.Web.UI.WebControls.CheckBox chkUnSales;
		protected ProductCategoriesDropDownList dropCategories;
		protected System.Web.UI.WebControls.DropDownList dropExportVersions;
		protected Grid grdProducts;
		protected System.Web.UI.WebControls.Label lblTotals;
		protected Pager pager;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected System.Web.UI.WebControls.TextBox txtSKU;
		private void BindExporter()
		{
			this.dropExportVersions.Items.Clear();
			this.dropExportVersions.Items.Add(new System.Web.UI.WebControls.ListItem("-请选择-", ""));
			System.Collections.Generic.Dictionary<string, string> exportAdapters = TransferHelper.GetExportAdapters(new YfxTarget("1.2"), "拍拍助理");
			foreach (string str in exportAdapters.Keys)
			{
				this.dropExportVersions.Items.Add(new System.Web.UI.WebControls.ListItem(exportAdapters[str].Replace("4.0", "2013"), str));
			}
		}
		private void BindProducts()
		{
			if (!this._includeUnSales && !this._includeOnSales && !this._includeInStock)
			{
				this.ShowMsg("至少要选择包含一个商品状态", false);
			}
			else
			{
				DbQueryResult exportProducts = ProductHelper.GetExportProducts(this.GetQuery(), (string)this.ViewState["RemoveProductIds"]);
				this.grdProducts.DataSource = exportProducts.Data;
				this.grdProducts.DataBind();
				this.pager.TotalRecords = exportProducts.TotalRecords;
				this.lblTotals.Text = exportProducts.TotalRecords.ToString(System.Globalization.CultureInfo.InvariantCulture);
			}
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			if (!this._includeUnSales && !this._includeOnSales && !this._includeInStock)
			{
				this.ShowMsg("至少要选择包含一个商品状态", false);
			}
			else
			{
				string selectedValue = this.dropExportVersions.SelectedValue;
				if (string.IsNullOrEmpty(selectedValue) || selectedValue.Length == 0)
				{
					this.ShowMsg("请选择一个导出版本", false);
				}
				else
				{
					bool includeCostPrice = false;
					bool includeStock = this.chkExportStock.Checked;
					bool flag3 = true;
					string str2 = "http://" + System.Web.HttpContext.Current.Request.Url.Host + ((System.Web.HttpContext.Current.Request.Url.Port == 80) ? "" : (":" + System.Web.HttpContext.Current.Request.Url.Port)) + Globals.ApplicationPath;
					string applicationPath = Globals.ApplicationPath;
					System.Data.DataSet set = ProductHelper.GetExportProducts(this.GetQuery(), includeCostPrice, includeStock, (string)this.ViewState["RemoveProductIds"]);
					TransferHelper.GetExporter(selectedValue, new object[]
					{
						set,
						includeCostPrice,
						includeStock,
						flag3,
						str2,
						applicationPath
					}).DoExport();
				}
			}
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReSearchProducts();
		}
		private AdvancedProductQuery GetQuery()
		{
			AdvancedProductQuery entity = new AdvancedProductQuery
			{
				Keywords = this._productName,
				ProductCode = this._productCode,
				CategoryId = this._categoryId,
				PageSize = this.pager.PageSize,
				PageIndex = this.pager.PageIndex,
				SaleStatus = ProductSaleStatus.OnSale,
				SortOrder = SortAction.Desc,
				SortBy = "DisplaySequence",
				StartDate = this._startDate,
				EndDate = this._endDate,
				IncludeInStock = this._includeInStock,
				IncludeOnSales = this._includeOnSales,
				IncludeUnSales = this._includeUnSales
			};
			if (this._categoryId.HasValue)
			{
				entity.MaiCategoryPath = CatalogHelper.GetCategory(this._categoryId.Value).Path;
			}
			Globals.EntityCoding(entity, true);
			return entity;
		}
		private void grdProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Remove")
			{
				int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
				int num2 = (int)this.grdProducts.DataKeys[rowIndex].Value;
				string str = (string)this.ViewState["RemoveProductIds"];
				if (string.IsNullOrEmpty(str))
				{
					str = num2.ToString();
				}
				else
				{
					str = str + "," + num2.ToString();
				}
				this.ViewState["RemoveProductIds"] = str;
				this.BindProducts();
			}
		}
		private void LoadParameters()
		{
			this._productName = this.txtSearchText.Text.Trim();
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productName"]))
			{
				this._productName = Globals.UrlDecode(this.Page.Request.QueryString["productName"]);
				this.txtSearchText.Text = this._productName;
			}
			this._productCode = this.txtSKU.Text.Trim();
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productCode"]))
			{
				this._productCode = Globals.UrlDecode(this.Page.Request.QueryString["productCode"]);
				this.txtSKU.Text = this._productCode;
			}
			this._categoryId = this.dropCategories.SelectedValue;
			int num;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["categoryId"]) && int.TryParse(this.Page.Request.QueryString["categoryId"], out num))
			{
				this._categoryId = new int?(num);
				this.dropCategories.SelectedValue = this._categoryId;
			}
			this._startDate = this.calendarStartDate.SelectedDate;
			System.DateTime time;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["startDate"]) && System.DateTime.TryParse(this.Page.Request.QueryString["startDate"], out time))
			{
				this._startDate = new System.DateTime?(time);
				this.calendarStartDate.SelectedDate = this._startDate;
			}
			this._endDate = this.calendarEndDate.SelectedDate;
			System.DateTime time2;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["endDate"]) && System.DateTime.TryParse(this.Page.Request.QueryString["endDate"], out time2))
			{
				this._endDate = new System.DateTime?(time2);
				this.calendarEndDate.SelectedDate = this._endDate;
			}
			this._includeOnSales = this.chkOnSales.Checked;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["includeOnSales"]))
			{
				bool.TryParse(this.Page.Request.QueryString["includeOnSales"], out this._includeOnSales);
				this.chkOnSales.Checked = this._includeOnSales;
			}
			this._includeUnSales = this.chkUnSales.Checked;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["includeUnSales"]))
			{
				bool.TryParse(this.Page.Request.QueryString["includeUnSales"], out this._includeUnSales);
				this.chkUnSales.Checked = this._includeUnSales;
			}
			this._includeInStock = this.chkInStock.Checked;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["includeInStock"]))
			{
				bool.TryParse(this.Page.Request.QueryString["includeInStock"], out this._includeInStock);
				this.chkInStock.Checked = this._includeInStock;
			}
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.grdProducts.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdProducts_RowCommand);
			if (!this.Page.IsPostBack)
			{
				this.dropCategories.DataBind();
				this.BindExporter();
			}
			this.LoadParameters();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindProducts();
			}
		}
		private void ReSearchProducts()
		{
			NameValueCollection queryStrings = new NameValueCollection
			{

				{
					"productName",
					Globals.UrlEncode(this.txtSearchText.Text.Trim())
				},

				{
					"productCode",
					Globals.UrlEncode(Globals.HtmlEncode(this.txtSKU.Text.Trim()))
				},

				{
					"pageSize",
					this.pager.PageSize.ToString()
				},

				{
					"includeOnSales",
					this.chkOnSales.Checked.ToString()
				},

				{
					"includeUnSales",
					this.chkUnSales.Checked.ToString()
				},

				{
					"includeInStock",
					this.chkInStock.Checked.ToString()
				}
			};
			if (this.dropCategories.SelectedValue.HasValue)
			{
				queryStrings.Add("categoryId", this.dropCategories.SelectedValue.ToString());
			}
			queryStrings.Add("pageIndex", this.pager.PageIndex.ToString());
			if (this.calendarStartDate.SelectedDate.HasValue)
			{
				queryStrings.Add("startDate", this.calendarStartDate.SelectedDate.Value.ToString());
			}
			if (this.calendarEndDate.SelectedDate.HasValue)
			{
				queryStrings.Add("endDate", this.calendarEndDate.SelectedDate.Value.ToString());
			}
			base.ReloadPage(queryStrings);
		}
	}
}
