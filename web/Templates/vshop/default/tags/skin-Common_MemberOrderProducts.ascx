<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%--<a href="<%# Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?productId=" + Eval("ProductId")%>">--%>
<a href='<%# Globals.ApplicationPath + "/Vshop/ProductReview.aspx?OrderId="+Request["OrderId"]+"&&ProductId=" + Eval("ProductId") %>'>
    <div class="box">
        <div class="left">
            <Hi:ListImage runat="server" DataField="ThumbnailsUrl" />
        </div>
        <div class="right">
            <div class="name bcolor text-overflow">
                <%# Eval("ItemDescription")%></div>
            <div class="specification">
                <input type="hidden" value="<%# Eval("SkuContent")%>" />
            </div>
            <div class="price text-danger">
                ¥<%# Eval("ItemAdjustedPrice", "{0:F2}")%><span class="bcolor"> x
                    <%# Eval("Quantity")%></span></div>
        </div>
    </div>
</a>

<script type="text/javascript">
    $(function () {
        var skuInputs = $('.specification input');
        $.each(skuInputs, function (j, input) {
            var text = '';
            var sku = $(input).val().split(';');
            var changsku='';
            for (var i = sku.length - 2; i >= 0; i--) {
                changsku += sku[i]+';';
            }
            $.each(changsku.split(';'), function (i, sku) {
                if ($.trim(sku))
                    text += '<span class="property">' + sku.split('：')[1] + '</span>';
            });
            $(input).parent().html(text);


        });

    });
        
        
</script>
