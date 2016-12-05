using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class AddSingleArticle : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnCreate;
		protected System.Web.UI.WebControls.CheckBox chkKeys;
		protected System.Web.UI.WebControls.CheckBox chkNo;
		protected System.Web.UI.WebControls.CheckBox chkSub;
		protected KindeditorControl fkContent;
		protected System.Web.UI.WebControls.HiddenField hdpic;
		protected System.Web.UI.WebControls.Label LbimgTitle;
		protected System.Web.UI.WebControls.Label Lbmsgdesc;
		protected YesNoRadioButtonList radDisable;
		protected YesNoRadioButtonList radMatch;
		protected System.Web.UI.WebControls.TextBox Tbdescription;
		protected System.Web.UI.WebControls.TextBox Tbtitle;
		protected System.Web.UI.WebControls.TextBox TbUrl;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.Tbtitle.Text) && !string.IsNullOrEmpty(this.Tbdescription.Text) && !string.IsNullOrEmpty(this.hdpic.Value))
			{
				if (!string.IsNullOrEmpty(this.txtKeys.Text) && ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
				{
					this.ShowMsg("关键字重复!", false);
				}
				else
				{
					NewsReplyInfo reply = new NewsReplyInfo
					{
						IsDisable = !this.radDisable.SelectedValue
					};
					if (this.chkKeys.Checked && !string.IsNullOrWhiteSpace(this.txtKeys.Text))
					{
						reply.Keys = this.txtKeys.Text.Trim();
					}
					if (this.chkKeys.Checked && string.IsNullOrWhiteSpace(this.txtKeys.Text))
					{
						this.ShowMsg("你选择了关键字回复，必须填写关键字！", false);
					}
					else
					{
						reply.MatchType = (this.radMatch.SelectedValue ? MatchType.Like : MatchType.Equal);
						reply.MessageType = MessageType.News;
						if (this.chkKeys.Checked)
						{
							reply.ReplyType |= ReplyType.Keys;
						}
						if (this.chkSub.Checked)
						{
							reply.ReplyType |= ReplyType.Subscribe;
						}
						if (this.chkNo.Checked)
						{
							reply.ReplyType |= ReplyType.NoMatch;
						}
						if (reply.ReplyType == ReplyType.None)
						{
							this.ShowMsg("请选择回复类型", false);
						}
						else
						{
							if (string.IsNullOrEmpty(this.Tbtitle.Text))
							{
								this.ShowMsg("请输入标题", false);
							}
							else
							{
								if (string.IsNullOrEmpty(this.hdpic.Value))
								{
									this.ShowMsg("请上传封面图", false);
								}
								else
								{
									if (string.IsNullOrEmpty(this.Tbdescription.Text))
									{
										this.ShowMsg("请输入摘要", false);
									}
									else
									{
										if (string.IsNullOrEmpty(this.fkContent.Text) && string.IsNullOrEmpty(this.TbUrl.Text))
										{
											this.ShowMsg("请输入内容或自定义链接", false);
										}
										else
										{
											NewsMsgInfo item = new NewsMsgInfo
											{
												Reply = reply,
												Content = this.fkContent.Text,
												Description = System.Web.HttpUtility.HtmlEncode(this.Tbdescription.Text),
												PicUrl = this.hdpic.Value,
												Title = System.Web.HttpUtility.HtmlEncode(this.Tbtitle.Text),
												Url = this.TbUrl.Text.Trim()
											};
											reply.NewsMsg = new System.Collections.Generic.List<NewsMsgInfo>();
											reply.NewsMsg.Add(item);
											if (ReplyHelper.SaveReply(reply))
											{
												this.ShowMsg("添加成功", true);
											}
											else
											{
												this.ShowMsg("添加失败", false);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				this.ShowMsg("您填写的信息不完整!", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.radMatch.Items[0].Text = "模糊匹配";
			this.radMatch.Items[1].Text = "精确匹配";
			this.radDisable.Items[0].Text = "启用";
			this.radDisable.Items[1].Text = "禁用";
			this.chkNo.Enabled = (ReplyHelper.GetMismatchReply() == null);
			this.chkSub.Enabled = (ReplyHelper.GetSubscribeReply() == null);
			if (!this.chkNo.Enabled)
			{
				this.chkNo.ToolTip = "该类型已被使用";
			}
			if (!this.chkSub.Enabled)
			{
				this.chkSub.ToolTip = "该类型已被使用";
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["iscallback"]) && System.Convert.ToBoolean(base.Request.QueryString["iscallback"]))
			{
				this.UploadImage();
			}
			else
			{
				if (!string.IsNullOrEmpty(base.Request.Form["del"]))
				{
					string path = base.Request.Form["del"];
					string str2 = Globals.PhysicalPath(path);
					try
					{
						if (System.IO.File.Exists(str2))
						{
							System.IO.File.Delete(str2);
							base.Response.Write("true");
						}
					}
					catch (System.Exception)
					{
						base.Response.Write("false");
					}
					base.Response.End();
				}
			}
		}
		private void UploadImage()
		{
			try
			{
				System.Web.HttpPostedFile file = base.Request.Files["Filedata"];
				string str = System.DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string str2 = "/Storage/master/reply/";
				string str3 = str + System.IO.Path.GetExtension(file.FileName);
				file.SaveAs(Globals.MapPath(str2 + str3));
				base.Response.StatusCode = 200;
				base.Response.Write(str2 + str3);
			}
			catch (System.Exception)
			{
				base.Response.StatusCode = 500;
				base.Response.Write("服务器错误");
				base.Response.End();
			}
			finally
			{
				base.Response.End();
			}
		}
	}
}
