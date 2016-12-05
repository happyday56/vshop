using Hidistro.ControlPanel.Store;
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
	public class AddTopic : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddTopic;
		protected KindeditorControl fcContent;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected System.Web.UI.WebControls.TextBox txtTopicTitle;
		protected void btnAddTopic_Click(object sender, System.EventArgs e)
		{
			if (ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
			{
				this.ShowMsg("关键字重复!", false);
			}
			else
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
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
						return;
					}
				}
				TopicInfo target = new TopicInfo
				{
					Title = this.txtTopicTitle.Text.Trim(),
					Keys = this.txtKeys.Text.Trim(),
					IconUrl = str,
					Content = this.fcContent.Text,
					AddedDate = System.DateTime.Now,
					IsRelease = true
				};
				ValidationResults results = Validation.Validate<TopicInfo>(target, new string[]
				{
					"ValTopicInfo"
				});
				string msg = string.Empty;
				if (results.IsValid)
				{
					int num;
					if (VShopHelper.Createtopic(target, out num) && num > 0)
					{
						base.Response.Redirect("SetTopicProducts.aspx?topicid=" + num);
					}
					else
					{
						this.ShowMsg("添加专题错误", false);
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
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
	}
}
