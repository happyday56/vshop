<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.VServerConfig" CodeBehind="VServerConfig.aspx.cs" MasterPageFile="~/Admin/Admin.Master" %>

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
            <h1>公众账号信息配置</h1>
        </div>
        <div class="datafrom">
            <div class="formitem validator1">
                <ul>
                    <li>
                        <h2 class="colorE">基本通讯配置</h2>
                        <span>请将URL与TOKEN配置到 <a target="_blank" href="http://mp.weixin.qq.com">微信公众平台</a>下。</span></li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">URL：</span>
                        <abbr class="formselect" style="line-height: 28px;">
                            <asp:Literal runat="server" ID="txtUrl"></asp:Literal>
                        </abbr>
                    </li>
                    <li style="line-height: 28px;"><span class="formitemtitle Pw_198">Token：</span>
                        <asp:Literal runat="server" ID="txtToken"></asp:Literal>
                    </li>

                    <li>
                        <h2 class="colorE">自定义菜单权限配置</h2>
                        <span>如果您开通了自定义菜单，请将<a target="_blank" href="http://mp.weixin.qq.com">微信公众平台</a>下的AppId与AppSecret配置在下方。</span></li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">AppId：</span>
                        <asp:TextBox ID="txtAppId" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">AppSecret：</span>
                        <asp:TextBox ID="txtAppSecret" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <%--          <li><h2 class="colorE">积分设置</h2></li>--%>
                    <li class="clearfix" style="display: none"><span class="formitemtitle Pw_198">注册分销商可获得：</span>
                        <input type="text" class="forminput formwidth" runat="server" id="txtregisterpoints" /><label for="ctl00_contentHolder_chkIsValidationService">积分</label>
                    </li>
                    <li class="clearfix" style="display: none"><span class="formitemtitle Pw_198">多少元一积分：</span>
                        <input type="text" id="txtorderpoints" runat="server" />
                    </li>
                    <li>
                        <h2 class="colorE">登录接口配置</h2>
                    </li>
                    <li class="clearfix" style="line-height: 28px;"><span class="formitemtitle Pw_198">微信官方登录接口：</span>
                        <input type="checkbox" id="chkIsValidationService" runat="server" style="position: relative; top: 3px;" /><label for="ctl00_contentHolder_chkIsValidationService">确认使用</label><span style="color: Red;">(仅认证服务号可用）</span>
                    </li>
                    <li style="display: none;" class="clearfix"><span class="formitemtitle Pw_198">微信二维码：</span>
                        <asp:FileUpload ID="fileUpload" CssClass="forminput" runat="server" />
                        <asp:Button ID="btnUpoad" runat="server" Text="上传" CssClass="submit_queding" Style="margin-left: 5px;" />
                        <div class="Pa_128 Pg_8 clear">
                            <table width="300" border="0" cellspacing="0">
                                <tr>
                                    <td width="80">
                                        <Hi:HiImage runat="server" ID="imgPic" Width="100px" CssClass="Img100_60" /></td>
                                    <td width="80" align="left">
                                        <Hi:ImageLinkButton ID="btnPicDelete" runat="server" IsShow="true" Text="删除" /></td>
                                </tr>
                                <tr>
                                    <td width="160" colspan="2"></td>
                                </tr>
                            </table>
                        </div>
                    </li>
                    <li style="display: none;" class="clearfix"><span class="formitemtitle Pw_198">微信账号：</span>
                        <asp:TextBox ID="txtWeixinNumber" CssClass="forminput formwidth" runat="server" />
                        <p>请在微信公众平台-设置-账号信息下获取“微信号”</p>
                    </li>
                    <li class="clearfix" style="display: none"><span class="formitemtitle Pw_198">微信通用登录接口：</span>
                        <asp:TextBox ID="txtWeixinLoginUrl" CssClass="forminput formwidth" runat="server" />
                    </li>
                    <li>
                        <h2 class="colorE">商城名称配置</h2>
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">商城名称：</span>
                        <asp:TextBox ID="txtSiteName" CssClass="forminput formwidth" runat="server" />
                        <p id="txtSiteNameTip" runat="server">店铺名称为必填项，长度限制在60字符以内</p>
                    </li>


                    <li class="clearfix"><span class="formitemtitle Pw_198">商城介绍：</span>
                        <asp:TextBox ID="txtShopIntroduction" CssClass="forminput formwidth" Width="400px" Height="100px" TextMode="MultiLine" runat="server" />
                        <p id="txtShopIntroductionTip" runat="server">长度限制在60字符以内</p>
                    </li>
                    <li>
                        <h2 class="colorE">引导关注公众号设置</h2>
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">微信引导页面地址：</span>
                        <asp:TextBox ID="txtGuidePageSet" CssClass="forminput formwidth" Width="400px" Height="100px" TextMode="MultiLine" runat="server" />
                        <p id="P2" runat="server">注：未关注公众号时，引导至此页面,<%--<a style="font-size:12px;" href='javascript:window.showModalDialog("../help/wechat-help.html",null,"dialogWidth="+($(document).width()+100)+"px;dialogHeight=1000px")'>查看示例</a>--%></p>
                    </li>
                    <li>
                        <h2 class="colorE">多客服设置</h2>
                    </li>
                    <li class="clearfix" style="line-height: 28px;">
                        <span class="formitemtitle Pw_198">是否开通多客服：</span>
                        <input type="checkbox" id="chk_manyService" runat="server" style="position: relative; top: 3px;" /><label for="ctl00_contentHolder_chk_manyService">已开通</label><span style="color: Red;">(仅认证服务号可在公众平台开通多客服）</span>
                    </li>
                    <li style="display: none">
                        <h2 class="colorE">系统接口设置</h2>
                    </li>
                    <li style="display: none"><span class="formitemtitle Pw_198">安全校验码：</span>
                        <asp:Literal ID="litKeycode" runat="server" />
                        <p id="P1" runat="server">系统开放的接口，需要用到此安全校验码进行签名验证，如果安全校验码泄漏，请联系客户人员更换</p>
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
            initValid(new InputValidator('ctl00_contentHolder_txtSiteName', 1, 60, false, null, '店铺名称为必填项，长度限制在60字符以内'));
            initValid(new InputValidator('ctl00_contentHolder_txtShopIntroduction', 1, 60, true, null, '长度限制在60字符以内'));
           // initValid(new InputValidator('ctl00_contentHolder_txtregisterpoints', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
            //initValid(new InputValidator('ctl00_contentHolder_txtorderpoints', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
        }
        $(document).ready(function () { InitValidators(); });
    </script>
</asp:Content>

