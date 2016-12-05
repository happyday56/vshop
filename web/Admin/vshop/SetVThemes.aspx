<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="SetVThemes.aspx.cs" Inherits="Hidistro.UI.Web.Admin.SetVThemes" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <script type="text/javascript">
        var auth = "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value) %>";
    </script>
    <script src="../js/swfupload/swfupload.js" type="text/javascript"></script>
    <script src="../js/UploadHandler.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                您正在使用“<a href="ManageVThemes.aspx"><asp:Literal ID="litThemeName" runat="server"></asp:Literal></a>”模板</h1>
            <span>微信商城的页面风格，您可以设置当前模板相关配置参数</span>
        </div>
        <div class="datafrom">
            <div class="formitem validator1">
                <ul>
                    <li>
                        <h2 class="colorE">
                            基本信息</h2>
                    </li>
                    <li><span class="formitemtitle Pw_198">专题页最多显示商品数：</span>
                        <asp:TextBox ID="txtTopicProductMaxNum" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="txtTopicProductMaxNumTip" runat="server">
                            专题页最多显示商品数不能为空,必须在0-1000之间</p>
                    </li>
                    <li>
                        <h2 class="colorE">
                            背景图片设置</h2>
                    </li>
                    <li><span class="formitemtitle Pw_198">首页背景图：</span>
                        <asp:RadioButton GroupName="templatebg" ID="RadDefault" runat="server"  />默认背景 &nbsp;&nbsp;
                        <asp:RadioButton GroupName="templatebg" ID="RadCustom" runat="server" />
                        自定义背景 （建议尺寸：360*550）
                        <div id="customerbg" runat="server">
                            <a id="delpic" runat="server" href='javascript:void(0)' onclick='RemovePic()'>删除</a>
                            <span id="spanButtonPlaceholder"></span><span id="divFileProgressContainer"></span>
                        </div>
                        &nbsp;<div class="Pa_128 Pg_8 clear">
                            <table width="500" border="0" cellspacing="0">
                                <tr>
                                    <td width="80" id="defaultBGImg">
                                        <Hi:HiImage runat="server" ID="imgPic" Width="100px" CssClass="Img100_60" />
                                    </td>
                                    <td width="80" align="left" id="templatetd" style="display:none;">
                                        <img id="littlepic" runat="server" src="" class="Img100_60" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </li>
                    <li>
                        <h2 class="colorE">
                            价格重命名</h2>
                    </li>
                    <li><span class="formitemtitle Pw_198">市场价：</span>
                        <asp:TextBox ID="txtMarketPrice" runat="server" CssClass="forminput"></asp:TextBox>
                    </li>
                    <li><span class="formitemtitle Pw_198">售价：</span>
                        <asp:TextBox ID="txtSalePrice" runat="server" CssClass="forminput"></asp:TextBox>
                    </li>
                    <li>
                        <h2 class="colorE">
                            菜单配置</h2>
                    </li>
                    <li><span class="formitemtitle Pw_198">电话：</span>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="forminput"></asp:TextBox>
                    </li>
                    <li><span class="formitemtitle Pw_198">导航：</span>
                        <asp:TextBox ID="txtNavigate" runat="server" CssClass="forminput" Width="200px"></asp:TextBox>
                        
                    </li>
                </ul>
                <ul class="btntf Pa_198">
                    <asp:Button ID="btnOK" runat="server" Text="提 交" CssClass="submit_DAqueding inbnt"
                        OnClientClick="return PageIsValid();" />
                </ul>
                <!--隐藏图片地址-->
                <input id="fmSrc" runat="server" clientidmode="Static" type="hidden" value="" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript">
        function InitValidators() {
            initValid(new InputValidator('txtTopicProductMaxNum', 1, 100, false, '-?[0-9]\\d*', '专题页最多显示商品数不能为空,必须在0-100之间'));
            appendValid(new NumberRangeValidator('txtTopicProductMaxNum', 0, 100, '专题页最多显示商品数不能为空,必须在0-100之间'));
        }
        $(document).ready(function () {
            InitValidators();
            $("input[type='radio']").bind("click", function () { SwicthBG() });
        });

        $(function () {
            SwicthBG();
        });


        function SwicthBG() {
            var checked = $("#RadDefault:checked");
            if (checked.length > 0) {
                $("#defaultBGImg").show();
                $("#templatetd").hide();
                $("#customerbg").hide();
            }
            else {
                $("#defaultBGImg").hide();
                $("#templatetd").show();
                $("#customerbg").show();
            }

        }





    </script>
    <script type="text/javascript" src="../js/UploadTemplateBg.js"></script>
</asp:Content>
