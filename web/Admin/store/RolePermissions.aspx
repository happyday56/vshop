<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RolePermissions.aspx.cs" Inherits="Hidistro.UI.Web.Admin.RolePermissions" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Import Namespace="Hidistro.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
<style>
	.PurviewItem {clear:both}
	.PurviewItemSave{float:left;height:25px;line-height:25px;padding-left:20px;margin:0 0 0 5px;_margin-left:3px;vertical-align:middle;background:url(images/saveitem.gif) no-repeat 0px 5px; padding-left:20px;}
	.PurviewItem ul{width:850px;list-style:none;}
	.PurviewItem ul li{float:left;height:20px;line-height:20px;margin-right:8px;width:140px;}
	.PurviewItem ol {clear:both;padding-left:98px;}
	.PurviewItem ol li{float:left;height:20px;line-height:20px;margin-right:8px;width:140px;}
	.PurviewItemDivide {height:1px;width:100%;overflow:hidden;background-color:#ddd;margin:5px 0;}
	.PurviewItemBackground {background:#E1F3FF;border:1px solid #8ACEFF;} 
	.clear{ clear:both;}

</style> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
 <div class="dataarea mainwidth databody">
  <div class="title">
  <em><img src="../images/03.gif" width="32" height="32" /></em>
  <h1>设置部门权限</h1>
  <span>根据不同的部门设置不同的权限，方便控制店铺的管理 </span></div>
        <div class="datalist">
        <table width="100%"  height="30px"  border="0" cellspacing="0">
		    <tr class="table_title">	          
	          <td width="9%" class="td_left"><strong>当前部门：</strong></td>
	          <td align="left" class="td_right"><span style=" font-weight:800;"><strong><asp:Literal runat="server" Id="lblRoleName"></asp:Literal></strong></span></td>	          
      </tr>
      
      </table>
      
      <div style="margin-left:15px; margin-top:10px;">
          <span class="submit_btnquanxuan"><%--<asp:LinkButton ID="btnSetTop" runat="server" Text="保存" />--%></span>
	  <span class="submit_btnchexiao"><a href="Roles.aspx">返回</a></span></div>
         
           
		  <div class="grdGroupList clear" style="padding-left:10px;margin-top:5px">

			<div style="color:Blue;font-weight:700;"><label><asp:CheckBox ID="cbAll" runat="server"  />全部选定</label></div>
			<div class="PurviewItemDivide"></div>

            <asp:Literal runat="server" ID="litPrivilege" />

			<%--<div style="font-weight:700;color:#000066"><label><asp:CheckBox ID="cbSummary" runat="server" />后台即时营业信息</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="font-weight:700;color:#000066"><label><asp:CheckBox ID="cbShop" runat="server"  />店铺管理</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="padding-left:20px;">
				<div class="PurviewItem">
					<ul>
						<li style="width:90px;font-weight:700;">基本设置：</li>
						<li><label><asp:CheckBox ID="cbSiteContent" runat="server" />网店基本设置</label></li>		
					</ul>
				</div>
                
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">支付设置：</li>
						<li><label><asp:CheckBox ID="cbPaymentModes" runat="server" />支付方式</label></li>
					</ul>
				</div>
                <div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">配送设置：</li>
						<li><label><asp:CheckBox ID="cbShippingModes" runat="server" />配送方式列表</label></li>
                        <li><label><asp:CheckBox ID="cbShippingTemplets" runat="server" />运费模板</label></li>
                        <li><label><asp:CheckBox ID="cbExpressComputerpes" runat="server" />物流公司</label></li>
					</ul>
				</div>

               <div class="PurviewItem">
					<ul>
					    <li style="width:100px;font-weight:700;">图库管理：</li>
						<li><label><asp:CheckBox ID="cbPictureMange" runat="server" />图库管理</label></li>
					</ul>
				</div>
			</div>


           <div style="clear:both;margin-top:40px;font-weight:700;color:#000066"><label><asp:CheckBox ID="cbPageManger" runat="server"  />页面管理</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="padding-left:20px;">
                <div class="PurviewItem">                	
                    <ol>
						<li><label><asp:CheckBox ID="cbVotes" runat="server" />投票调查</label></li>
                    </ol>
                </div>
            </div>
           

            <div style="clear:both;margin-top:40px;font-weight:700; color:#000066"><label><asp:CheckBox ID="cbProductCatalog" runat="server"  />商品管理</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="padding-left:20px;">
			
					
				<div class="PurviewItem">
					<ul>
						<li style="width:90px;font-weight:700;">商品管理：</li>
						<li><label><asp:CheckBox ID="cbManageProducts" runat="server"  />商品：</label></li>
						<li><label><asp:CheckBox ID="cbManageProductsView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbManageProductsAdd" runat="server" />添加</label></li>
						<li><label><asp:CheckBox ID="cbManageProductsEdit" runat="server" />编辑</label></li>
						<li><label><asp:CheckBox ID="cbManageProductsDelete" runat="server" />删除</label></li>
						<li style="width: 90px">&nbsp;</li>
						<li>&nbsp;</li>
					    <li><label><asp:CheckBox ID="cbInStock" runat="server" />入库</label></li>
					    <li><label><asp:CheckBox ID="cbManageProductsUp" runat="server" />上架</label></li>
					    <li><label><asp:CheckBox ID="cbManageProductsDown" runat="server" />下架</label></li>		
					</ul>					
					<ol>
					<li><label><asp:CheckBox ID="cbProductUnclassified" runat="server" />商品扩展分类</label></li>
					<li><label><asp:CheckBox ID="cbSubjectProducts" runat="server" />商品标签管理</label></li>
					<li><label><asp:CheckBox ID="cbProductBatchUpload" runat="server" />批量上传</label></li>
				    <li><label><asp:CheckBox ID="cbProductBatchExport" runat="server" />批量导出</label></li>
					</ol>
				</div>

                	<div class="PurviewItem">
					<ul>
						<li style="width:90px;font-weight:700;">商品类型：</li>
						<li><label><asp:CheckBox ID="cbProductTypes" runat="server"  />商品类型：</label></li>
						<li><label><asp:CheckBox ID="cbProductTypesView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbProductTypesAdd" runat="server" />添加</label></li>
						<li><label><asp:CheckBox ID="cbProductTypesEdit" runat="server" />编辑</label></li>
						<li><label><asp:CheckBox ID="cbProductTypesDelete" runat="server" />删除</label></li>
					</ul>
				</div>
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">商品分类：</li>
						<li><label><asp:CheckBox ID="cbManageCategories" runat="server"  />商品分类：</label></li>
						<li><label><asp:CheckBox ID="cbManageCategoriesView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbManageCategoriesAdd" runat="server" />添加</label></li>
						<li><label><asp:CheckBox ID="cbManageCategoriesEdit" runat="server" />编辑</label></li>
						<li><label><asp:CheckBox ID="cbManageCategoriesDelete" runat="server" />删除</label></li>
					</ul>
				</div>
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">品牌分类：</li>
						<li><label><asp:CheckBox ID="cbBrandCategories" runat="server"  />品牌分类</label></li>						
					</ul>
				</div>		
			</div>

                
			<div style="clear:both;margin-top:40px;font-weight:700; color:#000066"><label><asp:CheckBox ID="cbSales" runat="server"  />订单管理</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="padding-left:20px;">
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">订单管理：</li>
						<li><label><asp:CheckBox ID="cbManageOrder" runat="server"  />订单管理：</label></li>
						<li><label><asp:CheckBox ID="cbManageOrderView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbManageOrderDelete" runat="server" />删除</label></li>
						<li><label><asp:CheckBox ID="cbManageOrderEdit" runat="server" />修改</label></li>
						<li><label><asp:CheckBox ID="cbManageOrderConfirm" runat="server" />确认收款</label></li>
						<li style="width:90px;">&nbsp;</li>
						<li><label><asp:CheckBox ID="cbManageOrderSendedGoods" runat="server" />订单发货</label></li>
						<li><label><asp:CheckBox ID="cbExpressPrint" runat="server" />快递单打印</label></li>
						<li><label><asp:CheckBox ID="cbManageOrderRemark" runat="server" />管理员备注</label></li>
					</ul>
				
				</div>

				<div class="PurviewItem">
					<ul>
					   <li style="width:90px;font-weight:700;">快递单模板：</li>
					    <li style="width:90px;"><label><asp:CheckBox ID="cbExpressTemplates" runat="server" />快递单模板</label></li>
					</ul>
				</div>
				<div class="PurviewItem">
					<ul>
					   <li style="width:90px;font-weight:700;">发货人信息：</li>
					    <li style="width:90px;"><label><asp:CheckBox ID="cbShipper" runat="server" />发货人信息</label></li>
					</ul>
				</div>
	            <div class="PurviewItem" style="display:none;">
					<ul>
					   <li style="width:90px;font-weight:700;">退换货设置：</li>
					    <li style="width:90px;"><label><asp:CheckBox ID="cbOrderRefundApply" runat="server" />退款申请单</label></li>
                        <li style="width:90px;"><label><asp:CheckBox ID="cbOrderReturnsApply" runat="server" />退货申请单</label></li>
                        <li style="width:90px;"><label><asp:CheckBox ID="cbOrderReplaceApply" runat="server" />换货申请单</label></li>
					</ul>
				</div>

			</div>
                        
			<div style="clear:both;margin-top:40px;font-weight:700; color:#000066"><label><asp:CheckBox ID="cbManageUsers" runat="server"  />会员管理</label></div>
			<div class="PurviewItemDivide"></div>
			<div style="padding-left:20px;">
	
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">会员：</li>
						<li><label><asp:CheckBox ID="cbManageMembers" runat="server"  />会员：</label></li>
						<li><label><asp:CheckBox ID="cbManageMembersView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbManageMembersEdit" runat="server" />编辑</label></li>
						<li><label><asp:CheckBox ID="cbManageMembersDelete" runat="server" />删除</label></li>
					</ul>
				</div>					
				
                
				<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">会员等级：</li>
						<li><label><asp:CheckBox ID="cbMemberRanks" runat="server"  />会员等级：</label></li>
						<li><label><asp:CheckBox ID="cbMemberRanksView" runat="server" />查看</label></li>
						<li><label><asp:CheckBox ID="cbMemberRanksAdd" runat="server" />添加</label></li>
						<li><label><asp:CheckBox ID="cbMemberRanksEdit" runat="server" />编辑</label></li>
						<li><label><asp:CheckBox ID="cbMemberRanksDelete" runat="server" />删除</label></li>
					</ul>
				</div>
			</div>

           	<div style="clear:both;margin-top:40px;font-weight:700;color:#000066"><label><asp:CheckBox ID="cbCRMmanager" runat="server"  />CRM管理</label></div>
			<div class="PurviewItemDivide"></div>
            <div style="padding-left:20px;">
                <div class="PurviewItem">
                     <ul>
                           <li style="width:100px;font-weight:700;">会员深度营销：</li>
                           <li style="margin-left:-10px;"><label><asp:CheckBox ID="cbMemberMarket" runat="server"  />会员深度营销：</label></li>
                           <li><label><asp:CheckBox ID="cbClientGroup" runat="server" />客户分组</label></li>
                           <li><label><asp:CheckBox ID="cbClientNew" runat="server" />新客户</label></li>
                           <li><label><asp:CheckBox ID="cbClientActivy" runat="server" />活跃客户</label></li>
                           <li><label><asp:CheckBox ID="cbClientSleep" runat="server" />休眠客户</label></li>
                     </ul>
                </div>
            </div>

            <div style="clear:both;margin-top:40px;font-weight:700;color:#000066"><label><asp:CheckBox ID="cbMarketing" runat="server"  />营销推广</label></div>
			<div class="PurviewItemDivide"></div>
             <div style="padding-left:20px;">
                <div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">积分商城：</li>
						<li><label><asp:CheckBox ID="cbGifts" runat="server" />礼品</label></li>
					</ul>
				</div>

                   <div class="PurviewItem" style="display:none;">
					<ul>
					    <li style="width:100px;font-weight:700;">店铺促销活动：</li>
						<li><label><asp:CheckBox ID="cbProductPromotion" runat="server" />商品促销</label></li>
                        <li><label><asp:CheckBox ID="cbOrderPromotion" runat="server" />订单促销</label></li>
                        <li><label><asp:CheckBox ID="cbGroupBuy" runat="server" />团购</label></li>
                        <li><label><asp:CheckBox ID="cbCountDown" runat="server" />限时抢购</label></li>

					</ul>
				</div>
                	<div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">优惠券：</li>
						<li><label><asp:CheckBox ID="cbCoupons" runat="server" />优惠券</label></li>
					</ul>
				</div>
             </div>


            <div style="clear:both;margin-top:40px;font-weight:700;color:#000066"><label><asp:CheckBox ID="cbTotalReport" runat="server"  />统计报表</label></div>
			<div class="PurviewItemDivide"></div>
             <div style="padding-left:20px;">
                 <div class="PurviewItem">
					<ul>
					    <li style="width:90px;font-weight:700;">零售业务统计：</li>
						<li><label><asp:CheckBox ID="cbSaleTotalStatistics" runat="server" />生意报告</label></li>
						<li><label><asp:CheckBox ID="cbUserOrderStatistics" runat="server" />订单统计</label></li>
						<li><label><asp:CheckBox ID="cbSaleList" runat="server" />销售明细</label></li>
						<li><label><asp:CheckBox ID="cbSaleTargetAnalyse" runat="server" />销售指标分析</label></li>
					</ul>
					<ol>
						<li><label><asp:CheckBox ID="cbProductSaleRanking" runat="server" />商品销售排行</label></li>
						<li style="width:130px;"><label><asp:CheckBox ID="cbProductSaleStatistics" runat="server" />商品访问与购买次数 </label></li>
                        <li><label><asp:CheckBox ID="cbMemberRanking" runat="server" />会员消费排行</label></li>
						<li style="width:110px;"><label><asp:CheckBox ID="cbMemberArealDistributionStatistics" runat="server" />会员分布统计</label></li>
						<li style="width:110px;"><label><asp:CheckBox ID="cbUserIncreaseStatistics" runat="server" />会员增长统计</label></li>
					</ol>			
				</div>
             </div>--%>

             

			<div class="PurviewItemDivide"></div>
            
	        <div style="margin-top:10px;margin-bottom:10px;">
                <asp:HiddenField runat="server" ID="hdPersissions" />
                <asp:Button ID="btnSet1" runat="server" Text="保 存" class="submit_queding" OnClientClick="return getPermissions();" ></asp:Button>
                <input type="button" value="返 回" class="submit_queding" onclick="link()" />
	        </div>
            
        </div>
		</div>
		</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
<Hi:Script runat="server" Src="/admin/js/PrivilegeInRoles.js" />

    <script type="text/javascript" language="javascript">
        function link()
        {
            window.location.href='Roles.aspx';
        }
        function getPermissions() {
            var persissions = [];
            $.each($("[type=checkbox][Privilege]:checked"), function (a, b) {
                persissions.push($(b).attr("privilege"));
            });
            $("[type=hidden][id*=hdPersissions]").val(persissions.join(','));
            return true;
        }
    </script>
</asp:Content>
