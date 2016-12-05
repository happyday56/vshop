using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.BrandCategories)]
	public class AddBrandCategory : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddBrandCategory;
		protected System.Web.UI.WebControls.Button btnSave;
		protected ProductTypesCheckBoxList chlistProductTypes;
		protected KindeditorControl fckDescription;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected System.Web.UI.WebControls.TextBox txtBrandName;
		protected System.Web.UI.WebControls.TextBox txtCompanyUrl;
		protected System.Web.UI.WebControls.TextBox txtkeyword;
		protected System.Web.UI.WebControls.TextBox txtMetaDescription;
		protected System.Web.UI.WebControls.TextBox txtReUrl;
		protected void btnAddBrandCategory_Click(object sender, System.EventArgs e)
		{
			BrandCategoryInfo brandCategoryInfo = this.GetBrandCategoryInfo();
			if (this.fileUpload.HasFile)
			{
				try
				{
					brandCategoryInfo.Logo = CatalogHelper.UploadBrandCategorieImage(this.fileUpload.PostedFile);
				}
				catch
				{
					this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
					return;
				}
			}
			if (this.ValidationBrandCategory(brandCategoryInfo))
			{
				if (CatalogHelper.AddBrandCategory(brandCategoryInfo))
				{
					this.txtBrandName.Text = "";
					this.txtCompanyUrl.Text = "";
					this.txtkeyword.Text = "";
					this.txtMetaDescription.Text = "";
					this.txtReUrl.Text = "";
					this.fckDescription.Text = "";
					this.ShowMsg("成功添加品牌分类", true);
				}
				else
				{
					this.ShowMsg("添加品牌分类失败", true);
				}
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			BrandCategoryInfo brandCategoryInfo = this.GetBrandCategoryInfo();
			if (this.fileUpload.HasFile)
			{
				try
				{
					brandCategoryInfo.Logo = CatalogHelper.UploadBrandCategorieImage(this.fileUpload.PostedFile);
					if (this.ValidationBrandCategory(brandCategoryInfo))
					{
						if (CatalogHelper.AddBrandCategory(brandCategoryInfo))
						{
							base.Response.Redirect(Globals.GetAdminAbsolutePath("/product/BrandCategories.aspx"), true);
						}
						else
						{
							this.ShowMsg("添加品牌分类失败", true);
						}
					}
					return;
				}
				catch
				{
					this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
					return;
				}
			}
			this.ShowMsg("请上传一张品牌logo图片", false);
		}
		private BrandCategoryInfo GetBrandCategoryInfo()
		{
			BrandCategoryInfo info = new BrandCategoryInfo
			{
				BrandName = Globals.HtmlEncode(this.txtBrandName.Text.Trim())
			};
			if (!string.IsNullOrEmpty(this.txtCompanyUrl.Text))
			{
				info.CompanyUrl = this.txtCompanyUrl.Text.Trim();
			}
			else
			{
				info.CompanyUrl = null;
			}
			info.RewriteName = Globals.HtmlEncode(this.txtReUrl.Text.Trim());
			info.MetaKeywords = Globals.HtmlEncode(this.txtkeyword.Text.Trim());
			info.MetaDescription = Globals.HtmlEncode(this.txtMetaDescription.Text.Trim());
			System.Collections.Generic.IList<int> list = new System.Collections.Generic.List<int>();
			foreach (System.Web.UI.WebControls.ListItem item in this.chlistProductTypes.Items)
			{
				if (item.Selected)
				{
					list.Add(int.Parse(item.Value));
				}
			}
			info.ProductTypes = list;
			info.Description = ((!string.IsNullOrEmpty(this.fckDescription.Text) && this.fckDescription.Text.Length > 0) ? this.fckDescription.Text : null);
			return info;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnAddBrandCategory.Click += new System.EventHandler(this.btnAddBrandCategory_Click);
			if (!base.IsPostBack)
			{
				this.chlistProductTypes.DataBind();
			}
		}
		private bool ValidationBrandCategory(BrandCategoryInfo brandCategory)
		{
			ValidationResults results = Validation.Validate<BrandCategoryInfo>(brandCategory, new string[]
			{
				"ValBrandCategory"
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
