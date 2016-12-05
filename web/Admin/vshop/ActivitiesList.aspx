<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeFile="ActivitiesList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.ActivitiesList" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1>活动列表</h1>
            <span>对活动进行管理，您可以查询活动和详细信息。</span>
        </div>
        <!-- 添加按钮-->
        <div class="btn">
            <a href="AddActivities.aspx" class="submit_jia">添加新活动</a>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist" style="padding:20px;">
            <div class="optiongroup mainwidth">
                <ul>
                    <li id="anchors"><asp:HyperLink ID="hlinkAllActivities" NavigateUrl="?activities=" runat="server"><span>全部活动</span></asp:HyperLink></li>
                    <li id="anchors1"><asp:HyperLink ID="hlinkActivitesing" runat="server" NavigateUrl="?activities=1" Text=""><span>进行中</span></asp:HyperLink></li>
                    <li id="anchors2"><asp:HyperLink ID="hlinkNotStartActivites" runat="server" NavigateUrl="?activities=2" Text=""><span>未开始</span></asp:HyperLink></li>
                    <li id="anchors3"><asp:HyperLink ID="hlinkOverActivites" runat="server" NavigateUrl="?activities=3" Text=""><span>已结束</span></asp:HyperLink></li>
                </ul>
            </div>
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>活动名称：</span> <span><asp:TextBox ID="txtActivitiesName" CssClass="forminput" runat="server" /></span></li>
                    <li><asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" /></li>
                </ul>
            </div>
            <div class="functionHandleArea m_none">
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>活动名称</td>
                        <td>类目名称</td>
                        <td>满足金额</td>
                        <td>优惠金额</td>
                        <td>封面显示首页</td>
                        <td>生效次数</td>
                        <td>参与活动的商户</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reActivities" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>&nbsp;<%# Eval("ActivitiesName")%></td>
                                <td>&nbsp;<%# Eval("CategoriesName").ToString() == "" ? "全部" : Eval("CategoriesName")%></td>
                                <td>&nbsp;<%# Eval("MeetMoney", "{0:F2}")%></td>
                                <td>&nbsp;<%# Eval("ReductionMoney","{0:F2}")%></td>
                                <td>&nbsp;<%# Eval("IsDisplayHome").ToString() == "1" ? "显示" : "不显示"%></td>
                                <td>&nbsp;<%# Eval("TakeEffect")%></td>
                                <td>&nbsp;<a href="ActivitiesShops.aspx?id=<%# Eval("ActivitiesId") %>"><%# Eval("MerchantsCount")%></a></td>
                                <td>&nbsp; <span class="submit_bianji">
                                    <asp:HyperLink ID="lkbView" runat="server" Text="编辑" NavigateUrl='<%# "EditActivities.aspx?activitiesid="+Eval("ActivitiesId")%>'></asp:HyperLink></span>
                                    <span class="submit_bianji"><Hi:ImageLinkButton ID="ImageLinkButton1" runat="server" IsShow="true" Text="删除" CommandArgument='<%# Eval("ActivitiesId")%>' CommandName="Delete" DeleteMsg="确定要删除吗？" /></span>
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
    <div><asp:Label ID="lblStatus" runat="server" Style="display: none;"></asp:Label></div>
    <script type="text/javascript">
        function ShowState() {
            var status;
            if (navigator.appName.indexOf("Explorer") > -1) {

                status = document.getElementById("ctl00_contentHolder_lblStatus").innerText;

            } else {

                status = document.getElementById("ctl00_contentHolder_lblStatus").textContent;

            }
            if (status == "") {
                document.getElementById("anchors").className = 'optionstar';
            }
            document.getElementById("anchors" + status).className = 'menucurrent';
        }
        $(function () {
            ShowState();
        });
        
    </script>
</asp:Content>
