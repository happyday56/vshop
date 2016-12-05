<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="TopicList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.TopicList" %>

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
                专题管理</h1>
            <span>微信商城专题管理</span></div>
        <!-- 添加按钮-->
        <!--结束-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search singbtn">
                <span class="signicon"></span><a href="AddTopic.aspx">添加新专题</a>
                <asp:LinkButton ID="Lksave" runat="server" OnClick="Lksave_Click">保存排序</asp:LinkButton>
            </div>
            <asp:Repeater ID="rpTopic" runat="server" OnItemCommand="rpTopic_ItemCommand">
                <HeaderTemplate>
                    <table border="0" cellspacing="0" width="80%" cellpadding="0">
                        <tr class="table_title">
                            <td>
                                专题图片
                            </td>
                            <td>
                                专题标题
                            </td>
                            <td>
                                关键字
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
                            <%#Eval("Keys")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("Addeddate")%>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Lbtopicid" Visible="false" Text='<%#Eval("Topicid")%>' runat="server"></asp:Label>
                            <asp:TextBox ID="txtSequence" runat="server" Text='<%# Eval("DisplaySequence") %>'
                                Width="60px" />
                        </td>
                        <td>
                            <a href='<%# "../../admin/vshop/EditTopic.aspx?TopicId=" + Eval("Topicid")%> ' class="SmallCommonTextButton">
                                编辑</a>
                            <asp:LinkButton ID="Lkbtndel" CommandName="del" CommandArgument='<%#Eval("Topicid")%>'
                                runat="server">删除</asp:LinkButton>
                            <a href='<%# Globals.GetAdminAbsolutePath(string.Format("/vshop/SetTopicProducts.aspx?TopicId={0}", Eval("topicId")))%>'>
                                关联商品</a>
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
