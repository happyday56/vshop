using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class EditSingleArticle : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnCreate;
		protected System.Web.UI.WebControls.CheckBox chkKeys;
		protected System.Web.UI.WebControls.CheckBox chkNo;
		protected System.Web.UI.WebControls.CheckBox chkSub;
		protected KindeditorControl fkContent;
		protected System.Web.UI.WebControls.HiddenField hdpic;
		private int id;
		protected System.Web.UI.WebControls.Label LbimgTitle;
		protected System.Web.UI.WebControls.Label Lbmsgdesc;
		protected YesNoRadioButtonList radDisable;
		protected YesNoRadioButtonList radMatch;
		protected System.Web.UI.WebControls.TextBox Tbdescription;
		protected System.Web.UI.WebControls.TextBox Tbtitle;
		protected System.Web.UI.WebControls.TextBox TbUrl;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected System.Web.UI.HtmlControls.HtmlImage uploadpic;
		protected void BindSingleArticle(int id)
		{
			NewsReplyInfo reply = ReplyHelper.GetReply(id) as NewsReplyInfo;
			if (reply == null || reply.NewsMsg == null || reply.NewsMsg.Count == 0)
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.ViewState["MsgId"] = reply.Id;
				this.txtKeys.Text = reply.Keys;
				this.radMatch.SelectedValue = (reply.MatchType == MatchType.Like);
				this.radDisable.SelectedValue = !reply.IsDisable;
				this.chkKeys.Checked = (ReplyType.Keys == (reply.ReplyType & ReplyType.Keys));
				this.chkSub.Checked = (ReplyType.Subscribe == (reply.ReplyType & ReplyType.Subscribe));
				this.chkNo.Checked = (ReplyType.NoMatch == (reply.ReplyType & ReplyType.NoMatch));
				if (this.chkNo.Checked)
				{
					this.chkNo.Enabled = true;
					this.chkNo.ToolTip = "";
				}
				if (this.chkSub.Checked)
				{
					this.chkSub.Enabled = true;
					this.chkSub.ToolTip = "";
				}
				this.Tbtitle.Text = reply.NewsMsg[0].Title;
				this.LbimgTitle.Text = reply.NewsMsg[0].Title;
				this.Tbdescription.Text = System.Web.HttpUtility.HtmlDecode(reply.NewsMsg[0].Description);
				this.fkContent.Text = reply.NewsMsg[0].Content;
				this.Lbmsgdesc.Text = reply.NewsMsg[0].Description;
				this.TbUrl.Text = reply.NewsMsg[0].Url;
				this.uploadpic.Src = reply.NewsMsg[0].PicUrl;
				this.hdpic.Value = reply.NewsMsg[0].PicUrl;
			}
		}
		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Tbtitle.Text) || string.IsNullOrEmpty(this.Tbdescription.Text) || string.IsNullOrEmpty(this.hdpic.Value))
			{
				this.ShowMsg("您填写的信息不完整!", false);
			}
			else
			{
				NewsReplyInfo reply = ReplyHelper.GetReply(base.GetUrlIntParam("id")) as NewsReplyInfo;
				if (reply == null || reply.NewsMsg == null || reply.NewsMsg.Count == 0)
				{
					base.GotoResourceNotFound();
				}
				else
				{
					if (!string.IsNullOrEmpty(this.txtKeys.Text) && reply.Keys != this.txtKeys.Text.Trim() && ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
					{
						this.ShowMsg("关键字重复!", false);
					}
					else
					{
						reply.IsDisable = !this.radDisable.SelectedValue;
						if (this.chkKeys.Checked && !string.IsNullOrWhiteSpace(this.txtKeys.Text))
						{
							reply.Keys = this.txtKeys.Text.Trim();
						}
						else
						{
							reply.Keys = string.Empty;
						}
						reply.MatchType = (this.radMatch.SelectedValue ? MatchType.Like : MatchType.Equal);
						reply.ReplyType = ReplyType.None;
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
											reply.NewsMsg[0].Content = this.fkContent.Text;
											reply.NewsMsg[0].Description = this.Tbdescription.Text;
											reply.NewsMsg[0].PicUrl = this.hdpic.Value;
											reply.NewsMsg[0].Title = this.Tbtitle.Text;
											reply.NewsMsg[0].Url = this.TbUrl.Text;
											if (ReplyHelper.UpdateReply(reply))
											{
												this.ShowMsg("修改成功", true);
											}
											else
											{
												this.ShowMsg("修改失败", false);
											}
										}
									}
								}
							}
						}
					}
				}
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
			if (!base.IsPostBack)
			{
				this.id = base.GetUrlIntParam("id");
				this.BindSingleArticle(this.id);
			}
			else
			{
				this.uploadpic.Src = this.hdpic.Value;
			}
			if (base.GetUrlBoolParam("iscallback"))
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
			System.Drawing.Image image = null;
			System.Drawing.Image image2 = null;
			Bitmap bitmap = null;
			Graphics graphics = null;
			System.IO.MemoryStream stream = null;
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
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
				if (graphics != null)
				{
					graphics.Dispose();
				}
				if (image2 != null)
				{
					image2.Dispose();
				}
				if (image != null)
				{
					image.Dispose();
				}
				if (stream != null)
				{
					stream.Close();
				}
				base.Response.End();
			}
		}
	}
}
