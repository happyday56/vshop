using ASPNET.WebControls;
using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Data;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.product
{
	[PrivilegeCheck(Privilege.EditProducts)]
	public class EditBaseInfo : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddOK;
		protected System.Web.UI.WebControls.Button btnReplaceOK;
		protected System.Web.UI.WebControls.Button btnSaveInfo;
		protected Grid grdSelectedProducts;
		private string productIds = string.Empty;
		protected System.Web.UI.WebControls.TextBox txtNewWord;
		protected System.Web.UI.WebControls.TextBox txtOleWord;
		protected System.Web.UI.WebControls.TextBox txtPrefix;
		protected System.Web.UI.WebControls.TextBox txtSuffix;
		private void BindProduct()
		{
			string str = this.Page.Request.QueryString["ProductIds"];
			if (!string.IsNullOrEmpty(str))
			{
				this.grdSelectedProducts.DataSource = ProductHelper.GetProductBaseInfo(str);
				this.grdSelectedProducts.DataBind();
			}
		}
		private void btnAddOK_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtPrefix.Text.Trim()) && string.IsNullOrEmpty(this.txtSuffix.Text.Trim()))
			{
				this.ShowMsg("前后缀不能同时为空", false);
			}
			else
			{
				if (ProductHelper.UpdateProductNames(this.productIds, this.txtPrefix.Text.Trim(), this.txtSuffix.Text.Trim()))
				{
					this.ShowMsg("为商品名称添加前后缀成功", true);
				}
				else
				{
					this.ShowMsg("为商品名称添加前后缀失败", false);
				}
				this.BindProduct();
			}
		}
		private void btnReplaceOK_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtOleWord.Text.Trim()))
			{
				this.ShowMsg("查找字符串不能为空", false);
			}
			else
			{
				if (ProductHelper.ReplaceProductNames(this.productIds, this.txtOleWord.Text.Trim(), this.txtNewWord.Text.Trim()))
				{
					this.ShowMsg("为商品名称替换字符串缀成功", true);
				}
				else
				{
					this.ShowMsg("为商品名称替换字符串缀失败", false);
				}
				this.BindProduct();
			}
		}
		private void btnSaveInfo_Click(object sender, System.EventArgs e)
		{
			System.Data.DataTable dt = new System.Data.DataTable();
			dt.Columns.Add("ProductId");
			dt.Columns.Add("ProductName");
			dt.Columns.Add("ProductCode");
			dt.Columns.Add("MarketPrice");
			if (this.grdSelectedProducts.Rows.Count > 0)
			{
				decimal result = 0m;
				foreach (System.Web.UI.WebControls.GridViewRow row in this.grdSelectedProducts.Rows)
				{
					int num = (int)this.grdSelectedProducts.DataKeys[row.RowIndex].Value;
					System.Web.UI.WebControls.TextBox box = row.FindControl("txtProductName") as System.Web.UI.WebControls.TextBox;
					System.Web.UI.WebControls.TextBox box2 = row.FindControl("txtProductCode") as System.Web.UI.WebControls.TextBox;
					System.Web.UI.WebControls.TextBox box3 = row.FindControl("txtMarketPrice") as System.Web.UI.WebControls.TextBox;
					if (!string.IsNullOrEmpty(box3.Text.Trim()) && !decimal.TryParse(box3.Text.Trim(), out result))
					{
						break;
					}
					if (string.IsNullOrEmpty(box3.Text.Trim()))
					{
						result = 0m;
					}
					System.Data.DataRow row2 = dt.NewRow();
					row2["ProductId"] = num;
					row2["ProductName"] = Globals.HtmlEncode(box.Text.Trim());
					row2["ProductCode"] = Globals.HtmlEncode(box2.Text.Trim());
					if (result >= 0m)
					{
						row2["MarketPrice"] = result;
					}
					dt.Rows.Add(row2);
				}
				if (ProductHelper.UpdateProductBaseInfo(dt))
				{
					this.CloseWindow();
				}
				else
				{
					this.ShowMsg("批量修改商品信息失败", false);
				}
				this.BindProduct();
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.productIds = this.Page.Request.QueryString["productIds"];
			this.btnSaveInfo.Click += new System.EventHandler(this.btnSaveInfo_Click);
			this.btnAddOK.Click += new System.EventHandler(this.btnAddOK_Click);
			this.btnReplaceOK.Click += new System.EventHandler(this.btnReplaceOK_Click);
			if (!this.Page.IsPostBack)
			{
				this.BindProduct();
			}
		}
	}
}
