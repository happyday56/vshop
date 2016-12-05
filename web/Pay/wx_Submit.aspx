<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wx_Submit.aspx.cs" Inherits="Hidistro.UI.Web.Pay.wx_Submit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script src="http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js"></script>
    
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.invoke('getBrandWCPayRequest', <%= pay_json %>, function(res){
            if(res.err_msg == "get_brand_wcpay_request:ok" ) {
                var orderId = <%= orderId %>;
                var isPromotionBuy = <%= isPromotionBuy %>;
                if(isPromotionBuy == "1"){
                    location.href = "/vshop/DistributorCenter.aspx";
                }else{
                    alert("订单支付成功!点击确认进入我的订单中心");
                    SubmitCalCommission(orderId);
                    //location.href = "/vshop/MemberCenter.aspx?status=3";
                }
            }
            else
            {
                alert("支付取消或者失败");
                location.href = "/vshop/MemberCenter.aspx?status=1";
            }
        });
    });

    function SubmitCalCommission(orderId) {
        
        $.ajax({
            url: "/API/VshopProcess.ashx",
            type: 'POST', dataType: 'json', timeout: 10000,
            data: { action: "SubmitCalCommission", Params: orderId },
            async: false,
            success: function (resultData) {
                location.href = "/vshop/MemberCenter.aspx?status=3";
            }
        });
    }

</script>
</body>
</html>
