<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<tr>
    <td class="c0"><%# Eval("InviteRealName") %></td>
    <td class="c1" colspan="2" style="text-align: left;"><%# Eval("InvitePhone") %></td>
    <td class="c2"><%# Eval("InviteStatusText") %></td>
</tr>
