<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SaleOrderStatistics.aspx.cs" Inherits="Hidistro.UI.Web.Admin.SaleOrderStatistics" %>

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
            <h1>销售订单列表</h1>
            <span></span>
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
                    <li><span>订单状态：</span><span>
                        <abbr class="formselect">
                            <asp:DropDownList runat="server" ID="ddlOrderStatus" Width="107" />
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
                    <asp:TemplateField HeaderText="成交时间" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemTemplate>
                            <Hi:FormatedTimeLabel ID="litODDateTime" Time='<%# Eval("OrderDate") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="付款时间" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemTemplate>
                            <Hi:FormatedTimeLabel ID="litPDDateTime" Time='<%# Eval("PayDate") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货时间" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemTemplate>
                            <Hi:FormatedTimeLabel ID="litSDDateTime" Time='<%# Eval("ShippingDate") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="来源店铺" DataField="StoreName" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="来源店主" DataField="StoreUserName" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="购买者" DataField="MemberUserName" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="订单总额" DataField="Amount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="优惠减免" DataField="DiscountAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="红包抵扣" DataField="RedPagerAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="金贝抵扣" DataField="VirtualPointAmount" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="赠送店主金贝" DataField="StoreGiftMoney" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="赠送会员金贝" DataField="MemberGiftMoney" HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="实收金额" DataField="OrderTotal" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="订单类型" DataField="OrderTypeName" HeaderStyle-CssClass="td_right td_left" />
                    <asp:BoundField HeaderText="订单状态" DataField="OrderStatusName" HeaderStyle-CssClass="td_right td_left" />
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
