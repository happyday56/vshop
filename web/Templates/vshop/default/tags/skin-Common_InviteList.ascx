<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<tr>
    <td class="c0"><%# Eval("Code") %></td>
    <td class="c1" style="text-align:left;"><%# Eval("ProductName") %></td>
    <td class="c2"><%# Eval("InviteStatusText") %></td>
    <td class="c3">
        <a href='<%# Globals.ApplicationPath + "/Vshop/invite_success.aspx?invitecode=" + Eval("Code")%>' class="btn">邀请</a>
    </td>
</tr>
