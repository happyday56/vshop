<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTemplateId.aspx.cs" Inherits="Hidistro.UI.Web.Admin.tools.EditTemplateId"  MasterPageFile="~/Admin/Admin.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">

    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title ">
                <em>
                    <img src="../images/01.gif" width="32" height="32" /></em>
                <h1>编辑微信模板消息Id</h1>
                <span>模板为Id是在微信公众平台添加对应的模板后随机生成的一串字符，系统根据此Id来确定所使用的模板。</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <li><span class="formitemtitle Pw_100">模板Id：</span>
                        <asp:TextBox ID="txtTemplateId" runat="server" ClientIDMode="Static" Width="300px" CssClass="forminput"></asp:TextBox>
                        &nbsp; <a href="weixinSettings.html#step5" target="_blank">点击查看帮助</a>
                    </li>
                </ul>
            </div>
            <div class="btn Pa_140 clear">
                <asp:Button ID="btnSaveEmailTemplet"  runat="server" OnClientClick="return PageIsValid();" Text="保 存" CssClass="submit_DAqueding inbnt" /></div>

        </div>

    </div>

    <script type="text/javascript">

        function PageIsValid() {
            var templateId = $('#txtTemplateId').val();
            if (!templateId) {
                alert('请填写模板Id');
                return false;
            }
            return true;
        }



    </script>


</asp:Content>

