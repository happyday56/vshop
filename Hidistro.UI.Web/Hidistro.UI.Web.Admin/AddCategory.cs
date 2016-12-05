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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.AddProductCategory)]
	public class AddCategory : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSaveAddCategory;
		protected System.Web.UI.WebControls.Button btnSaveCategory;
		protected ProductCategoriesDropDownList dropCategories;
		protected ProductTypeDownList dropProductTypes;
		protected KindeditorControl fckNotes1;
		protected KindeditorControl fckNotes2;
		protected KindeditorControl fckNotes3;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected System.Web.UI.HtmlControls.HtmlGenericControl liURL;
		protected System.Web.UI.WebControls.TextBox txtCategoryName;
		protected System.Web.UI.WebControls.TextBox txtfirst;
		protected System.Web.UI.WebControls.TextBox txtPageDesc;
		protected System.Web.UI.WebControls.TextBox txtPageKeyTitle;
		protected System.Web.UI.WebControls.TextBox txtPageKeyWords;
		protected System.Web.UI.WebControls.TextBox txtRewriteName;
		protected System.Web.UI.WebControls.TextBox txtsecond;
		protected System.Web.UI.WebControls.TextBox txtsecondtothree;
		protected System.Web.UI.WebControls.TextBox txtseoncdtoone;
		protected System.Web.UI.WebControls.TextBox txtSKUPrefix;
		protected System.Web.UI.WebControls.TextBox txtthird;
		protected System.Web.UI.WebControls.TextBox txtthirdtoone;
        protected System.Web.UI.WebControls.FileUpload coverFileUpload;
        protected System.Web.UI.WebControls.RadioButton radOnHome;
        protected System.Web.UI.WebControls.RadioButton radUnHome;

        private void btnSaveAddCategory_Click(object sender, System.EventArgs e)
		{
			CategoryInfo category = this.GetCategory();
			if (category != null)
			{
				if (CatalogHelper.AddCategory(category) == CategoryActionStatus.Success)
				{
					this.ShowMsg("成功添加了商品分类", true);
					this.dropCategories.DataBind();
					this.dropProductTypes.DataBind();
					this.txtCategoryName.Text = string.Empty;
					this.txtSKUPrefix.Text = string.Empty;
					this.txtRewriteName.Text = string.Empty;
					this.txtPageKeyTitle.Text = string.Empty;
					this.txtPageKeyWords.Text = string.Empty;
					this.txtPageDesc.Text = string.Empty;
					this.fckNotes1.Text = string.Empty;
					this.fckNotes2.Text = string.Empty;
					this.fckNotes3.Text = string.Empty;
				}
				else
				{
					this.ShowMsg("添加商品分类失败,未知错误", false);
				}
			}
		}
		private void btnSaveCategory_Click(object sender, System.EventArgs e)
		{
			CategoryInfo category = this.GetCategory();
			if (category != null)
			{
				if (CatalogHelper.AddCategory(category) == CategoryActionStatus.Success)
				{
					base.Response.Redirect(Globals.GetAdminAbsolutePath("/product/ManageCategories.aspx"), true);
				}
				else
				{
					this.ShowMsg("添加商品分类失败,未知错误", false);
				}
			}
		}
		private CategoryInfo GetCategory()
		{
			string str = string.Empty;
			if (this.fileUpload.HasFile)
			{
				try
				{
					str = VShopHelper.UploadTopicImage(this.fileUpload.PostedFile);
				}
				catch
				{
				}
			}
            string cover = string.Empty;
            if (this.coverFileUpload.HasFile)
            {
                try
                {
                    cover = VShopHelper.UploadTopicImage(this.coverFileUpload.PostedFile);
                }
                catch
                {
                }
            }
            CategoryInfo target = new CategoryInfo
			{
				IconUrl = str,
				Name = this.txtCategoryName.Text.Trim(),
				ParentCategoryId = this.dropCategories.SelectedValue,
				SKUPrefix = this.txtSKUPrefix.Text.Trim(),
				AssociatedProductType = this.dropProductTypes.SelectedValue,
                CoverUrl=cover
			};
			if (!string.IsNullOrEmpty(this.txtRewriteName.Text.Trim()))
			{
				target.RewriteName = this.txtRewriteName.Text.Trim();
			}
			else
			{
				target.RewriteName = null;
			}
			target.MetaTitle = this.txtPageKeyTitle.Text.Trim();
			target.MetaKeywords = this.txtPageKeyWords.Text.Trim();
			target.MetaDescription = this.txtPageDesc.Text.Trim();
			target.Notes1 = this.fckNotes1.Text;
			target.Notes2 = this.fckNotes2.Text;
			target.Notes3 = this.fckNotes3.Text;
			target.DisplaySequence = 0;
            if (this.radOnHome.Checked)
            {
                target.IsDisplayHome = 1;
            }
            if (this.radUnHome.Checked)
            {
                target.IsDisplayHome = 0;
            }

			CategoryInfo result2;
			if (target.ParentCategoryId.HasValue)
			{
				CategoryInfo category = CatalogHelper.GetCategory(target.ParentCategoryId.Value);
				if (category == null || category.Depth >= 5)
				{
					this.ShowMsg(string.Format("您选择的上级分类有误，商品分类最多只支持{0}级分类", 5), false);
					result2 = null;
					return result2;
				}
				if (string.IsNullOrEmpty(target.Notes1))
				{
					target.Notes1 = category.Notes1;
				}
				if (string.IsNullOrEmpty(target.Notes2))
				{
					target.Notes2 = category.Notes2;
				}
				if (string.IsNullOrEmpty(target.Notes3))
				{
					target.Notes3 = category.Notes3;
				}
				if (string.IsNullOrEmpty(target.RewriteName))
				{
					target.RewriteName = category.RewriteName;
				}
			}
            // 添加于2015-09-28
            if (string.IsNullOrEmpty(this.txtfirst.Text) || string.IsNullOrEmpty(this.txtsecond.Text) || string.IsNullOrEmpty(this.txtthird.Text))
            // 注销于2015－09－28
            //if (string.IsNullOrEmpty(this.txtfirst.Text) || string.IsNullOrEmpty(this.txtseoncdtoone.Text) || 
            //    string.IsNullOrEmpty(this.txtthirdtoone.Text) || string.IsNullOrEmpty(this.txtsecond.Text) || 
            //    string.IsNullOrEmpty(this.txtsecondtothree.Text) || string.IsNullOrEmpty(this.txtthird.Text))
			{
				this.ShowMsg("分佣设置不允许为空！", false);
				result2 = null;
			}
			else
			{
                // 添加于2015-09-28
                target.FirstCommission = this.txtfirst.Text;
                target.SecondCommission = this.txtsecond.Text;
                target.ThirdCommission = this.txtthird.Text;

                // 注销于2015－09－28
				//target.FirstCommission = string.Concat(new string[]
				//{
				//	this.txtfirst.Text,
				//	"|",
				//	this.txtseoncdtoone.Text,
				//	"|",
				//	this.txtthirdtoone.Text
				//});
				//target.SecondCommission = this.txtsecond.Text + "|" + this.txtsecondtothree.Text;
				//target.ThirdCommission = this.txtthird.Text;
				ValidationResults results = Validation.Validate<CategoryInfo>(target, new string[]
				{
					"ValCategory"
				});
				string msg = string.Empty;
				if (results.IsValid)
				{
					result2 = target;
				}
				else
				{
					foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
					{
						msg += Formatter.FormatErrorMessage(result.Message);
					}
					this.ShowMsg(msg, false);
					result2 = null;
				}
			}
			return result2;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSaveCategory.Click += new System.EventHandler(this.btnSaveCategory_Click);
			this.btnSaveAddCategory.Click += new System.EventHandler(this.btnSaveAddCategory_Click);
			if (!string.IsNullOrEmpty(base.Request["isCallback"]) && base.Request["isCallback"] == "true")
			{
				int result = 0;
				int.TryParse(base.Request["parentCategoryId"], out result);
				CategoryInfo category = CatalogHelper.GetCategory(result);
				if (category != null)
				{
					base.Response.Clear();
					base.Response.ContentType = "application/json";
					base.Response.Write("{ ");
					base.Response.Write(string.Format("\"SKUPrefix\":\"{0}\"", category.SKUPrefix));
					base.Response.Write("}");
					base.Response.End();
				}
			}
			if (!this.Page.IsPostBack)
			{
				this.dropCategories.DataBind();
				this.dropProductTypes.DataBind();
			}
		}
	}
}
