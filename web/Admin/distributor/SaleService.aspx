<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SaleService.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.SaleService" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title ">
                <em>
                    <img src="../images/01.gif" width="32" height="32" /></em>
                <h1>
                    售后服务设置</h1>
                <span>管理员可管理售后服务QQ帐号和手机号码。</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <li><span class="formitemtitle Pw_110">售后服务设置：</span>
                        <abbr class="formselect">
                            <Kindeditor:KindeditorControl ID="fkContent" runat="server" Width="530px" Height="200px" />
                        </abbr> 
                        <p   style="padding-left: 20px;">
                          </p>
                    </li>
                </ul>
                <ul class="btn Pa_100 clearfix">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="保存"
                        CssClass="submit_DAqueding float" />
                </ul>
            </div>
        </div>
    </div>
    <script type="text/javascript">

     


    </script>
</asp:Content>
