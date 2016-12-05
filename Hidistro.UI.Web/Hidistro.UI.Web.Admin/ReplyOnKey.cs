using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class ReplyOnKey : AdminPage
	{
		protected Grid grdArticleCategories;
		private void BindArticleCategory()
		{
			System.Collections.Generic.List<ReplyInfo> list = ReplyHelper.GetAllReply().ToList<ReplyInfo>().FindAll((ReplyInfo a) => a.ReplyType < ReplyType.Wheel);
			this.grdArticleCategories.DataSource = list;
			this.grdArticleCategories.DataBind();
		}
		protected string GetReplyTypeName(object obj)
		{
			ReplyType en = (ReplyType)obj;
			string str = string.Empty;
			bool flag = false;
			if (ReplyType.Subscribe == (en & ReplyType.Subscribe))
			{
				str += "[关注时回复]";
				flag = true;
			}
			if (ReplyType.NoMatch == (en & ReplyType.NoMatch))
			{
				str += "[无匹配回复]";
				flag = true;
			}
			if (ReplyType.Keys == (en & ReplyType.Keys))
			{
				str += "[关键字回复]";
				flag = true;
			}
			if (!flag)
			{
				str = en.ToShowText();
			}
			return str;
		}
		private void grdArticleCategories_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
			int id = (int)this.grdArticleCategories.DataKeys[rowIndex].Value;
			if (e.CommandName == "Delete")
			{
				ReplyHelper.DeleteReply(id);
				base.Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString(), true);
			}
			else
			{
				if (e.CommandName == "Release")
				{
					ReplyHelper.UpdateReplyRelease(id);
					base.Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString(), true);
				}
				else
				{
					if (e.CommandName == "Edit")
					{
						ReplyInfo reply = ReplyHelper.GetReply(id);
						if (reply != null)
						{
							switch (reply.MessageType)
							{
							case MessageType.Text:
								base.Response.Redirect(string.Format("EditReplyOnKey.aspx?id={0}", id));
								break;
							case MessageType.News:
								base.Response.Redirect(string.Format("EditSingleArticle.aspx?id={0}", id));
								break;
							case MessageType.List:
								base.Response.Redirect(string.Format("EditMultiArticle.aspx?id={0}", id));
								break;
							}
						}
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.grdArticleCategories.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdArticleCategories_RowCommand);
			if (!this.Page.IsPostBack)
			{
				this.BindArticleCategory();
			}
		}
	}
}
