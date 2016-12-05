<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>


<div class="index-content well">
    <a href="<%# Globals.ApplicationPath + "/Vshop/BusinessArticleDetails.aspx?ArticelId=" + Eval("ArticleId") %>">
        <div class="font18 ui_plr10">
            <%# Eval("Title") %>
        </div>
        <div class="ui_plr10 font-m">
            <%# Eval("AddedDate") %>
        </div><br />
        <div class="ui_plr10">
            <Hi:ListImage ID="ListImage1" runat="server" DataField="IConUrl" />
        </div>
        <div class="info font16">
            <div class="name bcolor"><%# Eval("Summary") %></div>
        </div>
        <div class="ui_plr10">
            <hr style="height:1px;border:none;border-top:1px solid #d3d0d0;" />
        </div>
        <div class="ui_plr10">
            <div style="float:left;">阅读原文</div><div style="float:right;">></div>
        </div><br />
        <div style="height:5px;"></div>
    </a>
</div>



