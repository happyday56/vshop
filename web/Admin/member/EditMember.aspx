<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditMember.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditMember" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<%@ Import Namespace="Hidistro.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" Runat="Server">
<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <h1>编辑会员信息</h1>
            <span>编辑会员各项信息资料</span>
          </div>
          <%--<div class="formtab Pg_45">
                   <ul>
                      <li class="visited">基本信息</li>                                      
                      <li><a href='<%="EditMemberLoginPassword.aspx?userId="+Page.Request.QueryString["userId"] %>'>登录密码</a></li>
                      <li><a href='<%="EditMemberTransactionPassword.aspx?userId="+Page.Request.QueryString["userId"] %>'>交易密码</a></li>
            </ul>
          </div>--%>
      <div class="formitem validator4 clearfix">
        <ul>
          <li> <span class="formitemtitle Pw_110">昵称：</span>
               <strong class="colorE"><asp:Literal ID="lblLoginNameValue" runat="server"></asp:Literal></strong></li>
          <li> <span class="formitemtitle Pw_110"><em >*</em>会员等级：</span>
            <abbr class="formselect">
            <Hi:MemberGradeDropDownList ID="drpMemberRankList" runat="server" AllowNull="false" />
          </abbr></li>
          <li> <span class="formitemtitle Pw_110">姓名：</span>
            <asp:TextBox ID="txtRealName" runat="server" CssClass="forminput"></asp:TextBox>
             <p id="ctl00_contentHolder_txtRealNameTip">姓名长度在20个字符以内</p>
          </li>
            <li> <span class="formitemtitle Pw_110"><asp:Literal ID="lblVirtualPoints" runat="server"></asp:Literal>：</span>
            <asp:TextBox ID="txtVirtualPoints" runat="server" CssClass="forminput"></asp:TextBox>
             <p id="ctl00_contentHolder_txtVirtualPointsTip">只能输入实数型数值</p>
          </li>
         
          <li> <span class="formitemtitle Pw_110">电子邮件地址：</span>
            <asp:TextBox ID="txtprivateEmail" runat="server" CssClass="forminput"></asp:TextBox>
            <p  id="ctl00_contentHolder_txtprivateEmailTip">请输入正确电子邮件，长度在1-256个字符以内</p>
          </li>
          <li> <span class="formitemtitle Pw_110">详细地址：</span>
            <Hi:RegionSelector runat="server" ID="rsddlRegion" />
          </li>
          <li> <span class="formitemtitle Pw_110">街道地址：</span>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="forminput"></asp:TextBox>
             <p id="ctl00_contentHolder_txtAddressTip">街道地址必须控制在100个字符以内</p>
          </li>
          
          <li> <span class="formitemtitle Pw_110">QQ：</span>
            <asp:TextBox ID="txtQQ" runat="server" CssClass="forminput"></asp:TextBox>
            <p  id="ctl00_contentHolder_txtQQTip">QQ号长度限制在3-20个字符之间，只能输入数字</p>
          </li>
          
          <li> <span class="formitemtitle Pw_110">手机号码：</span>
            <asp:TextBox ID="txtCellPhone" runat="server" CssClass="forminput"></asp:TextBox>
            <p  id="ctl00_contentHolder_txtCellPhoneTip">手机号码长度限制在3-20个字符之间,只能输入数字</p>
          </li>
          <li> <span class="formitemtitle Pw_110">注册日期：</span>
            <Hi:FormatedTimeLabel ID="lblRegsTimeValue" runat="server" />
          </li>
          
          <li> <span class="formitemtitle Pw_110">总消费金额：</span>
             <asp:Literal ID="lblTotalAmountValue" runat="server"></asp:Literal>
          </li>
      </ul>
      <ul class="btn Pa_110">
        <asp:Button ID="btnEditUser" runat="server" Text="确 定" OnClientClick="return PageIsValid('validatAccount');"  CssClass="submit_DAqueding" />
        </ul>
      </div>

      </div>
  </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" Runat="Server">
    <script type="text/javascript" language="javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtRealName', 0, 20, true, null, '姓名长度在20个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtAddress', 0, 100, true, null, '详细地址必须控制在100个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtQQ', 3, 20, true, '^[0-9]*$', 'QQ号长度限制在3-20个字符之间，只能输入数字'));
            initValid(new InputValidator('ctl00_contentHolder_txtWangwang', 3, 20, true, '', '旺旺长度限制在3-20个字符之间'));
            initValid(new InputValidator('ctl00_contentHolder_txtMSN', 1, 256, true, '([a-zA-Z\\.0-9_-])+@([a-zA-Z0-9_-])+((\\.[a-zA-Z0-9_-]{2,3}){1,2})', '请输入正确MSN帐号，长度在1-256个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtTel', 3, 20, true,'^[0-9-]*$', '电话号码长度限制在3-20个字符之间，只能输入数字和字符“-”'));
            initValid(new InputValidator('ctl00_contentHolder_txtCellPhone', 3, 20, true, '^[0-9]*$', '手机号码长度限制在3-20个字符之间,只能输入数字'));
            initValid(new InputValidator('ctl00_contentHolder_txtVirtualPoints', 1, 7, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtVirtualPoints', 0.00, 9999999, '输入的数值超出了系统表示范围'));
        }
        $(document).ready(function() { InitValidators(); });
    </script>
</asp:Content>