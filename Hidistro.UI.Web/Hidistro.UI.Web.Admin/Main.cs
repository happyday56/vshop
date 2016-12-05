using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Summary)]
	public class Main : AdminPage
	{
		protected HiControls HiControlsId;
		protected ClassShowOnDataLitl lblOrderPriceMonth;
		protected ClassShowOnDataLitl lblOrderPriceYesterDay;
		protected ClassShowOnDataLitl lblTodayFinishOrder;
		protected ClassShowOnDataLitl lblTodayOrderAmout;
		protected ClassShowOnDataLitl lblTotalMembers;
		protected ClassShowOnDataLitl lblTotalProducts;
		protected ClassShowOnDataLitl lblUserNewAddYesterToday;
		protected ClassShowOnDataLitl lblYesterdayFinishOrder;
		protected ClassShowOnDataLitl ltrTodayAddMemberNumber;
		protected System.Web.UI.WebControls.HyperLink ltrWaitSendOrdersNumber;
		protected System.Web.UI.WebControls.HyperLink WaitForRefund;

        protected Literal litFastMenu;

        private void BindStatistics(StatisticsInfo statistics)
        {
            var currentMember = ManagerHelper.GetCurrentManager();
            if (statistics.OrderNumbWaitConsignment > 0)
            {
                this.ltrWaitSendOrdersNumber.NavigateUrl = "javascript:ShowSecondMenuLeft('微订单','sales/manageorder.aspx','" + Globals.ApplicationPath + "/Admin/sales/ManageOrder.aspx?orderStatus=2')";
                this.ltrWaitSendOrdersNumber.Text = statistics.OrderNumbWaitConsignment.ToString() + "条";
            }
            else
            {
                this.ltrWaitSendOrdersNumber.Text = "<font style=\"color:#2d2d2d\">0条</font>";
            }
            if (statistics.GroupBuyNumWaitRefund > 0)
            {
                this.WaitForRefund.NavigateUrl = "javascript:ShowSecondMenuLeft('微营销','promotion/groupbuys.aspx','" + Globals.ApplicationPath + "/Admin/promotion/groupbuys.aspx?state=5')";
                this.WaitForRefund.Text = statistics.GroupBuyNumWaitRefund.ToString() + "个";
            }
            else
            {
                this.WaitForRefund.Text = "<font style=\"color:#2d2d2d\">0个</font>";
            }
            this.lblTodayOrderAmout.Text = ((statistics.OrderPriceToday > 0m) ? ("￥" + Globals.FormatMoney(statistics.OrderPriceToday)) : string.Empty);
            this.ltrTodayAddMemberNumber.Text = ((statistics.UserNewAddToday > 0) ? statistics.UserNewAddToday.ToString() : string.Empty);
            this.lblTodayFinishOrder.Text = ((statistics.TodayFinishOrder > 0) ? statistics.TodayFinishOrder.ToString() : string.Empty);
            this.lblYesterdayFinishOrder.Text = ((statistics.YesterdayFinishOrder > 0) ? statistics.YesterdayFinishOrder.ToString() : string.Empty);
            this.lblOrderPriceYesterDay.Text = ((statistics.OrderPriceYesterday > 0m) ? ("￥" + statistics.OrderPriceYesterday.ToString("F2")) : string.Empty);
            this.lblUserNewAddYesterToday.Text = ((statistics.UserNewAddYesterToday > 0) ? (statistics.UserNewAddYesterToday.ToString() + "位") : string.Empty);
            this.lblTotalMembers.Text = ((statistics.TotalMembers > 0) ? (statistics.TotalMembers.ToString() + "位") : string.Empty);
            this.lblTotalProducts.Text = ((statistics.TotalProducts > 0) ? (statistics.TotalProducts.ToString() + "条") : string.Empty);
            this.lblOrderPriceMonth.Text = ((statistics.OrderPriceMonth > 0m) ? ("￥" + statistics.OrderPriceMonth.ToString("F2")) : string.Empty);

            var currentPrivilege = ManagerHelper.GetPrivilegeByRoles(currentMember.RoleId);
            //快捷入口目前被固定了，所以写死
            Dictionary<int, string> dicFastMenu = new Dictionary<int, string>() {
               {10004,"<li><a href=\"javascript:ShowSecondMenuLeft('微商品','product/selectcategory.aspx',null)\"><img src=\"images/1.png\" /><br />添加商品</a></li>"},
               {10006,"<li><a href=\"javascript:ShowSecondMenuLeft('微订单','sales/manageorder.aspx',null)\"><img src=\"images/4.png\" /><br />订单列表</a></li>"},
               {10002,"<li><a href=\"javascript:ShowSecondMenuLeft('微会员','member/managemembers.aspx',null)\"><img src=\"images/5.png\" /><br />会员管理</a></li>"},
               {10007,"<li><a href=\"javascript:ShowSecondMenuLeft('微统计','sales/salereport.aspx',null)\"><img src=\"images/10.png\" /><br />零售统计</a></li>"},
               {10001,"<li><a href=\"javascript:ShowSecondMenuLeft('微配置','vshop/ManageMenu.aspx',null)\"><img src=\"images/21.png\" /><br />自定义菜单</a></li><li><a href=\"javascript:ShowSecondMenuLeft('微配置','vshop/ReplyOnKey.aspx',null)\"><img src=\"images/23.png\" /><br />自定义回复</a></li>"},
            };
            string strFastMenus = "";
            foreach (var key in dicFastMenu.Keys)
            {
                if (currentPrivilege.Contains(key))
                {
                    strFastMenus += dicFastMenu[key];
                }
            }
            this.litFastMenu.Text = strFastMenus;

        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				StatisticsInfo statistics = SalesHelper.GetStatistics();
				this.BindStatistics(statistics);
			}
		}
	}
}
