<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<div class="col-xs-6">
    <div class="index-content well">
        <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl310" />
        <div class="content-right">
            <div>
                <a href='<%# Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?ProductId=" + Eval("ProductId") %>'>
                    <%# Eval("ProductName") %></a>
            </div>

            <%--<div class="price text-danger">
            ¥<%# Eval("SalePrice", "{0:F2}") %><span class="text-muted">已售<%# Eval("SaleCounts")%>件</span></div>--%>
            <div class="price text-danger">
                售价&nbsp;¥<%# Eval("SalePrice", "{0:F2}") %>
                <span>销量&nbsp;<%# Eval("SaleCounts")%></span><br />
            </div>
            <div class="price">
                佣金&nbsp;¥<%# Eval("CommissionPrice","{0:F2}")%>
                <span>库存&nbsp;<%# Eval("Stock")%></span>
            </div>
        </div>
    </div>
    <div class="right">
        <input type="checkbox" name="DistributorCheckGroup" id='CheckGroup<%#Eval("ProductId") %>' value='<%# Eval("ProductId") %>' /></div>
    <div class="btn btn-info btn-block"><a href='<%# Globals.ApplicationPath + "/Vshop/PLDetails.aspx?ProductId=" + Eval("ProductId") %>'>商品文案</a></div>

</div>
