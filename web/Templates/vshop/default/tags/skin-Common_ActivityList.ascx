<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>

<a class="list-group-item" href="ActivitieDetails.aspx?id=<%# Eval("ActivitiesId") %>">
    <button data-activityid="<%# Eval("ActivitiesId") %>" type="button" class="btn <%# (int)Eval("IsFocus") > 0 ? "btn-defaul" : "btn-success" %> btn-sm pull-right"><%# (int)Eval("IsFocus") > 0 ? "参与中" : "我要参与" %></button>
    <span class="title"><%# Eval("ActivitiesName") %></span><span class="validdate"><%# Eval("StartTime","{0:yyyy/MM/dd}") %> 至 <%# Eval("EndTIme","{0:yyyy/MM/dd}") %></span>
    <p class="details">满足金额:<i>￥<%# Eval("MeetMoney","{0:F2}") %></i> 减免金额 :<i>￥<%# Eval("ReductionMoney","{0:F2}") %></i></p>
</a>
