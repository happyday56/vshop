using ASPNET.WebControls;
using Hidistro.ControlPanel.Promotions;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.GroupBuy)]
	public class GroupBuys : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton btnOrder;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Grid grdGroupBuyList;
		protected System.Web.UI.WebControls.DropDownList GroupBuyState;
		protected PageSize hrefPageSize;
		protected ImageLinkButton lkbtnDeleteCheck;
		protected Pager pager;
		protected Pager pager1;
		private string productName = string.Empty;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		private void BindGroupBuy()
		{
			GroupBuyQuery query = new GroupBuyQuery
			{
				ProductName = this.productName,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortBy = "DisplaySequence",
				SortOrder = SortAction.Desc
			};
			int num = 0;
			if (int.TryParse(base.Request.QueryString["state"], out num))
			{
				query.State = num;
			}
			DbQueryResult groupBuyList = GroupBuyHelper.GetGroupBuyList(query);
			this.grdGroupBuyList.DataSource = groupBuyList.Data;
			this.grdGroupBuyList.DataBind();
			this.pager.TotalRecords = groupBuyList.TotalRecords;
			this.pager1.TotalRecords = groupBuyList.TotalRecords;
		}
		private void BindGroupBuyState()
		{
			this.GroupBuyState.Items.Clear();
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("所有状态", "0"));
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("进行中", "1"));
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("结束未处理", "2"));
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("成功", "3"));
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("失败", "4"));
			this.GroupBuyState.Items.Add(new System.Web.UI.WebControls.ListItem("失败待退款", "5"));
			int result = 0;
			int.TryParse(base.Request.QueryString["state"], out result);
			this.GroupBuyState.SelectedIndex = result;
		}
		private void btnOrder_Click(object sender, System.EventArgs e)
		{
			foreach (System.Web.UI.WebControls.GridViewRow row in this.grdGroupBuyList.Rows)
			{
				int result = 0;
				System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)row.FindControl("txtSequence");
				if (int.TryParse(box.Text.Trim(), out result))
				{
					int groupBuyId = (int)this.grdGroupBuyList.DataKeys[row.RowIndex].Value;
					GroupBuyHelper.SwapGroupBuySequence(groupBuyId, result);
				}
			}
			this.BindGroupBuy();
		}
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.ReloadHelpList(true);
		}
		private void grdGroupBuyList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
			{
				FormatedMoneyLabel label = (FormatedMoneyLabel)e.Row.FindControl("lblCurrentPrice");
				int groupBuyId = System.Convert.ToInt32(this.grdGroupBuyList.DataKeys[e.Row.RowIndex].Value.ToString());
				int prodcutQuantity = int.Parse(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "ProdcutQuantity").ToString());
				label.Money = GroupBuyHelper.GetCurrentPrice(groupBuyId, prodcutQuantity);
			}
		}
		private void grdGroupBuyList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			GroupBuyInfo groupBuy = GroupBuyHelper.GetGroupBuy((int)this.grdGroupBuyList.DataKeys[e.RowIndex].Value);
			if (groupBuy.StartDate < System.DateTime.Now && (groupBuy.Status == GroupBuyStatus.UnderWay || groupBuy.Status == GroupBuyStatus.EndUntreated))
			{
				this.ShowMsg("团购活动正在进行中或结束未处理，不允许删除", false);
			}
			else
			{
				if (GroupBuyHelper.DeleteGroupBuy((int)this.grdGroupBuyList.DataKeys[e.RowIndex].Value))
				{
					this.BindGroupBuy();
					this.ShowMsg("成功删除了选择的团购活动", true);
				}
				else
				{
					this.ShowMsg("删除失败", false);
				}
			}
		}
		private void lkbtnDeleteCheck_Click(object sender, System.EventArgs e)
		{
			int? nullable = null;
			foreach (System.Web.UI.WebControls.GridViewRow row in this.grdGroupBuyList.Rows)
			{
				System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("checkboxCol");
				if (box.Checked)
				{
					nullable = new int?(nullable.GetValueOrDefault());
					int groupBuyId = System.Convert.ToInt32(this.grdGroupBuyList.DataKeys[row.RowIndex].Value, System.Globalization.CultureInfo.InvariantCulture);
					GroupBuyInfo groupBuy = GroupBuyHelper.GetGroupBuy(groupBuyId);
					if (groupBuy.Status != GroupBuyStatus.UnderWay && groupBuy.Status != GroupBuyStatus.EndUntreated)
					{
						nullable = new int?(nullable.GetValueOrDefault() + 1);
						GroupBuyHelper.DeleteGroupBuy(groupBuyId);
					}
				}
			}
			if (nullable.HasValue)
			{
				this.BindGroupBuy();
				this.ShowMsg(string.Format("成功删除{0}条团购活动", nullable), true);
			}
			else
			{
				this.ShowMsg("请先选择需要删除的团购活动", false);
			}
		}
		private void LoadParameters()
		{
			if (!base.IsPostBack)
			{
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["productName"]))
				{
					this.productName = Globals.UrlDecode(this.Page.Request.QueryString["productName"]);
				}
				this.txtProductName.Text = this.productName;
			}
			else
			{
				this.productName = this.txtProductName.Text;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
			this.grdGroupBuyList.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdGroupBuyList_RowDeleting);
			this.grdGroupBuyList.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdGroupBuyList_RowDataBound);
			this.lkbtnDeleteCheck.Click += new System.EventHandler(this.lkbtnDeleteCheck_Click);
			this.LoadParameters();
			if (!base.IsPostBack)
			{
				this.BindGroupBuy();
				this.BindGroupBuyState();
			}
			CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
		}
		private void ReloadHelpList(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("productName", Globals.UrlEncode(this.txtProductName.Text.Trim()));
			if (!isSearch)
			{
				queryStrings.Add("PageIndex", this.pager.PageIndex.ToString());
			}
			queryStrings.Add("PageSize", this.hrefPageSize.SelectedSize.ToString());
			queryStrings.Add("SortBy", this.grdGroupBuyList.SortOrderBy);
			queryStrings.Add("SortOrder", SortAction.Desc.ToString());
			queryStrings.Add("state", this.GroupBuyState.SelectedValue);
			base.ReloadPage(queryStrings);
		}
	}
}
