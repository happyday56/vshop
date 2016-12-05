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
using System.IO;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.product
{
    [PrivilegeCheck(Privilege.ProductBatchUpload)]
    public partial class ImportFromTB : AdminPage
    {
        string _dataPath;

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
            System.Collections.Generic.Dictionary<string, string> importAdapters = TransferHelper.GetImportAdapters(new YfxTarget("1.2"), "淘宝助理");
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
                int categoryId = this.dropCategories.SelectedValue.Value;
                int? brandId = this.dropBrandList.SelectedValue;
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
                selectedValue = System.IO.Path.Combine(this._dataPath, selectedValue);
                if (!System.IO.File.Exists(selectedValue))
                {
                    this.ShowMsg("选择的数据包文件有问题！", false);
                }
                else
                {
                    importer.PrepareDataFiles(new object[] { selectedValue });
                    try
                    {
                        ProductHelper.ImportProducts((System.Data.DataTable)importer.ParseProductData(new object[] { path })[0], categoryId, 0, brandId, delete, true);
                        System.IO.File.Delete(selectedValue);
                        System.IO.Directory.Delete(path, true);
                        this.BindFiles();
                        this.ShowMsg("此次商品批量导入操作已成功！", true);
                    }
                    catch
                    {
                        System.IO.File.Delete(selectedValue);
                        System.IO.Directory.Delete(path, true);
                        this.ShowMsg("选择的数据包文件有问题！", false);
                    }
                }
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
        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            this._dataPath = this.Page.Request.MapPath("~/storage/data/taobao");
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            if (!this.Page.IsPostBack)
            {
                this.dropCategories.DataBind();
                this.dropBrandList.DataBind();
                this.BindImporters();
                this.BindFiles();
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }
    }
}
