<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeFile="SendRedpackRecord.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.SendRedpackRecord" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/04.gif" width="32" height="32" /></em>
            <h1>微信红包提现</h1>
            <span>管理员可以操作将客户的提现申请，以微信红包的形式发放.注意 :<%--每分钟发送红包数量不得超过1800个； --%>北京时间0：00-8：00不触发红包赠送；2. 红包规则:单个红包金额介于[1.00元，200.00元]之间；</span>
        </div><div class="btn">
            <asp:Button ID="btnSendAllRedPack" runat="server" Text="发送全部微信红包" CssClass="inbnt" OnClientClick="return confirm('确认要发送当前所有未发送的微信红包吗？')" OnClick="btnSendAllRedPack_Click" />
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <table>
                <thead>
                    <tr class="table_title">
                        <td>
                            提现金额
                        </td>
                        <td>
                            是否发送
                        </td>
                        <td>
                            发送时间
                        </td>
                        <td>
                            <%=htmlOperatorName %>
                        </td>
                    </tr>
                </thead>
                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    &nbsp;￥<%#(decimal.Parse(Eval("Amount").ToString())/100).ToString("0.00")%>
                                </td>                                
                                <td>&nbsp;<%#Eval("IsSend").ToString()=="False"?"<span style='color:green;text-indent:0;display: inline;'>待发送</span>":"<span style='color:red;text-indent:0;display: inline;'>已发送</span>"%></td>
                                <td><%#Eval("SendTime","{0:yyyy-MM-dd HH:mm}")%>&nbsp;</td>
                                <td width="188">                        
                                        <span class="submit_bianji"><asp:LinkButton ID="lbtnSend" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="send">发送微信红包</asp:LinkButton></span>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="blank12 clearfix">
            </div>
        </div>
        <!--数据列表底部功能区域-->
        <div class="bottomPageNumber clearfix">
            <div class="pageNumber">
                <div class="pagination" style="width: auto">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager" DefaultPageSize="100" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
