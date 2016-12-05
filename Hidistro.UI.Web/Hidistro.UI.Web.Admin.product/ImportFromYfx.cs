using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.TransferManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin.product
{
	[PrivilegeCheck(Privilege.ProductBatchUpload)]
	public class ImportFromYfx : AdminPage
	{
		private string _dataPath;
		protected System.Web.UI.WebControls.Button btnImport;
		protected System.Web.UI.WebControls.Button btnUpload;
		protected System.Web.UI.WebControls.CheckBox chkFlag;
		protected System.Web.UI.WebControls.CheckBox chkIncludeCostPrice;
		protected System.Web.UI.WebControls.CheckBox chkIncludeImages;
		protected System.Web.UI.WebControls.CheckBox chkIncludeStock;
		protected BrandCategoriesDropDownList dropBrandList;
		protected ProductCategoriesDropDownList dropCategories;
		protected System.Web.UI.WebControls.DropDownList dropFiles;
		protected System.Web.UI.WebControls.DropDownList dropImportVersions;
		protected System.Web.UI.WebControls.FileUpload fileUploader;
		protected System.Web.UI.WebControls.Literal lblQuantity;
		protected System.Web.UI.WebControls.Literal lblVersion;
		protected System.Web.UI.WebControls.RadioButton radInStock;
		protected System.Web.UI.WebControls.RadioButton radOnSales;
		protected System.Web.UI.WebControls.RadioButton radUnSales;
		protected System.Web.UI.WebControls.TextBox txtMappedTypes;
		protected System.Web.UI.WebControls.TextBox txtProductTypeXml;
		protected System.Web.UI.WebControls.TextBox txtPTXml;
		private void BindFiles()
		{
			this.dropFiles.Items.Clear();
			this.dropFiles.Items.Add(new System.Web.UI.WebControls.ListItem("-请选择-", ""));
			System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(this._dataPath);
			System.IO.FileInfo[] files = info.GetFiles("*.zip", System.IO.SearchOption.TopDirectoryOnly);
			for (int i = 0; i < files.Length; i++)
			{
				System.IO.FileInfo info2 = files[i];
				string name = info2.Name;
				this.dropFiles.Items.Add(new System.Web.UI.WebControls.ListItem(name, name));
			}
		}
		private void BindImporters()
		{
			this.dropImportVersions.Items.Clear();
			this.dropImportVersions.Items.Add(new System.Web.UI.WebControls.ListItem("-请选择-", ""));
			System.Collections.Generic.Dictionary<string, string> importAdapters = TransferHelper.GetImportAdapters(new YfxTarget("1.2"), "分销商城");
			foreach (string str in importAdapters.Keys)
			{
				this.dropImportVersions.Items.Add(new System.Web.UI.WebControls.ListItem(importAdapters[str], str));
			}
		}
		private void btnImport_Click(object sender, System.EventArgs e)
		{
			if (this.CheckItems())
			{
				string selectedValue = this.dropFiles.SelectedValue;
				string path = System.IO.Path.Combine(this._dataPath, System.IO.Path.GetFileNameWithoutExtension(selectedValue));
				ImportAdapter importer = TransferHelper.GetImporter(this.dropImportVersions.SelectedValue, new object[0]);
				System.Data.DataSet mappingSet = null;
				if (this.txtMappedTypes.Text.Length > 0)
				{
					XmlDocument document = new XmlDocument();
					document.LoadXml(this.txtMappedTypes.Text);
					mappingSet = (importer.CreateMapping(new object[]
					{
						document,
						path
					})[0] as System.Data.DataSet);
					ProductHelper.EnsureMapping(mappingSet);
				}
				bool includeCostPrice = this.chkIncludeCostPrice.Checked;
				bool includeStock = this.chkIncludeStock.Checked;
				bool includeImages = this.chkIncludeImages.Checked;
				int categoryId = this.dropCategories.SelectedValue.Value;
				int? bandId = this.dropBrandList.SelectedValue;
				ProductSaleStatus delete = ProductSaleStatus.Delete;
				if (this.radInStock.Checked)
				{
					delete = ProductSaleStatus.OnStock;
				}
				if (this.radUnSales.Checked)
				{
					delete = ProductSaleStatus.UnSale;
				}
				if (this.radOnSales.Checked)
				{
					delete = ProductSaleStatus.OnSale;
				}
				ProductHelper.ImportProducts((System.Data.DataSet)importer.ParseProductData(new object[]
				{
					mappingSet,
					path,
					includeCostPrice,
					includeStock,
					includeImages
				})[0], categoryId, 0, bandId, delete, includeCostPrice, includeStock, includeImages);
				System.IO.File.Delete(System.IO.Path.Combine(this._dataPath, selectedValue));
				System.IO.Directory.Delete(path, true);
				this.chkFlag.Checked = false;
				this.txtMappedTypes.Text = string.Empty;
				this.txtProductTypeXml.Text = string.Empty;
				this.txtPTXml.Text = string.Empty;
				this.OutputProductTypes();
				this.BindFiles();
				this.ShowMsg("此次商品批量导入操作已成功！", true);
			}
		}
		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			if (this.dropImportVersions.SelectedValue.Length == 0)
			{
				this.ShowMsg("请先选择一个导入插件", false);
			}
			else
			{
				if (!this.fileUploader.HasFile)
				{
					this.ShowMsg("请先选择一个数据包文件", false);
				}
				else
				{
					if (this.fileUploader.PostedFile.ContentLength == 0 || (this.fileUploader.PostedFile.ContentType != "application/x-zip-compressed" && this.fileUploader.PostedFile.ContentType != "application/zip" && this.fileUploader.PostedFile.ContentType != "application/octet-stream"))
					{
						this.ShowMsg("请上传正确的数据包文件", false);
					}
					else
					{
						string fileName = System.IO.Path.GetFileName(this.fileUploader.PostedFile.FileName);
						this.fileUploader.PostedFile.SaveAs(System.IO.Path.Combine(this._dataPath, fileName));
						this.BindFiles();
						this.dropFiles.SelectedValue = fileName;
						this.PrepareZipFile(fileName);
					}
				}
			}
		}
		private bool CheckItems()
		{
			string str = "";
			if (this.dropImportVersions.SelectedValue.Length == 0)
			{
				str += Formatter.FormatErrorMessage("请选择一个导入插件！");
			}
			if (this.dropFiles.SelectedValue.Length == 0)
			{
				str += Formatter.FormatErrorMessage("请选择要导入的数据包文件！");
			}
			if (!this.dropCategories.SelectedValue.HasValue)
			{
				str += Formatter.FormatErrorMessage("请选择要导入的商品分类！");
			}
			bool result;
			if (string.IsNullOrEmpty(str) && str.Length <= 0)
			{
				result = true;
			}
			else
			{
				this.ShowMsg(str, false);
				result = false;
			}
			return result;
		}
		private void DoCallback()
		{
			base.Response.Clear();
			base.Response.ContentType = "text/xml";
			string text = base.Request.QueryString["action"];
			if (text != null)
			{
				if (!(text == "getAttributes"))
				{
					if (text == "getValues")
					{
						AttributeInfo attribute = ProductTypeHelper.GetAttribute(int.Parse(base.Request.QueryString["attributeId"]));
						System.Text.StringBuilder builder2 = new System.Text.StringBuilder();
						builder2.Append("<xml><values>");
						if (attribute != null && attribute.AttributeValues.Count > 0)
						{
							foreach (AttributeValueInfo info3 in attribute.AttributeValues)
							{
								builder2.Append("<item valueId=\"").Append(info3.ValueId.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" valueStr=\"").Append(info3.ValueStr).Append("\" attributeId=\"").Append(info3.AttributeId.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" />");
							}
						}
						builder2.Append("</values></xml>");
						base.Response.Write(builder2.ToString());
					}
				}
				else
				{
					System.Collections.Generic.IList<AttributeInfo> attributes = ProductTypeHelper.GetAttributes(int.Parse(base.Request.QueryString["typeId"]));
					System.Text.StringBuilder builder3 = new System.Text.StringBuilder();
					builder3.Append("<xml><attributes>");
					foreach (AttributeInfo info4 in attributes)
					{
						builder3.Append("<item attributeId=\"").Append(info4.AttributeId.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" attributeName=\"").Append(info4.AttributeName).Append("\" typeId=\"").Append(info4.TypeId.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" />");
					}
					builder3.Append("</attributes></xml>");
					base.Response.Write(builder3.ToString());
				}
			}
			base.Response.End();
		}
		private void dropFiles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.dropFiles.SelectedValue.Length > 0 && this.dropImportVersions.SelectedValue.Length == 0)
			{
				this.ShowMsg("请先选择一个导入插件", false);
				this.dropFiles.SelectedValue = "";
			}
			else
			{
				this.PrepareZipFile(this.dropFiles.SelectedValue);
			}
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			if (base.Request.QueryString["isCallback"] == "true")
			{
				this.DoCallback();
			}
			else
			{
				this._dataPath = this.Page.Request.MapPath("~/storage/data/yfx");
				this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
				this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
				this.dropFiles.SelectedIndexChanged += new System.EventHandler(this.dropFiles_SelectedIndexChanged);
				if (!this.Page.IsPostBack)
				{
					this.dropCategories.DataBind();
					this.dropBrandList.DataBind();
					this.BindImporters();
					this.BindFiles();
					this.OutputProductTypes();
				}
			}
		}
		private void OutputProductTypes()
		{
			System.Collections.Generic.IList<ProductTypeInfo> productTypes = ProductTypeHelper.GetProductTypes();
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("<xml><types>");
			foreach (ProductTypeInfo info in productTypes)
			{
				builder.Append("<item typeId=\"").Append(info.TypeId.ToString(System.Globalization.CultureInfo.InvariantCulture)).Append("\" typeName=\"").Append(info.TypeName).Append("\" />");
			}
			builder.Append("</types></xml>");
			this.txtProductTypeXml.Text = builder.ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
		private void PrepareZipFile(string filename)
		{
			if (string.IsNullOrEmpty(filename) || filename.Length == 0)
			{
				this.chkFlag.Checked = false;
				this.txtPTXml.Text = string.Empty;
			}
			else
			{
				filename = System.IO.Path.Combine(this._dataPath, filename);
				if (!System.IO.File.Exists(filename))
				{
					this.chkFlag.Checked = false;
					this.txtPTXml.Text = string.Empty;
				}
				else
				{
					ImportAdapter importer = TransferHelper.GetImporter(this.dropImportVersions.SelectedValue, new object[0]);
					string str = importer.PrepareDataFiles(new object[]
					{
						filename
					});
					object[] objArray = importer.ParseIndexes(new object[]
					{
						str
					});
					this.lblVersion.Text = (string)objArray[0];
					this.lblQuantity.Text = objArray[1].ToString();
					this.chkIncludeCostPrice.Checked = (bool)objArray[2];
					this.chkIncludeStock.Checked = (bool)objArray[3];
					this.chkIncludeImages.Checked = (bool)objArray[4];
					this.txtPTXml.Text = (string)objArray[5];
					this.chkFlag.Checked = true;
				}
			}
		}
	}
}
