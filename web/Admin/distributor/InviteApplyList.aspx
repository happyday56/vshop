<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="InviteApplyList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.InviteApplyList" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
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
                邀请码限额申请列表</h1>
            <span></span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>真实姓名：</span> <span>
                        <asp:TextBox ID="txtRealName" CssClass="forminput" runat="server" /></span>
                    </li>
                     <li><span>手机号码：</span> <span>
                        <asp:TextBox ID="txtCellPhone" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>状态：</span> <span><abbr class="formselect"> 
                        <asp:DropDownList ID="DrStatus" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                     <asp:ListItem Value="0">待审核</asp:ListItem>
                      <asp:ListItem Value="1">通过审核</asp:ListItem>
                      <asp:ListItem Value="2">拒绝审核</asp:ListItem> 
                        </asp:DropDownList></abbr></span></li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>

                </ul>
            </div>
            <div class="functionHandleArea m_none">
                   <!--分页功能-->
		  <div class="pageHandleArea" style="float:left;">
		    <ul>
		      <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" /></li>
	        </ul>
	          
	      </div>
               
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>申请人</td>
                        <td>申请时间</td>
                        <td>申请数量</td>
                         <td>审核状态</td>
                        <td>审核人</td>
                        <td>操作时间</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <asp:Repeater ID="rptApplylist" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                 &nbsp; <%# Eval("UserName")%>
                                </td>
                                 <td>  &nbsp;<%# Eval("ApplyTime")%></td>
                                <td>  &nbsp;<%# Eval("ApplyNum")%>
                                </td>
                                <td>  &nbsp;<%# Eval("IsAuditText")%>
                                </td>
                                  <td>  &nbsp;<%# Eval("AuditUserIdName")%>
                                </td>
                                <td>  &nbsp;<%# Eval("TimeStamp")%>
                                </td>
                                <td>  &nbsp;
                                 <span class="submit_bianji">
                                     <asp:HyperLink ID="lkOk" runat="server" Text="通过" 
                                         NavigateUrl='<%# "?IsAudit=1&ApplyId="+Eval("ApplyId")%>' 
                                         Visible='<%# Eval("IsAudit").ToString()=="0"?true:false %>' ></asp:HyperLink>
                                     <asp:HyperLink ID="lkCancel" runat="server" Text="拒绝" 
                                         NavigateUrl='<%# "?IsAudit=0&ApplyId="+Eval("ApplyId")%>' 
                                         Visible='<%# Eval("IsAudit").ToString()=="0"?true:false %>' ></asp:HyperLink>
                                 </span>
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