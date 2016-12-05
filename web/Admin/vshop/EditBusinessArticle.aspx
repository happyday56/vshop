<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditBusinessArticle.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.EditBusinessArticle" %>
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
            <h1>编辑专题</h1>
            <span>编辑专题</span>
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
            <asp:FileUpload ID="fileUpload" CssClass="forminput" runat="server" /> 图片尺寸建议：650px*200px
            <div class="Pa_128 Pg_8 clear">
              <table width="300" border="0" cellspacing="0">
                <tr>
                <td width="80"> <Hi:HiImage runat="server" ID="imgPic" CssClass="Img100_60"/></td><td width="80" align="left"> 
                    <Hi:ImageLinkButton Id="btnPicDelete" runat="server" IsShow="true"  Text="删除" 
                        onclick="btnPicDelete_Click" /></td></tr>
                  <tr><td width="160" colspan="2"></td>
                </tr>
              </table>
              </div>
          </li>
      
          <li> <span class="formitemtitle Pw_100">文章内容：<em >*</em></span>
		  <div>单张图片尺寸建议不要超过：650px * 1000px</div>
            <span><Kindeditor:KindeditorControl ID="fcContent" runat="server" Width="700px" Height="300px" /></span>
          </li>
      </ul>
      <ul class="btn Pa_100 clearfix">
        <asp:Button ID="btnUpdateBA" runat="server" 
              OnClientClick="return PageIsValid();" Text="保 存"  
              CssClass="submit_DAqueding" onclick="btnUpdateBA_Click" 
             />
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