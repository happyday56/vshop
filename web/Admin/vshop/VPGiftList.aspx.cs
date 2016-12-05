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
    public partial class VPGiftList : AdminPage
	{
		private string m_VPGiftName = "";

		private string m_Status = "";

		private void btnSearchButton_Click (object obj, EventArgs eventArg)
		{
			this.ReSearch(true);
		}

		private void reActivities_ItemCommand(object obj, RepeaterCommandEventArgs repeaterCommandEventArg)
		{
            if (repeaterCommandEventArg.CommandName == "Delete")
			{
                if (VShopHelper.DeleteVPGift(int.Parse(repeaterCommandEventArg.CommandArgument.ToString())))
				{
                    this.BindVPGift();
                    this.ShowMsg("É¾³ý³É¹¦", true);
					return;
				}
                this.ShowMsg("É¾³ýÊ§°Ü", false);
			}
            else if (repeaterCommandEventArg.CommandName == "DetailDelete")
            {
                if (VShopHelper.DeleteVPGiftDetail(int.Parse(repeaterCommandEventArg.CommandArgument.ToString())))
                {
                    this.BindVPGift();
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
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["vpgift"]))
				{
                    this.m_Status = base.Server.UrlDecode(this.Page.Request.QueryString["vpgift"]);
					this.lblStatus.Text = this.m_Status;
				}
                this.m_VPGiftName = this.txtVPGiftName.Text;
				return;
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["VPGiftName"]))
			{
                this.m_VPGiftName = base.Server.UrlDecode(this.Page.Request.QueryString["VPGiftName"]);
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["vpgift"]))
			{
                this.m_Status = base.Server.UrlDecode(this.Page.Request.QueryString["vpgift"]);
				this.lblStatus.Text = this.m_Status;
			}
            this.txtVPGiftName.Text = this.m_VPGiftName;
		}

		private void ReSearch(bool flag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection()
			{
				{ "VPGiftName", this.txtVPGiftName.Text },
				{ "Status", this.m_Status }
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

		private void BindVPGift()
		{
            VPGiftQuery query = new VPGiftQuery()
			{
                VPGiftName = this.m_VPGiftName,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
                SortBy = "VPGiftId",
				State = this.m_Status
			};
			this.lblStatus.Text = this.m_Status;
            Globals.EntityCoding(query, true);
            DbQueryResult dataList = VShopHelper.GetVPGiftList(query);
            this.reActivities.DataSource = dataList.Data;
			this.reActivities.DataBind();
            this.pager.TotalRecords = dataList.TotalRecords;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click );
			this.reActivities.ItemCommand += new RepeaterCommandEventHandler(this.reActivities_ItemCommand);
			this.LoadParams();
			if (!base.IsPostBack)
			{
                this.BindVPGift();
			}
		}

        protected void reActivities_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            System.Web.UI.WebControls.Repeater repeater = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("reDetails");
            string vpGiftId = ((System.Data.DataRowView)e.Item.DataItem)["VPGiftId"].ToString();
            if (vpGiftId != "")
            {
                repeater.DataSource = VShopHelper.GetVPGiftInfoDatail(int.Parse(vpGiftId)).VPGiftItems;
                repeater.DataBind();
            }
        }

        protected void reDetails_ItemCommand(object obj, RepeaterCommandEventArgs repeaterCommandEventArg)
        {
            if (repeaterCommandEventArg.CommandName == "DetailDelete")
            {
                if (VShopHelper.DeleteVPGiftDetail(int.Parse(repeaterCommandEventArg.CommandArgument.ToString())))
                {
                    this.BindVPGift();
                    this.ShowMsg("É¾³ý³É¹¦", true);
                    return;
                }
                this.ShowMsg("É¾³ýÊ§°Ü", false);
            }
        }
	}
}