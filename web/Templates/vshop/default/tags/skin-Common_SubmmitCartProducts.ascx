<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<a href="<%# Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?productId=" + Eval("ProductId")%>" class="detailLink">
    <div class="box">
        <div class="left">
        <Hi:ListImage runat="server" DataField="ThumbnailUrl60" />
        </div>
        <div class="right">
            <div class="bcolor name">
                <%# Eval("Name")%></div>
            <div class="specification">
            <input type="hidden" value="<%# Eval("SkuContent")%>" />
            </div>
            <div class="price text-danger">
                ¥<%# Eval("AdjustedPrice", "{0:F2}")%><span class="bcolor"> x
                    <%# Eval("Quantity")%></span></div>
        </div>
    </div>
</a>



        <script>
            $(function () {
                var skuInputs = $('.specification input');
                $.each(skuInputs, function (j, input) {
                    var text = '';
                    $.each($(input).val().split(';'), function (i, sku) {
                        if ($.trim(sku))
                            text += '<span class="badge-h">' + sku.split('：')[1] + '</span>';
                    });
                    $(input).parent().html(text);


                });

            });
        
        
        </script>
