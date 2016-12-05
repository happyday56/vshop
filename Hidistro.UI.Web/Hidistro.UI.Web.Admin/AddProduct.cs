using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hishop.Components.Validation;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.AddProducts)]
    public class AddProduct : ProductBasePage
    {
        protected System.Web.UI.WebControls.Button btnAdd;
        private int categoryId;
        protected System.Web.UI.WebControls.CheckBox ChkisfreeShipping;
        protected System.Web.UI.WebControls.CheckBox chkSkuEnabled;
        protected System.Web.UI.WebControls.CheckBox ckbIsDownPic;
        protected BrandCategoriesDropDownList dropBrandCategories;
        protected ProductTypeDownList dropProductTypes;
        protected KindeditorControl editDescription;
        protected System.Web.UI.HtmlControls.HtmlGenericControl l_tags;
        protected System.Web.UI.WebControls.Literal litCategoryName;
        protected ProductTagsLiteral litralProductTag;
        protected System.Web.UI.WebControls.HyperLink lnkEditCategory;
        protected System.Web.UI.WebControls.RadioButton radInStock;
        protected System.Web.UI.WebControls.RadioButton radOnSales;
        protected System.Web.UI.WebControls.RadioButton radUnSales;
        protected Script Script1;
        protected Script Script2;
        protected TrimTextBox txtAttributes;
        protected TrimTextBox txtCostPrice;
        protected TrimTextBox txtMarketPrice;
        protected TrimTextBox txtMemberPrices;
        protected TrimTextBox txtProductCode;
        protected TrimTextBox txtProductName;
        protected TrimTextBox txtProductTag;
        protected TrimTextBox txtSalePrice;
        protected TrimTextBox txtShortDescription;
        protected TrimTextBox txtSku;
        protected TrimTextBox txtSkus;
        protected TrimTextBox txtStock;
        protected TrimTextBox txtUnit;
        protected TrimTextBox txtWeight;
        protected ProductFlashUpload ucFlashUpload1;
        protected TrimTextBox txtShowSaleCounts;
        protected TrimTextBox txtVirtualPointRate;
        protected Literal litVPName;
        protected System.Web.UI.WebControls.FileUpload homePicUrlFileUpload;

        protected System.Web.UI.WebControls.RadioButton radOnHome;
        protected System.Web.UI.WebControls.RadioButton radUnHome;
        protected RadioButton radOnCross;
        protected RadioButton radUnCross;
        protected TrimTextBox txtMaxCross;

        //protected ImageUploader uploader1;
        //protected ImageUploader uploader2;
        //protected ImageUploader uploader3;
        //protected ImageUploader uploader4;
        //protected ImageUploader uploader5;
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            decimal num3;
            decimal? nullable;
            decimal? nullable2;
            int num4;
            decimal? nullable3;
            int num5;
            if (this.ValidateConverts(this.chkSkuEnabled.Checked, out num3, out nullable, out nullable2, out num4, out nullable3, out num5))
            {
                if (!this.chkSkuEnabled.Checked)
                {
                    if (num3 <= 0m)
                    {
                        this.ShowMsg("商品一口价必须大于0", false);
                        return;
                    }
                    if (nullable.HasValue && nullable.Value >= num3)
                    {
                        this.ShowMsg("商品成本价必须小于商品一口价", false);
                        return;
                    }
                }
                string text = this.editDescription.Text;
                if (this.ckbIsDownPic.Checked)
                {
                    text = base.DownRemotePic(text);
                }

                string str1 = this.ucFlashUpload1.Value.Trim();
                //this.ucFlashUpload1.Value = str1;
                string[] strArrays = str1.Split(new char[] { ',' });
                string[] strArrays1 = new string[] { "", "", "", "", "" };
                string[] strArrays2 = strArrays1;
                for (int i = 0; i < (int)strArrays.Length && i < 5; i++)
                {
                    strArrays2[i] = strArrays[i];
                }

                int tmpSaleCounts;
                int.TryParse(this.txtShowSaleCounts.Text, out tmpSaleCounts);
                decimal tmpVirtualPointRate;
                decimal.TryParse(this.txtVirtualPointRate.Text, out tmpVirtualPointRate);

                string homePicUrl = string.Empty;
                if (this.homePicUrlFileUpload.HasFile)
                {
                    try
                    {
                        homePicUrl = VShopHelper.UploadTopicImage(this.homePicUrlFileUpload.PostedFile);
                    }
                    catch
                    {
                    }
                }

                ProductInfo target = new ProductInfo
                {
                    CategoryId = this.categoryId,
                    TypeId = this.dropProductTypes.SelectedValue,
                    ProductName = this.txtProductName.Text,
                    ProductCode = this.txtProductCode.Text,
                    ShowSaleCounts = tmpSaleCounts,
                    MarketPrice = nullable2,
                    Unit = this.txtUnit.Text,
                    ImageUrl1 = strArrays2[0],
                    ImageUrl2 = strArrays2[1],
                    ImageUrl3 = strArrays2[2],
                    ImageUrl4 = strArrays2[3],
                    ImageUrl5 = strArrays2[4],
                    ThumbnailUrl40 = strArrays2[0].Replace("/images/", "/thumbs40/40_"),
                    ThumbnailUrl60 = strArrays2[0].Replace("/images/", "/thumbs60/60_"),
                    ThumbnailUrl100 = strArrays2[0].Replace("/images/", "/thumbs100/100_"),
                    ThumbnailUrl160 = strArrays2[0].Replace("/images/", "/thumbs160/160_"),
                    ThumbnailUrl180 = strArrays2[0].Replace("/images/", "/thumbs180/180_"),
                    ThumbnailUrl220 = strArrays2[0].Replace("/images/", "/thumbs220/220_"),
                    ThumbnailUrl310 = strArrays2[0].Replace("/images/", "/thumbs310/310_"),
                    ThumbnailUrl410 = strArrays2[0].Replace("/images/", "/thumbs410/410_"),
                    ShortDescription = this.txtShortDescription.Text,
                    IsfreeShipping = this.ChkisfreeShipping.Checked,
                    Description = (!string.IsNullOrEmpty(text) && text.Length > 0) ? text : null,
                    AddedDate = System.DateTime.Now,
                    BrandId = this.dropBrandCategories.SelectedValue,
                    MainCategoryPath = CatalogHelper.GetCategory(this.categoryId).Path + "|",
                    IsDistributorBuy = 0,
                    VirtualPointRate = tmpVirtualPointRate,
                    HomePicUrl = homePicUrl

                };
                ProductSaleStatus onSale = ProductSaleStatus.OnSale;
                if (this.radInStock.Checked)
                {
                    onSale = ProductSaleStatus.OnStock;
                }
                if (this.radUnSales.Checked)
                {
                    onSale = ProductSaleStatus.UnSale;
                }
                if (this.radOnSales.Checked)
                {
                    onSale = ProductSaleStatus.OnSale;
                }
                target.SaleStatus = onSale;
                if (this.radOnHome.Checked)
                {
                    target.IsDisplayHome = 1;
                }
                if (this.radUnHome.Checked)
                {
                    target.IsDisplayHome = 0;
                }
                if (this.radOnCross.Checked)
                {
                    target.IsCross = 1;
                }
                if (this.radUnCross.Checked)
                {
                    target.IsCross = 0;
                }

                if (string.IsNullOrEmpty(this.txtMaxCross.Text.Trim()))
                {
                    target.MaxCross = 1;
                }
                else
                {
                    target.MaxCross = int.Parse(this.txtMaxCross.Text.Trim());
                }

                ManagerInfo manager = ManagerHelper.GetCurrentManager();
                if (null != manager)
                {
                    target.AddUserId = manager.UserId;
                }
                else
                {
                    target.AddUserId = 0;
                }

                System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> attrs = null;
                System.Collections.Generic.Dictionary<string, SKUItem> skus;
                if (this.chkSkuEnabled.Checked)
                {
                    target.HasSKU = true;
                    skus = base.GetSkus(this.txtSkus.Text);
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, SKUItem> dictionary3 = new System.Collections.Generic.Dictionary<string, SKUItem>();
                    SKUItem item = new SKUItem
                    {
                        SkuId = "0",
                        SKU = this.txtSku.Text,
                        SalePrice = num3,
                        CostPrice = nullable.HasValue ? nullable.Value : 0m,
                        Stock = num4,
                        Weight = nullable3.HasValue ? nullable3.Value : 0m
                    };
                    dictionary3.Add("0", item);
                    skus = dictionary3;
                    if (this.txtMemberPrices.Text.Length > 0)
                    {
                        base.GetMemberPrices(skus["0"], this.txtMemberPrices.Text);
                    }
                }
                if (!string.IsNullOrEmpty(this.txtAttributes.Text) && this.txtAttributes.Text.Length > 0)
                {
                    attrs = base.GetAttributes(this.txtAttributes.Text);
                }
                ValidationResults validateResults = Validation.Validate<ProductInfo>(target, new string[]
				{
					"AddProduct"
				});
                if (!validateResults.IsValid)
                {
                    this.ShowMsg(validateResults);
                }
                else
                {
                    System.Collections.Generic.IList<int> tagsId = new System.Collections.Generic.List<int>();
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
                        for (int i = 0; i < array.Length; i++)
                        {
                            string str3 = array[i];
                            tagsId.Add(System.Convert.ToInt32(str3));
                        }
                    }
                    switch (ProductHelper.AddProduct(target, skus, attrs, tagsId))
                    {
                        case ProductActionStatus.Success:
                            this.ShowMsg("添加商品成功", true);
                            base.Response.Redirect(Globals.GetAdminAbsolutePath(string.Format("/product/AddProductComplete.aspx?categoryId={0}&productId={1}", this.categoryId, target.ProductId)), true);
                            return;
                        case ProductActionStatus.DuplicateName:
                            this.ShowMsg("添加商品失败，商品名称不能重复", false);
                            return;
                        case ProductActionStatus.DuplicateSKU:
                            this.ShowMsg("添加商品失败，商家编码不能重复", false);
                            return;
                        case ProductActionStatus.SKUError:
                            this.ShowMsg("添加商品失败，商家编码不能重复", false);
                            return;
                        case ProductActionStatus.AttributeError:
                            this.ShowMsg("添加商品失败，保存商品属性时出错", false);
                            return;
                        case ProductActionStatus.ProductTagEroor:
                            this.ShowMsg("添加商品失败，保存商品标签时出错", false);
                            return;
                    }
                    this.ShowMsg("添加商品失败，未知错误", false);
                }
            }
        }
        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["isCallback"]) && base.Request.QueryString["isCallback"] == "true")
            {
                base.DoCallback();
            }
            else
            {
                if (!int.TryParse(base.Request.QueryString["categoryId"], out this.categoryId))
                {
                    base.GotoResourceNotFound();
                }
                else
                {
                    SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

                    this.litVPName.Text = siteSettings.VirtualPointName;

                    if (!this.Page.IsPostBack)
                    {
                        this.litCategoryName.Text = CatalogHelper.GetFullCategory(this.categoryId);
                        CategoryInfo category = CatalogHelper.GetCategory(this.categoryId);
                        if (category == null)
                        {
                            base.GotoResourceNotFound();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.litralProductTag.Text))
                            {
                                this.l_tags.Visible = true;
                            }
                            this.lnkEditCategory.NavigateUrl = "SelectCategory.aspx?categoryId=" + this.categoryId.ToString(System.Globalization.CultureInfo.InvariantCulture);
                            this.dropProductTypes.DataBind();
                            this.dropProductTypes.SelectedValue = category.AssociatedProductType;
                            this.dropBrandCategories.DataBind();
                            this.txtProductCode.Text = (this.txtSku.Text = category.SKUPrefix + new System.Random(System.DateTime.Now.Millisecond).Next(1, 99999).ToString(System.Globalization.CultureInfo.InvariantCulture).PadLeft(5, '0'));
                        }

                        ManagerInfo manager = ManagerHelper.GetCurrentManager();
                        var currentPrivilege = ManagerHelper.GetPrivilegeByRoles(manager.RoleId);

                        bool isEditStatus = false;

                        if (null != currentPrivilege)
                        {
                            if (currentPrivilege.Contains(10009))
                            {
                                isEditStatus = true;
                            }
                        }

                        if (isEditStatus)
                        {
                            this.radOnSales.Enabled = true;
                            this.radInStock.Enabled = true;
                            this.radUnSales.Enabled = true;
                        }
                        else
                        {
                            this.radOnSales.Enabled = false;
                            this.radInStock.Enabled = false;
                            this.radUnSales.Enabled = false;
                        }
                        
                    }
                }
            }
        }
        private bool ValidateConverts(bool skuEnabled, out decimal salePrice, out decimal? costPrice, out decimal? marketPrice, out int stock, out decimal? weight, out int lineId)
        {
            string str = string.Empty;
            costPrice = new decimal?(0m);
            marketPrice = new decimal?(0m);
            weight = new decimal?(0m);
            stock = (lineId = 0);
            salePrice = 0m;
            if (this.txtProductCode.Text.Length > 20)
            {
                str += Formatter.FormatErrorMessage("商家编码的长度不能超过20个字符");
            }
            if (!string.IsNullOrEmpty(this.txtMarketPrice.Text))
            {
                decimal num;
                if (decimal.TryParse(this.txtMarketPrice.Text, out num))
                {
                    marketPrice = new decimal?(num);
                }
                else
                {
                    str += Formatter.FormatErrorMessage("请正确填写商品的市场价");
                }
            }
            if (!skuEnabled)
            {
                if (string.IsNullOrEmpty(this.txtSalePrice.Text) || !decimal.TryParse(this.txtSalePrice.Text, out salePrice))
                {
                    str += Formatter.FormatErrorMessage("请正确填写商品一口价");
                }
                if (!string.IsNullOrEmpty(this.txtCostPrice.Text))
                {
                    decimal num2;
                    if (decimal.TryParse(this.txtCostPrice.Text, out num2))
                    {
                        costPrice = new decimal?(num2);
                    }
                    else
                    {
                        str += Formatter.FormatErrorMessage("请正确填写商品的成本价");
                    }
                }
                if (string.IsNullOrEmpty(this.txtStock.Text) || !int.TryParse(this.txtStock.Text, out stock))
                {
                    str += Formatter.FormatErrorMessage("请正确填写商品的库存数量");
                }
                if (!string.IsNullOrEmpty(this.txtWeight.Text))
                {
                    decimal num3;
                    if (decimal.TryParse(this.txtWeight.Text, out num3))
                    {
                        weight = new decimal?(num3);
                    }
                    else
                    {
                        str += Formatter.FormatErrorMessage("请正确填写商品的重量");
                    }
                }
            }
            bool result;
            if (!string.IsNullOrEmpty(str))
            {
                this.ShowMsg(str, false);
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
