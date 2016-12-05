<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.VParamConfig" CodeBehind="VParamConfig.aspx.cs" MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title  m_none td_bottom">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>参数信息配置</h1>
        </div>
        <div class="datafrom">
            <div class="formitem validator1">
                <ul>
                    <li>
                        <h2 class="colorE">参数设置</h2>
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">店主赠送金币：</span>
                        <asp:TextBox ID="txtDefaultVirtualPoint" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">钻石会员赠送金币：</span>
                        <asp:TextBox ID="txtTempStorePoint" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">钻石会员每月销售最低额度：</span>
                        <asp:TextBox ID="txtTempStoreSaleAmount" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li class="clearfix" style="display: none"><span class="formitemtitle Pw_198">默认招募数(店主)：</span>
                        <asp:TextBox ID="txtRecruitCnt" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li class="clearfix" style="display: none"><span class="formitemtitle Pw_198">默认招募数赠送金币(店主)：</span>
                        <asp:TextBox ID="txtRecruitCntPoint" CssClass="forminput formwidth" runat="server" />
                    </li>
                </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnAdd" Text="保 存" OnClientClick="return PageIsValid();" OnClick="btnOK_Click" CssClass="submit_DAqueding inbnt" />
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtDefaultVirtualPoint', 1, 8, false, null, '默认赠送金币为必填项，长度限制在8字符以内'));
        }
        $(document).ready(function () { InitValidators(); });
    </script>
</asp:Content>

