<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddTopic.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.AddTopic" %>
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
            <h1>添加专题</h1>
            <span>您可以为即将到来的节日添加一系列相关商品，与用户共度狂欢~</span>
          </div>
      <div class="formitem validator2">
        <ul>
         <!-- <p id="ctl00_contentHolder_dropArticleCategoryTip">选择文章的所属分类</p>-->
          <li> <span class="formitemtitle Pw_100">专题名称：<em >*</em></span>
            <asp:TextBox ID="txtTopicTitle" runat="server" CssClass="forminput"></asp:TextBox>
            <p id="txtTopicTitleTip">限制在60个字符以内</p>
          </li>
          <li><span class="formitemtitle Pw_100"><em>*</em>关键字：</span> 
            <asp:TextBox ID="txtKeys" runat="server" CssClass="forminput"></asp:TextBox>
            <p id="txtKeysTip">（同时作为图文推送的标题来使用。）</p>
          </li>
          <li> <span class="formitemtitle Pw_100">专题图片：</span>
            <asp:FileUpload ID="fileUpload" CssClass="forminput" runat="server" />
            图片尺寸建议：650px * 200px
          </li>
          <li> <span class="formitemtitle Pw_100">详细介绍：<em >*</em></span>
          单张图片尺寸建议不要超过：650px * 1000px
          </li>
            <li><Kindeditor:KindeditorControl ID="fcContent" runat="server" Width="700px" Height="300px" /></li>
      </ul>
      <ul class="btn Pa_100 clearfix">
        <asp:Button ID="btnAddTopic" runat="server" 
              OnClientClick="return PageIsValid();" Text="添加关联商品"  CssClass="submit_jixu inbnt m_none" 
              onclick="btnAddTopic_Click"/>
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
        initValid(new InputValidator('txtTopicTitle', 1, 60, false, null, '专题标题不能为空，长度限制在60个字符以内'))
        initValid(new InputValidator('txtKeys', 1, 50, false, null, '必填 关键字不能为空，在1至50个字符之间'))
    }
    $(document).ready(function () { InitValidators(); });
</script>

</asp:Content>