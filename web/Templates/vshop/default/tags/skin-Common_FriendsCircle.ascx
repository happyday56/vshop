<%@ Control Language="C#" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<div class="pbox firends-circle clearfix well">
<div>
  <div class="goods">
    <div><p><%#Eval("ExensiontRemark")%></p></div>
  </div>
  <div class="content clearfix row wechart-img"><asp:Literal runat="server" ID="ImgPic" /></div>
</div>
<div class="font-s text-muted"><%#Eval("CreateTime")%></div>
</div>