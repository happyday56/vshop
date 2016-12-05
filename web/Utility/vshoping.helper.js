$(document).ready(function () {
    $("input[name='inputQuantity']").bind("blur", function () { chageQuantity(this); }); //立即购买
    $("[name='iDelete']").bind("click", function () {
        var obj = this;
        myConfirm('询问', '确定要从购物车里删除该商品吗？', '确认删除', function () {
            deleteCartProduct(obj);
        });
    }); //立即购买
    $("#selectShippingType").bind("change", function () { chageShippingType() });
    $("#selectCoupon").bind("change", function () { chageCoupon() });
    $("#aSubmmitorder").bind("click", function () { submmitorder() });
});

function deleteCartProduct(obj) {
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "DeleteCartProduct", skuId: $(obj).attr("skuId") },
        success: function (resultData) {
            if (resultData.Status == "OK") {
                location.href = "/Vshop/ShoppingCart.aspx";
            }
        }
    });
}

function chageQuantity(obj) {
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "ChageQuantity", skuId: $(obj).attr("skuId"), quantity: parseInt($(obj).val()) },
        success: function (resultData) {
            if (resultData.Status == "OK") {
                location.href = "/Vshop/ShoppingCart.aspx";
            }
            else {
                alert_h("最多只可购买" + resultData.Status + "件", function () {
                    location.href = "/Vshop/ShoppingCart.aspx";
                });

            }
        }
    });
}

function chageShippingType() {
    var freight = 0;
    if ($("#selectShippingType").val() != "-1") {
        var selectedShippingType = $("#selectShippingType option:selected").text();
        freight = parseFloat(selectedShippingType.substring(selectedShippingType.lastIndexOf("￥") + 1));
    }

    var discountValue = 0;
    if ($("#selectCoupon").val() != undefined && $("#selectCoupon").val() != "") {
        var selectCoupon = $("#selectCoupon option:selected").text();
        discountValue = parseFloat(selectCoupon.substring(selectCoupon.lastIndexOf("￥") + 1));
    }

    var orderTotal = parseFloat($("#vSubmmitOrder_hiddenCartTotal").val()) + freight - discountValue;
    $("#strongTotal").html("¥" + orderTotal.toFixed(2));
}

function chageCoupon() {
    var freight = 0;
    if ($("#selectShippingType").val() != "-1") {
        var selectedShippingType = $("#selectShippingType option:selected").text();
        freight = parseFloat(selectedShippingType.substring(selectedShippingType.lastIndexOf("￥") + 1));
    }

    var discountValue = 0;
    if ($("#selectCoupon").val() != "") {
        var selectCoupon = $("#selectCoupon option:selected").text();
        discountValue = parseFloat(selectCoupon.substring(selectCoupon.lastIndexOf("￥") + 1));
    }

    var orderTotal = parseFloat($("#vSubmmitOrder_hiddenCartTotal").val()) + freight - discountValue;
    $("#strongTotal").html("¥" + orderTotal.toFixed(2));
}

function submmitorder() {
    if (!$("#selectShippingType").val()) {
        alert_h("请选择配送方式");
        return false;
    }
    if (!$("#selectPaymentType").val()) {
        alert_h("请选择支付方式");
        return false;
    }

    if (!$('#selectShipToDate').val()) {
        alert_h("请选择送货上门时间");
        return false;
    }
    
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: {
            action: "Submmitorder",
            shippingType: $("#selectShippingType").val(),
            paymentType: $("#selectPaymentType").val(),
            couponCode: $("#selectCoupon").val(),
            redpagerid: $("#selectRedPager").val(),
            shippingId: $('#selectShipTo').val(),
            productSku: getParam("productSku"),
            buyAmount: getParam("buyAmount"),
            from: getParam("from"),
            shiptoDate: $("#selectShipToDate").val(),
            groupbuyId: $('#groupbuyHiddenBox').val(),
            remark: $('#remark').val(),
            idcard: $('#idcard').val(),
            points: $("#ckbVirtualPoints").attr("data-value")
        },
        success: function (resultData) {
            if (resultData.Status == "OK") {
                location.href = "/Vshop/FinishOrder.aspx?orderId=" + resultData.OrderId;
            }
            else if (resultData.ErrorMsg) {
                alert_h(resultData.ErrorMsg);
            }
        }
    });
}
