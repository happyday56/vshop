using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class EditMultiArticle : AdminPage
	{
		protected string articleJson;
		protected System.Web.UI.WebControls.CheckBox chkKeys;
		protected System.Web.UI.WebControls.CheckBox chkNo;
		protected System.Web.UI.WebControls.CheckBox chkSub;
		protected KindeditorControl fkContent;
		protected int MaterialID;
		protected YesNoRadioButtonList radDisable;
		protected YesNoRadioButtonList radMatch;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(base.Request.QueryString["id"], out this.MaterialID))
			{
				base.Response.Redirect("ReplyOnKey.aspx");
			}
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
			if (!string.IsNullOrEmpty(base.Request.QueryString["cmd"]))
			{
				if (!string.IsNullOrEmpty(base.Request.Form["MultiArticle"]))
				{
					string str = base.Request.Form["MultiArticle"];
					System.Collections.Generic.List<ArticleList> list = JsonConvert.DeserializeObject<System.Collections.Generic.List<ArticleList>>(str);
					if (list != null && list.Count > 0)
					{
						NewsReplyInfo reply = ReplyHelper.GetReply(this.MaterialID) as NewsReplyInfo;
						reply.IsDisable = (base.Request.Form["radDisable"] != "true");
						string str2 = base.Request.Form.Get("Keys");
						if (base.Request.Form["chkKeys"] == "true")
						{
							if (!string.IsNullOrEmpty(str2) && reply.Keys != str2 && ReplyHelper.HasReplyKey(str2))
							{
								base.Response.Write("key");
								base.Response.End();
							}
							reply.Keys = str2;
						}
						else
						{
							reply.Keys = string.Empty;
						}
						reply.MatchType = ((base.Request.Form["radMatch"] == "true") ? MatchType.Like : MatchType.Equal);
						reply.ReplyType = ReplyType.None;
						if (base.Request.Form["chkKeys"] == "true")
						{
							reply.ReplyType |= ReplyType.Keys;
						}
						if (base.Request.Form["chkSub"] == "true")
						{
							reply.ReplyType |= ReplyType.Subscribe;
						}
						if (base.Request.Form["chkNo"] == "true")
						{
							reply.ReplyType |= ReplyType.NoMatch;
						}
						foreach (NewsMsgInfo info2 in reply.NewsMsg)
						{
							ReplyHelper.DeleteNewsMsg(info2.Id);
						}
						System.Collections.Generic.List<NewsMsgInfo> list2 = new System.Collections.Generic.List<NewsMsgInfo>();
						foreach (ArticleList list3 in list)
						{
							if (list3.Status != "del")
							{
								NewsMsgInfo item = list3;
								if (item != null)
								{
									item.Reply = reply;
									list2.Add(item);
								}
							}
						}
						reply.NewsMsg = list2;
						if (ReplyHelper.UpdateReply(reply))
						{
							base.Response.Write("true");
							base.Response.End();
						}
					}
				}
			}
			else
			{
				NewsReplyInfo info3 = ReplyHelper.GetReply(this.MaterialID) as NewsReplyInfo;
				if (info3 != null)
				{
					System.Collections.Generic.List<ArticleList> list4 = new System.Collections.Generic.List<ArticleList>();
					if (info3.NewsMsg != null && info3.NewsMsg.Count > 0)
					{
						int num = 1;
						foreach (NewsMsgInfo info4 in info3.NewsMsg)
						{
							ArticleList list5 = new ArticleList
							{
								PicUrl = info4.PicUrl,
								Title = info4.Title,
								Url = info4.Url,
								Description = info4.Description,
								Content = info4.Content
							};
							list5.BoxId = num++.ToString();
							list5.Status = "";
							list4.Add(list5);
						}
						this.articleJson = JsonConvert.SerializeObject(list4);
					}
					this.fkContent.Text = info3.NewsMsg[0].Content;
					this.txtKeys.Text = info3.Keys;
					this.radMatch.SelectedValue = (info3.MatchType == MatchType.Like);
					this.radDisable.SelectedValue = !info3.IsDisable;
					this.chkKeys.Checked = (ReplyType.Keys == (info3.ReplyType & ReplyType.Keys));
					this.chkSub.Checked = (ReplyType.Subscribe == (info3.ReplyType & ReplyType.Subscribe));
					this.chkNo.Checked = (ReplyType.NoMatch == (info3.ReplyType & ReplyType.NoMatch));
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
				}
			}
		}
	}
}
