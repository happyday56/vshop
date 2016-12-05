using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.product
{
	[PrivilegeCheck(Privilege.AddProductType)]
	public class AddProductType : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddProductType;
		protected BrandCategoriesCheckBoxList chlistBrand;
		protected System.Web.UI.WebControls.TextBox txtRemark;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtRemarkTip;
		protected System.Web.UI.WebControls.TextBox txtTypeName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtTypeNameTip;
		private void btnAddProductType_Click(object sender, System.EventArgs e)
		{
			ProductTypeInfo productType = new ProductTypeInfo
			{
				TypeName = this.txtTypeName.Text.Trim(),
				Remark = this.txtRemark.Text
			};
			System.Collections.Generic.IList<int> list = new System.Collections.Generic.List<int>();
			foreach (System.Web.UI.WebControls.ListItem item in this.chlistBrand.Items)
			{
				if (item.Selected)
				{
					list.Add(int.Parse(item.Value));
				}
			}
			productType.Brands = list;
			if (this.ValidationProductType(productType))
			{
				int num = ProductTypeHelper.AddProductType(productType);
				if (num > 0)
				{
					base.Response.Redirect(Globals.GetAdminAbsolutePath("/product/AddAttribute.aspx?typeId=" + num), true);
				}
				else
				{
					this.ShowMsg("添加商品类型失败", false);
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnAddProductType.Click += new System.EventHandler(this.btnAddProductType_Click);
			if (!base.IsPostBack)
			{
				this.chlistBrand.DataBind();
			}
		}
		private bool ValidationProductType(ProductTypeInfo productType)
		{
			ValidationResults results = Validation.Validate<ProductTypeInfo>(productType, new string[]
			{
				"ValProductType"
			});
			string msg = string.Empty;
			if (!results.IsValid)
			{
				foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
				{
					msg += Formatter.FormatErrorMessage(result.Message);
				}
				this.ShowMsg(msg, false);
			}
			return results.IsValid;
		}
	}
}
