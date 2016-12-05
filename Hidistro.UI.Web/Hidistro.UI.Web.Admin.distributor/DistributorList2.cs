using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class DistributorList2 : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearchButton;
		private string CellPhone = "";
        protected DistributorGradeDropDownList DrGrade;
		protected System.Web.UI.WebControls.DropDownList DrStatus;
		private string Grade = "0";
		private string MicroSignal = "";
		protected Pager pager;
		private string RealName = "";
		protected System.Web.UI.WebControls.Repeater reDistributor;
		private string Status = "0";
		private string StoreName = "";
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.TextBox txtMicroSignal;
		protected System.Web.UI.WebControls.TextBox txtRealName;
		protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected DropDownList DrDeadlineStatus;
        private int DeadlineStatus = 0;
        protected WebCalendar calendarStartDate;
        private DateTime? currSelectTime;
        protected DropDownList DrFinishStatus;
        private string FinishStatus = "0";

		private void BindData()
		{
			DistributorsQuery entity = new DistributorsQuery
			{
				GradeId = int.Parse(this.Grade),
				StoreName = this.StoreName,
				CellPhone = this.CellPhone,
				RealName = this.RealName,
				MicroSignal = this.MicroSignal,
				ReferralStatus = int.Parse(this.Status),
                DeadlineStatus = this.DeadlineStatus,
                FinishStatus = int.Parse(this.FinishStatus),
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "userid"
			};
            
            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

			Globals.EntityCoding(entity, true);
            DbQueryResult distributors = VShopHelper.GetDistributors2(entity, siteSettings.TempStoreSaleAmount, DateTime.Parse( (this.currSelectTime.HasValue ? this.currSelectTime : DateTime.Now).ToString()));
			this.reDistributor.DataSource = distributors.Data;
			this.reDistributor.DataBind();
			this.pager.TotalRecords = distributors.TotalRecords;
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
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Grade"]))
				{
					this.Grade = base.Server.UrlDecode(this.Page.Request.QueryString["Grade"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Status"]))
				{
					this.Status = base.Server.UrlDecode(this.Page.Request.QueryString["Status"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RealName"]))
				{
					this.RealName = base.Server.UrlDecode(this.Page.Request.QueryString["RealName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CellPhone"]))
				{
					this.CellPhone = base.Server.UrlDecode(this.Page.Request.QueryString["CellPhone"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["MicroSignal"]))
				{
					this.MicroSignal = base.Server.UrlDecode(this.Page.Request.QueryString["MicroSignal"]);
				}
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["DeadlineStatus"]))
                {
                    this.DeadlineStatus = int.Parse(base.Server.UrlDecode(this.Page.Request.QueryString["DeadlineStatus"]));
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["LimitTime"]))
                {
                    this.currSelectTime = DateTime.Parse(this.Page.Request.QueryString["LimitTime"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["FinishStatus"]))
                {
                    this.FinishStatus = base.Server.UrlDecode(this.Page.Request.QueryString["FinishStatus"]);
                }
				this.DrStatus.SelectedValue = this.Status;
				this.txtStoreName.Text = this.StoreName;
                this.DrGrade.DataBind();
				this.DrGrade.SelectedValue = int.Parse( this.Grade );
				this.txtCellPhone.Text = this.CellPhone;
				this.txtMicroSignal.Text = this.MicroSignal;
				this.txtRealName.Text = this.RealName;
                this.DrDeadlineStatus.SelectedValue = this.DeadlineStatus.ToString();
                this.calendarStartDate.SelectedDate = this.currSelectTime;
                this.DrFinishStatus.SelectedValue = this.FinishStatus;
			}
			else
			{
				this.Status = this.DrStatus.SelectedValue;
				this.StoreName = this.txtStoreName.Text;
				this.Grade = this.DrGrade.SelectedValue.ToString();
				this.CellPhone = this.txtCellPhone.Text;
				this.RealName = this.txtRealName.Text;
				this.MicroSignal = this.txtMicroSignal.Text;
                this.DeadlineStatus = int.Parse(this.DrDeadlineStatus.SelectedValue);
                this.currSelectTime = this.calendarStartDate.SelectedDate;
                this.FinishStatus = this.DrFinishStatus.SelectedValue;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			this.LoadParameters();
            SiteSettings siteSetting = SettingsManager.GetMasterSettings(false);

			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("Grade", this.DrGrade.Text);
			queryStrings.Add("StoreName", this.txtStoreName.Text);
			queryStrings.Add("CellPhone", this.txtCellPhone.Text);
			queryStrings.Add("RealName", this.txtRealName.Text);
			queryStrings.Add("MicroSignal", this.txtMicroSignal.Text);
			queryStrings.Add("Status", this.DrStatus.SelectedValue);
            queryStrings.Add("DeadlineStatus", this.DrDeadlineStatus.SelectedValue);
            queryStrings.Add("LimitTime", (this.calendarStartDate.SelectedDate.HasValue ? this.calendarStartDate.SelectedDate : DateTime.Now).ToString());
            queryStrings.Add("FinishStatus", this.DrFinishStatus.SelectedValue);
			queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}
        
        protected void reDistributor_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string str2;
            int result = 0;
            int.TryParse(e.CommandArgument.ToString(), out result);
            if ((result > 0) && ((str2 = e.CommandName) != null))
            {
                //if (str2 == "Frozen")
                //{
                //    VShopHelper.UpdateDistributorReferralStatusById(0, result);
                //    this.ShowMsg("处理成功！", false);
                //    this.ReBind(true);
                //}
                //else if (str2 == "OneYear")
                //{
                //    VShopHelper.UpdateDistributorDeadlineTimeById(1, result);
                //    this.ShowMsg("处理成功！", false);
                //    this.ReBind(true);
                //}
                //else if (str2 == "TwoYear")
                //{
                //    VShopHelper.UpdateDistributorDeadlineTimeById(2, result);
                //    this.ShowMsg("处理成功！", false);
                //    this.ReBind(true);
                //}
            }
        }

        protected void reDistributor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
            }
        }
    }
}
