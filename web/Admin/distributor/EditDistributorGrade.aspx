<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="EditDistributorGrade.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditDistributorGrade" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" Runat="Server">
<style type="text/css">.Pw_110{width:145px;padding-right: 5px;}.errorFocus{width:220px;}.forminput{width:220px;padding:4px 0px 4px 2px}.areacolumn .columnright .formitem li{margin-bottom:0;}</style>
<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1><%=htmlOperatorName %>分销商等级</h1>
            <span>不同的分销商对应不同的分佣比</span>
          </div>
      <div class="formitem validator4 clearfix">
        <ul>
          <li><span class="formitemtitle Pw_110">分销商等级名称</span><asp:TextBox ID="txtName" runat="server" CssClass="forminput"></asp:TextBox><p id="ctl00_contentHolder_txtNameTip"></p></li>
          <li><span class="formitemtitle Pw_110">需要销售金额达到</span><asp:TextBox ID="txtCommissionsLimit" runat="server" CssClass="forminput"></asp:TextBox><span style="padding-left:5px">元</span> <span style="color:#ccc; padding-left:5px;">(算三级内的佣金)</span><p id="ctl00_contentHolder_txtCommissionsLimitTip"></p></li>         
          <li><span class="formitemtitle Pw_110"><em ></em>直接销售佣金上浮</span><asp:TextBox ID="txtFirstCommissionRise" runat="server" CssClass="forminput"></asp:TextBox>%<p id="ctl00_contentHolder_txtFirstCommissionRiseTip"></p></li>
          <li><span class="formitemtitle Pw_110">二级抽佣比上浮</span><asp:TextBox ID="txtSecondCommissionRise" runat="server" CssClass="forminput"></asp:TextBox>%<p id="ctl00_contentHolder_txtSecondCommissionRiseTip"></p></li>
          <li><span class="formitemtitle Pw_110">三级抽佣比上浮</span><asp:TextBox ID="txtThirdCommissionRise" runat="server" CssClass="forminput"></asp:TextBox>%<p id="ctl00_contentHolder_txtThirdCommissionRiseTip"></p></li>

            <li><span class="formitemtitle Pw_110">推荐收入</span><asp:TextBox ID="txtRecommendedIncome" runat="server" CssClass="forminput"></asp:TextBox><span style="padding-left:5px">元</span> <span style="color:#ccc; padding-left:5px;">(推广收入)</span><p id="ctl00_contentHolder_txtRecommendedIncomeTip"></p></li>
            <li><span class="formitemtitle Pw_110">额外提成比</span><asp:TextBox ID="txtAdditionalFees" runat="server" CssClass="forminput"></asp:TextBox>%<p id="ctl00_contentHolder_txtAdditionalFeesTip"></p></li>

          <li><span class="formitemtitle Pw_110">等级图标</span><div class="uploadimages"><Hi:UpImg runat="server" ID="uploader1" IsNeedThumbnail="false" UploadType="vote"  /></div><p id="uploader1_uploadedImageUrlTip" style="padding-left:23px;">（建议上传PNG背景透明的图片，大小50px * 50px）</p></li>
          <li style="margin-bottom:10px;"> <span class="formitemtitle Pw_110">设成默认：</span><asp:RadioButtonList ID="rbtnlIsDefault" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="0">是</asp:ListItem><asp:ListItem Value="1" Selected="True">否</asp:ListItem></asp:RadioButtonList></li>
          <li style="margin-bottom:10px;"> <span class="formitemtitle Pw_110">备注</span><asp:TextBox ID="txtDescription" runat="server" CssClass="forminput" TextMode="MultiLine" Width="350" Height="90"></asp:TextBox></li>
      </ul>
      <ul class="btn Pa_110">
        <asp:Button ID="btnEditUser" runat="server" Text="确 定" OnClientClick="return CheckForm()"  CssClass="submit_DAqueding" />
       </ul>
      </div>

      </div>
  </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" Runat="Server">
    <script type="text/javascript">
        function CheckForm() {
            if (PageIsValid()) {
                var info = $("#uploader1_uploadedImageUrl").val();
                if (info.length < 10) {
                    $("#uploader1_uploadedImageUrlTip").attr("class", "msgError").html("请上传等级图标");
                    return false;
                } else {
                    $("#uploader1_uploadedImageUrlTip").attr("class", "").html("");
                }
            } else {
                return false;
            }
        }
        function InitValidators() {

            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtFirstCommissionRise', 0, 100, '佣金上浮必须在0-100之间'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtSecondCommissionRise', 0, 100, '佣金上浮必须在0-100之间'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtThirdCommissionRise', 0, 100, '佣金上浮必须在0-100之间'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtAdditionalFees', 0, 100, '佣金上浮必须在0-100之间'));

            initValid(new InputValidator('ctl00_contentHolder_txtName', 1, 20, false, null, '分销商等级名称在20个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtCommissionsLimit', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtFirstCommissionRise', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtSecondCommissionRise', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtThirdCommissionRise', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
            initValid(new InputValidator('ctl00_contentHolder_txtAdditionalFees', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
             //initValid(new InputValidator('uploader1_uploadedImageUrl', 1, 200, false, null, '请上传等级图标'));
        }
        $(document).ready(function() { InitValidators(); });

    </script>
    
</asp:Content>