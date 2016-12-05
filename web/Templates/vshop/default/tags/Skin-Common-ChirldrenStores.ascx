<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<a class="list-group-item disabled"><%# Eval("UserName")%><span class="pull-right"><%# Eval("DisplayGradeName")%></span></a>
<a class="list-group-item" href="ChirldrenDistributorsDetails.aspx?gradeId=<%# Eval("DistributorGradeId")%>&uid=<%# Eval("ReferralUserId")%>">
    团队总收入：￥<%# Eval("SumAllBalance", "{0:F2}")%><br />
    团队成员：<%# Eval("DistriCnt")%>人
    <span class="glyphicon glyphicon-menu-right pull-right"></span>
</a>