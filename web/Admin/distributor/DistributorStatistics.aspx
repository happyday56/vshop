<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="DistributorStatistics.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorStatistics" %>

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
            <h1>
                分销商统计</h1>
            <span>对分销商进行统计排名</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <table>
                <thead>
                    <tr class="table_title">
                        <td>
                            排名
                        </td>
                        <td>
                            店铺名
                        </td>
                        <td>
                            总销量（元）
                        </td>
                        <td>
                            佣金总额（元）
                        </td>
                        <td>
                            已提现总额（元）
                        </td>
                         
                    </tr>
                </thead>
                <asp:Repeater ID="reDistributor" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                     &nbsp;<asp:Literal runat="server" ID="litph"  Text="0"/>
                                </td>
                                <td>
                                    &nbsp;<%# Eval("StoreName")%>
                                </td>
                                <td>
                                    &nbsp;<%# Eval("OrdersTotal","{0:F2}")%>
                                </td>
                                <td>
                                    &nbsp;<%# decimal.Parse( Eval("ReferralBlance", "{0:F2}")) + decimal.Parse( Eval("ReferralRequestBalance", "{0:F2}"))%>
                                </td>
                                <td>
                                    &nbsp;<%# Eval("ReferralRequestBalance", "{0:F2}")%>
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
