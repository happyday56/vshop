<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.Main" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Default.aspx.cs"%>
<%@ Import Namespace="Hidistro.Core"%>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
<script>
    function ShowSecondMenuLeft(firstnode, secondurl, threeurl) {
        window.parent.ShowMenuLeft(firstnode, secondurl, threeurl);
        art.dialog.close();
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
<div class="hishop_quick">运营快捷入口：</div>
<div class="hishop_list">
    <ul>
        <asp:Literal runat="server" ID="litFastMenu" />
      <%--   <li><a href="javascript:ShowSecondMenuLeft('微商品','product/selectcategory.aspx',null)"><img src="images/1.png" /><br />添加商品</a></li>
       <li><a href="javascript:ShowSecondMenuLeft('微统计','tools/cnzzstatistictotal.aspx',null)"><img src="images/3.png" /><br />网站流量</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微订单','sales/manageorder.aspx',null)"><img src="images/4.png" /><br />订单列表</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微会员','member/managemembers.aspx',null)"><img src="images/5.png" /><br />会员管理</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微统计','sales/salereport.aspx',null)"><img src="images/10.png" /><br />零售统计</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微配置','vshop/ManageMenu.aspx',null)"><img src="images/21.png" /><br />自定义菜单</a></li>
       <li><a href="javascript:ShowSecondMenuLeft('微配置','vshop/TopicList.aspx',null)"><img src="images/20.png" /><br />专题管理</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微配置','vshop/ReplyOnKey.aspx',null)"><img src="images/23.png" /><br />自定义回复</a></li>
        <li><a href="javascript:ShowSecondMenuLeft('微会员','vshop/VIPcard.aspx',null)"><img src="images/2.png" /><br />会员卡设置</a></li>--%>
    </ul>
    <div class="clear">
    </div>
</div>	 

<div class="hishop_list_content">
    <div class="hishop_list_l">
        <div class="hishop_contenttitle">待处理事务</div>
        <table>
            <tr>
                <td class="td_cont_left">等待发货订单</td><td class="sp_img"><asp:HyperLink ID="ltrWaitSendOrdersNumber" runat="server" ></asp:HyperLink></td>
            </tr>
            <tr style="display:none">
                <td class="td_cont_left">等待退款的团购</td><td class="sp_img"><asp:HyperLink ID="WaitForRefund" runat="server" >0个</asp:HyperLink></td>
            </tr>
        </table>

        <div class="hishop_contenttitle">微信动态</div>
        <table>
            <tr>
                <td class="td_cont_left">今日关注量</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server"  DefaultText="0位" Id="ltrTodayAddMemberNumber" /></td>
                <td class="td_cont_left">昨日关注量</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="0位" ID="lblUserNewAddYesterToday" /></td>
            </tr>
        </table>

        <div class="hishop_contenttitle">近两日业务量</div>
        <table>
            <tr>
                <td class="td_cont_left">今日成交订单数</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="0条" ID="lblTodayFinishOrder"></Hi:ClassShowOnDataLitl></td>
                <td class="td_cont_left">昨日成交订单数</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="0条" ID="lblYesterdayFinishOrder"></Hi:ClassShowOnDataLitl></td>
            </tr>
            <tr>
                <td class="td_cont_left">今日订单金额</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server"  DefaultText="￥0.00" Id="lblTodayOrderAmout" /></td>
                <td class="td_cont_left">昨日订单金额</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="￥0.00" ID="lblOrderPriceYesterDay"></Hi:ClassShowOnDataLitl></td>
            </tr>                 
        </table>
        
        <div class="hishop_contenttitle">店铺信息</div>
        <table>
            <tr>
                <td class="td_cont_left">会员总数</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="0位" ID="lblTotalMembers"></Hi:ClassShowOnDataLitl></td>
                <td class="td_cont_left">商品总数</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="0条" ID="lblTotalProducts"></Hi:ClassShowOnDataLitl></td>
            </tr>
            <tr>
                <td class="td_cont_left">最近30天的订单总金额</td><td class="sp_img"><Hi:ClassShowOnDataLitl runat="server" DefaultText="￥0.00" ID="lblOrderPriceMonth"></Hi:ClassShowOnDataLitl></td>
                <td class="td_cont_left">当前版本</td><td class="sp_img">V1.0</td>
            </tr>
            <tr>
            </tr>
        </table>
        <div class="clear"></div>
    </div>    
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server"></asp:Content>

