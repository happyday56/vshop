<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeFile="ActivitiesShops.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.ActivitiesShops" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1>参与活动商户</h1>
            <span></span>
        </div>
        <!--搜索-->

        <!--数据列表区域-->
        <div class="datalist" style="padding:20px;">
            <table>
                <thead>
                    <tr class="table_title">
                        <td>用户头像</td>
                        <td>微信OpenID</td>
                        <td>昵称</td>
                        <td>参与时间</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reShops" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><img src="<%# Eval("UserHead") %>" alt="" height="35" /></td>
                                <td>&nbsp;<%# Eval("OpenId")%></td>
                                <td>&nbsp;<%# Eval("RealName") %></td>
                                <td>&nbsp;<%# Eval("JoinDate","{0:yyyy/MM/dd HH:mm:dd}")%></td>
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
