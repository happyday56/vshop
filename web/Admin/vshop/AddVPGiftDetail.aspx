<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="AddVPGiftDetail.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.AddVPGiftDetail" %>


<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title">
                <em>
                    <img src="../images/06.gif" width="32" height="32" /></em>
                <h1>修改活动详情</h1>
                <span>修改活动详情信息</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <li><span class="formitemtitle Pw_100"><em>*</em>活动名称：</span>
                        <asp:TextBox ID="txtName" runat="server" CssClass="forminput" Width="400" ReadOnly="true"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtNameTip"></p>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>赠送类型：</span>
                        <asp:RadioButton runat="server" ID="radVPGiftType1" GroupName="VPGiftTypeStatus" Text="首单赠送" Checked="true" Enabled="false"></asp:RadioButton>
                        <asp:RadioButton runat="server" ID="radVPGiftType2" GroupName="VPGiftTypeStatus"  Text="购买赠送"  Enabled="false" ></asp:RadioButton>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>赠送类别：</span>
                        <asp:RadioButton runat="server" ID="radVPGiftCategory1" GroupName="VPGiftCategoryStatus" Text="固定金额" Checked="true"  Enabled="false"></asp:RadioButton>
                        <asp:RadioButton runat="server" ID="radVPGiftCategory2" GroupName="VPGiftCategoryStatus"  Text="百分比"  Enabled="false"></asp:RadioButton>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>开始时间：</span>
                        <UI:WebCalendar ID="txtStartDate" runat="server" CssClass="forminput" ReadOnly="true" />
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>结束时间：</span>
                        <UI:WebCalendar ID="txtEndDate" runat="server" CssClass="forminput" ReadOnly="true" />
                    </li>                    
                    <li><span class="formitemtitle Pw_100"><em>*</em>满足金额：</span>
                        <asp:TextBox ID="txtMeetMoney" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtMeetMoneyTip">必须是整数</p>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>赠送金额：</span>
                        <asp:TextBox ID="txtGiftMoney" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtGiftMoneyTip">必须是整数</p>
                    </li>
                </ul>
                <ul class="btn Pa_100 clear">
                    <asp:Button ID="btnEditActivity" runat="server" Text="添加" OnClientClick="return PageIsValid();" CssClass="submit_DAqueding" />
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtName', 1, 100, false, null, '必填 活动名称不能为空，在1至100个字符之间'));
            initValid(new InputValidator('ctl00_contentHolder_txtMeetMoney', 1, 10, false, '[0-9]\\d*', '必须是整数'));
            initValid(new InputValidator('ctl00_contentHolder_txtGiftMoney', 1, 10, false, '[0-9]\\d*', '必须是整数'));
        }
        $(document).ready(function () { InitValidators(); });
    </script>
</asp:Content>
