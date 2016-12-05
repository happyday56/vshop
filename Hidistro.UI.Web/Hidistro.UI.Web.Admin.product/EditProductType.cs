using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
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
	[PrivilegeCheck(Privilege.EditProductType)]
	public class EditProductType : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnEditProductType;
		protected BrandCategoriesCheckBoxList chlistBrand;
		protected System.Web.UI.WebControls.TextBox txtRemark;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtRemarkTip;
		protected System.Web.UI.WebControls.TextBox txtTypeName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtTypeNameTip;
		private int typeId;
		private void btnEditProductType_Click(object sender, System.EventArgs e)
		{
			ProductTypeInfo productType = new ProductTypeInfo
			{
				TypeId = this.typeId,
				TypeName = this.txtTypeName.Text,
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
			if (this.ValidationProductType(productType) && ProductTypeHelper.UpdateProductType(productType))
			{
				this.ShowMsg("修改成功", true);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["typeId"]))
			{
				int.TryParse(this.Page.Request.QueryString["typeId"], out this.typeId);
			}
			this.btnEditProductType.Click += new System.EventHandler(this.btnEditProductType_Click);
			if (!this.Page.IsPostBack)
			{
				this.chlistBrand.DataBind();
				ProductTypeInfo productType = ProductTypeHelper.GetProductType(this.typeId);
				if (productType == null)
				{
					base.GotoResourceNotFound();
				}
				else
				{
					this.txtTypeName.Text = productType.TypeName;
					this.txtRemark.Text = productType.Remark;
					foreach (System.Web.UI.WebControls.ListItem item in this.chlistBrand.Items)
					{
						if (productType.Brands.Contains(int.Parse(item.Value)))
						{
							item.Selected = true;
						}
					}
				}
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
