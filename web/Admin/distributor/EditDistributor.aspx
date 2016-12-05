<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditDistributor.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.EditDistributor" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="Server">
    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title">
                <em>
                    <img src="../images/04.gif" width="32" height="32" /></em>
                <h1>修改分销商信息</h1>
                <span>管理员只能修改分销商部分信息 </span>
            </div>
            <div class="formitem validator4">
                <ul>
                    <li><span class="formitemtitle Pw_110"><em>*</em>店铺名：</span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" />
                        <p id="ctl00_contentHolder_txtStoreNameTip">店铺名不能为空，长度限制在20字符以内</p>
                    </li>
                    <li style="display: none;"><span class="formitemtitle Pw_110">分销商状态：</span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrStatus" runat="server">
                                <asp:ListItem Value="0">待审核</asp:ListItem>
                                <asp:ListItem Value="1">未审核</asp:ListItem>
                                <asp:ListItem Value="2">通过审核</asp:ListItem>
                                <asp:ListItem Value="3">拒绝审核</asp:ListItem>
                            </asp:DropDownList></abbr>
                    </li>
                    <li>
                        <span class="formitemtitle Pw_110">分销商等级：</span>
                        <abbr class="formselect"><Hi:DistributorGradeDropDownList ID="dropDistributorGrade" runat="server" AllowNull="false"  style="padding:5px 0px;"/></abbr>
                    </li>
                    <li><span class="formitemtitle Pw_110">店铺描述：</span>
                        <asp:TextBox ID="txtStoreDescription" runat="server" TextMode="MultiLine" CssClass="forminput" Width="450" Height="120"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtStoreDescriptionTip"></p>
                    </li>
                </ul>
                <ul class="btn Pa_110 clear">
                    <asp:Button ID="btnSubmit" OnClientClick="return PageIsValid();" Text="修 改" CssClass="submit_DAqueding" runat="server" />
                </ul>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="Server">
    <script type="text/javascript" language="javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtStoreName', 1, 20, false, null, '店铺名不能为空，长度限制在20个字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtStoreDescription', 0, 100, true, null, '店铺描述的长度限制在100个字符以内'));
        }
        $(document).ready(function () { InitValidators(); });
    </script>

</asp:Content>
