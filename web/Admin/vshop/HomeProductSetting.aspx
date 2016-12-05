<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="HomeProductSetting.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.HomeProductSetting" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript">
        function ShowAddDiv() {
            DialogFrame("./vshop/SearchHomeProduct.aspx", "添加首页商品", 975, null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
    <input type="hidden" id="hdtopic" runat="server" />
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/03.gif" width="32" height="32" /></em>
            <h1>
                首页商品
            </h1>
            <span>如果您选择了首页带有商品的模板，您需要在此配置好相关参数。</span>
        </div>
        <!--结束-->
        <div class="btn">
            <div style="float: right; margin-right: 15px;" class="delete">
                <Hi:ImageLinkButton ID="btnDeleteAll" runat="server" Text="清空" IsShow="true" DeleteMsg="确定要清空首页商品吗？" /></div>
            <a class="submit_jia" href="javascript:void(0)" onclick="ShowAddDiv()">添加首页商品</a>

            <asp:LinkButton ID="btnFinish" ClientIDMode="Static" runat="server" Text="保存排序" CssClass="base_blue_btn"
                    OnClick="btnFinish_Click"></asp:LinkButton>
   
        </div>
        <!--数据列表区域-->
        <div class="datalist datalist-img">
            <UI:Grid ID="grdHomeProducts" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                DataKeyNames="ProductId" HeaderStyle-CssClass="table_title" GridLines="None"
                Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="商品名称" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <div style="float: left; margin-right: 10px;">
                                <a href='<%#"/vshop/ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank">
                                    <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl40" />
                                </a>
                            </div>
                            <div style="float: left;">
                                <span class="Name"><a href='<%#"/vshop/ProductDetails.aspx?productId="+Eval("ProductId")%>'
                                    target="_blank">
                                    <%# Eval("ProductName") %></a></span> <span class="colorC">商家编码：<%# Eval("ProductCode") %></span></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库存" ItemStyle-Width="10%" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' Width="25"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <Hi:MoneyColumnForAdmin HeaderText=" 市场价" ItemStyle-Width="10%" DataField="MarketPrice"
                        HeaderStyle-CssClass="td_right td_left" />
                    <Hi:MoneyColumnForAdmin HeaderText="一口价" ItemStyle-Width="10%" DataField="SalePrice"
                        HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField HeaderText="排序" ItemStyle-Width="10%" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSequence" runat="server" Text='<%# Eval("DisplaySequence") %>'
                                Width="60px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="td_left td_right_fff">
                        <ItemStyle />
                        <ItemTemplate>
                            <span class="submit_shanchu">
                                <Hi:ImageLinkButton runat="server" ID="Delete" Text="删除" IsShow="true" CommandName="Delete"></Hi:ImageLinkButton></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
        </div>
                
    </div>
</asp:Content>
