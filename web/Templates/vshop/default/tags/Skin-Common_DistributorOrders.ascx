<%@ Control Language="C#" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<div class="well member-orders-nav">
<div class="nav-title clearfix">
	<div class="nav-title-left">
	<p><span class="text-right">订单编号：</span><span><%# Eval("OrderId")%></span></p>	
	<p><span class="text-right">下单日期：</span><span><%# Convert.ToDateTime(Eval("OrderDate")).ToString("yyyy-MM-dd")%></span></p>
   
   
   </div>
	<div class="nav-title-middle">
		<p class="text-right"><span class="text-danger"><Hi:OrderStatusLabel ID="OrderStatusLabel1" OrderStatusCode='<%# Eval("OrderStatus") %>'
    runat="server" /></span></p>
      <p><span class="text-right">成交金额：</span><span><%# Eval("OrderTotal","{0:F2}")%></span>元</p>
		<!--<p><span class="text-right">优惠：</span><span><Hi:FormatedMoneyLabel ID="lbladjustsum" runat="server"></Hi:FormatedMoneyLabel> 元</span></p>-->
	</div>
</div>


<hr style="margin:0 -10px 0 -10px;">
<asp:Repeater ID="rporderitems" runat="server" DataSource='<%# Eval("OrderItems") %>'>
<ItemTemplate>
<div class="member-orders-content">
   <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailsUrl" />
    <div class="info">
        <div class="name bcolor"><%# Eval("ItemDescription")%></div>
       <p class="text-muted update-price" style="display:none;"><em>数量:<%# Eval("Quantity")%></em>佣金:<del><%# Eval("ItemsCommission","{0:F2}")%>元</del><span class="text-color"><%# decimal.Parse(Eval("ItemsCommission", "{0:F2}"))-decimal.Parse(Eval("ItemAdjustedCommssion","{0:F2}"))%>元</span><%# Eval("OrderItemsStatus").ToString().Equals("1") ? "<a onclick=\"UpdatePrice(" + Eval("ItemsCommission", "{0:F2}") + "," + Eval("OrderId") + ",'"+Eval("SkuId")+"')\">改价</a>" : ""%>
       </p>
    
 
    </div>
 </div>
     <hr style="margin:0 -10px 0 0px;">
</ItemTemplate>
</asp:Repeater>
    <div class="member-bottom text-right"><a href='<%# Hidistro.Core.Globals.ApplicationPath + "/Vshop/MyLogistics.aspx?OrderId=" + Eval("OrderId") %> '
            class='btn btn-warning btn-xs <%# ((int)Eval("OrderStatus") == 3 || (int)Eval("OrderStatus") == 5) ? "btn btn-warning btn-xs" : "hide"%>'>查看物流</a></div>
 <p class="text-right" style="padding:10px 0;"><span class="text-right">订单总收益：</span><span><asp:Literal ID="litCommission" runat="server" Text='<%# Eval("FirstCommission", "{0:F2}") %>'></asp:Literal></span>元</p>		
</div>
