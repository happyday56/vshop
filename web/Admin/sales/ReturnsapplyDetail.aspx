<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeFile="ReturnsapplyDetail.aspx.cs" Inherits="Hidistro.UI.Web.Admin.sales.ReturnsapplyDetail" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="Server">
    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title">
                <em>
                    <img src="../images/04.gif" width="32" height="32" /></em> <span>退货(款)的详细信息</span>
            </div>
            <div class="datalist">
                <table width="100%" border="1" cellspacing="0" cellpadding="5" style="table-layout: fixed; border: solid 1px;">
                    <tr style="background-color: rgb(241, 239, 239);">
                        <td>订单编号</td>
                        <td>会员名</td>
                        <td>申请备注</td>
                        <td>管理员备注</td>
                        <td>申请时间</td>
                        <td>审核时间</td>
                        <td>退款时间</td>
                        <td>处理时间</td>
                        <td>处理状态</td>
                        <td>操作人</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="litOrderid" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litUserName" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litComments" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litAdminRemark" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litApplyForTime" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litAuditTime" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litRefundTime" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litHandleTime" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litHandleStatus" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litOperator" /></td>
                    </tr>
                    <tr style="background-color: rgb(241, 239, 239);">
                        <td>订单总额</td>
                        <td>优惠减免</td>
                        <td>红包抵扣</td>
                        <td>金贝抵扣</td>
                        <td>实收金额</td>
                        <td>应退金额</td>
                        <td>收款帐号</td>
                        <td>订单类型</td>
                        <td>订单状态</td>
                        <td>跨境订单</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="lblAmount" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblDiscountAmount" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblRedPagerAmount" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblVirtualPointAmount" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblOrderTotal" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litRefundMoney" /></td>
                        <td>
                            <asp:Literal runat="server" ID="litAccount" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblOrderTypeName" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblOrderStatusName" /></td>
                        <td>
                            <asp:Literal runat="server" ID="lblIsCrossOrder" /></td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr style="background-color: rgb(241, 239, 239);">
                        <td>商品名称</td>
                        <td>货号</td>
                        <td>商品单价</td>
                        <td>购买数量</td>
                        <td>发货数量</td>
                        <td>小计(元)</td>
                    </tr>
                    <asp:Repeater ID="reProductDetail" runat="server">
                        <ItemTemplate>
                            <tr class="tr_title">
                                <td><%# Eval("ItemDescription")%></td>
                                <td><%# Eval("SKU")%></td>
                                <td>
                                    <Hi:FormatedMoneyLabel ID="lblProductPrice" Money='<%#Eval("ItemListPrice") %>' runat="server" /></td>
                                <td><%# Eval("Quantity")%></td>
                                <td><%# Eval("ShipmentQuantity")%></td>
                                <td>
                                    <Hi:FormatedMoneyLabel ID="lblProductTotalAmount" Money='<%#Eval("ProductTotalAmount") %>' runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <ul class="btn Pa_110" style="display: none">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="submit_DAqueding" Text="返回" />
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="Server">
</asp:Content>
