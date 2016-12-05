<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.FeedBackNotify" MasterPageFile="~/Admin/Admin.Master" %>
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
  <h1>反馈通知管理</h1>
  <span>管理微信发来的反馈通知</span>
</div>

		<!--数据列表区域-->
		<div class="datalist">
		<div class="searcharea clearfix br_search">
			<ul>
				<li>
          <span>消息状态：</span>
					<abbr class="formselect">
						<Hi:MemberGradeDropDownList ID="rankList" runat="server" AllowNull="true" NullToDisplay="全部">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="未处理" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已处理" Value="2"></asp:ListItem>
                        </Hi:MemberGradeDropDownList>
				</abbr>
				</li>
				<li>
				    <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
				</li>
			</ul>
	  </div>

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
		   <asp:DataList ID="dlstPtReviews" runat="server" DataKeyField="FeedBackNotifyID" Style="width: 100%;" >
                <HeaderTemplate>
                    <table width="200" cellspacing="0px" border="0px"  >
                        <tr class="table_title">
                            <td width="10%" class="td_right td_left"> 微信OpenId </td>
                            <td width="10%" class="td_right td_left"> 消息状态</td>
                            <td width="10%" class="td_right td_left"> 微信订单号 </td>
                            <td width="10%" class="td_right td_left"> 用户投诉原因 </td>
                            <td width="180" class="td_right td_left"> 用户希望的解决方案 </td>
                            <td width="10%" class="td_right td_left"> 备注信息 </td>
                            <td width="10%" class="td_right td_left"> 时间 </td>
                            <td width="11%" class="td_left td_right_fff"> 操作 </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                      <td ><asp:Label ID="Label1" runat="server" Text='<%# Eval("OpenId") %>' CssClass=""></asp:Label> </td>
                      <td ><asp:Label ID="lblText" runat="server" Text='<%# Eval("MsgType") %>' CssClass=""></asp:Label> </td>
                      <td ><asp:Label ID="Label3" runat="server" Text='<%# Eval("TransId") %>' CssClass=""></asp:Label> </td>
                      <td ><asp:Label ID="Label4" runat="server" Text='<%# Eval("Reason") %>' CssClass=""></asp:Label> </td>
                      <td ><asp:Label ID="Label5" runat="server" Text='<%# Eval("Solution") %>' CssClass=""></asp:Label> </td>
                      <td ><asp:Label ID="Label2" runat="server" Text='<%# Eval("ExtInfo") %>' CssClass=""></asp:Label> </td>
                      <td><Hi:FormatedTimeLabel ID="ConsultationDateTime" Time='<%# Eval("TimeStamp") %>' runat="server" /></td>
                      <td>
                      <span class="submit_shanchu"><Hi:ImageLinkButton ID="btnReviewUpdate" runat="server" CommandName="Update" Visible=<%# Eval("MsgType").ToString() == "未处理" %> IsShow="false" CommandArgument='<%# Eval("FeedBackNotifyID")%>' Text="处理" /></span>
                      <span class="submit_shanchu"><Hi:ImageLinkButton ID="btnReviewDelete" runat="server" CommandName="Delete" Visible=<%# Eval("MsgType").ToString() != "未处理" %> IsShow="true" CommandArgument='<%# Eval("FeedBackNotifyID")%>' Text="删除" /></span>
                      </td>
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

