<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditVote.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditVote" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Import Namespace="System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="server">

<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/01.gif" width="32" height="32" /></em>
            <h1>编辑投票</h1>
            <span>管理店铺的在线投票调查，您可以新建投票或查看原有投票数据。</span>
        </div>
      <div class="formitem validator3">
        <ul>
          <li> <span class="formitemtitle Pw_128"><em >*</em>投票标题：</span>
            <asp:TextBox ID="txtAddVoteName" runat="server" CssClass="forminput" />
            <p id="ctl00_contentHolder_txtAddVoteNameTip">投票调查的标题，长度限制在60个字符以内</p>
          </li>
          
          <li> <span class="formitemtitle Pw_128"><em >*</em>最多可选项数：</span>
            <asp:TextBox ID="txtMaxCheck" runat="server" Text="1"  CssClass="forminput" />
            <p id="ctl00_contentHolder_txtMaxCheckTip">最多可选项数不允许为空，范围为1-100之间的整数</p>
          </li>
          
           <li> 
            <span class="formitemtitle Pw_128"><em >*</em>开始日期：</span>
            <UI:WebCalendar runat="server" CssClass="forminput" ID="calendarStartDate" />
            <p id="P1">只有达到开始日期，活动才会生效。</p>
        </li>
        <li> 
            <span class="formitemtitle Pw_128"><em >*</em>结束日期：</span>
            <UI:WebCalendar runat="server" CssClass="forminput" ID="calendarEndDate" />
            <p id="P2">当达到结束日期时，活动会结束。</p>
        </li>
          <li><span class="formitemtitle Pw_128"><em >*</em>投票选项：</span>
           <asp:TextBox ID="txtValues" runat="server" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
            <p id="ctl00_contentHolder_txtValuesTip">一行一个投票选项</p>
          </li>
         <li> <span class="formitemtitle Pw_128"><em >*</em>关键字：</span>
            <asp:TextBox ID="txtKeys" runat="server" CssClass="forminput" />
            <p id="ctl00_contentHolder_txtKeysTip">关键字，长度限制在60个字符以内</p>
          </li>
          <li> <span class="formitemtitle Pw_128"><em >*</em>封面图片：</span>
                 <div class="uploadimages">
                    <Hi:UpImg runat="server" ID="uploader1" IsNeedThumbnail="false" UploadType="Vote"  />
                </div>
              <p id="uploader1_uploadedImageUrlTip">（图片尺寸建议：320px * 200px）</p>



          </li>
      </ul>
      <ul class="btn Pa_128">
         <asp:Button ID="btnEditVote"  runat="server" Text="确 定"  OnClientClick="return CheckForm()"  CssClass="submit_DAqueding"/>  
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
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
<script type="text/javascript" language="javascript">
    function CheckForm() {
        if (PageIsValid()) {
            var info = $("#uploader1_uploadedImageUrl").val();
            if (info.length < 10) {
                $("#uploader1_uploadedImageUrlTip").attr("class", "msgError").html("请上传封面图片");
                return false;
            } else {
                $("#uploader1_uploadedImageUrlTip").attr("class", "").html("");
            }
        } else {
            return false;
        }
    }
    function InitValidators() {
        initValid(new InputValidator('ctl00_contentHolder_txtAddVoteName', 1, 60, false, null, '投票调查的标题，长度限制在60个字符以内'));
        initValid(new InputValidator('ctl00_contentHolder_txtMaxCheck', 1, 10, false, '-?[0-9]\\d*', '设置一次投票最多可以选择投几个选项'));
        appendValid(new NumberRangeValidator('ctl00_contentHolder_txtMaxCheck', 1, 100, '您设置的最多可选项数量超出了系统允许1-100的范围'));
        initValid(new InputValidator('ctl00_contentHolder_txtValues', 0, 300, false, null, '在输入框中用回车换行区分多个选项值'));
    }
    $(document).ready(function() { InitValidators(); });
</script>
</asp:Content>
