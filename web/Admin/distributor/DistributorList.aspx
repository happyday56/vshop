<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="DistributorList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorList" %>

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
            <h1>分销商列表</h1>
            <span>对分销商进行管理，您可以查询分销商的佣金和详细信息。</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>店铺名：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>联系人：</span> <span>
                        <asp:TextBox ID="txtRealName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>手机号码：</span> <span>
                        <asp:TextBox ID="txtCellPhone" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>微信号：</span> <span>
                        <asp:TextBox ID="txtMicroSignal" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>分销等级：</span>

                        <abbr class="formselect">
                            <Hi:DistributorGradeDropDownList ID="DrGrade" runat="server" AllowNull="true" NullToDisplay="全部" />
                        </abbr>
                        </span></li>
                    <li><span>分销商状态：</span> <span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrStatus" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">正常</asp:ListItem>
                                <asp:ListItem Value="2">已冻结</asp:ListItem>
                            </asp:DropDownList></abbr></span></li>
                    <li>
                    <li><span>续费状态：</span> <span>
                        <abbr class="formselect">
                            <asp:DropDownList ID="DrDeadlineStatus" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">正常</asp:ListItem>
                                <asp:ListItem Value="2">续费</asp:ListItem>
                                <asp:ListItem Value="3">过期</asp:ListItem>
                            </asp:DropDownList></abbr></span></li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>

                </ul>
            </div>
            <div class="functionHandleArea m_none">
                <!--分页功能-->
                <div class="pageHandleArea" style="float: left;">
                    <ul>
                        <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" />
                        </li>
                    </ul>
                </div>
                <div class="blank8 clearfix"></div>
                <div class="batchHandleArea">
                    <ul>
                        <li class="batchHandleButton selectButton">
                            <span class="signicon"></span><span class="allSelect"><a href="javascript:void(0);" onclick="SelectAll2()">全选</a></span>
                            <span class="reverseSelect"><a href="javascript:void(0);" onclick="ReverseSelect2()">反选</a></span>
                            <span id="Span1" class="sendsite" runat="server"><a href="javascript:void(0);" onclick="return SendMessage()">短信群发</a></span>
                        </li>
                    </ul>
                </div>
            </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>选择</td>
                        <td>头像</td>
                        <td>店铺名
                        </td>
                        <td>分销等级
                        </td>
                        <td>分销商状态
                        </td>
                        <td>联系人</td>
                        <td>手机号码</td>
                        <%--<td>QQ</td>--%>
                        <td>微信昵称</td>
                        <td>开店时间</td>
                        <td>截止时间</td>
                        <td>剩余天数</td>
                        <td>续费状态</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reDistributor"  OnItemCommand="reDistributor_ItemCommand"  OnItemDataBound="reDistributor_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><input name="CheckBoxGroup" id="CheckBoxGroup" type="checkbox" value='<%#Eval("UserId") %>' runat="server" /></td>
                                <td>&nbsp;<%# Eval("UserHead").ToString()!=""?"<img  src='"+Eval("UserHead")+"' width=\"50\" height=\"50\"/>":""%>
                                </td>
                                <td>&nbsp; <%# Eval("StoreName")%>
                                </td>
                                <td>&nbsp; <%# Eval("Name")%> 
                                </td>
                                <td>&nbsp; <%# Eval("ReferralStatus").ToString().Trim()=="2" ? "<p style=\"color:red;\">已冻结</p>" : "正常"%>
                                </td>
                                <td>&nbsp; <%# Eval("RealName")%>
                                </td>

                                <td><asp:Literal ID="litCellPhone" runat="server" Text='<%#Eval("CellPhone")%>'></asp:Literal></td>
                                <%--<td>&nbsp;<%# Eval("QQ")%>
                                </td>--%>
                                <td>&nbsp;<%# Eval("MicroSignal")%>
                                </td>
                                <td>&nbsp;<%# Eval("CreateTime","{0:d}")%>
                                </td>
                                <td>&nbsp;<%# Eval("DeadlineTime","{0:d}")%>
                                </td>
                                <td>&nbsp;<%# Eval("DiffDay")%>
                                </td>
                                <td>&nbsp;<%# Eval("DeadlineStatus").ToString().Trim() == "1" ? Eval("DeadlineStatusName") : (Eval("DeadlineStatus").ToString().Trim() == "2" ? "<p style=\"color:red;\">" + Eval("DeadlineStatusName") + "</p>" : "<p style=\"color:green;\">" + Eval("DeadlineStatusName") + "</p>" ) %>
                                </td>
                                <td>&nbsp;
                                 <span class="submit_bianji">
                                     <asp:HyperLink ID="lkbView" runat="server" Text="编辑" NavigateUrl='<%# "EditDistributor.aspx?UserId="+Eval("UserId")%>'></asp:HyperLink></span>
                                    <span class="submit_bianji">
                                        <asp:HyperLink ID="lkbView1" runat="server" Text="详细" NavigateUrl='<%# "DistributorDetails.aspx?UserId="+Eval("UserId")%>'></asp:HyperLink></span>
                                    <span class="submit_bianji">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="佣金明细" NavigateUrl='<%# "CommissionsList.aspx?UserId="+Eval("UserId")%>'></asp:HyperLink></span>
                                    <br />
                                    <span class="submit_bianji">
                                        <Hi:ImageLinkButton ID="btnFrozen" CommandName="Frozen" CommandArgument='<%# Eval("UserId")%>' runat="server" Text="冻结" IsShow="true"
                                            DeleteMsg="确定要冻结分销商！" />
                                    </span>
                                    <span class="submit_bianji">
                                        <Hi:ImageLinkButton ID="btnOneYear" CommandName="OneYear" CommandArgument='<%# Eval("UserId")%>' runat="server" Text="增加1年" IsShow="true"
                                            DeleteMsg="确定要增加1年使用时间！" />
                                    </span>
                                    <span class="submit_bianji">
                                        <Hi:ImageLinkButton ID="btnTwoYear" CommandName="TwoYear" CommandArgument='<%# Eval("UserId")%>' runat="server" Text="增加2年" IsShow="true"
                                            DeleteMsg="确定要增加2年使用时间！" />
                                    </span>
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
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                </div>
            </div>
        </div>
    </div>
      <!--会员短信群发-->
    <div id="div_sendmsg" style="display: none;">
        <p>短信群发</p>
        <p>
            <h4>发送对象(共<font style="color: Red">0</font>个会员)</h4>
        </p>
        <div id="send_member" style="overflow-x: hidden; overflow-y: auto; margin-bottom: 20px">
            <ul class="menber"></ul>
        </div>
        <p>
            <textarea id="txtmsgcontent" runat="server" style="width: 750px; height: 240px;" class="forminput" value="输入发送内容……" onfocus="javascript:addfocus(this);" onblur="javascript:addblur(this);"></textarea></p>
    </div>
    <div style="display: none">
        <input type="hidden" id="hdenablemsg" runat="server" value="0" />
        <asp:Button ID="btnSendMessage" runat="server" Text="短信群发" CssClass="submit_DAqueding" />
    </div>
    <script type="text/javascript">

        $(function () {

        });

        //样式控制
        function showcss(divobj, rownumber) {
            if (rownumber > 12) {
                $("#" + divobj).css("height", 100);
            }
        }

        //短信群发
        function SendMessage() {
            if ($("#ctl00_contentHolder_hdenablemsg").val().replace(/\s/g, "") == "0") {
                alert("您还未进行短信配置，无法发送短信");
                return false;
            }
            var v_str = 0;
            var regphone = "^0?(13|15|18|14|17)[0-9]{9}$";
            var html_str = "";
            $("#div_sendmsg .menber").html('');
            $("#div_sendmsg h4 font").text('0');
            $(".datalist input[type='checkbox']:checked").each(function (rowIndex, rowItem) {
                var realname = $(this).parent("td").parent("tr").find("td:eq(5)").text().replace(/\s/g, "");
                var cellphone = $(this).parent("td").parent("tr").find("td:eq(6)").text().replace(/\s/g, "").replace("　", "");
                var cellphone = $(this).parent("td").parent("tr").find("td:eq(6)").text().replace(/\s/g, "").replace(/\s+/g, "").replace(" ", "");
                if (cellphone.length > 11) cellphone = cellphone.substring(0, 11);
                var IsCellphone = new RegExp(regphone).test(cellphone);
                if (cellphone != "" && cellphone != "undefined" && IsCellphone) {
                    html_str = html_str + "<li>" + realname + "(" + cellphone + ")</li>";
                    v_str++;
                } else {
                    $(this).attr("checked", false);
                }
            });
            if (html_str == "") {
                alert("请先选择要发送的对象或检查手机号格式是否正确！");
                return false;
            }
            $("#div_sendmsg .menber").html(html_str);
            $("#div_sendmsg h4 font").text(v_str);
            arrytext = null;
            formtype = "sendmsg";
            DialogShow("会员短信群发", 'sendmsg', 'div_sendmsg', 'ctl00_contentHolder_btnSendMessage');
            art.dialog.list['sendmsg'].size('50%', '50%');
            showcss("send_member", v_str);
        }

        function addfocus(obj) {
            if (obj.value.replace(/\s/g, "") == "输入发送内容……") {
                obj.value = "";
            }
        }

        function addblur(obj) {
            if (obj.value.replace(/\s/g, "") == "") {
                obj.value = "输入发送内容……";
            }
        }

        //检验群发信息条件
        function CheckSendMessage() {
            var sendcount = $("#div_sendmsg h4 font").text(); //获取发送对象数量
            var smscount = $("#div_sendmsg h1 font").text(); //获取剩余短信条数
            if (parseInt(sendcount) > parseInt(smscount)) {
                alert("您剩余短信条数不足，请先充值");
                return false;
            }
            if ($("#ctl00_contentHolder_txtmsgcontent").val().replace(/\s/g, "") == "" || $("#ctl00_contentHolder_txtmsgcontent").val().replace(/\s/g, "") == "输入发送内容……") {
                alert("请先输入要发送的信息内容！");
                return false;
            }
            setArryText("ctl00_contentHolder_txtmsgcontent", $("#ctl00_contentHolder_txtmsgcontent").val().replace(/\s/g, ""));
            return true;

        }

        //验证
        function validatorForm() {
            switch (formtype) {
                case "sendemail":
                    return CheckSendEmail();
                    break;
                case "sendmsg":
                    return CheckSendMessage();
                    break;
            };
            return true;
        }

    </script>
</asp:Content>
