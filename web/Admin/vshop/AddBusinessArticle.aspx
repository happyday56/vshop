<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddBusinessArticle.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.AddBusinessArticle" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
          <div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/01.gif" width="32" height="32" /></em>
            <h1>添加专题内容</h1>
          </div>
      <div class="formitem validator2">
        <ul>
          <li> <span class="formitemtitle Pw_100">文章标题：<em >*</em></span>
            <asp:TextBox ID="txtBATitle" runat="server" CssClass="forminput" Width="300"></asp:TextBox>
            <p id="txtTopicTitleTip">限制在60个字符以内</p>
          </li>
          <li><span class="formitemtitle Pw_100">文章摘要：</span> 
            <asp:TextBox ID="txtSummary" runat="server" CssClass="forminput" Width="600"></asp:TextBox>
            <p id="txtKeysTip"></p>
          </li>
          <li> <span class="formitemtitle Pw_100">文章首图：</span>
            <asp:FileUpload ID="fileUpload" CssClass="forminput" runat="server" />
            图片尺寸建议：650px * 200px
          </li>
          <li> <span class="formitemtitle Pw_100">文章内容：<em >*</em></span>
          单张图片尺寸建议不要超过：650px * 1000px
          </li>
            <li><Kindeditor:KindeditorControl ID="fcContent" runat="server" Width="700px" Height="300px" /></li>
      </ul>
      <ul class="btn Pa_100 clearfix">
        <asp:Button ID="btnAddBA" runat="server" 
              OnClientClick="return PageIsValid();" Text="保存"  CssClass="submit_jixu inbnt m_none" 
              onclick="btnAddBA_Click"/>
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
<script type="text/javascript" language="javascript">
    function InitValidators() {
        initValid(new InputValidator('txtBATitle', 1, 60, false, null, '文章标题不能为空，长度限制在60个字符以内'))
    }
    $(document).ready(function () { InitValidators(); });
</script>

</asp:Content>