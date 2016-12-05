using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
    public class AddBusinessArticle : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnAddBA;
        protected KindeditorControl fcContent;
        protected System.Web.UI.WebControls.FileUpload fileUpload;
        protected System.Web.UI.WebControls.TextBox txtSummary;
        protected System.Web.UI.WebControls.TextBox txtBATitle;

        protected void btnAddBA_Click(object sender, System.EventArgs e)
        {
            string str = string.Empty;
            if (this.fileUpload.HasFile)
            {
                try
                {
                    str = VShopHelper.UploadBusinessArticleImage(this.fileUpload.PostedFile);
                }
                catch
                {
                    this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
                    return;
                }
            }
            BusinessArticleInfo target = new BusinessArticleInfo
            {
                Title = this.txtBATitle.Text.Trim(),
                Summary = this.txtSummary.Text.Trim(),
                IconUrl = str,
                ArtContent = this.fcContent.Text,
                AddedDate = System.DateTime.Now
            };
            ValidationResults results = Validation.Validate<BusinessArticleInfo>(target, new string[]
				{
					"ValBusinessArticleInfo"
				});
            string msg = string.Empty;
            if (results.IsValid)
            {
                int num;

                if (VShopHelper.CreateBusinessArticle(target, out num) && num > 0)
                {
                    this.ShowMsg("添加文章成功", true);
                    base.Response.Redirect(Globals.GetAdminAbsolutePath("/vshop/BusinessArticleList.aspx"), true);
                    return;
                }
                else
                {
                    this.ShowMsg("添加文章错误", false);
                }
            }
            else
            {
                foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
                {
                    msg += Formatter.FormatErrorMessage(result.Message);
                }
                this.ShowMsg(msg, false);
            }

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }
    }
}
