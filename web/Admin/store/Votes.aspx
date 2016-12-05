<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Votes.aspx.cs"  Inherits="Hidistro.UI.Web.Admin.Votes" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="server">    
    <style>    table  tr td object {
        float:left}
    </style>
    <script type="text/javascript" src="/Utility/swfupload/swfobject.js"></script>
    <script type="text/javascript">
        function copySuccess() {
            alert("该投票链接地址已经复制，你可以使用Ctrl+V 粘贴！");
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
            p += 'dialogTop:' + (top-10) + 'px;';
            return showModalDialog(url, paramObj, p);
        }
        function ShowShareLink(ShareUrl) {
            $("#ifmLinkShow").html("<iframe src='" + ShareUrl + "' frameborder='0' style='width: " + ShareUrl.length * 4 + "px; height: 395px;'></iframe>");
            ShowMessageDialog("活动链接", 'sharedetails', 'divShareProduct');
        }
    </script>

  <div class="dataarea mainwidth databody">
    <div class="title"> <em><img src="../images/01.gif" width="32" height="32" /></em>
      <h1>微投票管理 </h1>
     <span>您可以发起一个投票，投票完成后可点击展开查看投票结果。</span></div>
    <!-- 添加按钮-->
    <div class="datalist">
     	<div class="searcharea clearfix br_search">
			<ul><li><a href="AddVote.aspx" class="submit_jia">添加新投票</a></li></ul>
		</div>
    <asp:DataList ID="dlstVote" runat="server" Width="100%" DataKeyField="VoteId">
                <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr class="table_title">
                      <th width="5%" class=" td_right td_left">展开 </td>
                      <th class=" td_right td_left">投票标题 </td>
                      <th width="80" class=" td_right td_left">开始日期 </td>
                      <th width="80" class=" td_right td_left">结束日期 </td>
                      <th class=" td_right td_left" width="100">最多可选项数 </td>
                      <th class=" td_right td_left" width="60">投票总数 </td>
                      <th width="6%" style="display:none" class=" td_right td_left">是否开启</td>
                      <th class=" td_right td_left">关键字</td>
                      <th width="145" class=" td_left td_right_fff">操作</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
              <tr>
                <td><img src="../../utility/pics/plus.gif" onclick="errorEventTable(this);" /></td>
                <td>
                    <asp:Label ID="lblVoteName" runat="server" Text='<%# Eval("VoteName") %>'></asp:Label>
                </td>
                <td>
                    <%# Eval("StartDate","{0:yyyy-MM-dd}")%>
                </td>
                <td>
                     <%# Eval("EndDate","{0:yyyy-MM-dd}")%>
                </td>
                <td><asp:Label ID="lblMaxCheck" runat="server" Text='<%# Eval("MaxCheck") %>' ></asp:Label></td>
                <td><asp:Label ID="lblVoteCounts" runat="server" Text='<%# Eval("VoteCounts") %>' ></asp:Label></td>
                <td  style="display:none">
                    <asp:ImageButton ID="lmgbtnIsBackup" runat="server" CommandName="IsBackup" ImageUrl='<%# (bool)Eval("IsBackup") ? "../../utility/pics/true.gif" : "../../utility/pics/false.gif" %>'></asp:ImageButton>
                </td>
                <td>
                    <span><%# Eval("Keys")%></span>
                </td>
                <td><span class="submit_bianji" style="width: 23px; float: left; padding-top: 6px;line-height: 23px;height: 23px;display:none"><span id="spcopy<%#Container.ItemIndex+1 %>" title="复制"></span></span>
                   <span class="submit_bianji"><a href="javascript:void(0)" onclick="ShowShareLink('../vshop/showactivityurl.aspx?url=<%#Server.UrlEncode(GetUrl(Eval("VoteId"))) %>')">活动链接</a></span>
                    <span class="submit_bianji"><a href='<%# Globals.GetAdminAbsolutePath("/store/EditVote.aspx?VoteId=" + Eval("VoteId"))%>&reurl=<%=LocalUrl %>' style="margin-right:5px;">编辑</a></span>
                   <span class="submit_shanchu"><Hi:ImageLinkButton ID="lkbDelete"  runat="server"  CommandName="Delete" Text="删除" IsShow="true" /></span>
                    <script> bindFlashCopyButton("/Vshop/Vote.aspx?voteId=<%# DataBinder.Eval(Container.DataItem, "VoteId")%>", 'spcopy<%#Container.ItemIndex+1 %>');</script>
                </td>
              </tr>
              <tr style="display: none;" >
                <td colspan="6">
                  <div class="tpiao">                  
                    <asp:GridView ID="grdVoteItem" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="VoteItemId" GridLines="None" HeaderStyle-CssClass="table_title">                                                        
                    <Columns>                                       
		                <asp:TemplateField HeaderText="选项值" ControlStyle-Width="50px" HeaderStyle-CssClass="spanB">
		                    <ItemTemplate>
		                        <%# Eval("VoteItemName") %>
		                    </ItemTemplate>
		                    <EditItemTemplate>
		                        <Hi:HtmlDecodeTextBox ID="txtVoteItemName" runat="server" Text='<%# Eval("VoteItemName") %>' />
		                    </EditItemTemplate>
		                </asp:TemplateField>
		                <asp:TemplateField HeaderText="比例示意图" ControlStyle-Width="150px" HeaderStyle-CssClass="spanB">
                            <ItemTemplate>
                                <div style='width:<%# string.Format("{0}px", Eval("Lenth")) %>'  class="votelenth"/></div>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="百分比" ControlStyle-Width="50px" HeaderStyle-CssClass="spanB">
                            <ItemTemplate>
                                <asp:Label ID="lblPercentage" runat="server" Text='<%# string.Format("{0}%", Eval("Percentage")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField HeaderText="票数" DataField="ItemCount"  ReadOnly="true" ControlStyle-Width="50px" HeaderStyle-CssClass="spanB" />
                    </Columns>
                    </asp:GridView>
                  </div>
                </td>
              </tr>
            </ItemTemplate>           
            
            <FooterTemplate>
                </table>
            </FooterTemplate>
          </asp:DataList>
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
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
<script type="text/javascript" language="javascript">
    var isIE = !!document.all;
    function errorEventTable(tableObject) {
        tableObject.runat = "server";
        //if (isIE) {
        //    var nextNodeObject = tableObject.parentNode.parentNode.nextSibling;
        //}
        //else {
        //    var nextNodeObject = tableObject.parentNode.parentNode.nextSibling.nextSibling;
        //}
        var nextNodeObject = tableObject.parentNode.parentNode.nextSibling.nextSibling;
        (nextNodeObject.style.display == "none") ? nextNodeObject.style.display = "" : nextNodeObject.style.display = "none";
        (nextNodeObject.style.display == "none") ? tableObject.src = '<%= Globals.ApplicationPath + "/utility/pics/plus.gif" %>' : tableObject.src = '<%= Globals.ApplicationPath + "/utility/pics/minus.gif" %>';
    }

   
</script>
</asp:Content>
