<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CommissionsAllList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.CommissionsAllList" %>


<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/04.gif" width="32" height="32" /></em>
            <h1>佣金产生记录列表</h1>
            <span>管理员查询所有分销商产生的佣金记录。</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>产生时间：</span> <span>
                        <UI:WebCalendar runat="server" CssClass="forminput1" ID="txtStartTime" Width="100" />-</span>
                        <span>
                            <UI:WebCalendar runat="server" CssClass="forminput1" ID="txtEndTime" Width="100" />
                        </span>
                    </li>
                    <li><span>订单号：</span> <span>
                        <asp:TextBox ID="txtOrderId" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>来源店铺：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>分佣者：</span> <span>
                        <asp:TextBox ID="txtUserName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>分佣者店铺：</span> <span>
                        <asp:TextBox ID="txtOneStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>佣金类型：</span><span>
                        <abbr class="formselect">
                            <asp:DropDownList runat="server" ID="ddlCommType" Width="107" />
                        </abbr>
                    </span></li>
                    <li><span>订单类型：</span><span>
                        <abbr class="formselect">
                            <asp:DropDownList runat="server" ID="ddlOrderType" Width="107" />
                        </abbr>
                    </span></li>
                    <li><span>收支类型：</span><span>
                        <abbr class="formselect">
                            <asp:DropDownList runat="server" ID="ddlIncomeType" Width="107" />
                        </abbr>
                    </span></li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>
                    <li>
                        <asp:LinkButton ID="btnCreateReport" runat="server" Text="导出" />
                    </li>

                </ul>
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>订单号
                        </td>
                        <td>佣金时间
                        </td>
                        <td>来源店铺
                        </td>
                        <td>分佣者ID
                        </td>
                        <td>分佣者
                        </td>
                        <td>分佣者店铺
                        </td>
                        <td>分佣者等级
                        </td>
                        <td>订单金额(元)
                        </td>
                        <td>订单实收金额(元)
                        </td>
                        <td>佣金比率
                        </td>
                        <td>佣金(元)
                        </td>
                        <td>佣金类型
                        </td>
                        <td>订单类型
                        </td>
                        <td>收支类型
                        </td>
                    </tr>
                </thead>
                <asp:Repeater ID="reCommissions" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td width="140">&nbsp;<%# Eval("OrderId")%>
                                </td>
                                <td width="160">&nbsp; <%# Eval("TradeTime", "{0:yyyy-MM-dd hh:mm:ss}")%>
                                </td>
                                <td>&nbsp;<%# Eval("StoreName")%>
                                </td>
                                <td>&nbsp; <%# Eval("UserId")%>
                                </td>
                                <td>&nbsp; <%# Eval("OneUserName")%>
                                </td>
                                <td>&nbsp; <%# Eval("OneStoreName")%>
                                </td>
                                <td>&nbsp; <%# Eval("OneGradeName")%>
                                </td>
                                <td>&nbsp; <%# Eval("Amount", "{0:F2}")%>
                                </td>
                                <td>&nbsp; <%# Eval("OrderTotal", "{0:F2}")%>
                                </td>
                                <td>&nbsp; <%# Eval("Rate","{0:F2}")%>%
                                </td>
                                <td>&nbsp; <%# Eval("CommTotal","{0:F2}")%>
                                </td>
                                <td>&nbsp;<%# Eval("CommTypeName")%>
                                </td>
                                <td>&nbsp;<%# Eval("OrderTypeName")%>
                                </td>
                                <td>&nbsp;<%# Eval("IncomeTypeName")%>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="blank12 clearfix">
            </div>
        </div>
        <!--数据列表底部功能区域-->
        <div class="bottomPageNumber clearfix">
            <div class="pageNumber">
                <div class="pagination" style="width: auto">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {

        });

    </script>
</asp:Content>
