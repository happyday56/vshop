using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Orders;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class BalanceDrawRequestList : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearchButton;
		private string CheckStartTime = "";
        private string CheckEndTime = "";
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater reBalanceDrawRequest;
		private string RequestStartTime = "";
        private string RequestEndTime = "";
		private string StoreName = "";
        private string Mobile = "";
        private int? CheckType;
        protected WebCalendar txtCheckStartTime;
        protected WebCalendar txtCheckEndTime;
        protected WebCalendar txtRequestStartTime;
        protected WebCalendar txtRequestEndTime;
		protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected TextBox txtMobile;
        protected DropDownList ddlCheckType;

		private void BindData()
		{
			BalanceDrawRequestQuery entity = new BalanceDrawRequestQuery
			{
				CheckStartTime = this.CheckStartTime,
                CheckEndTime = this.CheckEndTime,
                RequestStartTime = this.RequestStartTime,
                RequestEndTime = this.RequestEndTime,
				StoreName = this.StoreName,
                Mobile = this.Mobile,
                CheckType = this.CheckType,
                CheckTime = "",
                RequestTime = "",
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "UserId",
				IsCheck = ""
			};
			Globals.EntityCoding(entity, true);
            DbQueryResult balanceDrawRequest = VShopHelper.GetBalanceDrawRequestTwo(entity);
			this.reBalanceDrawRequest.DataSource = balanceDrawRequest.Data;
			this.reBalanceDrawRequest.DataBind();
			this.pager.TotalRecords = balanceDrawRequest.TotalRecords;
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.ReBind(true);
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StoreName"]))
				{
					this.StoreName = base.Server.UrlDecode(this.Page.Request.QueryString["StoreName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CheckStartTime"]))
				{
                    this.CheckStartTime = base.Server.UrlDecode(this.Page.Request.QueryString["CheckStartTime"]);
				}
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CheckEndTime"]))
                {
                    this.CheckEndTime = base.Server.UrlDecode(this.Page.Request.QueryString["CheckEndTime"]);
                }
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RequestStartTime"]))
				{
                    this.RequestStartTime = base.Server.UrlDecode(this.Page.Request.QueryString["RequestStartTime"]);
				}
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RequestEndTime"]))
                {
                    this.RequestEndTime = base.Server.UrlDecode(this.Page.Request.QueryString["RequestEndTime"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Mobile"]))
                {
                    this.Mobile = base.Server.UrlDecode(this.Page.Request.QueryString["Mobile"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CheckType"]))
                {
                    int num9 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["CheckType"], out num9))
                    {
                        this.CheckType = new int?(num9);
                    }
                }
				this.txtStoreName.Text = this.StoreName;
                this.txtRequestStartTime.Text = this.RequestStartTime;
                this.txtRequestEndTime.Text = this.RequestEndTime;
                this.txtCheckStartTime.Text = this.CheckStartTime;
                this.txtCheckEndTime.Text = this.CheckEndTime;
                this.txtMobile.Text = this.Mobile;
                if (this.CheckType.HasValue)
                {
                    this.ddlCheckType.SelectedValue = this.CheckType.Value.ToString();
                }

			}
			else
			{
				this.StoreName = this.txtStoreName.Text;
                this.RequestStartTime = this.txtRequestStartTime.Text;
                this.RequestEndTime = this.txtRequestEndTime.Text;
                this.CheckStartTime = this.txtCheckStartTime.Text;
                this.CheckEndTime = this.txtCheckEndTime.Text;
                this.Mobile = this.txtMobile.Text;
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CheckType"]))
                {
                    int num9 = 0;
                    if (int.TryParse(this.Page.Request.QueryString["CheckType"], out num9))
                    {
                        this.CheckType = new int?(num9);
                    }
                }
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			this.LoadParameters();
			if (!base.IsPostBack)
			{
                this.ddlCheckType.Items.Clear();
                this.ddlCheckType.Items.Add(new ListItem("全部", string.Empty));
                this.ddlCheckType.Items.Add(new ListItem("未发放", "0"));
                this.ddlCheckType.Items.Add(new ListItem("已发放", "1"));
				this.BindData();
			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("StoreName", this.txtStoreName.Text);
            queryStrings.Add("RequestStartTime", this.txtRequestStartTime.Text);
            queryStrings.Add("RequestEndTime", this.txtRequestEndTime.Text);
            queryStrings.Add("CheckStartTime", this.txtCheckStartTime.Text);
            queryStrings.Add("CheckEndTime", this.txtCheckEndTime.Text);
            queryStrings.Add("Mobile", this.txtMobile.Text);
            if (!string.IsNullOrEmpty(this.ddlCheckType.SelectedValue))
            {
                queryStrings.Add("CheckType", this.ddlCheckType.SelectedValue);
            }
			queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}
	}
}
