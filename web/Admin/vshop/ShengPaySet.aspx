<%@ Page Language="C#"  MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ShengPaySet.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.ShengPaySet" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
<div class="dataarea mainwidth databody">
      <div class="title"> <em><img src="../images/01.gif" width="32" height="32" /></em>
        <h1>盛付通收款信息设置</h1>
        <span>请设置好您的盛付通信息。 还没开通盛付通？ <a target="_blank" href="https://zhuanye.shengpay.com/SP/Business/quicklygather.aspx">立即免费申请开通盛付通接口</a></span>
      </div>
      <div class="datafrom">
        <div class="formitem validator1">
          <ul>
            <li><h2 class="colorE">盛付通手机网页支付</h2></li>
           <li class="clearfix"><span class="formitemtitle Pw_198">是否开启：</span>
                <abbr class="formselect">
            <Hi:YesNoRadioButtonList ID="radEnableWapShengPay" runat="server" RepeatLayout="Flow" />
          </abbr>
            </li>
            <li><span class="formitemtitle Pw_198">商户号：</span>
              <asp:TextBox ID="txtPartner" CssClass="forminput formwidth" runat="server" />
            </li>
            <li><span class="formitemtitle Pw_198">商家密钥：</span>
              <asp:TextBox ID="txtKey" CssClass="forminput formwidth" runat="server" />
            </li> 
<%--            <li><span class="formitemtitle Pw_198">设置须知：</span>
                填写完成以上配置后，盛付通接口将自动启用
            </li>--%>
          </ul>
          <div style="clear:both"></div>
           <ul class="btntf Pa_198">
            <asp:Button ID="btnOK" runat="server" Text="保 存" CssClass="submit_DAqueding inbnt" 
                   OnClientClick="return PageIsValid();" onclick="btnOK_Click" />
	       </ul>
        </div>
      </div>
</div>
	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
