<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.OffLinePay" CodeBehind="OffLinePay.aspx.cs" MasterPageFile="~/Admin/Admin.Master" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
<div class="dataarea mainwidth databody">
	  <div class="title  m_none td_bottom"> <em><img src="../images/01.gif" width="32" height="32" /></em>
	    <h1>线下支付</h1>
      </div>
	  <div class="datafrom">
	    <div class="formitem validator1">
	      <ul>
          <li><h2 class="colorE">线下支付</h2><span>请填写您的线下付款方式说明，如银行卡转账，支付宝转账等。</span><br />
              <div>例如：【支付宝 test@aplipay.com 张三】 或者 【工商银行 5855518888888888855 张三】</div>
          </li>
          <li class="clearfix"> <span class="formitemtitle Pw_198">
          
          </span>
          <abbr class="formselect">
          <Kindeditor:KindeditorControl id="fkContent" runat="server" Width="670px"  height="200px" />  
          </abbr>
          </li>
          <li class="clearfix"> <span>是否开启：</span>
          <abbr class="formselect">
            <Hi:YesNoRadioButtonList ID="radEnableOffLinePay" runat="server" RepeatLayout="Flow" />
          </abbr>
          </li>
            
            <li><h2 class="colorE">货到付款</h2></li>
            <li class="clearfix"><span>是否开启：</span>
                <abbr class="formselect">
            <Hi:YesNoRadioButtonList ID="radEnablePro" runat="server" RepeatLayout="Flow" />
          </abbr>
            </li>
	      </ul>
	      <ul class="btntf Pa_198 clear">
	        <asp:Button runat="server" ID="btnAdd" Text="保 存" onclick="btnOK_Click" CssClass="submit_DAqueding inbnt" />
          </ul>
        </div>
      </div>
</div>	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>

