<%@ Control Language="C#"%>
<%@ Import Namespace="Hidistro.Core" %>
<li>
    <a href='<%# Globals.ApplicationPath + "/Vshop/ProductList.aspx?categoryId=" + Eval("CategoryId") %> '>
        
        <%# Eval("Name") %>
    </a>
</li>