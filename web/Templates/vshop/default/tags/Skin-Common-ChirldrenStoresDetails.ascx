<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<a href="ChirldrenDistributorsDetails.aspx?gradeId=<%# Eval("DistributorGradeId")%>&uid=<%# Eval("UserId")%>">
    <div class="row data">
        <div class="col-xs-4"><%# Eval("UserName")%></div>
        <div class="col-xs-4"><%# Eval("DisplayGradeName")%></div>
        <div class="col-xs-4">￥<%# Eval("SumAllBalance", "{0:F2}")%></div>
    </div>
</a>