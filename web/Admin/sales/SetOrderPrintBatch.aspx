<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master"
   CodeBehind="SetOrderPrintBatch.aspx.cs" Inherits="Hidistro.UI.Web.Admin.sales.SetOrderPrintBatch" %>
<%@ Import Namespace="Hidistro.Core"%>
<%@ Import Namespace="Hidistro.Entities.Sales"%>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
<div id="bg"></div>
    <div class="dataarea mainwidth databody">
        <div class="title  m_none td_bottom" style="padding: 5px 0px 5px 12px">
            订单打印批次号：<asp:Literal runat="server" ID="litBatchNo" />
        </div>
    </div>
</asp:Content>
