using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class ImageReplace : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSaveImageData;
		protected System.Web.UI.WebControls.FileUpload FileUpload1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.WebControls.HiddenField RePlaceId;
		protected System.Web.UI.WebControls.HiddenField RePlaceImg;
		protected void btnSaveImageData_Click(object sender, System.EventArgs e)
		{
			string str = this.RePlaceImg.Value;
			int photoId = System.Convert.ToInt32(this.RePlaceId.Value);
			string photoPath = GalleryHelper.GetPhotoPath(photoId);
			string str2 = photoPath.Substring(photoPath.LastIndexOf("."));
			string extension = string.Empty;
			string str3 = string.Empty;
			try
			{
				System.Web.HttpPostedFile postedFile = base.Request.Files[0];
				extension = System.IO.Path.GetExtension(postedFile.FileName);
				if (str2 != extension)
				{
					this.ShowMsg("上传图片类型与原文件类型不一致！", false);
				}
				else
				{
					string str4 = Globals.GetStoragePath() + "/gallery";
					str3 = photoPath.Substring(photoPath.LastIndexOf("/") + 1);
					string str5 = str.Substring(str.LastIndexOf("/") - 6, 6);
					string virtualPath = str4 + "/" + str5 + "/";
					int contentLength = postedFile.ContentLength;
					string path = base.Request.MapPath(virtualPath);
					string text = str5 + "/" + str3;
					System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(path);
					if (!info.Exists)
					{
						info.Create();
					}
					if (!ResourcesHelper.CheckPostedFile(postedFile))
					{
						this.ShowMsg("文件上传的类型不正确！", false);
					}
					else
					{
						if (contentLength >= 2048000)
						{
							this.ShowMsg("图片文件已超过网站限制大小！", false);
						}
						else
						{
							postedFile.SaveAs(base.Request.MapPath(virtualPath + str3));
							GalleryHelper.ReplacePhoto(photoId, contentLength);
							this.CloseWindow();
						}
					}
				}
			}
			catch
			{
				this.ShowMsg("替换文件错误!", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack && !string.IsNullOrEmpty(this.Page.Request.QueryString["imgsrc"]) && !string.IsNullOrEmpty(this.Page.Request.QueryString["imgId"]))
			{
				string str = Globals.HtmlDecode(this.Page.Request.QueryString["imgsrc"]);
				string str2 = Globals.HtmlDecode(this.Page.Request.QueryString["imgId"]);
				this.RePlaceImg.Value = str;
				this.RePlaceId.Value = str2;
			}
			this.btnSaveImageData.Click += new System.EventHandler(this.btnSaveImageData_Click);
		}
	}
}
