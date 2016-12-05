<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageActivity.aspx.cs"
    Inherits="Hidistro.UI.Web.Admin.ManageActivity" MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="Server">
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
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/06.gif" width="32" height="32" /></em>
            <h1>
                微报名列表
            </h1>
            <span>您可以在此管理好您的微报名，并在自定义回复中使用它们。</span></div>
        <!-- 添加按钮-->
        <div class="btn">
            <a href="AddActivity.aspx" class="submit_jia">添加微报名</a>
        </div>
        <!--结束-->
        <!--数据列表区域-->
        <div class="datalist">
            <UI:Grid ID="grdActivity" runat="server" AutoGenerateColumns="False" Width="100%"
                DataKeyNames="ActivityId" HeaderStyle-CssClass="table_title" GridLines="None" SortOrderBy="ActivityId"
                SortOrder="DESC">
                <Columns>
                    <asp:TemplateField HeaderText="活动名称" SortExpression="Name">
                        <ItemTemplate>
                            <Hi:SubStringLabel ID="lblCouponName" StrLength="60" StrReplace="..." Field="Name" runat="server"></Hi:SubStringLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Keys" HeaderText="关键字">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="报名人数">
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick="javascript:DialogFrame('vshop/ActivityDetail.aspx?id=<%# Eval("ActivityId") %>', '报名详细', 800, null);"><%#Eval("CurrentValue")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="活动时间">
                        <ItemTemplate>
                            <div style="width: 200px;">
                                <Hi:FormatedTimeLabel ID="lblStartTimes" Time='<%#Eval("StartDate")%>' FormatDateTime="yyyy-MM-dd" runat="server"></Hi:FormatedTimeLabel>
                                至 <Hi:FormatedTimeLabel ID="lblClosingTimes" Time='<%#Eval("EndDate")%>' FormatDateTime="yyyy-MM-dd" runat="server"></Hi:FormatedTimeLabel>    
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" ItemStyle-Width="239">
                        <ItemTemplate>
                            <span class="submit_bianji"><span class="submit_bianji" style="width: 23px; float: left; padding-top: 6px;line-height: 23px;height: 23px;display:none"><span id="spcopy<%#Eval("ActivityId") %>" title="复制"></span></span>
                            <span class="submit_bianji"><a href="javascript:void(0)" onclick="ShowShareLink('../vshop/showactivityurl.aspx?url=<%#Server.UrlEncode(GetUrl(Eval("ActivityId"))) %>')">活动链接</a></span>
                            <span class="submit_bianji"><a href='/admin/vshop/EditActivity.aspx?id=<%# Eval("ActivityId")%>'>编辑</a></span>
                            <span class="submit_bianji"><a href="javascript:void(0);" onclick="javascript:DialogFrame('vshop/ActivityDetail.aspx?id=<%# Eval("ActivityId") %>', '报名详细', 800, null);">查看报名人数</a></span>
                            </span><span class="submit_shanchu">
                                <Hi:ImageLinkButton ID="lkbDelete" runat="server" CommandName="Delete" Text="删除"
                                    OnClientClick="javascript:return confirm('确定要执行删除操作吗？删除后将不可以恢复')"></Hi:ImageLinkButton>
                            </span><script> bindFlashCopyButton("<%# GetUrl(DataBinder.Eval(Container.DataItem, "ActivityID"))%>", 'spcopy<%#Eval("ActivityId") %>');</script>
                               
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>
        </div>
        <div class="blank5 clearfix">
        </div>
        <div class="page">
            <div class="bottomPageNumber clearfix">
                <div class="pageNumber">
                    <div class="pagination">
                        <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                    </div>
                </div>
            </div>
        </div>
        <!--数据列表底部功能区域-->
    </div>
	

    <div id="divShareProduct" style="display: none">
        <div class="frame-content" id="ifmLinkShow">
            <p>
                <span id="SpanShareId"></span>
            </p>

            <table style="width: 300px; height: 340px;">
                <tr>
                    <td>
                        <img id="imgsrc" src="" type="img" width="300px" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
