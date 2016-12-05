<%@ Control Language="C#" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>


<div class="item">
    <div class="flex top">
        <div class="flex_item time"><%# Convert.ToDateTime(Eval("OrderDate")).ToString("yyyy年MM月dd日 HH:mm:ss")%></div>
        <div class="right">
            <Hi:OrderStatusLabel ID="OrderStatusLabel2" OrderStatusCode='<%# Eval("OrderStatus") %>'
                runat="server" />
        </div>
    </div>
    <asp:Repeater ID="rporderitems" runat="server" DataSource='<%# Eval("OrderItems") %>'>
        <ItemTemplate>
            <div class="pro flex">
                <div class="imgwrap">
                    <Hi:ListImage ID="ListImage1" runat="server" DataField="ThumbnailsUrl" />
                </div>
                <div class="de flex_item">
                    <h1><%# Eval("ItemDescription")%></h1>
                    <div class="re"></div>
                    <div class="flex pri">
                        <div class="lef flex_item" style="display:none;">佣金：<%# Eval("ItemsCommission","{0:F2}")%></div>
                        <div class="righ flex_item">¥ <%# Eval("ItemListPrice","{0:F2}")%></div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="bo">
        <p>运费:¥ <%# Eval("AdjustedFreight", "{0:F2}") %></p>
        <p>实付款:<em class="red">¥ <%# Eval("OrderTotal","{0:F2}")%></em></p>
        <p class="red">人民币</p>
    </div>
</div>

