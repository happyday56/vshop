<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PrizeRecord.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.PrizeRecord" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="dataarea mainwidth databody">
    <div class="title"> <em><img src="../images/01.gif" width="32" height="32" /></em>
      <h1><asp:Literal ID="LitTitle" runat="server"></asp:Literal>中奖名单
        </h1>        
     <span><asp:Literal ID="Litdesc" runat="server">您可以在此管理好您的抽奖活动，并在自定义回复中使用它们。</asp:Literal></span></div>
    <!-- 添加按钮-->
    <!--结束-->
    <!--数据列表区域-->
    <div class="datalist">
          <asp:Repeater ID="rpMaterial" runat="server">
                    <HeaderTemplate>
                        <table border="0" cellspacing="0" width="80%" cellpadding="0">
                            <tr class="table_title">
                                <td>昵称</td>
                                <td>状态</td>
                                <td>奖品</td>
                                <td>领奖人姓名</td>
                                <td>联系电话</td>
                                <td>中奖时间</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("UserName")%>&nbsp;</td> 
                            <td><%#Eval("Prizelevel")%>&nbsp;</td>
                            <td><%#Eval("PrizeName")%>&nbsp;</td>
                            <td><%#Eval("RealName")%>&nbsp;</td>
                            <td><%#Eval("CellPhone")%>&nbsp;</td>
                            <td><%#Eval("PrizeTime")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>        
                
    </div>
    <!--数据列表底部功能区域-->
      
  </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
