<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DistributorDetails.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorDetails" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" Runat="Server">

<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/04.gif" width="32" height="32" /></em>
            <span>分销商的详细信息</span>
          </div>
        <div class="formitem clearfix">
          <ul>
          <li style="display:none;"><span class="formitemtitle Pw_110">分店名称：<span style="padding-top:3px;"></span><asp:Literal runat="server" ID="lblStoreName" /></span></li>
          <li><span class="formitemtitle Pw_110">联系人：</span><span style="padding-top:3px;"> <asp:Literal runat="server" ID="litRealName" /></span></li>
          <li><span class="formitemtitle Pw_110">手机号码：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litCellPhone" /></span></li>
          <li><span class="formitemtitle Pw_110">QQ：</span> <span style="padding-top:3px;"><asp:Literal runat="server" ID="litQQ" /></span></li>
          <li><span class="formitemtitle Pw_110">微信昵称：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litMicroSignal" /></span></li>
          <li style=" display:none"><span class="formitemtitle Pw_110">等级：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litGreade" /></span></li>   
          <li><span class="formitemtitle Pw_110">有效推广订单：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litOrders" /></span></li>        
          <li><span class="formitemtitle Pw_110">有效推广佣金：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litCommission" /></span></li>
          <li style=" display:none"><span class="formitemtitle Pw_110">所属上级：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litUpGrade" /> </span></li>
          <li style=" display:none"><span class="formitemtitle Pw_110">拥有下级数：</span><span style="padding-top:3px;"><asp:Literal runat="server" ID="litDownGradeNum" /></span> </li>
      </ul>
      <ul class="btn Pa_110">
        <asp:Button runat="server" ID="btnSubmit" CssClass="submit_DAqueding" Text="返回" />
        </ul>
      </div>

      </div>
  </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" Runat="Server">

</asp:Content>