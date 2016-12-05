<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendExtension.aspx.cs"
    MasterPageFile="~/Admin/Admin.Master" Inherits="Hidistro.UI.Web.Admin.distributor.FriendExtension" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
     <div class="dataarea mainwidth databody">
        <div class="columnright">
            <div class="title ">
                <em>
                    <img src="../images/01.gif" width="32" height="32" /></em>
                <h1>
                    朋友圈分享</h1>
                <span>管理员为分销商提供的朋友圈每日营销的文案资源。</span>
            </div>
        
            <div class="clear">
                <ul class="firend-list clearfix">
                    <li class="add">
                        <div>
                            <a href="AddFriendExtension.aspx">+</a>
                        </div>
                    </li>
                    <asp:Repeater ID="reFriend" runat="server">
                        <ItemTemplate>
                            <li>
                                <p>
                                    <%#Eval("ExensiontRemark")%>
                                  <asp:Literal runat="server" ID="Literal1" Visible="false"  Text='<%#Eval("ExensionImg")%>' />
                                </p>
                                <div class="clearfix">
                                    <asp:Literal runat="server" ID="ImgPic" />
                                </div>
                                <p class="clearfix">
                                    <span>
                                        <%#Eval("CreateTime") %></span>
                                    <Hi:ImageLinkButton ID="lkbtnConfirmPurchaseOrder" IsShow="true" runat="server" Text="删除"
                                        CommandArgument='<%# Eval("ExtensionId") %>' CommandName='Del' DeleteMsg="确认要删除吗？" 
                                        ForeColor="Red" /><a href='EditFriendExtension.aspx?ExtensionId=<%# Eval("ExtensionId") %>'>编辑</a></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
           
        </div>
         <div class="page">
                <div class="bottomPageNumber clearfix">
                    <div class="pageNumber">
                        <div class="pagination">
                            <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <script type="text/javascript">

      


    </script>
</asp:Content>
