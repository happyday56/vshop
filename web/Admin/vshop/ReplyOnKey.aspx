<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.ReplyOnKey"
 CodeBehind="~/Admin/vshop/ReplyOnKey.aspx.cs"
    MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    
<div class="dataarea mainwidth databody">
        <div class="title"> <em><img src="../images/01.gif" width="32" height="32" /></em>
      <h1>自定义回复</h1>
     <span>您可以在此管理好您的自定义回复。</span></div>
        <div class="btn add_btn_sm">
            <a href="AddReplyOnKey.aspx" class="submit_jia">添加文本回复</a>
            <a href="AddSingleArticle.aspx" class="submit_jia">添加单图文回复</a>
            <a href="AddMultiArticle.aspx" class="submit_jia">添加多图文回复</a>
        </div>
		<div class="clear"></div>
        <!--数据列表区域-->
        <div class="datalist">
            <UI:Grid ID="grdArticleCategories" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                DataKeyNames="Id" HeaderStyle-CssClass="table_title" GridLines="None"
                Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="关键字">
                        <ItemTemplate>
                            <%# Eval("Keys") %>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="回复类型" >
                        <ItemTemplate>
                            <asp:Literal runat="server" Text='<%# GetReplyTypeName(Eval("ReplyType")) %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="立即发布">
                    <ItemTemplate>
                        <div class="infoTitle">
                        <asp:LinkButton ID="btnrelease" runat="server" CommandName="Release" CommandArgument='<%# Eval("IsDisable") %>'><%#Eval("IsDisable").Equals(true) ? "<img alt='点击发布' src='../images/ta.gif' />" : "<img alt='点击取消' src='../images/iconaf.gif' />"%></asp:LinkButton>
                            </div>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="回复类型">
                        <ItemTemplate>
                            <asp:Literal ID="lblCategoryName" runat="server" Text='<%# Eval("MessageTypeName") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="最后修改日期">
                        <ItemTemplate>
                            <asp:Literal runat="server" Text='<%# Eval("LastEditDate") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="最后修改人">
                        <ItemTemplate>
                            <asp:Literal runat="server" Text='<%# Eval("LastEditor") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="95">
                        <ItemStyle CssClass="spanD spanN" />
                        <ItemTemplate>
                            <span class="submit_bianji">
                            <Hi:ImageLinkButton  ID="lkEdit" Text="编辑" IsShow="false"  CommandName="Edit" runat="server" />
                            </span>
                            <span class="submit_shanchu"><Hi:ImageLinkButton ID="lkDelete" Text="删除" IsShow="true"  CommandName="Delete" runat="server" /></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
            <div class="blank5 clearfix">
            </div>
        </div>
    </div>
    <div class="databottom">
    </div>
    <div class="bottomarea testArea">
        <!--顶部logo区域-->
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
