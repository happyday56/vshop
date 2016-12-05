<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>


<div class="well member-orders-nav" style="margin: 0; margin-top: 10px;">
    <div class="nav-title clearfix">
        <div class="nav-title-left">
            <p><span class="text-right">订单编号:</span><span class=""><%#Eval("OrderId") %></span></p>

            <p><span class="text-right">订单日期:</span><span><%# Eval("OrderDate","{0:d}")%></span></p>
            <p><span class="text-right">订单状态:</span><span class="text-danger"><Hi:OrderStatusLabel ID="OrderStatusLabel1" OrderStatusCode='<%# Eval("OrderStatus") %>' runat="server" /></span></p>
        </div>
        <div class="nav-title-middle">
            <p class="my-order-money">￥<span><%# Eval("OrderTotal","{0:F2}")%></span></p>
        </div>

    </div>


    <hr style="margin: 0 -10px 0 -10px;">
    <asp:Repeater ID="rporderitems" runat="server" DataSource='<%# Eval("OrderItems") %>'>
        <ItemTemplate>
            <div class="member-orders-content">
                <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailsUrl" />
                <div class="info">
                    <a href="<%# Globals.ApplicationPath + "/Vshop/MemberOrderDetails.aspx?OrderId=" + Eval("OrderId") %>">
                        <div class="name bcolor"><%# Eval("ItemDescription")%></div>
                    </a>
                    <%--<div class="order-date">订单日期：<span>2015.7.2</span></div>
        <div class="order-state">订单状态：<span>未发货</span></div>
        <div class="order-state">数量：<span><%# Eval("Quantity") %></span></div>--%>
                    <div class="specification">
                        <input type="hidden" value="<%# Eval("SkuContent")%>" />
                    </div>
                </div>

            </div>
            <hr style="margin: 0 -10px 0 0px;">
        </ItemTemplate>
    </asp:Repeater>
    <div class="member-bottom text-right">
        <a href='<%# Globals.ApplicationPath + "/Vshop/MyLogistics.aspx?OrderId=" + Eval("OrderId") %> '
            class='btn btn-warning btn-xs <%# ((int)Eval("OrderStatus") == 3 || (int)Eval("OrderStatus") == 5) ? "btn btn-warning btn-xs" : "hide"%>'>查看物流</a>
        <a href='javascript:void(0)' onclick='FinishOrder(<%#Eval("OrderId") %>)' class='btn btn-warning btn-xs <%# (int)Eval("OrderStatus") == 3 ? "btn btn-danger btn-xs" : "hide"%>'>确认收货</a>
        <a href='<%# Globals.ApplicationPath + "/Vshop/FinishOrder.aspx?OrderId=" + Eval("OrderId") %> '
            class='btn btn-warning btn-xs <%# (int)Eval("OrderStatus") == 1&&(int)Eval("PaymentTypeId")!=0&&(string)Eval("GateWay")!="hishop.plugins.payment.bankrequest"&&(string)Eval("GateWay")!="hishop.plugins.payment.podrequest"? "btn btn-danger btn-xs" : "hide"%>'>去付款</a>
        <a href='javascript:void(0)' onclick='CloseOrder(<%#Eval("OrderId") %>)'
            class='btn btn-warning btn-xs <%# (int)Eval("OrderStatus") == 1&&(int)Eval("PaymentTypeId")!=0&&(string)Eval("GateWay")!="hishop.plugins.payment.bankrequest"&&(string)Eval("GateWay")!="hishop.plugins.payment.podrequest"? "btn btn-danger btn-xs" : "hide"%>'>关闭订单</a>
        <%--<a href='<%# Globals.ApplicationPath + "/Vshop/FinishOrder.aspx?OrderId=" + Eval("OrderId")+"&onlyHelp=true" %> '
            class='btn btn-warning btn-xs <%# (int)Eval("PaymentTypeId")==99&&(int)Eval("OrderStatus")==1 ? "btn btn-danger btn-xs" : "hide"%>'>线下支付帮助</a>
        <%#(Eval("HasRedPage")).ToString()=="1"?"<a href='/Vshop/GetRedShare.aspx?orderid="+Eval("OrderId")+"' class='btn btn-warning btn-xs btn-danger'>发钱咯</a>":"" %>--%>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        var skuInputs = $('.specification input');
        $.each(skuInputs, function (j, input) {
            var text = '';
            var sku = $(input).val().split(';');
            var changsku = '';
            for (var i = sku.length - 2; i >= 0; i--) {
                changsku += sku[i] + ';';
            }
            $.each(changsku.split(';'), function (i, sku) {
                if ($.trim(sku))
                    text += '<span class="property">' + sku.split('：')[1] + '</span>';
            });
            $(input).parent().html(text);


        });

    });


</script>
