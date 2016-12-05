<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SaleDetails.aspx.cs" Inherits="Hidistro.UI.Web.Admin.SaleDetails" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">

    <!--选项卡-->
    <!--选项卡-->

    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/04.gif" width="32" height="32" /></em>
            <h1>商品销售明细</h1>
            <span>查询一段时间内每个订单内的商品销售量及销售价，默认排序为售出时间由新到旧(注：统计的商品不包括成功退款订单中的商品)。</span>
        </div>

        <!--数据列表区域-->
        <div class="datalist">
            <!--搜索-->
            <!--结束-->
            <div class="searcharea clearfix ">
                <ul class="a_none_left">

                    <li><span>成交时间：</span><span><UI:WebCalendar ID="calendarOrderStartDate" runat="server" class="forminput" /></span><span class="Pg_1010">至</span><span><UI:WebCalendar ID="calendarOrderEndDate" runat="server" class="forminput" IsStartTime="false" /></span></li>
                    <li><span>付款时间：</span><span><UI:WebCalendar ID="calendarPayStartDate" runat="server" class="forminput" /></span><span class="Pg_1010">至</span><span><UI:WebCalendar ID="calendarPayEndDate" runat="server" class="forminput" IsStartTime="false" /></span></li>
                    <li><span>发货时间：</span><span><UI:WebCalendar ID="calendarShippingStartDate" runat="server" class="forminput" /></span><span class="Pg_1010">至</span><span><UI:WebCalendar ID="calendarShippingEndDate" runat="server" class="forminput" IsStartTime="false" /></span></li>
                    <li><span>订单号：</span> <span>
                        <asp:TextBox ID="txtOrderId" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>订单类型：</span><span>
                        <abbr class="formselect">
                            <asp:DropDownList runat="server" ID="ddlOrderType" Width="107" />
                        </abbr>
                    </span></li>
                    <li>
                        <asp:Button ID="btnQuery" runat="server" Text="查询" class="searchbutton" /></li>
                    
                    <li>
                        <asp:LinkButton ID="btnCreateReport" runat="server" Text="导出" />
                    </li>
                </ul>
            </div>
            <div class="blank12 clearfix"></div>
            <asp:GridView ID="grdOrderLineItem" runat="server" AutoGenerateColumns="false" ShowHeader="true" AllowSorting="true" GridLines="None" HeaderStyle-CssClass="table_title">
                <Columns>
                    <asp:BoundField HeaderText="订单号" DataField="OrderId" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="货号" DataField="SKU" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="商品名称" DataField="ProductName" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="发货数量" DataField="ShipmentQuantity" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="商品单价" DataField="ItemAdjustedPrice" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="订单总价" DataField="Amount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="优惠减免" DataField="DiscountAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="红包抵扣" DataField="RedPagerAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="金贝抵扣" DataField="VirtualPointAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="实收金额" DataField="OrderTotal" HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField HeaderText="成交时间" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemTemplate>
                            <Hi:FormatedTimeLabel ID="litDateTime" Time='<%# Eval("orderDate") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货时间" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemTemplate>
                            <Hi:FormatedTimeLabel ID="litShippingDateTime" Time='<%# Eval("ShippingDate") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="订单类型" DataField="OrderTypeName" HeaderStyle-CssClass="td_right td_left" />
                </Columns>
            </asp:GridView>

            <div class="blank12 clearfix"></div>

        </div>
        <!--数据列表底部功能区域-->
        <div class="page">
            <div class="bottomPageNumber clearfix">
                <div class="pageNumber">
                    <div class="pagination">
                        <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                    </div>

                </div>
            </div>
        </div>

    </div>
    <div class="databottom"></div>
    <div class="bottomarea testArea">
        <!--顶部logo区域-->
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
