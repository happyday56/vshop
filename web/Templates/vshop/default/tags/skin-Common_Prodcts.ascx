<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>

<a href="<%# Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?ProductId=" + Eval("ProductId") %>">
    <div class="index-content well">
        <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailUrl220" />
        <div class="info">
            <div class="name bcolor"><%# Eval("ProductName") %></div>
            <div class="price text-danger">
                ¥<%# Eval("SalePrice", "{0:F2}") %>&nbsp;&nbsp;&nbsp;&nbsp;<del class="wellBala font-xs">¥<%# Eval("MarketPrice", "{0:F2}") %> </del>
                <span class="wellBala">销量&nbsp;&nbsp;<%# Eval("SaleCounts")%></span>
            </div>
        </div>
    </div>
</a>


