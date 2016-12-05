<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="BalanceDrawApplyList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.BalanceDrawApplyList" %>

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
            <h1>
                提现申请列表</h1>
            <span>显示未发送的分销商提现申请记录，其中使用微信红包提现步骤为：首先点击“生成微信红包”链接生成待发送的微信红包，然后在发送记录页面逐个红包发送。</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>店铺名：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>申请日期：</span> <span>
                        <UI:WebCalendar runat="server" CssClass="forminput1" ID="txtRequestStartTime" Width="100" />-</span>
                        <span>
                            <UI:WebCalendar runat="server" CssClass="forminput1" ID="txtRequestEndTime" Width="100" />
                        </span></li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>
                    <li>
                        <asp:LinkButton ID="btnCreateReport" runat="server" Text="导出提现佣金" />
                    </li>
                    <li>
                        <asp:LinkButton ID="btnBatchApply" runat="server" Text="批量线下打款" />
                    </li>
                </ul>
            </div>
            <div class="blank8 clearfix">
                </div>
                <div class="batchHandleArea">
                    <ul>
                        <li class="batchHandleButton"><span class="signicon"></span><span class="allSelect">
                            <a href="javascript:void(0)" onclick="SelectAll()">全选</a></span> <span class="reverseSelect">
                                <a href="javascript:void(0)" onclick=" ReverseSelect()">反选</a></span></li>
                    </ul>
                </div>
            <table>
                <thead>
                    <tr class="table_title">
                        <td>
                            选择
                        </td>                        
                        <td>
                            分销商ID
                        </td>
                        <td>
                            分销商店铺
                        </td>
                        <td>
                            申请提现金额
                        </td>
                        <td>
                            可提现金额
                        </td>
                        <td>
                            申请日期
                        </td>
                          <td>
                            手机号码
                        </td>
                          <td>
                            收款方式
                        </td>
                        <td>收款人帐号</td>
                        <td>收款人姓名</td>
                        <td>收款人银行</td>
                        <td>收款人开户行</td>
                        <td>收款人开户地</td>
                        <td>
                            操作
                        </td>
                    </tr>
                </thead>
                <asp:Repeater ID="reBalanceDrawRequest" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("SerialID") %>' />
                                </td>
                                <td><%# Eval("UserId")%>&nbsp;</td>
                                <td><%# Eval("StoreName")%>&nbsp;</td>
                                <td>￥<%# Eval("Amount", "{0:F2}")%>&nbsp;</td>
                                <td>￥<%# Eval("ReferralBlance", "{0:F2}")%>&nbsp;</td>
                                <td width="100"><%# Eval("RequestTime", "{0:yyyy-MM-dd}")%>&nbsp;</td>
                                 <td width="100"><%# Eval("CellPhone")%>&nbsp;
                                </td>
                                 <td width="100"><%# Eval("RequestType").ToString()=="1"?"银行账号":"微信红包"%>&nbsp;
                                </td>
                                <td><%# Eval("MerchantCode")%></td>
                                <td><%# Eval("AccountName")%></td>
                                <td><%# Eval("BankName")%></td>
                                <td><%# Eval("BankAddress")%></td>
                                <td><%# Eval("RegionAddress")%></td>
                                <td width="100">
                                    <span class="submit_bianji"><a <%#Eval("RequestType").ToString()=="1"?" style='color:red;cursor:pointer;' onclick=\"Apply("+ Eval("SerialID")+","+Eval("Amount")+","+Eval("ReferralBlance")+",'"+Eval("MerchantCode")+"','"+Eval("AccountName")+"','"+Eval("BankName")+"','"+Eval("BankAddress")+"',"+Eval("UserId")+","+Eval("RedpackRecordNum")+")\"":" style='cursor:pointer' onclick=\"if(confirm('客户选择的是【微信红包】提现，您确定要选择线下打款操作吗？')){Apply("+ Eval("SerialID")+","+Eval("Amount")+","+Eval("ReferralBlance")+",'"+Eval("MerchantCode")+"','"+Eval("AccountName")+"','"+Eval("BankName")+"','"+Eval("BankAddress")+"',"+Eval("UserId")+","+Eval("RedpackRecordNum")+")}\""%>>
                                        线下打款</a></span><br /><span class="submit_bianji"><asp:LinkButton ID="lkBtnSendRedPack" runat="server" CommandName="sendredpack" CommandArgument='<%#Eval("SerialID") %>'>生成微信红包</asp:LinkButton></span>
                                    <br /><span class="submit_bianji"><asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%#Eval("SerialID") %>' CommandName="del" OnClientClick="return deleteConfim();">删除</asp:LinkButton> </span>
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
     <input type="hidden" id="hdapplyid" runat="server" />
     <input type="hidden" id="hduserid" runat="server" />
     <input type="hidden" id="hdreferralblance" runat="server" />
     <input type="hidden" id="hdredpackrecordnum" runat="server" />
    <div style="display: none">
       
       <asp:Button ID="btapply" runat="server" Text="Button"  />
    </div>
    <div id="div_apply" style="display: none;">
        <div class="frame-content">
            <table>
                <tr>
                    <td>
                        收款人帐号：
                    </td>
                    <td>
                        <span id="AccountNumber"></span>
                        <br />
                        <span id="AccountTime" style="color: Red"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        收款人姓名：
                    </td>
                    <td>
                        <span id="AccountName"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        收款人银行：
                    </td>
                    <td>
                        <span id="BankName"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        收款人开户行：
                    </td>
                    <td>
                        <span id="BankAddress"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        结算备注：
                    </td>
                    <td>
                        <textarea id="txtcontent" runat="server" style="width: 300px; height: 140px;" class="forminput"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                     
                        
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        function Apply(id, amount, referralblance, merchantcode, accountname, bankname, bankaddress, userid, redpackrecordnum) {
            if (parseFloat(amount) > parseFloat(referralblance)) {
                alert("申请金额大于可提现金额！");
                return false;
            }
            $("#ctl00_contentHolder_txtcontent").val("");
            $.ajax({
                url: "../VsiteHandler.ashx",
                type: 'post', dataType: 'json', timeout: 10000,
                data: { actionName: "AccountTime", UserID: userid, merchantcode: merchantcode },
                success: function (resultData) {
                    if (resultData.Time != "")
                        $("#AccountTime").text("此账号于" + resultData.Time + "被修改过！");
                    else
                        $("#AccountTime").text("");
                }
            });
            setArryText("ctl00_contentHolder_txtcontent", $("#ctl00_contentHolder_txtcontent").val().replace(/\s/g, ""));
            $("#AccountNumber").text(merchantcode);
            $("#AccountName").text(accountname);
            $("#BankName").text(bankname);
            $("#BankAddress").text(bankaddress);
            $("#ctl00_contentHolder_hdapplyid").val(id);
            $("#ctl00_contentHolder_hduserid").val(userid);
            $("#ctl00_contentHolder_hdreferralblance").val(amount);
            $("#ctl00_contentHolder_hdredpackrecordnum").val(redpackrecordnum);
            DialogShow("线下打款确认", 'sendapply', 'div_apply', 'ctl00_contentHolder_btapply');

        }

        function deleteConfim() {
            if (confirm('确认删除此数据吗？')) {
                return true;
            }
            return false;
        }

        //验证
        function validatorForm() {
        
            return true;
        }
        $(function () {

        });
        
    </script>
</asp:Content>
