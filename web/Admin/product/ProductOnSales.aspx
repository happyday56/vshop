<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="ProductOnSales.aspx.cs" Inherits="Hidistro.UI.Web.Admin.ProductOnSales" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<%@ Import Namespace="Hidistro.Core" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/03.gif" width="32" height="32" /></em>
            <h1>
                商品管理</h1>
            <span>店铺中所有的商品，您可以对商品进行搜索，也能对商品进行编辑、上架、入库等操作</span>
        </div>
        <div class="datalist">
            <!--搜索-->
            <div class="searcharea clearfix" style="padding: 10px 0px 3px 0px;">
                <ul>
                    <li><span>商品名称：</span><span><asp:TextBox ID="txtSearchText" runat="server" CssClass="forminput" /></span></li>
                    <li>
                        <abbr class="formselect">
                            <Hi:ProductCategoriesDropDownList ID="dropCategories" runat="server" NullToDisplay="--请选择商品分类--"
                                Width="150" />
                        </abbr>
                    </li>
                    <li>
                        <abbr class="formselect">
                            <Hi:BrandCategoriesDropDownList runat="server" ID="dropBrandList" NullToDisplay="--请选择品牌--"
                                Width="153" />
                        </abbr>
                    </li>
                    <li style="display: none;">
                        <abbr class="formselect">
                            <Hi:ProductTagsDropDownList runat="server" ID="dropTagList" NullToDisplay="--请选择标签--"
                                Width="153" />
                        </abbr>
                    </li>
                    <li>
                        <abbr class="formselect">
                            <Hi:ProductTypeDownList ID="dropType" runat="server" NullToDisplay="--请选择类型--" Width="153" />
                        </abbr>
                    </li>
                </ul>
            </div>
            <div class="searcharea clearfix" style="padding: 3px 0px 10px 0px;">
                <ul>
                    <li><span>商家编码：</span><span>
                        <asp:TextBox ID="txtSKU" Width="74" runat="server" CssClass="forminput" /></span></li>
                    <li><span>添加时间：</span></li>
                    <li>
                        <UI:WebCalendar CalendarType="StartDate" ID="calendarStartDate" runat="server" CssClass="forminput" />
                        <span class="Pg_1010">至</span>
                        <UI:WebCalendar ID="calendarEndDate" runat="server" CalendarType="EndDate" CssClass="forminput" />
                    </li>
                    <li>
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="searchbutton" /></li>
                     <li>
                        <asp:LinkButton ID="btnCreateReport" runat="server" Text="导出" />
                        （导出数据一次最多6万条）
                    </li>
                </ul>
            </div>
            <!--结束-->
            <div class="functionHandleArea clearfix">
                <!--分页功能-->
                <div class="pageHandleArea">
                    <ul>
                        <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" />
                        </li>
                    </ul>
                </div>
                <div class="pageNumber">
                    <div class="pagination">
                        <UI:Pager runat="server" ShowTotalPages="false" ID="pager1" />
                    </div>
                </div>
                <!--结束-->
                <div class="blank8 clearfix">
                </div>
                <div class="batchHandleArea">
                    <ul>
                        <li class="batchHandleButton"><span class="signicon"></span><span class="allSelect">
                            <a href="javascript:void(0)" onclick="SelectAll()">全选</a></span> <span class="reverseSelect">
                                <a href="javascript:void(0)" onclick="ReverseSelect()">反选</a></span> <span class="delete">
                                    <Hi:ImageLinkButton ID="btnDelete" runat="server" Text="删除" IsShow="true" DeleteMsg="确定要把商品移入回收站吗？" /></span>
                            <span class="downproduct" style="visibility:hidden;">
                                <asp:HyperLink Target="_blank" runat="server" ID="btnDownTaobao" Text="下载淘宝商品" Visible="false" /></span>
                            <select id="dropBatchOperation">
                                <option value="">批量操作..</option>
                                <option value="1" style="<%=editStatusStyle%>">商品上架</option>
                               <%-- <option value="2">商品下架</option>--%>
                                <option value="3" style="<%=editStatusStyle%>">商品入库</option>
                                <option value="5">设置包邮</option>
                                <option value="6">取消包邮</option>
                                <option value="10">调整基本信息</option>
                                <option value="11">调整显示销售数量</option>
                                <option value="12">调整库存</option>
                                <option value="13">调整会员零售价</option>
                                <%-- <option value="15">调整商品关联标签</option>--%>
                            </select>
                        </li>
                    </ul>
                </div>
                <div class="filterClass">
                    <span><b>出售状态：</b></span> <span class="formselect">
                        <Hi:SaleStatusDropDownList AutoPostBack="true" ID="dropSaleStatus" runat="server" />
                    </span>
                </div>
            </div>
            <!--数据列表区域-->
            <UI:Grid runat="server" ID="grdProducts" Width="100%" AllowSorting="true" ShowOrderIcons="true"
                GridLines="None" DataKeyNames="ProductId" SortOrder="Desc" AutoGenerateColumns="false"
                HeaderStyle-CssClass="table_title" CssClass="goods-list">
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="选择" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("ProductId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="排序" DataField="DisplaySequence" ItemStyle-Width="35px"
                        HeaderStyle-CssClass="td_right td_left" />
                    <asp:TemplateField ItemStyle-Width="45%" HeaderText="商品" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <div style="float: left; margin-right: 10px;">
                                <a href='<%#"../../Vshop/ProductDetails.aspx?productId="+Eval("ProductId")%>' target="_blank">
                                    <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl40" />
                                </a>
                            </div>
                            <div style="float: left;">
                                <span class="Name"><a href='<%#"../../Vshop/ProductDetails.aspx?productId="+Eval("ProductId")%>'
                                    target="_blank">
                                    <%# Eval("ProductName") %></a></span> <span class="colorC" style="display:block">商家编码：<%# Eval("ProductCode") %>
                                        库存：<%# Eval("Stock") %>
                                        成本：<%# Eval("CostPrice", "{0:f2}")%>
                                    </span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="商品价格" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span class="Name">一口价：<%# Eval("SalePrice", "{0:f2}")%>
                                市场价：<asp:Literal ID="litMarketPrice" runat="server" Text='<%#Eval("MarketPrice", "{0:f2}")%>'></asp:Literal></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="80" HeaderStyle-CssClass="td_right td_left">
                        <HeaderTemplate><%=VPName%>使用率</HeaderTemplate>
                        <ItemTemplate>
                            <span>
                                <%#Eval("VirtualPointRate", "{0:f2}")%>%</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品状态" ItemStyle-Width="80" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span>
                                <asp:Literal ID="litSaleStatus" runat="server" Text='<%#Eval("SaleStatus")%>'></asp:Literal></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="首页显示" ItemStyle-Width="80" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span>
                                <asp:Literal ID="litIsDisplayHome" runat="server" Text='<%#Eval("IsDisplayHome")%>'></asp:Literal></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="跨境商品" ItemStyle-Width="80" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span>
                                <asp:Literal ID="litIsCross" runat="server" Text='<%#Eval("IsCross")%>'></asp:Literal>(<%#Eval("MaxCross")%>)</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="95" HeaderStyle-CssClass=" td_left td_right_fff">
                        <ItemTemplate>
                            <span class="submit_bianji"><a href="<%#"EditProduct.aspx?productId="+Eval("ProductId")%>&reurl=<%=LocalUrl %>">
                                编辑</a></span>
                            <span class="submit_bianji"><a href="<%#"EditProductLetter.aspx?productId="+Eval("ProductId")%>&reurl=<%=LocalUrl %>">
                                编辑文案</a></span>
                            <%--<span class="submit_bianji"><a href="javascript:void(0);" onclick="javascript:CollectionProduct('<%# "EditReleteProducts.aspx?productId="+Eval("ProductId")%>')">相关商品</a></span>--%>
                            <span class="submit_shanchu">
                                <Hi:ImageLinkButton ID="btnDel" CommandName="Delete" runat="server" Text="删除" IsShow="true"
                                    DeleteMsg="确定要把商品移入回收站吗？子站的相关商品也将删除！" /></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
            <div class="blank12 clearfix">
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
    <%-- 上架商品--%>
    <div id="divOnSaleProduct" style="display: none;">
        <div class="frame-content">
            <p>
                <em>确定要上架商品？上架后商品将前台出售</em></p>
        </div>
    </div>
    <%-- 下架商品--%>
    <div id="divUnSaleProduct" style="display: none;">
        <div class="frame-content">
            <p>
                <em>确定要下架商品？下架后商品将不在前台出售</em></p>
        </div>
    </div>
    <%-- 入库商品--%>
    <div id="divInStockProduct" style="display: none;">
        <div class="frame-content">
            <p>
                <em>确定要入库商品？入库后商品将不在前台显示</em></p>
        </div>
    </div>
    <%-- 设置包邮--%>
    <div id="divSetFreeShip" style="display: none;">
        <div class="frame-content">
            <p>
                <em>确定要设置这些商品包邮？</em></p>
        </div>
    </div>
        <%-- 取消包邮--%>
    <div id="divCancelFreeShip" style="display: none;">
        <div class="frame-content">
            <p>
                <em>确定要取消这些商品的包邮？</em></p>
        </div>
    </div>
    <%-- 商品标签--%>
    <div id="divTagsProduct" style="display: none;">
        <div class="frame-content">
            <Hi:ProductTagsLiteral ID="litralProductTag" runat="server"></Hi:ProductTagsLiteral>
        </div>
    </div>
    <div style="display: none">
        <asp:Button ID="btnUpdateProductTags" runat="server" Text="调整商品标签" CssClass="submit_DAqueding" />
        <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine"></Hi:TrimTextBox>
        <asp:Button ID="btnInStock" runat="server" Text="入库商品" CssClass="submit_DAqueding" />
        <asp:Button ID="btnUnSale" runat="server" Text="下架商品" CssClass="submit_DAqueding" Visible="false" />
        <asp:Button ID="btnUpSale" runat="server" Text="上架商品" CssClass="submit_DAqueding" />
        <asp:Button ID="btnSetFreeShip" runat="server" Text="设置包邮" CssClass="submit_DAqueding" />
        <asp:Button ID="btnCancelFreeShip" runat="server" Text="取消包邮" CssClass="submit_DAqueding" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="producttag.helper.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dropBatchOperation").bind("change", function () { SelectOperation(); });
        });

        function SelectOperation() {
            var Operation = $("#dropBatchOperation").val();
            var productIds = GetProductId();
            if (productIds.length > 0) {
                switch (Operation) {
                    case "1":
                        formtype = "onsale";
                        arrytext = null;
                        DialogShow("商品上架", "productonsale", "divOnSaleProduct", "ctl00_contentHolder_btnUpSale");
                        break;
                    case "2":
                        formtype = "unsale";
                        arrytext = null;
                        DialogShow("商品下架", "productunsale", "divUnSaleProduct", "ctl00_contentHolder_btnUnSale");
                        break;
                    case "3":
                        formtype = "instock";
                        arrytext = null;
                        DialogShow("商品入库", "productinstock", "divInStockProduct", "ctl00_contentHolder_btnInStock");
                        break;
                    case "5":
                        formtype = "setFreeShip";
                        arrytext = null;
                        DialogShow("设置包邮", "setFreeShip", "divSetFreeShip", "ctl00_contentHolder_btnSetFreeShip");
                        break;
                    case "6":
                        formtype = "cancelFreeShip";
                        arrytext = null;
                        DialogShow("取消包邮", "cancelFreeShip", "divCancelFreeShip", "ctl00_contentHolder_btnCancelFreeShip");
                        break;
                    case "4":
                    case "10":
                        DialogFrame("product/EditBaseInfo.aspx?ProductIds=" + productIds, "调整商品基本信息", null, null);
                        break;
                    case "11":
                        DialogFrame("product/EditSaleCounts.aspx?ProductIds=" + productIds, "调整前台显示的销售数量", null, null);
                        break;
                    case "12":
                        DialogFrame("product/EditStocks.aspx?ProductIds=" + productIds, "调整库存", 880, null);
                        break;
                    case "13":
                        DialogFrame("product/EditMemberPrices.aspx?ProductIds=" + productIds, "调整会员零售价", 1000, null);
                        break;
                    case "15":
                        formtype = "tag";
                        setArryText('ctl00_contentHolder_txtProductTag', "");
                        DialogShow("设置商品标签", "producttag", "divTagsProduct", "ctl00_contentHolder_btnUpdateProductTags");
                        break;
                }
            }
            $("#dropBatchOperation").val("");
        }

        function GetProductId() {
            var v_str = "";

            $("input[type='checkbox'][name='CheckBoxGroup']:checked").each(function (rowIndex, rowItem) {
                v_str += $(rowItem).attr("value") + ",";
            });

            if (v_str.length == 0) {
                alert("请选择商品");
                return "";
            }
            return v_str.substring(0, v_str.length - 1);
        }

        function CollectionProduct(url) {
            DialogFrame("product/" + url, "相关商品");
        }

        function validatorForm() {
            switch (formtype) {
                case "tag":
                    if ($("#ctl00_contentHolder_txtProductTag").val().replace(/\s/g, "") == "") {
                        alert("请选择商品标签");
                        return false;
                    }
                    break;
                case "onsale":
                    setArryText('ctl00_contentHolder_hdPenetrationStatus', $("#ctl00_contentHolder_hdPenetrationStatus").val());
                    break;
                case "unsale":
                    setArryText('ctl00_contentHolder_hdPenetrationStatus', $("#ctl00_contentHolder_hdPenetrationStatus").val());
                    break;
                case "instock":
                case "setFreeShip":
                case "cancelFreeShip":
                    setArryText('ctl00_contentHolder_hdPenetrationStatus', $("#ctl00_contentHolder_hdPenetrationStatus").val());
                    break;
            };
            return true;
        }

    </script>
</asp:Content>
