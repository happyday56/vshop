<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="VIPcard.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.VIPcard" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link href="../css/VipCard.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                会员卡</h1>
            <span>微信会员可在会员卡页领取您已设置好的会员卡。</span></div>
        <!-- 添加按钮-->
        <!--结束-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="cardView">
                <img src="vipcard.png" id="imgbg" runat="server" />
                <div id="logoText" class="logo">
                    logo文字</div>
                <div class="cardName">
                    海商会员卡</div>
                <div class="qrCode">
                    <img src="qrCode.jpg" id="imgqrcode" runat="server" /></div>
                <div class="vipGrade">
                    普通会员</div>
                <div class="cardNumTitle">
                    GW25462</div>
            </div>
            <div class="cardData">
                <div class="body">
                    <div class="fgroup">
                        <span>Logo文字：</span>
                        <div class="finput">
                            <asp:TextBox ID="Tblogo" for="logo" onkeyup="sysn(this)" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="fgroup">
                        <span>会员卡名：</span>
                        <div class="finput">
                            <asp:TextBox ID="TbCardName" for="cardName" runat="server" onkeyup="sysn(this)"></asp:TextBox></div>
                    </div>
                    <div class="fgroup">
                        <span>会员卡前缀：</span>
                        <div class="finput">
                            <asp:TextBox ID="TBPrefix" for="cardNumTitle" onkeyup="sysn(this)" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="fgroup">
                        <span>二维码：</span>
                        <div class="finput">
                            <asp:FileUpload ID="FileUploadQR" Width="150px" runat="server" />
                            <asp:Button ID="BtnUploadQR" runat="server" Text="上传" OnClick="BtnUploadQR_Click" />
                            <asp:LinkButton ID="Lkdel"  Visible="false" runat="server" onclick="Lkdel_Click">移除</asp:LinkButton>
                            </div>
                    </div>
                    <div class="fgroup">
                        <span>卡片背景：</span>
                        <div class="finput">
                            <asp:FileUpload ID="FileUploadBG" runat="server" Width="150px" />
                            <asp:Button ID="BtnUploadBG" runat="server" Text="上传" OnClick="BtnUploadBG_Click"
                                Style="height: 21px" /></div>
                    </div>
                </div>
                <i class="out" style="margin-top: 0px;"></i><i class="in" style="margin-top: 0px;">
                </i>
            </div>
            <div class="clearfix">
            </div>
            <div class="formitem validator1">
                <ul>
                    <h2 class="clear">
                        会员领取条件</h2>
                    <asp:CheckBox ID="ChkVipName" runat="server" Text="" />姓名
                    <asp:CheckBox ID="ChkVipMobile" runat="server" Text="" />手机
                    <asp:CheckBox ID="ChkVipQQ" runat="server" Text="" />QQ
                    <asp:CheckBox ID="ChkAddres" runat="server" Text="" />
                地址
            </div>
            <div class="formitem validator1" style="display:none">
                <ul>
                    <h2 class="clear">
                        会员卡集成功能:</h2>
                    <asp:CheckBox ID="ChkCoupon" runat="server" Text="优惠券" />
            </div>
            <div class="formitem validator1">
                <ul>
                    <h2 class="clear">会员卡说明:</h2>
                    <%--<Kindeditor:KindeditorControl id="txtVipRemark" runat="server" Width="90%" height="200px" />--%>
                    <asp:TextBox ID="txtVipRemark" runat="server" Width="600px" height="150px" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <ul class="btntf Pa_198">
            <asp:Button ID="btnOK" runat="server" Text="保存" CssClass="submit_DAqueding inbnt"
                OnClientClick="return PageIsValid();" OnClick="btnOK_Click" />
        </ul>
    </div>
    <!--数据列表底部功能区域-->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#logoText").text($("#Tblogo").val());
            $(".cardName").text($("#TbCardName").val());
            $(".cardNumTitle").text($("#TBPrefix").val());
        });
        function sysn(obj) {
            $("." + $(obj).attr("for")).text(obj.value);
        }

    </script>
</asp:Content>
