<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="DistributorList2.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorList2" %>

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
            <h1>钻石会员销售指标列表</h1>
            <span></span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>店铺名：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>联系人：</span> <span>
                        <asp:TextBox ID="txtRealName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>手机号码：</span> <span>
                        <asp:TextBox ID="txtCellPhone" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li style="display:none;"><span>微信号：</span> <span>
                        <asp:TextBox ID="txtMicroSignal" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li style="display:none;"><span>分销等级：</span>

                        <abbr class="formselect">
                            <Hi:DistributorGradeDropDownList ID="DrGrade" runat="server" AllowNull="true" NullToDisplay="全部" />
                        </abbr>
                        </span></li>
                    <li style="display:none;"><span>分销商状态：</span> <span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrStatus" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">正常</asp:ListItem>
                                <asp:ListItem Value="2">已冻结</asp:ListItem>
                            </asp:DropDownList></abbr></span></li>
                    <li>
                    <li style="display:none;"><span>续费状态：</span> <span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrDeadlineStatus" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">正常</asp:ListItem>
                                <asp:ListItem Value="2">续费</asp:ListItem>
                                <asp:ListItem Value="3">过期</asp:ListItem>
                            </asp:DropDownList></abbr></span></li>
                    <li>
                        <span>指标时间点：</span>
                        <span><UI:WebCalendar CalendarType="StartDate" ID="calendarStartDate" runat="server" CssClass="forminput" /></span>
                    </li>
                    <li><span>完成状态：</span> <span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrFinishStatus" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">未完成</asp:ListItem>
                                <asp:ListItem Value="2">已完成</asp:ListItem>
                            </asp:DropDownList></abbr></span></li>
                    <li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>

                </ul>
            </div>
            <div class="functionHandleArea m_none">
                <!--分页功能-->
                <div class="pageHandleArea" style="float: left;">
                    <ul>
                        <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" />
                        </li>
                    </ul>
                </div>
                <div class="blank8 clearfix"></div>
                <div class="batchHandleArea">
                    <ul>
                        <li class="batchHandleButton selectButton">
                            <span class="signicon"></span><span class="allSelect"><a href="javascript:void(0);" onclick="SelectAll2()">全选</a></span>
                            <span class="reverseSelect"><a href="javascript:void(0);" onclick="ReverseSelect2()">反选</a></span>
                        </li>
                    </ul>
                </div>
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>选择</td>
                        <td>头像</td>
                        <td>店铺名</td>
                        <td>分销等级</td>
                        <td>分销商状态</td>
                        <td>联系人</td>
                        <td>手机号码</td>
                        <td>微信昵称</td>
                        <td>开店时间</td>
                        <td>截止时间</td>
                        <td>指标开始时间</td>
                        <td>指标结束时间</td>
                        <td>指标金额</td>
                        <td>销售金额</td>
                        <td>完成状态</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reDistributor"  OnItemCommand="reDistributor_ItemCommand"  OnItemDataBound="reDistributor_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><input name="CheckBoxGroup" id="CheckBoxGroup" type="checkbox" value='<%#Eval("UserId") %>' runat="server" /></td>
                                <td>&nbsp;<%# Eval("UserHead").ToString()!=""?"<img  src='"+Eval("UserHead")+"' width=\"50\" height=\"50\"/>":""%>
                                </td>
                                <td>&nbsp; <%# Eval("StoreName")%>
                                </td>
                                <td>&nbsp; <%# Eval("Name")%> 
                                </td>
                                <td>&nbsp; <%# Eval("ReferralStatus").ToString().Trim()=="2" ? "<p style=\"color:red;\">已冻结</p>" : "正常"%>
                                </td>
                                <td>&nbsp; <%# Eval("RealName")%>
                                </td>
                                <td><asp:Literal ID="litCellPhone" runat="server" Text='<%#Eval("CellPhone")%>'></asp:Literal></td>                                
                                <td>&nbsp;<%# Eval("MicroSignal")%>
                                </td>
                                <td>&nbsp;<%# Eval("CreateTime","{0:d}")%>
                                </td>
                                <td>&nbsp;<%# Eval("DeadlineTime","{0:d}")%>
                                </td>
                                <td>&nbsp;<%# Eval("CurrFirstTime","{0:d}")%>
                                </td>
                                <td>&nbsp;<%# Eval("CurrLastTime","{0:d}")%>
                                </td>
                                <td>&nbsp; <%# Eval("LimitAmount", "{0:F2}")%>
                                </td>
                                <td>&nbsp; <%# Eval("OrderTotal", "{0:F2}")%>
                                </td>
                                <td>&nbsp; <%# Eval("FinishStatus").ToString() == "1" ? "<span style=\"color:red;display: inline;\">未完成</span>": "<span style=\"color:blue;display: inline;\">已完成</span>"%></td>
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

        //样式控制
        function showcss(divobj, rownumber) {
            if (rownumber > 12) {
                $("#" + divobj).css("height", 100);
            }
        }


    </script>
</asp:Content>
