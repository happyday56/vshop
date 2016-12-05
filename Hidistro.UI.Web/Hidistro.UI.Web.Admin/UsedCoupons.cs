using ASPNET.WebControls;
using Hidistro.ControlPanel.Promotions;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Coupons)]
	public class UsedCoupons : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		private string couponName = string.Empty;
		private string couponOrder = string.Empty;
		private int? couponstatus;
		protected System.Web.UI.WebControls.DropDownList Dpstatus;
		protected Grid grdCoupons;
		protected Pager pager;
		protected System.Web.UI.WebControls.TextBox txtCouponName;
		protected System.Web.UI.WebControls.TextBox txtOrderID;
		protected void BindCouponList()
		{
			CouponItemInfoQuery query = new CouponItemInfoQuery
			{
				CounponName = this.couponName,
				OrderId = this.couponOrder,
				CouponStatus = this.couponstatus,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortBy = "GenerateTime",
				SortOrder = SortAction.Desc
			};
			DbQueryResult couponsList = CouponHelper.GetCouponsList(query);
			this.pager.TotalRecords = couponsList.TotalRecords;
			this.grdCoupons.DataSource = couponsList.Data;
			this.grdCoupons.DataBind();
		}
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReloadHelpList(true);
		}
		protected string IsCouponEnd(object endtime)
		{
			System.DateTime time = System.Convert.ToDateTime(endtime);
			string result;
			if (time.CompareTo(System.DateTime.Now) > 0)
			{
				result = time.ToString();
			}
			else
			{
				result = "已过期";
			}
			return result;
		}
		private void LoadParameters()
		{
			if (!base.IsPostBack)
			{
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["couponName"]))
				{
					this.couponName = Globals.UrlDecode(this.Page.Request.QueryString["couponName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderID"]))
				{
					this.couponOrder = Globals.UrlDecode(this.Page.Request.QueryString["OrderID"]);
				}
				if (!string.IsNullOrEmpty(base.Request.QueryString["CouponStatus"]))
				{
					this.couponstatus = new int?(System.Convert.ToInt32(base.Request.QueryString["CouponStatus"]));
				}
				this.txtOrderID.Text = this.couponOrder;
				this.txtCouponName.Text = this.couponName;
				this.Dpstatus.SelectedValue = System.Convert.ToString(this.couponstatus);
			}
			else
			{
				this.couponName = this.txtCouponName.Text;
				this.couponOrder = this.txtOrderID.Text;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LoadParameters();
			if (!base.IsPostBack)
			{
				this.BindCouponList();
			}
		}
		private void ReloadHelpList(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("couponName", Globals.UrlEncode(this.txtCouponName.Text.Trim()));
			queryStrings.Add("OrderID", Globals.UrlEncode(this.txtOrderID.Text.Trim()));
			queryStrings.Add("CouponStatus", this.Dpstatus.SelectedValue);
			if (!isSearch)
			{
				queryStrings.Add("PageIndex", this.pager.PageIndex.ToString());
			}
			queryStrings.Add("SortOrder", SortAction.Desc.ToString());
			base.ReloadPage(queryStrings);
		}
	}
}
