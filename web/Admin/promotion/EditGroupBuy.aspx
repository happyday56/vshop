<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="EditGroupBuy.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditGroupBuy" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">


    <script type="text/javascript">
        function ShowAddDiv() {

            try {
                DialogFrame("/admin/promotion/SearchGroupbuyProduct.aspx?activityId=", "添加团购商品", 975, null);
            }
            catch (e) {

                console.log(e.message);
            }

        }

        function setProductInfo(id, name, price) {

            $('#productName').html(name);
            $('#lblPrice').html(price.toFixed(2));
            $('#li_price').show();
            
            $('#ProductId').val(id);
        }
    
    </script>





    <div class="areacolumn clearfix">
        <div class="columnright">
            <div class="title">
                <em>
                    <img src="../images/06.gif" width="32" height="32" /></em>
                <h1>
                    编辑团购活动</h1>
                <span>团购活动编辑</span>
            </div>
            <div class="formitem validator5" style="padding-left: 15px;">
                
                <ul>
                  <li> <asp:TextBox ID="ProductId" runat="server" ClientIDMode="Static" style="display:none;"  ></asp:TextBox></li>
                    <li>
                    <span  class="formitemtitle Pw_140">商品名称：</span><asp:Label runat="server" ID="productName" ClientIDMode="Static" Text=""></asp:Label>
                        <div class="btn">
                        <asp:HyperLink Text="修改团购商品" runat="server"   class="submit_jia" ID="addProduct" ClientIDMode="Static" NavigateUrl="javascript:void(0)" onclick="ShowAddDiv()"  style="font-size: 12px; color: #fff; display:inline-block;"></asp:HyperLink>
                     
                        </div>
                        <p id="P1">
                            当此团购活动有会员已订购时，商品不能再进行编辑</p>
                    </li>


                    <li ><span class="formitemtitle Pw_140">一口价：</span>
                        <abbr class="formselect" style="color: red; font-family: Arial, Helvetica, sans-serif;
                            font-size: 18px;">
                            <asp:Label ID="lblPrice" ClientIDMode="Static" runat="server"></asp:Label>
                        </abbr>
                        <span id="P4"></span></li>
                    <li><span class="formitemtitle Pw_140"><em>*</em>开始日期：</span>
                        <UI:WebCalendar runat="server" CssClass="forminput" ID="calendarStartDate" /><abbr
                            class="formselect"><Hi:HourDropDownList ID="drophours" runat="server" Style="margin-left: 5px;" />
                        </abbr>
                        <p id="P3">
                            当达到开始日期时，活动会自动变为正在参与活动状态。</p>
                    </li>
                    <li><span class="formitemtitle Pw_140"><em>*</em>结束日期：</span>
                        <UI:WebCalendar runat="server" CssClass="forminput" ID="calendarEndDate" /><abbr
                            class="formselect"><Hi:HourDropDownList ID="HourDropDownList1" runat="server" Style="margin-left: 5px;" />
                        </abbr>
                        <p id="P2">
                            当达到结束日期时，活动会自动变为结束未处理状态。</p>
                    </li>
                    <li style="display:none"><span class="formitemtitle Pw_140">违约金：</span>
                        <asp:TextBox ID="txtNeedPrice" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtNeedPriceTip">
                            违约将扣除的金额，不填表示没有违约金,客户在管理员主动将活动设置为失败的情况下不视为违约。</p>
                    </li>
                    <li><span class="formitemtitle Pw_140"><em>*</em>限购总数量：</span>
                        <asp:TextBox ID="txtMaxCount" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtMaxCountTip">
                            此次活动可购买的商品总数量,不能为空,订购达到此上限时，活动会自动变为结束未处理状态。</p>
                    </li>
                    <li><span class="formitemtitle Pw_140"><em>*</em>团购满足数量：</span>
                        <asp:TextBox ID="txtCount" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtCountTip">
                            满足此次活动的最低商品订购数量,不能为空。</p>
                    </li>
                    <li><span class="formitemtitle Pw_140"><em>*</em>团购价格：</span>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="forminput"></asp:TextBox>
                        <p id="ctl00_contentHolder_txtPriceTip">
                            达到订购数量后享受的团购价格,不能为空。</p>
                    </li>
                    <li><span class="formitemtitle Pw_140">活动说明：</span>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Columns="50" Rows="5"
                            CssClass="forminput"></asp:TextBox>
                    </li>
                    <li></li>
                </ul>
                <ul class="btn Pa_140 clear">
                    <li>
                        <asp:Button ID="btnUpdateGroupBuy" runat="server" Text="保存" OnClientClick="return PageIsValid();"
                            CssClass="submit_DAqueding float" />
                        <asp:Button ID="btnFinish" runat="server" Text="结束活动" Visible="false" CssClass="submit_DAqueding float" />
                        <asp:Button ID="btnSuccess" runat="server" Text="活动成功" Visible="false" CssClass="submit_DAqueding float" />
                        <asp:Button ID="btnFail" runat="server" Text="活动失败" Visible="false" CssClass="submit_DAqueding float" />
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="groupbuy.helper.js"></script>
    <script type="text/javascript" language="javascript">
        function InitValidators() {

            initValid(new InputValidator('ctl00_contentHolder_txtNeedPrice', 1, 10, true, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，违约金只能输入数值'))
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtNeedPrice', 1, 9999999, '输入的数值超出了系统表示范围'));

            initValid(new InputValidator('ctl00_contentHolder_txtMaxCount', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，限购数量只能输入整数型数值'))
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtMaxCount', 1, 9999999, '输入的数值超出了系统表示范围'));

            initValid(new InputValidator('ctl00_contentHolder_txtCount', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，限购数量只能输入整数型数值'))
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtCount', 1, 9999999, '输入的数值超出了系统表示范围'));

            initValid(new InputValidator('ctl00_contentHolder_txtPrice', 1, 10, false, '([0-9]\\d*(\\.\\d{1,2})?)', '团购价格只能输入数值,且不能超过2位小数'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtPrice', 0.01, 9999999, '输入的数值超出了系统表示范围'));


        }
        $(document).ready(function () {
            InitValidators();
            $("#li_price").hide();
            $.ajax({
                url: "EditGroupBuy.aspx",
                data:
                      {
                          isCallback: "true",
                          productId: $("#ctl00_contentHolder_dropGroupBuyProduct").val()
                      },
                type: 'GET', dataType: 'json', timeout: 10000,
                async: false,
                success: function (resultData) {
                    if (resultData.Status == "OK") {
                        var price = resultData.Price;
                        $("#ctl00_contentHolder_lblPrice").html(price);
                        $("#li_price").show();
                    }
                }
            });
            $("#ctl00_contentHolder_dropGroupBuyProduct").change(function () {
                var pId = $(this).val();
                if (pId == "") {
                    $("#li_price").hide();
                }
                else {
                    $.ajax({
                        url: "EditGroupBuy.aspx",
                        data:
                      {
                          isCallback: "true",
                          productId: pId
                      },
                        type: 'GET', dataType: 'json', timeout: 10000,
                        async: false,
                        success: function (resultData) {
                            if (resultData.Status == "OK") {
                                var price = resultData.Price;
                                $("#ctl00_contentHolder_lblPrice").html(price);
                                $("#li_price").show();
                            }
                        }
                    });
                }
            });
        });       
    </script>
</asp:Content>
