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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Products)]
	public class ProductOnSales : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnCancelFreeShip;
		protected ImageLinkButton btnDelete;
		protected System.Web.UI.WebControls.HyperLink btnDownTaobao;
		protected System.Web.UI.WebControls.Button btnInStock;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnSetFreeShip;
		protected System.Web.UI.WebControls.Button btnUnSale;
		protected System.Web.UI.WebControls.Button btnUpdateProductTags;
		protected System.Web.UI.WebControls.Button btnUpSale;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		private int? categoryId;
		protected BrandCategoriesDropDownList dropBrandList;
		protected ProductCategoriesDropDownList dropCategories;
		protected SaleStatusDropDownList dropSaleStatus;
		protected ProductTagsDropDownList dropTagList;
		protected ProductTypeDownList dropType;
		private System.DateTime? endDate;
		protected Grid grdProducts;
		protected PageSize hrefPageSize;
		public ProductTagsLiteral litralProductTag;
		protected Pager pager;
		protected Pager pager1;
		private string productCode;
		private string productName;
		private ProductSaleStatus saleStatus = ProductSaleStatus.All;
		private System.DateTime? startDate;
		private int? tagId;
		protected TrimTextBox txtProductTag;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected System.Web.UI.WebControls.TextBox txtSKU;
		private int? typeId;
        protected string LocalUrl = string.Empty;

        public bool isEditStatus = false;
        public string editStatusStyle = "";

        public string VPName = "";

		private void BindProducts()
		{
			this.LoadParameters();
			ProductQuery entity = new ProductQuery
			{
				Keywords = this.productName,
				ProductCode = this.productCode,
				CategoryId = this.categoryId,
				PageSize = this.pager.PageSize,
				PageIndex = this.pager.PageIndex,
				SortOrder = SortAction.Desc,
				SortBy = "DisplaySequence",
				StartDate = this.startDate,
				BrandId = this.dropBrandList.SelectedValue.HasValue ? this.dropBrandList.SelectedValue : null,
				TagId = this.dropTagList.SelectedValue.HasValue ? this.dropTagList.SelectedValue : null,
				TypeId = this.typeId,
				SaleStatus = this.saleStatus,
				EndDate = this.endDate
			};
			if (this.categoryId.HasValue && this.categoryId > 0)
			{
				entity.MaiCategoryPath = CatalogHelper.GetCategory(this.categoryId.Value).Path;
			}

            ManagerInfo manager = ManagerHelper.GetCurrentManager();
            var currentPrivilege = ManagerHelper.GetPrivilegeByRoles(manager.RoleId);
            
            if (null != currentPrivilege)
            {
                if (currentPrivilege.Contains(10010))
                {
                    entity.AddUserId = 0;
                }
                else
                {
                    entity.AddUserId = manager.UserId;
                }
            }
            else
            {
                entity.AddUserId = manager.UserId;
            }

			Globals.EntityCoding(entity, true);
			DbQueryResult products = ProductHelper.GetProducts(entity, true);
			this.grdProducts.DataSource = products.Data;
			this.grdProducts.DataBind();
			this.txtSearchText.Text = entity.Keywords;
			this.txtSKU.Text = entity.ProductCode;
			this.dropCategories.SelectedValue = entity.CategoryId;
			this.dropType.SelectedValue = entity.TypeId;
			this.pager1.TotalRecords = (this.pager.TotalRecords = products.TotalRecords);
		}
		private void btnCancelFreeShip_Click(object sender, System.EventArgs e)
		{
			throw new System.NotImplementedException();
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要删除的商品", false);
			}
			else
			{
				if (ProductHelper.RemoveProduct(str) > 0)
				{
					this.ShowMsg("成功删除了选择的商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("删除商品失败，未知错误", false);
				}
			}
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
					this.ShowMsg("成功入库选择的商品，您可以在仓库区的商品里面找到入库以后的商品", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg("入库商品失败，未知错误", false);
				}
			}
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReloadProductOnSales(true);
		}
		private void btnSetFreeShip_Click(object sender, System.EventArgs e)
		{
			bool isFree = ((System.Web.UI.WebControls.Button)sender).ID == "btnSetFreeShip";
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要设置为包邮的商品", false);
			}
			else
			{
				if (ProductHelper.SetFreeShip(str, isFree) > 0)
				{
					this.ShowMsg("成功" + (isFree ? "设置" : "取消") + "了商品包邮状态", true);
					this.BindProducts();
				}
				else
				{
					this.ShowMsg((isFree ? "设置" : "取消") + "商品包邮状态失败，未知错误", false);
				}
			}
		}
		private void btnUnSale_Click(object sender, System.EventArgs e)
		{
            if (isEditStatus)
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
			
		}
		private void btnUpdateProductTags_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择要关联的商品", false);
			}
			else
			{
				System.Collections.Generic.IList<int> list = new System.Collections.Generic.List<int>();
				if (!string.IsNullOrEmpty(this.txtProductTag.Text.Trim()))
				{
					string str2 = this.txtProductTag.Text.Trim();
					string[] strArray;
					if (str2.Contains(","))
					{
						strArray = str2.Split(new char[]
						{
							','
						});
					}
					else
					{
						strArray = new string[]
						{
							str2
						};
					}
					string[] array = strArray;
					for (int j = 0; j < array.Length; j++)
					{
						string str3 = array[j];
						list.Add(System.Convert.ToInt32(str3));
					}
				}
				string[] strArray2;
				if (str.Contains(","))
				{
					strArray2 = str.Split(new char[]
					{
						','
					});
				}
				else
				{
					strArray2 = new string[]
					{
						str
					};
				}
				int num = 0;
				string[] strArray3 = strArray2;
				for (int i = 0; i < strArray3.Length; i++)
				{
					string text = strArray3[i];
				}
				if (num > 0)
				{
					this.ShowMsg(string.Format("已成功修改了{0}件商品的商品标签", num), true);
				}
				else
				{
					this.ShowMsg("已成功取消了商品的关联商品标签", true);
				}
				this.txtProductTag.Text = "";
			}
		}
		private void btnUpSale_Click(object sender, System.EventArgs e)
		{
            if (isEditStatus)
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
			
		}
		private void dropSaleStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ReloadProductOnSales(true);
		}
		private void grdProducts_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
			{
				System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)e.Row.FindControl("litSaleStatus");
				System.Web.UI.WebControls.Literal literal2 = (System.Web.UI.WebControls.Literal)e.Row.FindControl("litMarketPrice");
                System.Web.UI.WebControls.Literal literal3 = (System.Web.UI.WebControls.Literal)e.Row.FindControl("litIsDisplayHome");
                System.Web.UI.WebControls.Literal literal4 = (System.Web.UI.WebControls.Literal)e.Row.FindControl("litIsCross");
				if (literal.Text == "1")
				{
					literal.Text = "出售中";
				}
				else
				{
					if (literal.Text == "2")
					{
						literal.Text = "下架区";
					}
					else
					{
						literal.Text = "仓库中";
					}
				}
				if (string.IsNullOrEmpty(literal2.Text))
				{
					literal2.Text = "-";
				}
                if (literal3.Text == "1")
                {
                    literal3.Text = "显示";
                }
                else
                {
                    literal3.Text = "不显示";
                }
                if (literal4.Text == "1")
                {
                    literal4.Text = "是";
                }
                else
                {
                    literal4.Text = "否";
                }
			}
		}
		private void grdProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			if (ProductHelper.RemoveProduct(this.grdProducts.DataKeys[e.RowIndex].Value.ToString()) > 0)
			{
				this.ShowMsg("删除商品成功", true);
				this.ReloadProductOnSales(false);
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
			int num3 = 0;
			if (int.TryParse(this.Page.Request.QueryString["tagId"], out num3))
			{
				this.tagId = new int?(num3);
			}
			int num4 = 0;
			if (int.TryParse(this.Page.Request.QueryString["typeId"], out num4))
			{
				this.typeId = new int?(num4);
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["SaleStatus"]))
			{
				this.saleStatus = (ProductSaleStatus)System.Enum.Parse(typeof(ProductSaleStatus), this.Page.Request.QueryString["SaleStatus"]);
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
			this.dropTagList.DataBind();
			this.dropTagList.SelectedValue = this.tagId;
			this.calendarStartDate.SelectedDate = this.startDate;
			this.calendarEndDate.SelectedDate = this.endDate;
			this.dropType.SelectedValue = this.typeId;
			this.dropSaleStatus.SelectedValue = this.saleStatus;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.LocalUrl = base.Server.UrlEncode(base.Request.Url.ToString());
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnUpSale.Click += new System.EventHandler(this.btnUpSale_Click);
			this.btnUnSale.Click += new System.EventHandler(this.btnUnSale_Click);
			this.btnInStock.Click += new System.EventHandler(this.btnInStock_Click);
			this.btnCancelFreeShip.Click += new System.EventHandler(this.btnSetFreeShip_Click);
			this.btnSetFreeShip.Click += new System.EventHandler(this.btnSetFreeShip_Click);
			this.btnUpdateProductTags.Click += new System.EventHandler(this.btnUpdateProductTags_Click);
			this.grdProducts.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdProducts_RowDataBound);
			this.grdProducts.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdProducts_RowDeleting);
			this.dropSaleStatus.SelectedIndexChanged += new System.EventHandler(this.dropSaleStatus_SelectedIndexChanged);
			if (!this.Page.IsPostBack)
			{
				this.dropCategories.IsUnclassified = true;
				this.dropCategories.DataBind();
				this.dropBrandList.DataBind();
				this.dropTagList.DataBind();
				this.dropType.DataBind();
				this.litralProductTag.DataBind();
				this.dropSaleStatus.DataBind();
				this.btnDownTaobao.NavigateUrl = string.Format("http://order1.kuaidiangtong.com/TaoBaoApi.aspx?Host={0}&ApplicationPath={1}", SettingsManager.GetMasterSettings(true).SiteUrl, Globals.ApplicationPath);
				this.BindProducts();
			}
			CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);

            SiteSettings siteSetting = SettingsManager.GetMasterSettings(false);
            VPName = siteSetting.VirtualPointName;

            ManagerInfo manager = ManagerHelper.GetCurrentManager();
            var currentPrivilege = ManagerHelper.GetPrivilegeByRoles(manager.RoleId);

            isEditStatus = false;
            editStatusStyle = "display:none";

            if (null != currentPrivilege)
            {
                if (currentPrivilege.Contains(10009))
                {
                    isEditStatus = true;
                    editStatusStyle = "display:block";
                }
            }

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
			if (this.dropTagList.SelectedValue.HasValue)
			{
				queryStrings.Add("tagId", this.dropTagList.SelectedValue.ToString());
			}
			if (this.dropType.SelectedValue.HasValue)
			{
				queryStrings.Add("typeId", this.dropType.SelectedValue.ToString());
			}
			queryStrings.Add("SaleStatus", this.dropSaleStatus.SelectedValue.ToString());
			base.ReloadPage(queryStrings);
		}
	}
}
