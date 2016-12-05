<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="ReplyProductReviews.aspx.cs" Inherits="Hidistro.UI.Web.Admin.ReplyProductReviews" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="server">

 
  <div class="dataarea mainwidth databody">
    <div class="title m_none td_bottom"> 
      <em><img src="../images/07.gif" width="32" height="32" /></em>
      <h1>商品评价回复</h1>
    <span>管理员回复商品评价</span></div>
    <div class="datafrom">
    <div class="formitem">
                <ul>
                  <li> <span class="formitemtitle Pw_100">评价用户：</span><asp:Literal ID="litUserName" runat="server"></asp:Literal></li>
                  
                  <li> <span class="formitemtitle Pw_100">评价时间：</span><Hi:FormatedTimeLabel ID="lblTime" runat="server"></Hi:FormatedTimeLabel></li>
                  <li> <span class="formitemtitle Pw_100">评价内容：</span><abbr class="colorQ"><asp:Literal ID="litReviewText" runat="server"></asp:Literal></abbr></li>
                  <li > <span class="formitemtitle Pw_100">回复：</span> 
                    <span style="float:left;"><Kindeditor:KindeditorControl id="fckReplyText" runat="server" Width="550px"  height="200px" /></span>
                  </li>
                </ul>
                <ul class="btntf Pa_100 clear">
                 <li><asp:Button ID="btnReplyProductReview" runat="server" Text="回复" CssClass="submit_DAqueding" /></li>
          </ul>
      </div>
    </div>
</div>
 <div class="bottomarea testArea">
    <!--顶部logo区域-->
  </div>

</asp:Content>