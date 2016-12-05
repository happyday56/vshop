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
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.EditProducts)]
    public class EditProduct : ProductBasePage
    {
        protected string ReUrl = "productonsales.aspx";
        protected System.Web.UI.WebControls.Button btnSave;
        private int categoryId;
        protected System.Web.UI.WebControls.CheckBox ChkisfreeShipping;
        protected System.Web.UI.WebControls.CheckBox chkSkuEnabled;
        protected System.Web.UI.WebControls.CheckBox ckbIsDownPic;
        protected BrandCategoriesDropDownList dropBrandCategories;
        protected ProductTypeDownList dropProductTypes;
        protected KindeditorControl fckDescription;
        protected System.Web.UI.HtmlControls.HtmlGenericControl l_tags;
        protected System.Web.UI.WebControls.Literal litCategoryName;
        protected ProductTagsLiteral litralProductTag;
        protected System.Web.UI.WebControls.HyperLink lnkEditCategory;
        private int productId;
        protected System.Web.UI.WebControls.RadioButton radInStock;
        protected System.Web.UI.WebControls.RadioButton radOnSales;
        protected System.Web.UI.WebControls.RadioButton radUnSales;
        protected Script Script1;
        protected Script Script2;
        //private string toline = "";
        protected TrimTextBox txtAttributes;
        protected TrimTextBox txtCostPrice;
        protected TrimTextBox txtDisplaySequence;
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
        protected HiImage imgCover;
        protected ImageLinkButton btnCoverDelete;

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
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (this.categoryId == 0)
            {
                this.categoryId = (int)this.ViewState["ProductCategoryId"];
            }
            int num;
            decimal num2;
            decimal? nullable;
            decimal? nullable2;
            int num3;
            decimal? nullable3;

            if (this.ValidateConverts(this.chkSkuEnabled.Checked, out num, out num2, out nullable, out nullable2, out num3, out nullable3))
            {
                if (!this.chkSkuEnabled.Checked)
                {
                    if (num2 <= 0m)
                    {
                        this.ShowMsg("商品一口价必须大于0", false);
                        return;
                    }
                    if (nullable.HasValue && nullable.Value >= num2)
                    {
                        this.ShowMsg("商品成本价必须小于商品一口价", false);
                        return;
                    }
                }
                string text = this.fckDescription.Text;
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

                if (string.IsNullOrEmpty(homePicUrl))
                {
                    if (string.IsNullOrEmpty(this.imgCover.ImageUrl))
                    {
                        homePicUrl = "";
                    }
                    else
                    {
                        homePicUrl = this.imgCover.ImageUrl;
                    }
                }

                ProductInfo target = new ProductInfo
                {
                    ProductId = this.productId,
                    CategoryId = this.categoryId,
                    TypeId = this.dropProductTypes.SelectedValue,
                    ProductName = this.txtProductName.Text,
                    ProductCode = this.txtProductCode.Text,
                    ShowSaleCounts = tmpSaleCounts,
                    DisplaySequence = num,
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

                CategoryInfo category = CatalogHelper.GetCategory(this.categoryId);
                if (category != null)
                {
                    target.MainCategoryPath = category.Path + "|";
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
                        SalePrice = num2,
                        CostPrice = nullable.HasValue ? nullable.Value : 0m,
                        Stock = num3,
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
                ValidationResults validateResults = Validation.Validate<ProductInfo>(target);
                if (!validateResults.IsValid)
                {
                    this.ShowMsg(validateResults);
                }
                else
                {
                    System.Collections.Generic.IList<int> tagIds = new System.Collections.Generic.List<int>();
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
                            tagIds.Add(System.Convert.ToInt32(str3));
                        }
                    }
                    switch (ProductHelper.UpdateProduct(target, skus, attrs, tagIds))
                    {
                        case ProductActionStatus.Success:
                            this.litralProductTag.SelectedValue = tagIds;
                            if (base.Request.QueryString["reurl"] != null)
                            {
                                this.ReUrl = base.Request.QueryString["reurl"].ToString();
                            }
                            this.ShowMsgAndReUrl("修改商品成功", true, this.ReUrl);
                            this.ShowMsg("修改商品成功", true);
                            break;
                        case ProductActionStatus.DuplicateName:
                            this.ShowMsg("修改商品失败，商品名称不能重复", false);
                            break;
                        case ProductActionStatus.DuplicateSKU:
                            this.ShowMsg("修改商品失败，商家编码不能重复", false);
                            break;
                        case ProductActionStatus.SKUError:
                            this.ShowMsg("修改商品失败，商家编码不能重复", false);
                            break;
                        case ProductActionStatus.AttributeError:
                            this.ShowMsg("修改商品失败，保存商品属性时出错", false);
                            break;
                        case ProductActionStatus.OffShelfError:
                            this.ShowMsg("修改商品失败， 子站没在零售价范围内的商品无法下架", false);
                            break;
                        case ProductActionStatus.ProductTagEroor:
                            this.ShowMsg("修改商品失败，保存商品标签时出错", false);
                            break;
                        default:
                            this.ShowMsg("修改商品失败，未知错误", false);
                            break;
                    }
                }
            }
        }
        private void LoadProduct(ProductInfo product, System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> attrs)
        {
            this.dropProductTypes.SelectedValue = product.TypeId;
            this.dropBrandCategories.SelectedValue = product.BrandId;
            this.txtDisplaySequence.Text = product.DisplaySequence.ToString();
            this.txtProductName.Text = Globals.HtmlDecode(product.ProductName);
            this.txtProductCode.Text = product.ProductCode;
            this.txtUnit.Text = product.Unit;
            this.txtShowSaleCounts.Text = product.ShowSaleCounts.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (product.MarketPrice.HasValue)
            {
                this.txtMarketPrice.Text = product.MarketPrice.Value.ToString("F2");
            }
            this.txtShortDescription.Text = product.ShortDescription;
            this.fckDescription.Text = product.Description;
            if (product.SaleStatus == ProductSaleStatus.OnSale)
            {
                this.radOnSales.Checked = true;
            }
            else
            {
                if (product.SaleStatus == ProductSaleStatus.UnSale)
                {
                    this.radUnSales.Checked = true;
                }
                else
                {
                    this.radInStock.Checked = true;
                }
            }
            if (product.IsDisplayHome == 1)
            {
                this.radOnHome.Checked = true;
            }
            else
            {
                this.radUnHome.Checked = true;
            }
            if (product.IsCross == 1)
            {
                this.radOnCross.Checked = true;
            }
            else
            {
                this.radUnCross.Checked = false;
            }

            this.txtMaxCross.Text = product.MaxCross.ToString();

            this.ChkisfreeShipping.Checked = product.IsfreeShipping;

            this.imgCover.ImageUrl = product.HomePicUrl;
            this.imgCover.Visible = !string.IsNullOrEmpty(product.HomePicUrl);

            string[] imageUrl1 = new string[] { product.ImageUrl1, ",", product.ImageUrl2, ",", product.ImageUrl3, ",", product.ImageUrl4, ",", product.ImageUrl5 };
            string str4 = string.Concat(imageUrl1);
            ProductFlashUpload productFlashUpload = this.ucFlashUpload1;
            string str5 = str4.Replace(",,", ",").Replace(",,", ",");
            char[] chrArray = new char[] { ',' };
            productFlashUpload.Value = str5.Trim(chrArray);

            if (attrs != null && attrs.Count > 0)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<xml><attributes>");
                foreach (int num in attrs.Keys)
                {
                    builder.Append("<item attributeId=\"").Append(num.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" usageMode=\"").Append(((int)ProductTypeHelper.GetAttribute(num).UsageMode).ToString()).Append("\" >");
                    foreach (int num2 in attrs[num])
                    {
                        builder.Append("<attValue valueId=\"").Append(num2.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" />");
                    }
                    builder.Append("</item>");
                }
                builder.Append("</attributes></xml>");
                this.txtAttributes.Text = builder.ToString();
            }
            this.chkSkuEnabled.Checked = product.HasSKU;
            if (product.HasSKU)
            {
                System.Text.StringBuilder builder2 = new System.Text.StringBuilder();
                builder2.Append("<xml><productSkus>");
                foreach (string str in product.Skus.Keys)
                {
                    SKUItem item = product.Skus[str];
                    string str2 = string.Concat(new string[]
					{
						"<item skuCode=\"",
						item.SKU,
						"\" salePrice=\"",
						item.SalePrice.ToString("F2"),
						"\" costPrice=\"",
						(item.CostPrice > 0m) ? item.CostPrice.ToString("F2") : "",
						"\" qty=\"",
						item.Stock.ToString(System.Globalization.CultureInfo.InvariantCulture),
						"\" weight=\"",
						(item.Weight > 0m) ? item.Weight.ToString("F2") : "",
						"\"><skuFields>"
					});
                    foreach (int num3 in item.SkuItems.Keys)
                    {
                        string[] strArray2 = new string[]
						{
							"<sku attributeId=\"",
							num3.ToString(System.Globalization.CultureInfo.InvariantCulture),
							"\" valueId=\"",
							item.SkuItems[num3].ToString(System.Globalization.CultureInfo.InvariantCulture),
							"\" />"
						};
                        string str3 = string.Concat(strArray2);
                        str2 += str3;
                    }
                    str2 += "</skuFields>";
                    if (item.MemberPrices.Count > 0)
                    {
                        str2 += "<memberPrices>";
                        foreach (int num4 in item.MemberPrices.Keys)
                        {
                            decimal num5 = item.MemberPrices[num4];
                            str2 += string.Format("<memberGrande id=\"{0}\" price=\"{1}\" />", num4.ToString(System.Globalization.CultureInfo.InvariantCulture), num5.ToString("F2"));
                        }
                        str2 += "</memberPrices>";
                    }
                    str2 += "</item>";
                    builder2.Append(str2);
                }
                builder2.Append("</productSkus></xml>");
                this.txtSkus.Text = builder2.ToString();
            }
            SKUItem defaultSku = product.DefaultSku;
            this.txtSku.Text = product.SKU;
            this.txtSalePrice.Text = defaultSku.SalePrice.ToString("F2");
            this.txtCostPrice.Text = ((defaultSku.CostPrice > 0m) ? defaultSku.CostPrice.ToString("F2") : "");
            if (product.VirtualPointRate.HasValue)
            {
                this.txtVirtualPointRate.Text = product.VirtualPointRate.Value.ToString("F2");
            }

            this.txtStock.Text = defaultSku.Stock.ToString(System.Globalization.CultureInfo.InvariantCulture);
            this.txtWeight.Text = ((defaultSku.Weight > 0m) ? defaultSku.Weight.ToString("F2") : "");
            if (defaultSku.MemberPrices.Count > 0)
            {
                this.txtMemberPrices.Text = "<xml><gradePrices>";
                foreach (int num6 in defaultSku.MemberPrices.Keys)
                {
                    decimal num7 = defaultSku.MemberPrices[num6];
                    this.txtMemberPrices.Text = this.txtMemberPrices.Text + string.Format("<grande id=\"{0}\" price=\"{1}\" />", num6.ToString(System.Globalization.CultureInfo.InvariantCulture), num7.ToString("F2"));
                }
                this.txtMemberPrices.Text = this.txtMemberPrices.Text + "</gradePrices></xml>";
            }
        }
        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            int.TryParse(base.Request.QueryString["productId"], out this.productId);
            int.TryParse(base.Request.QueryString["categoryId"], out this.categoryId);

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            this.litVPName.Text = siteSettings.VirtualPointName;

            if (!this.Page.IsPostBack)
            {
                System.Collections.Generic.IList<int> tagsId = null;
                System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> dictionary;
                ProductInfo product = ProductHelper.GetProductDetails(this.productId, out dictionary, out tagsId);
                if (product == null)
                {
                    base.GotoResourceNotFound();
                }
                else
                {
                    if (!string.IsNullOrEmpty(base.Request.QueryString["categoryId"]))
                    {
                        this.litCategoryName.Text = CatalogHelper.GetFullCategory(this.categoryId);
                        this.ViewState["ProductCategoryId"] = this.categoryId;
                        this.lnkEditCategory.NavigateUrl = "SelectCategory.aspx?categoryId=" + this.categoryId.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        this.litCategoryName.Text = CatalogHelper.GetFullCategory(product.CategoryId);
                        this.ViewState["ProductCategoryId"] = product.CategoryId;
                        this.lnkEditCategory.NavigateUrl = "SelectCategory.aspx?categoryId=" + product.CategoryId.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    this.lnkEditCategory.NavigateUrl = this.lnkEditCategory.NavigateUrl + "&productId=" + product.ProductId.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    this.litralProductTag.SelectedValue = tagsId;
                    if (tagsId.Count > 0)
                    {
                        foreach (int num in tagsId)
                        {
                            this.txtProductTag.Text = this.txtProductTag.Text + num.ToString() + ",";
                        }
                        this.txtProductTag.Text = this.txtProductTag.Text.Substring(0, this.txtProductTag.Text.Length - 1);
                    }
                    this.dropProductTypes.DataBind();
                    this.dropBrandCategories.DataBind();
                    this.LoadProduct(product, dictionary);

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

        private void btnCoverDelete_Click(object sender, System.EventArgs e)
        {
            this.imgCover.ImageUrl = string.Empty;
            this.btnCoverDelete.Visible = !string.IsNullOrEmpty(this.imgCover.ImageUrl);
            this.imgCover.Visible = !string.IsNullOrEmpty(this.imgCover.ImageUrl);
        }


        private bool ValidateConverts(bool skuEnabled, out int displaySequence, out decimal salePrice, out decimal? costPrice, out decimal? marketPrice, out int stock, out decimal? weight)
        {
            string str = string.Empty;
            costPrice = new decimal?(0m);
            marketPrice = new decimal?(0m);
            weight = new decimal?(0m);
            displaySequence = (stock = 0);
            salePrice = 0m;
            if (string.IsNullOrEmpty(this.txtDisplaySequence.Text) || !int.TryParse(this.txtDisplaySequence.Text, out displaySequence))
            {
                str += Formatter.FormatErrorMessage("请正确填写商品排序");
            }
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
