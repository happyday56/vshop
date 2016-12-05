<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BatchSendOrderGoods.aspx.cs" Inherits="Hidistro.UI.Web.Admin.sales.BatchSendOrderGoods" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
<div></div>
<div class="dataarea mainwidth databody">
    <div class="title"> <em><img src="../images/05.gif" width="32" height="32" /></em>
        <h1>订单批量发货</h1>
        <span>这里的订单将执行批量发货操作</span></div>
     <div class="searcharea clearfix">
        <ul>
            <li>配送方式： <hi:ShippingModeDropDownList runat="server" AllowNull="true" ID="dropShippingMode" /></li>
            <li><asp:Button ID="btnSetShippingMode" runat="server" Text="确定" CssClass="searchbutton"/></li>
               <li style="margin-left:10px;">物流公司 <asp:DropDownList ID="dropExpressComputerpe" ClientIDMode="Static" runat="server" /></li>
			<li style="margin-left:10px;">起始发货单号<asp:TextBox ID="txtStartShipOrderNumber" ClientIDMode="Static" runat="server" /></li>
			<li><asp:Button ID="btnSetShipOrderNumber" runat="server"  OnClientClick="return CheckShipNumber()" Text="确定" CssClass="searchbutton"/></li>
		</ul>
     </div>
	<!--数据列表区域-->
    <div class="datalist">
     <UI:Grid ID="grdOrderGoods" runat="server" ShowHeader="true" AutoGenerateColumns="false" DataKeyNames="OrderId" HeaderStyle-CssClass="table_title" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="订单编号" ItemStyle-Width="110px">
                    <itemtemplate>
                         <a href='<%# "OrderDetails.aspx?OrderId="+Eval("OrderId") %>'><%#Eval("OrderId") %></a>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收货人" ItemStyle-Width="60px">
                     <itemtemplate>
                           <asp:Literal runat="server" Text='<%# Eval("ShipTo") %>' />
                     </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地区">
                     <itemtemplate>                       
                         <asp:Literal runat="server" Text='<%# Eval("ShippingRegion") %>' />
                </itemtemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细地址" >
                     <itemtemplate>                       
                           <asp:Literal runat="server" Text='<%# Eval("Address") %>' />
                     </itemtemplate> 
                </asp:TemplateField>
                <UI:DropdownColumn HeaderText="配送方式" ItemStyle-Width="100px" 
                      JustForEditItem="false" ID="dropShippId" DataKey="ShippingModeId" DataTextField="Name"
                      DataValueField="ModeId" AllowNull="false" >
                </UI:DropdownColumn>
                <UI:DropdownColumn HeaderText="物流公司" ItemStyle-Width="140px"
                      JustForEditItem="false" ID="dropExpress" DataKey="ExpressCompanyAbb" DataTextField="name"
                      DataValueField="Kuaidi100Code" AllowNull="true" >
                </UI:DropdownColumn>
                <asp:TemplateField HeaderText="发货单号" ItemStyle-Width="110px">
                     <itemtemplate>
                         <asp:TextBox runat="server" ID="txtShippOrderNumber" Text='<%# Eval("ShipOrderNumber") %>' Width="110px" />
                     </itemtemplate>
                </asp:TemplateField>
            </Columns>
        </UI:Grid>
     </div>
     <div class="blank5 clearfix"></div>
     <div style="padding-left:380px;"><asp:Button runat="server" ID="btnBatchSendGoods" Text="批量发货" CssClass="submit_DAqueding" /></div>
</div>
<script type="text/javascript">

    function CheckShipNumber() {
        var no = $("#txtStartShipOrderNumber").val();
        if ($("#dropExpressComputerpe").val() == "") {
            alert('请先选择物流公司!');
            return false;
        }
        if ($("#txtStartShipOrderNumber").val() == "") {
            alert('请填写起始单号!');
            return false;
        }
        var end = no.substr(no.length - 1, 1);
        if (!is_num(end)) {
            alert('请输入正确的快递单号!');
            return false;
        }
        else if ($("#dropExpressComputerpe").val() == "EMS" && !isEMSNo(no)) {
            alert('请输入正确的EMS单号!');
            return false;
        }
        else if ($("#dropExpressComputerpe").val() == "顺丰快递" && !isSFNo(no)) {
            alert('请输入正确的顺丰单号!');
            return false;
        }
        return true;
    }


    function isSFNo(no) {

        if (!is_num(no) || no.length != 12) {
            return false;
        } else {
            return true;
        }
    }

    function is_num(str) {
        var pattrn = /^[0-9]+$/;
        if (pattrn.test(str)) {
            return true;
        } else {
            return false;
        }
    }
    function isEMSNo(no) {
        if (no.length != 13) {
            return false;
        }

        if (getEMSLastNum(no) == no.substr(10, 1)) {
            return true;
        } else {
            return false;
        }
    }
    function getEMSLastNum(no) {
        var v = new Number(no.substr(2, 1)) * 8;
        v += new Number(no.substr(3, 1)) * 6;
        v += new Number(no.substr(4, 1)) * 4;
        v += new Number(no.substr(5, 1)) * 2;
        v += new Number(no.substr(6, 1)) * 3;
        v += new Number(no.substr(7, 1)) * 5;
        v += new Number(no.substr(8, 1)) * 9;
        v += new Number(no.substr(9, 1)) * 7;
        v = 11 - v % 11;
        if (v == 10)
            v = 0;
        else if (v == 11)
            v = 5;
        return v.toString();
    } 
</script>
</asp:Content>
