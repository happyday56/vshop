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
    public partial class ActivitiesShops : AdminPage
	{
        private void BindActivities()
        {
            ActivitiesQuery activitiesQuery = new ActivitiesQuery()
            {
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortOrder = SortAction.Desc,
                SortBy = "ActivitiesId",
                ActivitiesId = this.Request.QueryString["id"] ?? ""
            };
            //Globals.EntityCoding(activitiesQuery, true);
            DbQueryResult shopsList = VShopHelper.GetActivitiesShopsList(activitiesQuery);
            this.reShops.DataSource = shopsList.Data;
            this.reShops.DataBind();
            this.pager.TotalRecords = shopsList.TotalRecords;
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindActivities();
			}
		}
	}
}