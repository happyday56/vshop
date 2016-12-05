<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" 
 Inherits="Hidistro.UI.Web.Admin.AddReplyOnKey"
 CodeBehind="AddReplyOnKey.aspx.cs" 
 MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
          <div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/01.gif" width="32" height="32" /></em>
            <h1>新增文本回复</h1>
            <span>添加文本回复</span>
          </div>
      <div class="formitem validator2">
        <ul>
        <li> <span class="formitemtitle Pw_100">回复内容：</span>
            <span><asp:TextBox ID="fcContent" runat="server" Width="600px" height="150px" TextMode="MultiLine" /></span>
          </li>
          <li> <span class="formitemtitle Pw_100">回复类型：</span>
              <asp:CheckBox ID="chkKeys" runat="server" Text="关键字回复" />
              <asp:CheckBox ID="chkSub" runat="server" Text="关注时回复" />
              <asp:CheckBox ID="chkNo" runat="server" Text="无匹配回复" />
          </li>
          <li class="likey"> <span class="formitemtitle Pw_100"><em >*</em>关键字：</span>
            <asp:TextBox ID="txtKeys" runat="server" CssClass="forminput"></asp:TextBox>
            <p id="ctl00_contentHolder_txtKeysTip">用户可通过该关键字搜到到这个内容</p>
          </li>          
          <li class="likey"> <span class="formitemtitle Pw_100">匹配模式：</span>
            <Hi:YesNoRadioButtonList ID="radMatch" runat="server" RepeatLayout="Flow" YesText="模糊匹配" NoText="精确匹配" />
          </li>
          <li> <span class="formitemtitle Pw_100">状态：</span>
            <Hi:YesNoRadioButtonList ID="radDisable" runat="server" RepeatLayout="Flow" YesText="启用" NoText="禁用" />
          </li>
      </ul>
      <ul class="btn Pa_100 clearfix">
        <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckKey();" Text="保 存"  CssClass="submit_DAqueding"/>
        </ul>
      </div>

      </div>
  </div>
<div class="databottom">
  <div class="databottom_bg"></div>
</div>
<div class="bottomarea testArea">
  <!--顶部logo区域-->
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
<script src="../js/ReplyOnKey.js" type="text/javascript"></script>
</asp:Content>
