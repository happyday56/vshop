<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
    <div>
        <div class="title">
            <%# Eval("Title")%></sapn>
        </div>
        <a href="<%# Globals.ApplicationPath + "/Vshop/Topics.aspx?TopicId=" + Eval("TopicId") %> ">
            <Hi:ListImage runat="server" DataField="IconUrl" />
        </a>
    </div>