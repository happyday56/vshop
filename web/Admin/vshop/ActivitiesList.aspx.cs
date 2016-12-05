using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.vshop
{
	public partial class ActivitiesList : AdminPage
	{
		private string m_ActivitiesName = "";

		private string m_Status = "";

		private void btnSearchButton_Click (object obj, EventArgs eventArg)
		{
			this.ReSearch(true);
		}

		private void reActivities_ItemCommand(object obj, RepeaterCommandEventArgs repeaterCommandEventArg)
		{
            if (repeaterCommandEventArg.CommandName == "Delete")
			{
				if (VShopHelper.DeleteActivities(int.Parse(repeaterCommandEventArg.CommandArgument.ToString())))
				{
					this.BindActivities();
                    this.ShowMsg("É¾³ý³É¹¦", true);
					return;
				}
                this.ShowMsg("É¾³ýÊ§°Ü", false);
			}
		}

		private void LoadParams()
		{
			if (this.Page.IsPostBack)
			{
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["activities"]))
				{
                    this.m_Status = base.Server.UrlDecode(this.Page.Request.QueryString["activities"]);
					this.lblStatus.Text = this.m_Status;
				}
				this.m_ActivitiesName = this.txtActivitiesName.Text;
				return;
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ActivitiesName"]))
			{
                this.m_ActivitiesName = base.Server.UrlDecode(this.Page.Request.QueryString["ActivitiesName"]);
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["activities"]))
			{
                this.m_Status = base.Server.UrlDecode(this.Page.Request.QueryString["activities"]);
				this.lblStatus.Text = this.m_Status;
			}
			this.txtActivitiesName.Text = this.m_ActivitiesName;
		}

		private void ReSearch(bool flag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection()
			{
				{ "ActivitiesName", this.txtActivitiesName.Text },
				{ "activities", this.m_Status }
			};
			this.lblStatus.Text = this.m_Status;
			string str = "pageSize";
			int pageSize = this.pager.PageSize;
			nameValueCollection.Add(str, pageSize.ToString(CultureInfo.InvariantCulture));
			if (!flag)
			{
                string str1 = "pageIndex";
				int pageIndex = this.pager.PageIndex;
				nameValueCollection.Add(str1, pageIndex.ToString(CultureInfo.InvariantCulture));
			}
			base.ReloadPage(nameValueCollection);
		}

		private void BindActivities()
		{
			ActivitiesQuery activitiesQuery = new ActivitiesQuery()
			{
				ActivitiesName = this.m_ActivitiesName,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "ActivitiesId",
				State = this.m_Status
			};
			this.lblStatus.Text = this.m_Status;
			Globals.EntityCoding(activitiesQuery, true);
			DbQueryResult activitiesList = VShopHelper.GetActivitiesList(activitiesQuery);
			this.reActivities.DataSource = activitiesList.Data;
			this.reActivities.DataBind();
			this.pager.TotalRecords = activitiesList.TotalRecords;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click );
			this.reActivities.ItemCommand += new RepeaterCommandEventHandler(this.reActivities_ItemCommand);
			this.LoadParams();
			if (!base.IsPostBack)
			{
				this.BindActivities();
			}
		}
	}
}