<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<div class="col-xs-6">
	<%
	    string plink=Globals.GetCurrentDistributorId()>0?"&&ReferralId="+Globals.GetCurrentDistributorId():"";
	  %>
	<a href='<%# Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?ProductId=" + Eval("ProductId")%><%=plink %>'>
		<div class="index-content well">
		
            <img data-original="<%#Eval("ThumbnailUrl310").ToString().Length>5?Eval("ThumbnailUrl310").ToString():"/utility/pics/none.gif" %>" src="/Utility/pics/lazy-ico.gif"/>
		    <div class="info">
		        <div class="title">
		            <%# Eval("ProductName") %>
		        </div>
		        <div class="price text-danger">
		            ¥<%# Eval("SalePrice", "{0:F2}") %><span>销量&nbsp;&nbsp;<%# Eval("ShowSaleCounts")%></span>
		        </div>
                <div class="price">
                    <s>¥<%# Eval("MarketPrice", "{0:F2}") %></s><span>评价&nbsp;&nbsp;<%# Eval("ReviewsCount")%></span>
                </div>
		    </div>
            <asp:Literal ID="litpromotion" runat="server"></asp:Literal> 
		</div>
	</a>
</div>