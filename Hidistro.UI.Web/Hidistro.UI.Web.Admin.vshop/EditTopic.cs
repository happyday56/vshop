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
	public class EditTopic : AdminPage
	{
		protected ImageLinkButton btnPicDelete;
		protected System.Web.UI.WebControls.Button btnUpdateTopic;
		protected KindeditorControl fcContent;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected HiImage imgPic;
		private int topicId;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected System.Web.UI.WebControls.TextBox txtTopicTitle;
		protected void btnPicDelete_Click(object sender, System.EventArgs e)
		{
			TopicInfo topic = VShopHelper.Gettopic(this.topicId);
			try
			{
				ResourcesHelper.DeleteImage(topic.IconUrl);
			}
			catch
			{
			}
			topic.IconUrl = (this.imgPic.ImageUrl = null);
			if (VShopHelper.Updatetopic(topic))
			{
				this.btnPicDelete.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
				this.imgPic.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
			}
		}
		protected void btnUpdateTopic_Click(object sender, System.EventArgs e)
		{
			TopicInfo topic = VShopHelper.Gettopic(this.topicId);
			if (topic.Keys != this.txtKeys.Text.Trim() && ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
			{
				this.ShowMsg("关键字重复!", false);
			}
			else
			{
				if (this.fileUpload.HasFile)
				{
					try
					{
						ResourcesHelper.DeleteImage(topic.IconUrl);
						topic.IconUrl = VShopHelper.UploadTopicImage(this.fileUpload.PostedFile);
						this.imgPic.ImageUrl = topic.IconUrl;
					}
					catch
					{
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
						return;
					}
				}
				topic.TopicId = this.topicId;
				topic.Title = this.txtTopicTitle.Text.Trim();
				topic.Keys = this.txtKeys.Text.Trim();
				topic.IconUrl = topic.IconUrl;
				topic.Content = this.fcContent.Text;
				topic.AddedDate = System.DateTime.Now;
				topic.IsRelease = true;
				ValidationResults results = Validation.Validate<TopicInfo>(topic, new string[]
				{
					"ValTopicInfo"
				});
				string msg = string.Empty;
				if (results.IsValid)
				{
					if (VShopHelper.Updatetopic(topic))
					{
						this.ShowMsg("已经成功修改当前专题", true);
					}
					else
					{
						this.ShowMsg("修改专题失败", false);
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
			if (!int.TryParse(this.Page.Request.QueryString["topicId"], out this.topicId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				if (!this.Page.IsPostBack)
				{
					TopicInfo topic = VShopHelper.Gettopic(this.topicId);
					if (topic == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
						Globals.EntityCoding(topic, false);
						this.txtTopicTitle.Text = topic.Title;
						this.txtKeys.Text = topic.Keys;
						this.imgPic.ImageUrl = topic.IconUrl;
						this.fcContent.Text = topic.Content;
						this.btnPicDelete.Visible = !string.IsNullOrEmpty(this.imgPic.ImageUrl);
					}
				}
			}
		}
	}
}
