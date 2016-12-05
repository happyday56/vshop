<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeFile="RedPagerActivityList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.RedPagerActivityList1" %>
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
            <h1>代金券活动列表</h1>
            <span>您可在这里管理我是土豪活动中土豪可发的代金券</span>
        </div>
        <div class="btn">
	    <a href="EditRedPagerActivity.aspx" class="submit_jia">添加活动</a>
    </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>活动名称：</span> <span>
                        <asp:TextBox ID="txtName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>
                </ul>
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>
                            代金券名称
                        </td>
                        <td>
                            是否开启
                        </td>
                        <td>
                            发券需订单金额到达
                        </td>
                        <td>最多被领取次数</td>
                          <td>
                            每次最多领取
                        </td>
                          <td>使用需订单金额到达</td>
                          <td>有效期</td>
                        <td width="120">
                            操作
                        </td>
                    </tr>
                </thead>
                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    &nbsp;<%# Eval("Name")%></td>
                                <td><asp:ImageButton ID="imgBtnIsOpen" runat="server" ImageUrl="../images/da.gif" CommandArgument='<%#Eval("RedPagerActivityId") %>' /></td>
                                <td>¥<%#Eval("MinOrderAmount","{0:F2}")%></td>
                                <td><%#Eval("MaxGetTimes")%></td>
                                <td>¥<%#Eval("ItemAmountLimit","{0:F2}")%></td>
                                <td>¥<%#Eval("OrderAmountCanUse","{0:F2}")%></td>
                                <td><%#Eval("ExpiryDays")%>天</td>
                                <td>
                                    <span class="submit_bianji"><a style=" cursor:pointer" href="EditRedPagerActivity.aspx?redpaperactivityid=<%# Eval("RedPagerActivityId")%>&reurl=<%=LocalUrl %>">
                                        编辑</a></span>                                        
                                        <span class="submit_bianji"><asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%#Eval("RedPagerActivityId") %>' CommandName="del" OnClientClick="return confirm('删除代金券活动,不会影响该活动已经产生的代金券!\n确定要删除该代金券活动吗？')">删除</asp:LinkButton> </span>
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
</asp:Content>
