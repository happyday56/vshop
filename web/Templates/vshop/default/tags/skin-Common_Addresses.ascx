<%@ Control Language="C#"%>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Import Namespace="Hidistro.Entities" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>

<li><a href="#" shippingId="<%# Eval("ShippingId")%>" name="<%# Eval("RegionId")%>" briefAddress="<%# Eval("ShipTo")%> &nbsp;<%# Eval("CellPhone")%> &nbsp; <%# Eval("Address")%>" > 

<%#Eval("ShipTo")+" "+ Eval("CellPhone")+" "+RegionHelper.GetFullRegion((int)Eval("RegionId")," ")+" "+ Eval("Address") %>
</a>

</li>
