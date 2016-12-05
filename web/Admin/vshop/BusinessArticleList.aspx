<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="BusinessArticleList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.BusinessArticleList" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                商学院</h1>
            <span>商学院文章管理</span></div>
        <!-- 添加按钮-->
        <!--结束-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search singbtn">
                <span class="signicon"></span><a href="AddBusinessArticle.aspx">添加新文章</a>
                <asp:LinkButton ID="Lksave" runat="server" OnClick="Lksave_Click">保存排序</asp:LinkButton>
            </div>
            <asp:Repeater ID="rpBusinessArticle" runat="server" OnItemCommand="rpBusinessArticle_ItemCommand">
                <HeaderTemplate>
                    <table border="0" cellspacing="0" width="80%" cellpadding="0">
                        <tr class="table_title">
                            <td>
                                文章首图
                            </td>
                            <td>
                                文章标题
                            </td>
                            <td>
                                文章摘要
                            </td>
                            <td>
                                添加时间
                            </td>
                            <td>
                                排序
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <img class="Img100_30" src='<%# Eval("IconUrl") %>' />
                        </td>
                        <td>
                            <%#Eval("title")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("Summary")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("Addeddate")%>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="LbArticleId" Visible="false" Text='<%#Eval("ArticleId")%>' runat="server"></asp:Label>
                            <asp:TextBox ID="txtSequence" runat="server" Text='<%# Eval("DisplaySequence") %>'
                                Width="60px" />
                        </td>
                        <td>
                            <a href='<%# "../../admin/vshop/EditBusinessArticle.aspx?ArticleId=" + Eval("ArticleId")%> ' class="SmallCommonTextButton">
                                编辑</a>
                            <asp:LinkButton ID="Lkbtndel" CommandName="del" CommandArgument='<%#Eval("ArticleId")%>'
                                runat="server">删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
        <!--数据列表底部功能区域-->
        <div class="page">
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
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
