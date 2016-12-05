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
    public class EditBusinessArticle : AdminPage
	{
		protected ImageLinkButton btnPicDelete;
        protected System.Web.UI.WebControls.Button btnUpdateBA;
		protected KindeditorControl fcContent;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected HiImage imgPic;
        private int articleId;
        protected System.Web.UI.WebControls.TextBox txtSummary;
        protected System.Web.UI.WebControls.TextBox txtBATitle;

		protected void btnPicDelete_Click(object sender, System.EventArgs e)
		{
            BusinessArticleInfo busarticle = VShopHelper.GetBusinessArticle(this.articleId);
            
			try
			{
                ResourcesHelper.DeleteImage(busarticle.IconUrl);
			}
			catch
			{
			}
            busarticle.IconUrl = (this.imgPic.ImageUrl = null);

            if (VShopHelper.UpdateBusinessArticle(busarticle))
			{
				this.btnPicDelete.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
				this.imgPic.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
			}
		}
		protected void btnUpdateBA_Click(object sender, System.EventArgs e)
		{
            BusinessArticleInfo busarticle = VShopHelper.GetBusinessArticle(this.articleId);
			
			
				if (this.fileUpload.HasFile)
				{
					try
					{
                        ResourcesHelper.DeleteImage(busarticle.IconUrl);
                        busarticle.IconUrl = VShopHelper.UploadBusinessArticleImage(this.fileUpload.PostedFile);
                        this.imgPic.ImageUrl = busarticle.IconUrl;
					}
					catch
					{
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
						return;
					}
				}
                busarticle.ArticleId = this.articleId;
                busarticle.Title = this.txtBATitle.Text.Trim();
                busarticle.Summary = this.txtSummary.Text.Trim();
                busarticle.IconUrl = busarticle.IconUrl;
                busarticle.ArtContent = this.fcContent.Text;
                busarticle.AddedDate = System.DateTime.Now;
                ValidationResults results = Validation.Validate<BusinessArticleInfo>(busarticle, new string[]
				{
					"ValBusinessArticleInfo"
				});
				string msg = string.Empty;
				if (results.IsValid)
				{
                    if (VShopHelper.UpdateBusinessArticle(busarticle))
					{
						this.ShowMsg("已经成功修改当前文章", true);
                        base.Response.Redirect(Globals.GetAdminAbsolutePath("/vshop/BusinessArticleList.aspx"), true);
                        return;
					}
					else
					{
                        this.ShowMsg("修改文章失败", false);
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
            if (!int.TryParse(this.Page.Request.QueryString["ArticleId"], out this.articleId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				if (!this.Page.IsPostBack)
				{
                    BusinessArticleInfo busarticle = VShopHelper.GetBusinessArticle(this.articleId);
                    if (busarticle == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
                        Globals.EntityCoding(busarticle, false);
                        this.txtBATitle.Text = busarticle.Title;
                        this.txtSummary.Text = busarticle.Summary;
                        this.imgPic.ImageUrl = busarticle.IconUrl;
                        this.fcContent.Text = busarticle.ArtContent;
						this.btnPicDelete.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
					}
				}
			}
		}
	}
}
