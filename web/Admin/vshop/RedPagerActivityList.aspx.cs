using ASPNET.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.vshop
{
	public partial class RedPagerActivityList1 : AdminPage
	{
		private string m_Name = "";

		protected string LocalUrl = string.Empty;


		private void AAbiuZJB()
		{
			if (this.Page.IsPostBack)
			{
				this.m_Name = this.txtName.Text;
				return;
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Name"]))
			{
                this.m_Name = base.Server.UrlDecode(this.Page.Request.QueryString["Name"]);
			}
			this.txtName.Text = this.m_Name;
		}

		private void ReSearch(bool flag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection()
			{
				{ "Name", this.txtName.Text }
			};
            string str = "pageSize";
			int pageSize = this.pager.PageSize;
			nameValueCollection.Add(str, pageSize.ToString(CultureInfo.InvariantCulture));
            string str1 = "pageIndex";
			int pageIndex = this.pager.PageIndex;
			nameValueCollection.Add(str1, pageIndex.ToString(CultureInfo.InvariantCulture));
			base.ReloadPage(nameValueCollection);
		}

		private void btnSearchButton_Click(object obj, EventArgs eventArg)
		{
			this.ReSearch(true);
		}

		private void BindRedPagerActivity()
		{
			RedPagerActivityQuery redPagerActivityQuery = new RedPagerActivityQuery()
			{
				Name = this.m_Name,
                SortBy = "RedPagerActivityId",
				SortOrder = SortAction.Desc
			};
			Globals.EntityCoding(redPagerActivityQuery, true);
			redPagerActivityQuery.PageIndex = this.pager.PageIndex;
			redPagerActivityQuery.PageSize = this.pager.PageSize;
			DbQueryResult redPagerActivityRequest = RedPagerActivityBrower.GetRedPagerActivityRequest(redPagerActivityQuery);
			this.rptList.DataSource = redPagerActivityRequest.Data;
			this.rptList.DataBind();
			this.pager.TotalRecords = redPagerActivityRequest.TotalRecords;
		}

		protected string FormatCommissionRise(object commissionrise)
		{
			string empty = string.Empty;
			decimal num = new decimal(0, 0, 0, false, 2);
			decimal.TryParse(commissionrise.ToString(), out num);
            empty = (num != new decimal(0, 0, 0, false, 2) ? string.Concat("+", num, "%") : "同类目佣金");
			return empty;
		}

		protected string GetCategoryName(object ocategoryid)
		{
            string name = "全部";
			int num = 0;
			int.TryParse(ocategoryid.ToString(), out num);
			CategoryInfo category = CategoryBrowser.GetCategory(num);
			if (category != null)
			{
				name = category.Name;
			}
			return name;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.LocalUrl = base.Server.UrlEncode(base.Request.Url.ToString());
			this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click);
			this.AAbiuZJB();
			if (!base.IsPostBack)
			{
				this.BindRedPagerActivity();
			}
		}

		protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			int num = 0;
			int.TryParse(e.CommandArgument.ToString(), out num);
			if (num > 0)
			{
				string commandName = e.CommandName;
				string str = commandName;
				if (commandName != null)
				{
                    if (str == "del")
					{
						RedPagerActivityBrower.DelRedPagerActivity(num);
					}
                    else if (str == "open")
					{
						RedPagerActivityBrower.SetIsOpen(num, true);
					}
                    else if (str == "close")
					{
						RedPagerActivityBrower.SetIsOpen(num, false);
					}
				}
				this.ReSearch(true);
			}
		}

		protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				ImageButton imageButton = (ImageButton)e.Item.FindControl("aQBtAGcAQgB0AG4ASQBzAE8AcABlAG4A");
                if (null != imageButton) { 
				if (((DataRowView)e.Item.DataItem).Row["SQBzAE8AcABlAG4A"].ToString() == "RgBhAGwAcwBlAA==")
				{
					imageButton.ImageUrl = "LgAuAC8AaQBtAGEAZwBlAHMALwB0AGEALgBnAGkAZgA=";
					imageButton.CommandName = "bwBwAGUAbgA=";
					imageButton.ToolTip = "uXD7UQBfL1Tli+NO0ZE4UjttqFI=";
					return;
				}
                
                imageButton.ToolTip = "点击关闭该代金券活动";
                imageButton.CommandName = "close";
                imageButton.OnClientClick = "return confirm('关闭代金券活动,不会影响该活动已经产生的代金券!\n确认要关闭该活动吗?')";
                }

			}
		}
	}
}