<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.AlarmNotify" MasterPageFile="~/Admin/Admin.Master" %>
<%@ Import Namespace="Hidistro.Core"%>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">

    <div class="dataarea mainwidth databody">
  <div class="title">
  <em><img src="../images/07.gif" width="32" height="32" /></em>
  <h1>告警通知管理</h1>
  <span>管理微信发来的告警通知</span>
</div>

		<!--数据列表区域-->
		<div class="datalist">
				
		<!--结束-->
		<div class="functionHandleArea m_none">
		  <!--分页功能-->
		  <div class="pageHandleArea">
		    <ul>
				<li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" /></li>
			</ul>
	      </div>
		<div class="pageNumber">
			<div class="pagination">
				<UI:Pager runat="server" ShowTotalPages="false" ID="pager1" />
				</div>
			</div>	
		  <!--结束-->
		  <div class="blank8 clearfix"></div>
		  
		  
    </div>
		   <asp:DataList ID="dlstPtReviews" runat="server" DataKeyField="AlarmNotifyId" Style="width: 100%;" >
                <HeaderTemplate>
                    <table width="200" cellspacing="0px" border="0px"  >
                        <tr class="table_title">
                            <td width="20%" class="td_right td_left"> 错误描述 </td>
                            <td width="9%" class="td_right td_left"> 错误详情</td>
                            <td width="20%" class="td_right td_left"> 时间 </td>
                            <td width="11%" class="td_left td_right_fff"> 操作 </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                      <td ><asp:Label ID="Label1" runat="server" Text='<%#  Eval("Description") %>' CssClass="line"></asp:Label> </td>
                      <td ><asp:Label ID="lblText" runat="server" Text='<%#  Eval("AlarmContent") %>' CssClass="line"></asp:Label> </td>
                      <td><Hi:FormatedTimeLabel ID="ConsultationDateTime" Time='<%# Eval("TimeStamp") %>' runat="server" /></td>
                      <td><span class="submit_shanchu"><Hi:ImageLinkButton ID="btnReviewDelete" runat="server" CommandName="Delete" IsShow="true" CommandArgument='<%# Eval("AlarmNotifyId")%>' Text="删除" /></span></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:DataList>
		  <div class="blank12 clearfix"></div>
		</div>
        
		<!--数据列表底部功能区域-->  
		<!--翻页-->
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server"></asp:Content>

