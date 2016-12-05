<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="DistributorApplySet.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorApplySet" %>

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
                    分销商申请设置</h1>
                <span>管理员可为分销商设置分销商提现金额和介绍。</span>
            </div>
            <div class="formitem validator2">
                <ul>
                    <li><span class="formitemtitle Pw_110">分销商注册入口：</span>
                        <input id="radiorequeston" type="radio" name="radiorequest" runat="server" value="1" />开启
                        <input id="radiorequestoff" type="radio" name="radiorequest" runat="server" value="2" />关闭（管理员可在分销商列表批量生成分销商账号)
                    </li>
                    <li style="display: none" id="li_requestmoney"><span class="formitemtitle Pw_110">分销商申请条件：</span>
                        <span>累计消费金额必须达到</span><input type="text" id="txtrequestmoney" class="forminput"
                            style="width: 100px" runat="server" />元（付款为准） </li>
                    <li><span class="formitemtitle Pw_110">分销商提现设置：</span>
                        <asp:TextBox ID="txtApplySet" runat="server" CssClass="forminput" />
                        <p id="ctl00_contentHolder_txtApplySetTip" style="padding-left: 20px;">
                            输入整数大于0，并不能为空(分销商提现必须大于等于设置金额)</p>
                    </li>
                      <li><span class="formitemtitle Pw_110">开启三级分佣：</span>
                      <input id="radioCommissionon" type="radio" name="radioCommission" runat="server" value="1" />开启
                        <input id="radioCommissionoff" type="radio" name="radioCommission" runat="server" value="2" />关闭（开启后下级，及下下级的销售额部分将计入佣金)
                       
                    </li>
                    <li><span class="formitemtitle Pw_110">分销商招募细则：</span>
                        <abbr class="formselect">
                            <Kindeditor:KindeditorControl ID="ApplicationDescription" runat="server" Width="630px"
                                Height="400px" />
                        </abbr>
                        <p style="padding-left: 20px;">
                            用户在申请分销商前需要阅读的分销商招募细则。</p>
                    </li>
                    <li><span class="formitemtitle Pw_110">分销商攻略设置：</span>
                        <abbr class="formselect">
                            <Kindeditor:KindeditorControl ID="fkContent" ImportLib="false" runat="server" Width="630px"
                                Height="400px" />
                        </abbr>
                        <p style="padding-left: 20px;">
                            用户在申请成为分销商时需阅读的分销攻略。
                        </p>
                    </li>
                </ul>
                <ul class="btn Pa_100 clearfix">
                    <asp:Button ID="btnSave" runat="server" OnClientClick="return PageIsValid();" OnClick="btnSave_Click"
                        Text="保存" CssClass="submit_DAqueding float" />
                </ul>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            InitValidators();
            $("input[type='radio'][name='ctl00$contentHolder$radiorequest']").bind("click", function () {
                if ($(this).is(":checked") && $(this).val() == "1") {
                    $("#li_requestmoney").css({ display: 'block' });
                } else {
                    $("#li_requestmoney").css({ display: 'none' });
                }
            });
            if ($("#ctl00_contentHolder_radiorequeston").is(":checked")) {
                $("#li_requestmoney").css({ display: 'block' });
            }
        });

        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtApplySet', 1, 10, false, '[0-9]\\d*', '限制分销商填写提现金额必须该数值的倍数，不能为空，必须是整数'));
        }


    </script>
</asp:Content>
