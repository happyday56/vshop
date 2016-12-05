<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="EditLotteryTicket.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditLotteryTicket" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <style>.Pw_100 {
    width: 120px;
}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title">
                <em>
                    <img src="../images/01.gif" width="32" height="32" /></em>
                    <h1>微抽奖设置</h1>
                    <span>微抽奖设置</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <!-- <p id="ctl00_contentHolder_dropArticleCategoryTip">选择文章的所属分类</p>-->
                    <li><span class="formitemtitle Pw_100">活动名称：<em>*</em></span>
                        <asp:TextBox ID="txtActiveName" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="txtTopicNameTip" style="padding-left:20px;">
                            限制在60个字符以内</p>
                    </li>
                    <li> <span class="formitemtitle Pw_100">报名开始日期：<em >*</em></span>
          <ui:webcalendar runat="server" CssClass="forminput" ID="calendarStartDate" /><span>（00:00）</span>
          </li>
                  <li><span class="formitemtitle Pw_100">抽奖开始时间：<em >*</em></span>
                  <ui:webcalendar runat="server" CssClass="forminput" ID="calendarOpenDate" />
                  <asp:DropDownList ID="ddlHours" runat="server">
                    <asp:ListItem Text="00:00"></asp:ListItem>
                    <asp:ListItem Text="01:00"></asp:ListItem>
                    <asp:ListItem Text="02:00"></asp:ListItem>
                    <asp:ListItem Text="03:00"></asp:ListItem>
                    <asp:ListItem Text="04:00"></asp:ListItem>
                    <asp:ListItem Text="05:00"></asp:ListItem>
                    <asp:ListItem Text="06:00"></asp:ListItem>
                    <asp:ListItem Text="07:00"></asp:ListItem>
                    <asp:ListItem Text="08:00"></asp:ListItem>
                    <asp:ListItem Text="09:00"></asp:ListItem>
                    <asp:ListItem Text="10:00"></asp:ListItem>
                    <asp:ListItem Text="11:00"></asp:ListItem>
                    <asp:ListItem Text="12:00"></asp:ListItem>
                    <asp:ListItem Text="13:00"></asp:ListItem>
                    <asp:ListItem Text="14:00"></asp:ListItem>
                    <asp:ListItem Text="15:00"></asp:ListItem>
                    <asp:ListItem Text="16:00"></asp:ListItem>
                    <asp:ListItem Text="17:00"></asp:ListItem>
                    <asp:ListItem Text="18:00"></asp:ListItem>
                    <asp:ListItem Text="19:00"></asp:ListItem>
                    <asp:ListItem Text="20:00"></asp:ListItem>
                    <asp:ListItem Text="21:00"></asp:ListItem>
                    <asp:ListItem Text="22:00"></asp:ListItem>
                    <asp:ListItem Text="23:00"></asp:ListItem>
                    <asp:ListItem Text="24:00"></asp:ListItem>
                  </asp:DropDownList>（报名结束时间）
                  </li>
                  <li> <span class="formitemtitle Pw_100">抽奖结束日期：<em >*</em></span>
                      <ui:webcalendar runat="server" CssClass="forminput" ID="calendarEndDate" /><span>（00:00）</span>
                      </li>
                    <li><span class="formitemtitle Pw_100">会员设置：<em>*</em></span>
                        <asp:CheckBoxList ID="cbList" runat="server" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </li>
                    <li><span class="formitemtitle Pw_100">人数下限：<em>*</em></span>
                        <asp:TextBox ID="txtMinValue" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="txtMinValueTip" style="padding-left:20px;">只能为正整数</p></li>
                    <li><span class="formitemtitle Pw_100">邀请码：</span>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="forminput"></asp:TextBox>
                         <p id="txtCodeTip" style="padding-left:20px;">
                            留空则表示无需邀请码</p>
                       </li>

                    <li><span class="formitemtitle Pw_100">关键字：<em>*</em></span>
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="txtKeywordTip" style="padding-left:20px;">请输入关键字</p></li>
                    <li><span class="formitemtitle Pw_100">活动简介：</span>
                        <asp:TextBox ID="txtdesc" runat="server" Rows="5" Height="100px" Width="400px" CssClass="forminput"
                            TextMode="MultiLine"></asp:TextBox>
                    </li>
                    <li><span class="formitemtitle Pw_100">一等奖描述：<em>*</em></span>
                        <asp:TextBox ID="txtPrize1" runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize1Num" runat="server" CssClass="forminput"></asp:TextBox></li>
                    <li><span class="formitemtitle Pw_100">二等奖描述：<em>*</em></span>
                    <asp:TextBox ID="txtPrize2" runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize2Num" runat="server" CssClass="forminput"></asp:TextBox></li>
                    <li><span class="formitemtitle Pw_100">三等奖描述：<em>*</em></span>
                    <asp:TextBox ID="txtPrize3" runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize3Num" runat="server" CssClass="forminput"></asp:TextBox></li>

                        <li>
                            <asp:CheckBox ID="ChkOpen" runat="server" onclick="CheckOpen(this)" />
                            开启四五六等奖&nbsp;
                       
                        </li>
                    <li class="hiddenli"><span class="formitemtitle Pw_100">四等奖描述：</span>
                        <asp:TextBox ID="txtPrize4" runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize4Num" runat="server" CssClass="forminput"></asp:TextBox></li>
                    <li class="hiddenli"><span class="formitemtitle Pw_100">五等奖描述：</span>
                        <asp:TextBox ID="txtPrize5" runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize5Num" runat="server" CssClass="forminput"></asp:TextBox></li>
                    <li class="hiddenli"><span class="formitemtitle Pw_100">六等奖描述：</span>
                    <asp:TextBox ID="txtPrize6"
                        runat="server" CssClass="forminput"></asp:TextBox>
                        <span>&nbsp;奖品数量：&nbsp;</span><asp:TextBox ID="txtPrize6Num" runat="server" CssClass="forminput"></asp:TextBox></li>

                        <li> <span class="formitemtitle Pw_100">图片封面：</span>
                 <div class="uploadimages">
                    <Hi:UpImg runat="server" ID="uploader1" IsNeedThumbnail="false" UploadType="topic"  />
                </div>
              <p id="uploader1_uploadedImageUrlTip">（图片尺寸建议：320px * 200px）</p>           
          </li>
                </ul>
                <ul class="btn Pa_100 clearfix">
                    <asp:Button ID="btnUpdateActivity" runat="server" OnClientClick="return checkForm();"
                        Text="保 存" CssClass="submit_DAqueding" onclick="btnUpdateActivity_Click" />
                </ul>
            </div>
        </div>
    </div>
    <div class="databottom">
        <div class="databottom_bg">
        </div>
    </div>
    <div class="bottomarea testArea">
        <!--顶部logo区域-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
<script type="text/javascript">
$(document).ready(function () { CheckOpen() });
function CheckOpen() {
    if ($("#ChkOpen:checked").length > 0) {
        $(".hiddenli").show();
    }
    else {
        $(".hiddenli").hide();
    }
}
function checkForm() {
    if (PageIsValid()) {
        if ($("#calendarStartDate").val() == "") {
            $("#calendarStartDate").click();
            location.hash = "#";
            return false;
        }
        if ($("#calendarOpenDate").val() == "") {
            $("#calendarOpenDate").click();
            location.hash = "#";
            return false;
        }
        if ($("#calendarEndDate").val() == "") {
            $("#calendarEndDate").click();
            location.hash = "#";
            return false;
        }
        if ($("#calendarStartDate").val() > $("#calendarOpenDate").val()) {
            alert("报名开始时间应小于抽奖开始时间");
            $("#calendarOpenDate").click();
            location.hash = "#";
            return false;
        }
        if ($("#calendarStartDate").val() >= $("#calendarEndDate").val()) {
            alert("抽奖开始时间应小于抽奖结束时间");
            $("#calendarEndDate").click();
            location.hash = "#";
            return false;
        }
        var info = $("#uploader1_uploadedImageUrl").val();
        if (info.length < 10) {
            $("#uploader1_uploadedImageUrlTip").attr("class", "msgError").html("请上传封面图片");
            return false;
        } else {
            $("#uploader1_uploadedImageUrlTip").attr("class", "").html("");
        }
        return true;
    } else {
        return false;
    }
}
function InitValidators() {
    initValid(new InputValidator('txtActiveName', 1, 60, false, null, '关键字，长度限制在60个字符以内'));
    initValid(new InputValidator('txtKeyword', 1, 60, false, null, '关键字，长度限制在60个字符以内'));
    initValid(new InputValidator('txtMinValue', 1, 10, false, '[0-9]\\d*', '人数下限必须是整数'));
    appendValid(new NumberRangeValidator('txtMinValue', 1, 999999999, '人数下限需要大于0'));
    initValid(new InputValidator('txtPrize1', 1, 60, false, null, '奖品描述不能为空'));
    initValid(new InputValidator('txtPrize2', 1, 60, false, null, '奖品描述不能为空'));
    initValid(new InputValidator('txtPrize3', 1, 60, false, null, '奖品描述不能为空'));
    initValid(new NumberRangeValidator('txtPrize1Num', 1, 999, '不允许为空，且必须为整数'));
    initValid(new InputValidator('txtPrize1Num', 1, 10, false, '[1-9]\\d*', '不允许为空，且必须为整数'));
    initValid(new InputValidator('txtPrize2Num', 1, 10, false, '[1-9]\\d*', '不允许为空，且必须为整数'));
    initValid(new InputValidator('txtPrize3Num', 1, 10, false, '[1-9]\\d*', '不允许为空，且必须为整数'));
}
$(document).ready(function () { InitValidators(); });
</script>
</asp:Content>
