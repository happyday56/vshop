<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeFile="EditRedPagerActivity.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.EditRedPagerActivity" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" Runat="Server">
    <style type="text/css">
        .Pw_110 {
            width:190px;
        }
        .validator4 li p {
            margin:5px 0px 0px 188px;
        }
            .validator4 li p.msgError {
            margin:5px 0px 0px 188px;
            }
    </style>
<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1><%=htmlEditName %>代金券活动信息</h1>
            <span><%--管理员只能修改分销商部分信息--%> </span>
          </div>
      <div class="formitem validator4">
        <ul>
          <li> <span class="formitemtitle Pw_110"><em >*</em>名称：</span>
            <asp:TextBox ID="txtName" CssClass="forminput" runat="server" />
            <p id="ctl00_contentHolder_txtNameTip">名称不能为空，长度限制在20字符以内</p>
          </li>
          <li > <span class="formitemtitle Pw_110">活动类目：</span>
          <abbr class="formselect"> <asp:DropDownList ID="ddlCategoryId" runat="server"></asp:DropDownList></abbr>
              <p>全部代表商城活动，具体类目代表类目活动</p>
          </li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>订单金额达到可发：</span>
            <asp:TextBox ID="txtMinOrderAmount" CssClass="forminput" runat="server" /> &nbsp;元
            <p id="ctl00_contentHolder_txtMinOrderAmountTip">订单达到该金额才能发红包</p>
          </li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>最多被领次数：</span>
            <asp:TextBox ID="txtMaxGetTimes" CssClass="forminput" runat="server" /> &nbsp;个
            <p id="ctl00_contentHolder_txtMaxGetTimesTip"></p>
          </li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>单次最高金额：</span>
            <asp:TextBox ID="txtItemAmountLimit" CssClass="forminput" runat="server" /> &nbsp;元
            <p id="ctl00_contentHolder_txtItemAmountLimitTip"></p>
          </li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>订单金额达到可使用：</span>
            <asp:TextBox ID="txtOrderAmountCanUse" CssClass="forminput" runat="server" /> &nbsp;元
            <p id="ctl00_contentHolder_txtOrderAmountCanUseTip"></p>
          </li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>有效时间：</span>
            <asp:TextBox ID="txtExpiryDays" CssClass="forminput" runat="server" /> &nbsp;天
            <p id="ctl00_contentHolder_txtExpiryDaysTip">红包领取后，多少天失效</p>
          </li>
      </ul>
      <ul class="btn Pa_110 clear">
        <asp:Button ID="btnSubmit" OnClientClick="return checkForm();" Text="修 改" CssClass="submit_DAqueding" runat="server"/>
        </ul>
      </div>
      </div>
  </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" Runat="Server">
        <script type="text/javascript" language="javascript">
            function InitValidators() {
                initValid(new InputValidator('ctl00_contentHolder_txtName', 1, 20, false, null, '名称不能为空，长度限制在20个字符以内'));
                initValid(new InputValidator('ctl00_contentHolder_txtMinOrderAmount', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '请正确输入订单金额，只能输入大于0的实数型数值'));
                appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMinOrderAmount', 1, 100000, '请输入1-100000之间的金额'));
                initValid(new InputValidator('ctl00_contentHolder_txtMaxGetTimes', 1, 10, false, '[1-9]\\d*', '请正确输入最多被领次数，只能输入大于0的整数型数值'));
                appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMaxGetTimes', 1, 100000, '请输入1-100000之间的数字'));
                initValid(new InputValidator('ctl00_contentHolder_txtItemAmountLimit', 1, 10, false, '([1-9]\\d*(\\.\\d{1,2})?)', '请正确输入单次最高金额，单次最高金额不小于1元，只能输入实数型数值'));
                appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtItemAmountLimit', 1, 100000, '请输入1-100000之间的金额'));
                initValid(new InputValidator('ctl00_contentHolder_txtOrderAmountCanUse', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '请正确输入订单金额，只能输入实数型数值'));
                appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtOrderAmountCanUse', 1, 100000, '请输入1-100000之间的金额'));
                initValid(new InputValidator('ctl00_contentHolder_txtExpiryDays', 1, 10, false, '[0-9]\\d*', '请正确输入有效时间，只能输入大于0的整数型数值'));
                appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtExpiryDays', 1, 100000, '请输入1-100000之间的数字'));
            }
            $(document).ready(function () { InitValidators(); });
            function checkForm() {
                if (PageIsValid()) {
                    var comparevalue = $("#ctl00_contentHolder_txtItemAmountLimit").val();
                    var thisvalue = $("#ctl00_contentHolder_txtOrderAmountCanUse").val();
                    if (thisvalue <= comparevalue) {
                        $("#ctl00_contentHolder_txtOrderAmountCanUseTip").html("订单金额需大于单次最高金额").attr("class", "msgError");
                        return false;
                    } else {
                        $("#ctl00_contentHolder_txtOrderAmountCanUseTip").html("订单金额不能为空").attr("class", "");
                        return true;
                    }
                } else {
                    return false;
                }
            }
        </script>       
</asp:Content>
