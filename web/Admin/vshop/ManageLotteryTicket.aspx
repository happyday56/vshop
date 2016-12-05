<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageLotteryTicket.aspx.cs" Inherits="Hidistro.UI.Web.Admin.ManageLotteryActivity" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <style>    table  tr td object {
        float:left}
        .dataarea .datalist table span {text-indent: 0px;
        }
    </style>
    <script type="text/javascript" src="/Utility/swfupload/swfobject.js"></script>
    <script type="text/javascript">
        function copySuccess() {
            alert("该活动链接地址已经复制，你可以使用Ctrl+V 粘贴！");
        }
        var myHerf = window.location.host;
        var myproto = window.location.protocol;
        function bindFlashCopyButton(value, containerID) {
            var flashvars = {
                content: encodeURIComponent(myproto + "//" + myHerf + applicationPath + value),
                uri: '/Utility/swfupload/flash_copy_btn.png'
            };
            var params = {
                wmode: "transparent",
                allowScriptAccess: "always"
            };
            swfobject.embedSWF("/Utility/swfupload/clipboard.swf", containerID, "23", "12", "9.0.0", null, flashvars, params);
        }


        function openShowModalDialog(url, param, whparam, e) {

            // 传递至子窗口的参数
            var paramObj = param || {};

            // 模态窗口高度和宽度
            var whparamObj = whparam || { width: 500, height: 500 };

            // 相对于浏览器的居中位置
            var bleft = ($(window).width() - whparamObj.width) / 2;
            var btop = ($(window).height() - whparamObj.height) / 2;

            // 根据鼠标点击位置算出绝对位置
            var tleft = e.screenX - e.clientX;
            var ttop = e.screenY - e.clientY;

            // 最终模态窗口的位置
            var left = bleft + tleft;
            var top = btop + ttop;
            if (top > 50) { top -= 50 };
            // 参数
            var p = "help:no;status:no;center:yes;";
            p += 'dialogWidth:' + (whparamObj.width) + 'px;';
            p += 'dialogHeight:' + (whparamObj.height) + 'px;';
            p += 'dialogLeft:' + left + 'px;';
            p += 'dialogTop:' + (top - 10) + 'px;';
            return showModalDialog(url, paramObj, p);
        }
        function ShowShareLink(ShareUrl) {
            $("#ifmLinkShow").html("<iframe src='" + ShareUrl + "' frameborder='0' style='width: " + ShareUrl.length * 4 + "px; height: 395px;'></iframe>");
            ShowMessageDialog("活动链接", 'sharedetails', 'divShareProduct');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="dataarea mainwidth databody">
    <div class="title"> <em><img src="../images/01.gif" width="32" height="32" /></em>
      <h1>微抽奖管理</h1>
        
     <span>您可以在此管理好您的微抽奖，并在自定义回复中使用它们。</span></div>
     <div class="btn">
            <a href="AddLotteryTicket.aspx" class="submit_jia">添加微抽奖</a>
        </div>
    <!-- 添加按钮-->
    <!--结束-->
    <!--数据列表区域-->
    <div class="datalist">
          <asp:Repeater ID="rpMaterial" runat="server" 
            onitemcommand="rpMaterial_ItemCommand" >
                    <HeaderTemplate>
                        <table border="0" cellspacing="0" width="80%" cellpadding="0">
                            <tr class="table_title">
                                <td>
                                  活动名称
                                </td>
                                <td>
                                  活动关键字
                                </td>
                                <td>活动开始时间</td>
                                <td>抽奖开始时间</td>
                                  <td>活动结束时间
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                              <%#Eval("ActivityName")%>
                            </td>
                             <td>
                              <%#Eval("ActivityKey")%>
                            </td>
                            <td><%#Eval("StartTime","{0:yyyy-MM-dd HH:mm}")%></td>
                            <td><%#Eval("OpenTime","{0:yyyy-MM-dd HH:mm}")%>&nbsp;</td>
                            <td>
                             <%#Eval("EndTime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                          <td style="width:302px"><span class="submit_bianji" style="width: 23px; float: left; padding-top: 6px;line-height: 23px;height: 23px;display:none"><span id="spcopy<%#Container.ItemIndex+1 %>" title="复制"></span></span>
                              <span class="submit_bianji"><a href="javascript:void(0)" onclick="ShowShareLink('../vshop/showactivityurl.aspx?url=<%#Server.UrlEncode(GetUrl(Eval("ActivityId"))) %>')">活动链接</a></span>
                              <span class="submit_bianji"><a href='EditLotteryTicket.aspx?id=<%#Eval("ActivityID")%>'>编辑</a></span>
                              <span class="submit_bianji"><asp:LinkButton ID="Lkbtndel" CommandName="del" CommandArgument='<%#Eval("ActivityID")%>' runat="server" OnClientClick="return confirm('确定要执行删除操作吗？删除后将不可以恢复')">删除</asp:LinkButton></span>
                              <span class="submit_bianji"><asp:LinkButton ID="LkStart" CommandName="start" CommandArgument='<%#Eval("ActivityID")%>' runat="server" Visible='<%# Convert.ToDateTime(Eval("StartTime"))<DateTime.Now &&Eval("OpenTime")!=DBNull.Value&&Convert.ToDateTime(Eval("OpenTime"))>DateTime.Now &&  Convert.ToDateTime(Eval("EndTime"))>DateTime.Now  %>' >立即开始</asp:LinkButton></span>
                              <span class="submit_bianji"><a href='PrizeRecord.aspx?id=<%#Eval("ActivityID")%>'>查看中奖信息</a> 
                           <script> bindFlashCopyButton("<%# GetUrl(DataBinder.Eval(Container.DataItem, "ActivityID"))%>", 'spcopy<%#Container.ItemIndex+1 %>');</script>
                               </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>        
                
    </div>
    <!--数据列表底部功能区域-->

      <div class="page">
            <div class="page">
                <div class="bottomPageNumber clearfix">
                    <div class="pageNumber">
                        <div class="pagination">
                            <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
  </div>

    <div id="divShareProduct" style="display: none">
        <div class="frame-content" id="ifmLinkShow">
            <p><span id='SpanShareId'></span></p><table style='width: 300px; height: 340px;'><tr><td>&nbsp;</td></tr></table>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>
