using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class SearchHomeTopic : AdminPage
	{
		protected ImageLinkButton btnAdd;
		protected Grid grdTopics;
		protected PageSize hrefPageSize;
		protected Pager pager;
		protected Pager pager1;
		protected void BindTopics()
		{
			TopicQuery page = new TopicQuery
			{
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				IsincludeHomeProduct = new bool?(false),
				SortBy = "DisplaySequence",
				SortOrder = SortAction.Asc
			};
			DbQueryResult topicList = VShopHelper.GettopicList(page);
			this.grdTopics.DataSource = topicList.Data;
			this.grdTopics.DataBind();
			this.pager1.TotalRecords = (this.pager.TotalRecords = topicList.TotalRecords);
		}
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["CheckBoxGroup"];
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请选择一个专题！", false);
			}
			else
			{
				string[] strArray = str.Split(new char[]
				{
					','
				});
				int num = 0;
				string[] array = strArray;
				for (int i = 0; i < array.Length; i++)
				{
					string str2 = array[i];
					if (VShopHelper.AddHomeTopic(System.Convert.ToInt32(str2)))
					{
						num++;
					}
				}
				if (num > 0)
				{
					this.CloseWindow();
				}
				else
				{
					this.ShowMsg("添加首页专题失败！", false);
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindTopics();
			}
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
		}
	}
}
