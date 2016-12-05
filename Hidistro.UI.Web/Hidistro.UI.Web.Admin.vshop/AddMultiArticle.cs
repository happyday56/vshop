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
	public class AddMultiArticle : AdminPage
	{
		protected System.Web.UI.WebControls.CheckBox chkKeys;
		protected System.Web.UI.WebControls.CheckBox chkNo;
		protected System.Web.UI.WebControls.CheckBox chkSub;
		protected KindeditorControl fkContent;
		protected YesNoRadioButtonList radDisable;
		protected YesNoRadioButtonList radMatch;
		protected System.Web.UI.WebControls.TextBox txtKeys;
		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
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
			if (!string.IsNullOrEmpty(base.Request.QueryString["cmd"]) && !string.IsNullOrEmpty(base.Request.Form["MultiArticle"]))
			{
				string str = base.Request.Form["MultiArticle"];
				System.Collections.Generic.List<ArticleList> list = JsonConvert.DeserializeObject<System.Collections.Generic.List<ArticleList>>(str);
				if (list != null && list.Count > 0)
				{
					NewsReplyInfo reply = new NewsReplyInfo
					{
						MessageType = MessageType.List,
						IsDisable = base.Request.Form["radDisable"] != "true"
					};
					if (base.Request.Form["chkKeys"] == "true")
					{
						reply.Keys = base.Request.Form.Get("Keys");
					}
					if (!string.IsNullOrWhiteSpace(reply.Keys) && ReplyHelper.HasReplyKey(reply.Keys))
					{
						base.Response.Write("key");
						base.Response.End();
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
					if (ReplyHelper.SaveReply(reply))
					{
						base.Response.Write("true");
						base.Response.End();
					}
				}
			}
		}
	}
}
