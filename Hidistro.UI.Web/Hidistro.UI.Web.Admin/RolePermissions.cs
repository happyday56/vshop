using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public class RolePermissions : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSet1;
		protected System.Web.UI.WebControls.LinkButton btnSetTop;
		protected System.Web.UI.WebControls.CheckBox cbAll;
		protected System.Web.UI.WebControls.CheckBox cbBrandCategories;
		protected System.Web.UI.WebControls.CheckBox cbClientActivy;
		protected System.Web.UI.WebControls.CheckBox cbClientGroup;
		protected System.Web.UI.WebControls.CheckBox cbClientNew;
		protected System.Web.UI.WebControls.CheckBox cbClientSleep;
		protected System.Web.UI.WebControls.CheckBox cbCountDown;
		protected System.Web.UI.WebControls.CheckBox cbCoupons;
		protected System.Web.UI.WebControls.CheckBox cbCRMmanager;
		protected System.Web.UI.WebControls.CheckBox cbExpressComputerpes;
		protected System.Web.UI.WebControls.CheckBox cbExpressPrint;
		protected System.Web.UI.WebControls.CheckBox cbExpressTemplates;
		protected System.Web.UI.WebControls.CheckBox cbGifts;
		protected System.Web.UI.WebControls.CheckBox cbGroupBuy;
		protected System.Web.UI.WebControls.CheckBox cbInStock;
		protected System.Web.UI.WebControls.CheckBox cbManageCategories;
		protected System.Web.UI.WebControls.CheckBox cbManageCategoriesAdd;
		protected System.Web.UI.WebControls.CheckBox cbManageCategoriesDelete;
		protected System.Web.UI.WebControls.CheckBox cbManageCategoriesEdit;
		protected System.Web.UI.WebControls.CheckBox cbManageCategoriesView;
		protected System.Web.UI.WebControls.CheckBox cbManageMembers;
		protected System.Web.UI.WebControls.CheckBox cbManageMembersDelete;
		protected System.Web.UI.WebControls.CheckBox cbManageMembersEdit;
		protected System.Web.UI.WebControls.CheckBox cbManageMembersView;
		protected System.Web.UI.WebControls.CheckBox cbManageOrder;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderConfirm;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderDelete;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderEdit;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderRemark;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderSendedGoods;
		protected System.Web.UI.WebControls.CheckBox cbManageOrderView;
		protected System.Web.UI.WebControls.CheckBox cbManageProducts;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsAdd;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsDelete;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsDown;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsEdit;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsUp;
		protected System.Web.UI.WebControls.CheckBox cbManageProductsView;
		protected System.Web.UI.WebControls.CheckBox cbManageUsers;
		protected System.Web.UI.WebControls.CheckBox cbMarketing;
		protected System.Web.UI.WebControls.CheckBox cbMemberArealDistributionStatistics;
		protected System.Web.UI.WebControls.CheckBox cbMemberMarket;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanking;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanks;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanksAdd;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanksDelete;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanksEdit;
		protected System.Web.UI.WebControls.CheckBox cbMemberRanksView;
		protected System.Web.UI.WebControls.CheckBox cbOrderPromotion;
		protected System.Web.UI.WebControls.CheckBox cbOrderRefundApply;
		protected System.Web.UI.WebControls.CheckBox cbOrderReplaceApply;
		protected System.Web.UI.WebControls.CheckBox cbOrderReturnsApply;
		protected System.Web.UI.WebControls.CheckBox cbPageManger;
		protected System.Web.UI.WebControls.CheckBox cbPaymentModes;
		protected System.Web.UI.WebControls.CheckBox cbPictureMange;
		protected System.Web.UI.WebControls.CheckBox cbProductBatchExport;
		protected System.Web.UI.WebControls.CheckBox cbProductBatchUpload;
		protected System.Web.UI.WebControls.CheckBox cbProductCatalog;
		protected System.Web.UI.WebControls.CheckBox cbProductPromotion;
		protected System.Web.UI.WebControls.CheckBox cbProductSaleRanking;
		protected System.Web.UI.WebControls.CheckBox cbProductSaleStatistics;
		protected System.Web.UI.WebControls.CheckBox cbProductTypes;
		protected System.Web.UI.WebControls.CheckBox cbProductTypesAdd;
		protected System.Web.UI.WebControls.CheckBox cbProductTypesDelete;
		protected System.Web.UI.WebControls.CheckBox cbProductTypesEdit;
		protected System.Web.UI.WebControls.CheckBox cbProductTypesView;
		protected System.Web.UI.WebControls.CheckBox cbProductUnclassified;
		protected System.Web.UI.WebControls.CheckBox cbSaleList;
		protected System.Web.UI.WebControls.CheckBox cbSales;
		protected System.Web.UI.WebControls.CheckBox cbSaleTargetAnalyse;
		protected System.Web.UI.WebControls.CheckBox cbSaleTotalStatistics;
		protected System.Web.UI.WebControls.CheckBox cbShipper;
		protected System.Web.UI.WebControls.CheckBox cbShippingModes;
		protected System.Web.UI.WebControls.CheckBox cbShippingTemplets;
		protected System.Web.UI.WebControls.CheckBox cbShop;
		protected System.Web.UI.WebControls.CheckBox cbSiteContent;
		protected System.Web.UI.WebControls.CheckBox cbSubjectProducts;
		protected System.Web.UI.WebControls.CheckBox cbSummary;
		protected System.Web.UI.WebControls.CheckBox cbTotalReport;
		protected System.Web.UI.WebControls.CheckBox cbUserIncreaseStatistics;
		protected System.Web.UI.WebControls.CheckBox cbUserOrderStatistics;
		protected System.Web.UI.WebControls.CheckBox cbVotes;
		protected System.Web.UI.WebControls.Literal lblRoleName;
        protected System.Web.UI.WebControls.HiddenField hdPersissions;

        protected Literal litPrivilege;

		private int roleId;
		private void btnSet_Click(object sender, System.EventArgs e)
		{
			this.PermissionsSet(this.roleId);
			this.Page.Response.Redirect(Globals.GetAdminAbsolutePath(string.Format("/store/RolePermissions.aspx?roleId={0}&Status=1", this.roleId)));
		}
		private void LoadData(int roleId)
		{
			System.Collections.Generic.IList<int> privilegeByRoles = ManagerHelper.GetPrivilegeByRoles(roleId);
        
            var lstAllPrivilege = PrivilegeContext.GetPrivilegeModule();
            StringBuilder str = new StringBuilder();
            bool isChecked = false;

            foreach (var p in lstAllPrivilege)
            {
                isChecked = privilegeByRoles.Contains(p.Privilege);
                str.AppendFormat("<div style=\"font-weight:700;color:#000066\"><label><input type='checkbox' privilege='{0}' value='{2}' {3}/>{1}</label></div><div class=\"PurviewItemDivide\"></div>"
                    , p.Privilege, p.PrivilegeName, p.PrivilegeCode, isChecked ? "checked=checked" : "");
            }

            this.litPrivilege.Text = str.ToString();

            return;
            this.cbSummary.Checked = privilegeByRoles.Contains(1000);
			this.cbSiteContent.Checked = privilegeByRoles.Contains(1001);
			this.cbVotes.Checked = privilegeByRoles.Contains(2009);
			this.cbShippingTemplets.Checked = privilegeByRoles.Contains(1006);
			this.cbExpressComputerpes.Checked = privilegeByRoles.Contains(1007);
			this.cbPictureMange.Checked = privilegeByRoles.Contains(1009);
			this.cbProductTypesView.Checked = privilegeByRoles.Contains(3017);
			this.cbProductTypesAdd.Checked = privilegeByRoles.Contains(3018);
			this.cbProductTypesEdit.Checked = privilegeByRoles.Contains(3019);
			this.cbProductTypesDelete.Checked = privilegeByRoles.Contains(3020);
			this.cbManageCategoriesView.Checked = privilegeByRoles.Contains(3021);
			this.cbManageCategoriesAdd.Checked = privilegeByRoles.Contains(3022);
			this.cbManageCategoriesEdit.Checked = privilegeByRoles.Contains(3023);
			this.cbManageCategoriesDelete.Checked = privilegeByRoles.Contains(3024);
			this.cbBrandCategories.Checked = privilegeByRoles.Contains(3025);
			this.cbManageProductsView.Checked = privilegeByRoles.Contains(3001);
			this.cbManageProductsAdd.Checked = privilegeByRoles.Contains(3002);
			this.cbManageProductsEdit.Checked = privilegeByRoles.Contains(3003);
			this.cbManageProductsDelete.Checked = privilegeByRoles.Contains(3004);
			this.cbInStock.Checked = privilegeByRoles.Contains(3005);
			this.cbManageProductsUp.Checked = privilegeByRoles.Contains(3006);
			this.cbManageProductsDown.Checked = privilegeByRoles.Contains(3007);
			this.cbProductUnclassified.Checked = privilegeByRoles.Contains(3010);
			this.cbProductBatchUpload.Checked = privilegeByRoles.Contains(3012);
			this.cbProductBatchExport.Checked = privilegeByRoles.Contains(3026);
			this.cbSubjectProducts.Checked = privilegeByRoles.Contains(3011);
			this.cbClientGroup.Checked = privilegeByRoles.Contains(7007);
			this.cbClientActivy.Checked = privilegeByRoles.Contains(7009);
			this.cbClientNew.Checked = privilegeByRoles.Contains(7008);
			this.cbClientSleep.Checked = privilegeByRoles.Contains(7010);
			this.cbMemberRanksView.Checked = privilegeByRoles.Contains(5004);
			this.cbMemberRanksAdd.Checked = privilegeByRoles.Contains(5005);
			this.cbMemberRanksEdit.Checked = privilegeByRoles.Contains(5006);
			this.cbMemberRanksDelete.Checked = privilegeByRoles.Contains(5007);
			this.cbManageMembersView.Checked = privilegeByRoles.Contains(5001);
			this.cbManageMembersEdit.Checked = privilegeByRoles.Contains(5002);
			this.cbManageMembersDelete.Checked = privilegeByRoles.Contains(5003);
			this.cbMemberArealDistributionStatistics.Checked = privilegeByRoles.Contains(10008);
			this.cbUserIncreaseStatistics.Checked = privilegeByRoles.Contains(10009);
			this.cbMemberRanking.Checked = privilegeByRoles.Contains(10007);
			this.cbManageOrderView.Checked = privilegeByRoles.Contains(4001);
			this.cbManageOrderDelete.Checked = privilegeByRoles.Contains(4002);
			this.cbManageOrderEdit.Checked = privilegeByRoles.Contains(4003);
			this.cbManageOrderConfirm.Checked = privilegeByRoles.Contains(4004);
			this.cbManageOrderSendedGoods.Checked = privilegeByRoles.Contains(4005);
			this.cbExpressPrint.Checked = privilegeByRoles.Contains(4006);
			this.cbManageOrderRemark.Checked = privilegeByRoles.Contains(4008);
			this.cbExpressTemplates.Checked = privilegeByRoles.Contains(4009);
			this.cbShipper.Checked = privilegeByRoles.Contains(4010);
			this.cbPaymentModes.Checked = privilegeByRoles.Contains(1004);
			this.cbShippingModes.Checked = privilegeByRoles.Contains(1005);
			this.cbOrderRefundApply.Checked = privilegeByRoles.Contains(4012);
			this.cbOrderReturnsApply.Checked = privilegeByRoles.Contains(4014);
			this.cbOrderReplaceApply.Checked = privilegeByRoles.Contains(4013);
			this.cbSaleTotalStatistics.Checked = privilegeByRoles.Contains(10001);
			this.cbUserOrderStatistics.Checked = privilegeByRoles.Contains(10002);
			this.cbSaleList.Checked = privilegeByRoles.Contains(10003);
			this.cbSaleTargetAnalyse.Checked = privilegeByRoles.Contains(10004);
			this.cbProductSaleRanking.Checked = privilegeByRoles.Contains(10005);
			this.cbProductSaleStatistics.Checked = privilegeByRoles.Contains(10006);
			this.cbGifts.Checked = privilegeByRoles.Contains(8001);
			this.cbGroupBuy.Checked = privilegeByRoles.Contains(8005);
			this.cbCountDown.Checked = privilegeByRoles.Contains(8006);
			this.cbCoupons.Checked = privilegeByRoles.Contains(8007);
			this.cbProductPromotion.Checked = privilegeByRoles.Contains(8002);
			this.cbOrderPromotion.Checked = privilegeByRoles.Contains(8003);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Page.Request.QueryString["roleId"]))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				int.TryParse(this.Page.Request.QueryString["roleId"], out this.roleId);
				this.btnSet1.Click += new System.EventHandler(this.btnSet_Click);
                //this.btnSetTop.Click += new System.EventHandler(this.btnSet_Click);
				if (!this.Page.IsPostBack)
				{
					RoleInfo role = ManagerHelper.GetRole(this.roleId);
					this.lblRoleName.Text = role.RoleName;
				}
				if (this.Page.Request.QueryString["Status"] == "1")
				{
					this.ShowMsg("设置部门权限成功", true);
				}
				this.LoadData(this.roleId);
			}
		}
		private void PermissionsSet(int roleId)
		{
			string str = string.Empty;

            str = this.hdPersissions.Value;
            //return;
            //if (this.cbSummary.Checked)
            //{
            //    str = str + 1000 + ",";
            //}
            //if (this.cbSiteContent.Checked)
            //{
            //    str = str + 1001 + ",";
            //}
            //if (this.cbVotes.Checked)
            //{
            //    str = str + 2009 + ",";
            //}
            //if (this.cbShippingTemplets.Checked)
            //{
            //    str = str + 1006 + ",";
            //}
            //if (this.cbExpressComputerpes.Checked)
            //{
            //    str = str + 1007 + ",";
            //}
            //if (this.cbPictureMange.Checked)
            //{
            //    str = str + 1009 + ",";
            //}
            //if (this.cbProductTypesView.Checked)
            //{
            //    str = str + 3017 + ",";
            //}
            //if (this.cbProductTypesAdd.Checked)
            //{
            //    str = str + 3018 + ",";
            //}
            //if (this.cbProductTypesEdit.Checked)
            //{
            //    str = str + 3019 + ",";
            //}
            //if (this.cbProductTypesDelete.Checked)
            //{
            //    str = str + 3020 + ",";
            //}
            //if (this.cbManageCategoriesView.Checked)
            //{
            //    str = str + 3021 + ",";
            //}
            //if (this.cbManageCategoriesAdd.Checked)
            //{
            //    str = str + 3022 + ",";
            //}
            //if (this.cbManageCategoriesEdit.Checked)
            //{
            //    str = str + 3023 + ",";
            //}
            //if (this.cbManageCategoriesDelete.Checked)
            //{
            //    str = str + 3024 + ",";
            //}
            //if (this.cbBrandCategories.Checked)
            //{
            //    str = str + 3025 + ",";
            //}
            //if (this.cbManageProductsView.Checked)
            //{
            //    str = str + 3001 + ",";
            //}
            //if (this.cbManageProductsAdd.Checked)
            //{
            //    str = str + 3002 + ",";
            //}
            //if (this.cbManageProductsEdit.Checked)
            //{
            //    str = str + 3003 + ",";
            //}
            //if (this.cbManageProductsDelete.Checked)
            //{
            //    str = str + 3004 + ",";
            //}
            //if (this.cbInStock.Checked)
            //{
            //    str = str + 3005 + ",";
            //}
            //if (this.cbManageProductsUp.Checked)
            //{
            //    str = str + 3006 + ",";
            //}
            //if (this.cbManageProductsDown.Checked)
            //{
            //    str = str + 3007 + ",";
            //}
            //if (this.cbProductUnclassified.Checked)
            //{
            //    str = str + 3010 + ",";
            //}
            //if (this.cbProductBatchUpload.Checked)
            //{
            //    str = str + 3012 + ",";
            //}
            //if (this.cbProductBatchExport.Checked)
            //{
            //    str = str + 3026 + ",";
            //}
            //if (this.cbSubjectProducts.Checked)
            //{
            //    str = str + 3011 + ",";
            //}
            //if (this.cbClientGroup.Checked)
            //{
            //    str = str + 7007 + ",";
            //}
            //if (this.cbClientNew.Checked)
            //{
            //    str = str + 7008 + ",";
            //}
            //if (this.cbClientSleep.Checked)
            //{
            //    str = str + 7010 + ",";
            //}
            //if (this.cbClientActivy.Checked)
            //{
            //    str = str + 7009 + ",";
            //}
            //if (this.cbMemberRanksView.Checked)
            //{
            //    str = str + 5004 + ",";
            //}
            //if (this.cbMemberRanksAdd.Checked)
            //{
            //    str = str + 5005 + ",";
            //}
            //if (this.cbMemberRanksEdit.Checked)
            //{
            //    str = str + 5006 + ",";
            //}
            //if (this.cbMemberRanksDelete.Checked)
            //{
            //    str = str + 5007 + ",";
            //}
            //if (this.cbManageMembersView.Checked)
            //{
            //    str = str + 5001 + ",";
            //}
            //if (this.cbManageMembersEdit.Checked)
            //{
            //    str = str + 5002 + ",";
            //}
            //if (this.cbManageMembersDelete.Checked)
            //{
            //    str = str + 5003 + ",";
            //}
            //if (this.cbMemberArealDistributionStatistics.Checked)
            //{
            //    str = str + 10008 + ",";
            //}
            //if (this.cbUserIncreaseStatistics.Checked)
            //{
            //    str = str + 10009 + ",";
            //}
            //if (this.cbMemberRanking.Checked)
            //{
            //    str = str + 10007 + ",";
            //}
            //if (this.cbManageOrderView.Checked)
            //{
            //    str = str + 4001 + ",";
            //}
            //if (this.cbManageOrderDelete.Checked)
            //{
            //    str = str + 4002 + ",";
            //}
            //if (this.cbManageOrderEdit.Checked)
            //{
            //    str = str + 4003 + ",";
            //}
            //if (this.cbManageOrderConfirm.Checked)
            //{
            //    str = str + 4004 + ",";
            //}
            //if (this.cbManageOrderSendedGoods.Checked)
            //{
            //    str = str + 4005 + ",";
            //}
            //if (this.cbExpressPrint.Checked)
            //{
            //    str = str + 4006 + ",";
            //}
            //if (this.cbExpressTemplates.Checked)
            //{
            //    str = str + 4009 + ",";
            //}
            //if (this.cbShipper.Checked)
            //{
            //    str = str + 4010 + ",";
            //}
            //if (this.cbPaymentModes.Checked)
            //{
            //    str = str + 1004 + ",";
            //}
            //if (this.cbShippingModes.Checked)
            //{
            //    str = str + 1005 + ",";
            //}
            //if (this.cbSaleTotalStatistics.Checked)
            //{
            //    str = str + 10001 + ",";
            //}
            //if (this.cbUserOrderStatistics.Checked)
            //{
            //    str = str + 10002 + ",";
            //}
            //if (this.cbSaleList.Checked)
            //{
            //    str = str + 10003 + ",";
            //}
            //if (this.cbSaleTargetAnalyse.Checked)
            //{
            //    str = str + 10004 + ",";
            //}
            //if (this.cbProductSaleRanking.Checked)
            //{
            //    str = str + 10005 + ",";
            //}
            //if (this.cbProductSaleStatistics.Checked)
            //{
            //    str = str + 10006 + ",";
            //}
            //if (this.cbOrderRefundApply.Checked)
            //{
            //    str = str + 4012 + ",";
            //}
            //if (this.cbOrderReplaceApply.Checked)
            //{
            //    str = str + 4013 + ",";
            //}
            //if (this.cbOrderReturnsApply.Checked)
            //{
            //    str = str + 4014 + ",";
            //}
            //if (this.cbGifts.Checked)
            //{
            //    str = str + 8001 + ",";
            //}
            //if (this.cbGroupBuy.Checked)
            //{
            //    str = str + 8005 + ",";
            //}
            //if (this.cbCountDown.Checked)
            //{
            //    str = str + 8006 + ",";
            //}
            //if (this.cbCoupons.Checked)
            //{
            //    str = str + 8007 + ",";
            //}
            //if (this.cbProductPromotion.Checked)
            //{
            //    str = str + 8002 + ",";
            //}
            //if (this.cbOrderPromotion.Checked)
            //{
            //    str = str + 8003 + ",";
            //}
            //if (!string.IsNullOrEmpty(str))
            //{
            //    str = str.Substring(0, str.LastIndexOf(","));
            //}
            ManagerHelper.ClearRolePrivilege(roleId);
			ManagerHelper.AddPrivilegeInRoles(roleId, str);
		
		}
	}
}
