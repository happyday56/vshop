<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="AddActivities.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.AddActivities" %>

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
                <h1>添加活动</h1>
                <span>创建活动信息</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <li>
                        <span class="formitemtitle Pw_100"><em>*</em>封面：</span>
                        <asp:FileUpload runat="server" ID="fileUploader" CssClass="forminput" />
                        <p id="P1">封面图片显示在列表中</p>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>活动名称：</span>
                        <asp:TextBox ID="txtName" runat="server" CssClass="forminput" Width="400"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtNameTip"></p>
                    </li>
                    <li>
                        <span class="formitemtitle Pw_100"><em>*</em>类目名称：</span>
                        <abbr class="formselect">
                            <Hi:ProductCategoriesDropDownList ID="dropCategories" runat="server" NullToDisplay="--全部--"
                                Width="150" />
                        </abbr>
                    </li>
                    <li><span class="formitemtitle Pw_100">满足金额：</span>
                        <asp:TextBox ID="txtMeetMoney" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtMeetMoneyTip">必须是整数</p>
                    </li>
                    <li><span class="formitemtitle Pw_100">减免金额：</span>
                        <asp:TextBox ID="txtReductionMoney" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtReductionMoneyTip">必须是整数</p>
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>开始日期：</span>
                        <UI:WebCalendar ID="txtStartDate" runat="server" CssClass="forminput" />
                    </li>
                    <li><span class="formitemtitle Pw_100"><em>*</em>结束日期：</span>
                        <UI:WebCalendar ID="txtEndDate" runat="server" CssClass="forminput" />
                    </li>
                    <li>
                        <span class="formitemtitle Pw_100">是否首页显示：</span>
                        <asp:RadioButton runat="server" ID="radOnHome" GroupName="DisplayHomeStatus" Text="显示"></asp:RadioButton>
                        <asp:RadioButton runat="server" ID="radUnHome" GroupName="DisplayHomeStatus" Text="不显示" Checked="true"></asp:RadioButton>
                    </li>
                    <li><span class="formitemtitle Pw_100">商品分类(除外)：</span>
                        <span style="float: left;">
                            <Hi:ProductCategoriesCheckBoxList runat="server" ID="chlistProductCategories" Width="100%" /></span>
                    </li>
                    <li><span class="formitemtitle Pw_100">活动简介：</span>
                        <Kindeditor:KindeditorControl ID="txtDescription" runat="server" Width="550px" Height="200px" />
                    </li>


                </ul>
                <ul class="btn Pa_100 clear">
                    <asp:Button ID="btnAddActivity" runat="server" Text="添加" OnClientClick="return PageIsValid();" CssClass="submit_DAqueding" />
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
            initValid(new InputValidator('ctl00_contentHolder_txtReductionMoney', 1, 10, false, '[0-9]\\d*', '必须是整数'));
        }
        $(document).ready(function () { InitValidators(); });
    </script>
</asp:Content>
