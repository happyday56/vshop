<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeFile="VPGiftList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.VPGiftList" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1>赠送活动列表</h1>
            <span>对赠送活动进行管理，您可以查询赠送活动和详细信息。</span>
        </div>
        <!-- 添加按钮-->
        <div class="btn">
            <a href="AddVPGift.aspx" class="submit_jia">添加新赠送活动</a>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist" style="padding:20px;">
            <div class="optiongroup mainwidth">
                <ul>
                    <li id="anchors"><asp:HyperLink ID="hlinkAllActivities" NavigateUrl="?vpgift=" runat="server"><span>全部活动</span></asp:HyperLink></li>
                    <li id="anchors1"><asp:HyperLink ID="hlinkActivitesing" runat="server" NavigateUrl="?vpgift=1" Text=""><span>进行中</span></asp:HyperLink></li>
                    <li id="anchors2"><asp:HyperLink ID="hlinkNotStartActivites" runat="server" NavigateUrl="?vpgift=2" Text=""><span>未开始</span></asp:HyperLink></li>
                    <li id="anchors3"><asp:HyperLink ID="hlinkOverActivites" runat="server" NavigateUrl="?vpgift=3" Text=""><span>已结束</span></asp:HyperLink></li>
                </ul>
            </div>
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>赠送活动名称：</span> <span><asp:TextBox ID="txtVPGiftName" CssClass="forminput" runat="server" /></span></li>
                    <li><asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" /></li>
                </ul>
            </div>
            <div class="functionHandleArea m_none">
            </div>
            <table id="TabGift">
                <thead>
                    <tr class="table_title">
                        <td>ID</td>
                        <td>活动名称</td>
                        <td>赠送类型</td>
                        <td>赠送类别</td>
                        <td>开始时间</td>
                        <td>结束时间</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reActivities" runat="server" OnItemDataBound="reActivities_ItemDataBound">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><%# Eval("VPGiftId")%></td>
                                <td>&nbsp;<%# Eval("VPGiftName")%></td>
                                <td>&nbsp;<%# Eval("VPGiftType").ToString() == "1" ? "首单赠送" : "购买赠送"%></td>
                                <td>&nbsp;<%# Eval("VPGiftCategory").ToString() == "1" ? "固定金额" : "百分比"%></td>
                                <td>&nbsp;<%# Eval("StartDate","{0:yyyy-MM-dd}")%></td>
                                <td>&nbsp;<%# Eval("EndDate","{0:yyyy-MM-dd}")%></td>
                                <td class="tr_title">&nbsp; <span class="submit_bianji">
                                    <asp:HyperLink ID="lkbView" runat="server" Text="编辑" NavigateUrl='<%# "EditVPGift.aspx?VPGiftId="+Eval("VPGiftId")%>'></asp:HyperLink></span>
                                    <span class="submit_bianji">
                                    <asp:HyperLink ID="lkbAddDetail" runat="server" Text="添加活动详情" NavigateUrl='<%# "AddVPGiftDetail.aspx?VPGiftId="+Eval("VPGiftId")%>'></asp:HyperLink></span>
                                    <span class="submit_bianji"><Hi:ImageLinkButton ID="ImageLinkButton1" runat="server" IsShow="true" Text="删除" CommandArgument='<%# Eval("VPGiftId")%>' CommandName="Delete" DeleteMsg="确定要删除吗？" /></span>
                                </td>
                            </tr>
                            <tr class="c_hidden">
                                <td colspan="7">
                                    <table width="100%">
                                        <tr class="tr_title">
                                            <td>满足金额</td>
                                            <td>赠送金额</td>
                                            <td>操作</td>
                                        </tr>
                                        <asp:Repeater ID="reDetails" runat="server" OnItemCommand="reDetails_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="tr_title">
                                                    <td><asp:Literal runat="server" ID="litMeetMoney" Text='<%#Eval("MeetMoney") %>' /></td>
                                                    <td><asp:Literal runat="server" ID="litGiftMoney" Text='<%#Eval("GiftMoney") %>' /></td>
                                                    <td><span class="submit_bianji"><Hi:ImageLinkButton ID="imgLinkDelete" runat="server" IsShow="true" Text="删除" CommandArgument='<%# Eval("VPGiftDetailId")%>' CommandName="DetailDelete" DeleteMsg="确定要删除吗？" /></span></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
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

        $("#TabGift tr").not(".table_title, .tr_title,.c_hidden").click(function () {
            //$(this).next("tr").removeClass("c_hidden");
            if ($(this).next("tr").is(":hidden")) {
                $(this).next("tr").removeClass("c_hidden");
                //$(this).next("tr").slideDown('slow');

            } else {
                $(this).next("tr").addClass("c_hidden");
                //$(this).next("tr").slideUp('2000');
            }

        });
        
    </script>
</asp:Content>
