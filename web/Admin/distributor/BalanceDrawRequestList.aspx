<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BalanceDrawRequestList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.BalanceDrawRequestList" %>



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
                提现结算列表</h1>
            <span>管理员查询所有分销商产生的结算记录。</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
           <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>店铺名：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput1" runat="server" /></span>
                    </li>
                    <li><span>申请日期：</span> <span>
                        <UI:WebCalendar runat="server"   CssClass="forminput1" ID="txtRequestStartTime" Width="100" />-
                        <UI:WebCalendar runat="server"   CssClass="forminput1" ID="txtRequestEndTime" Width="100" />
                                           </span>
                    </li>
                     
                     <li><span>结算日期：</span>  
                        <span> 
                        <UI:WebCalendar runat="server"   CssClass="forminput1" ID="txtCheckStartTime" Width="100"/>-
                            <UI:WebCalendar runat="server"   CssClass="forminput1" ID="txtCheckEndTime" Width="100"/>
                        </span>
                    </li>
                    <li><span>发放状态：</span><span>
                    <abbr class="formselect">
                        <asp:DropDownList runat="server" ID="ddlCheckType" Width="107" />
                    </abbr>
                </span></li>
                    <li><span>手机号码：</span> <span>
                        <asp:TextBox ID="txtMobile" CssClass="forminput1" runat="server" /></span>
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
                             分销商店铺
                        </td>
                        <td>
                          申请提现金额
                        </td>
                        <td>
                            申请日期
                        </td>
                        <td>
                          结算日期
                        </td>
                         <td>
                          是否发放
                        </td>
                         <td>
                          手机号码
                        </td>
                       <td>
                          备注
                        </td>
                    </tr>
                </thead>
                <asp:Repeater ID="reBalanceDrawRequest" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                             <td width="180">
                                   &nbsp; <%# Eval("StoreName")%>
                                </td>
                                 <td width="180">
                                   &nbsp;￥<%# Eval("Amount", "{0:F2}")%>
                                </td>
                                <td width="180">
                                   &nbsp; <%# Eval("RequestTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                 <td width="180">
                                  &nbsp;  <%# Eval("CheckTime", "{0:yyyy-MM-dd}")%>
                                </td> 
                                <td width="80">
                                   &nbsp; <%# Eval("IsCheck").ToString() == "False" ? "<span style=\"color:red;display: inline;\">未发放</span>": "<span style=\"color:blue;display: inline;\">已发放</span>"%>
                                </td>
                                 <td width="180">
                                  &nbsp;  <%# Eval("CellPhone")%>
                                </td>
                                <td width="280">
                                  &nbsp;  <%#int.Parse(Eval("RedpackRecordNum").ToString())>0?"<a href='SendRedpackRecord.aspx?serialid="+Eval("SerialID")+"' title='查看微信红包发送详情'>"+Eval("Remark").ToString()+"</a>":Eval("Remark").ToString()%>
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