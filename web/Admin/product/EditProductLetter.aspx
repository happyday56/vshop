<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="EditProductLetter.aspx.cs" Inherits="Hidistro.UI.Web.Admin.EditProductLetter" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link id="cssLink" rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <Hi:Script ID="Script2" runat="server" Src="/utility/jquery_hashtable.js" />
    <Hi:Script ID="Script1" runat="server" Src="/utility/jquery-powerFloat-min.js" />
    <link href="/utility/flashupload/flashupload.css" rel="stylesheet" type="text/css" />
    <Hi:Script ID="Script3" runat="server" Src="/utility/flashupload/flashupload.js" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>编辑商品文案信息</h1>
            <span>商品文案信息修改</span>
        </div>
        <div class="datafrom">
            <div class="formitem validator1">
                <ul>
                    <li>
                        <h2 class="colorE">基本信息</h2>
                    </li>
                    <li class=" clearfix"><span class="formitemtitle Pw_198">商品名称：<em>*</em></span>
                        <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductName" Width="350px" ReadOnly="true" />
                        <p id="ctl00_contentHolder_txtProductNameTip">限定在60个字符</p>
                    </li>
                    <li id="liImgList">
                        <h2 class="colorE">图片和描述</h2>
                    </li>

                    <li style="height: 126px;"><span class="formitemtitle Pw_198">商品文案图片：</span><Hi:ProductFlashUpload ID="ucFlashUpload1" runat="server" MaxNum="5" />
                    </li>
                    <li>
                        <p class="Pa_198 clearfix m_none" style="padding-left: 200px;">支持多图上传,最多5个,每个图应小于120k,jpg,gif,png格式。建议为500x500像素</p>
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">商品文案简介：</span>
                        <Hi:TrimTextBox runat="server" Rows="6" Height="100px" Columns="76" ID="txtProductShortLetter" TextMode="MultiLine" />
                        <p class="Pa_198">限定在300个字符以内</p>
                    </li>
                    <li class="clearfix"><span class="formitemtitle Pw_198">商品文案内容：</span>
                        <Kindeditor:KindeditorControl ID="fckProductLetter" runat="server" Height="300" />
                        <p style="color: Red;">
                            <asp:CheckBox runat="server" ID="ckbIsDownPic" Text="是否下载商品文案内容外站图片" /></p>
                        <p class="Pa_198">如果勾选此选项时，商品文案内容里面有外站的图片则会下载到本地，相反则不会，由于要下载图片，如果图片过多或图片很大，需要下载的时间就多，请慎重选择。</p>
                    </li>
                </ul>
                <ul class="btntf Pa_198 clear">
                    <asp:Button runat="server" ID="btnSave" Text="保 存" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
                </ul>
            </div>
        </div>
    </div>
    <div class="Pop_up" id="priceBox" style="display: none;">
        <h1>
            <span id="popTitle"></span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="关闭" width="38" height="20" />
        </div>
        <div class="mianform ">
            <div id="priceContent">
            </div>
            <div style="margin-top: 10px; text-align: center;">
                <input type="button" value="确定" onclick="doneEditPrice();" />
            </div>
        </div>
    </div>

    <div class="Pop_up" id="skuValueBox" style="display: none;">
        <h1>
            <span>选择要生成的规格</span>
        </h1>
        <div class="img_datala">
            <img src="../images/icon_dalata.gif" alt="关闭" width="38" height="20" />
        </div>

        <div class="mianform ">
            <div id="skuItems">
            </div>
            <div style="margin-top: 10px; text-align: center;">
                <input type="button" value="确定" id="btnGenerate" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" src="attributes.helper.js"></script>
    <script type="text/javascript" src="grade.price.helper.js"></script>
    <script type="text/javascript" src="producttag.helper.js"></script>
    <script type="text/javascript" language="javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtProductName', 1, 60, false, null, '商品名称字符长度在1-60之间'));
            initValid(new InputValidator('ctl00_contentHolder_txtDisplaySequence', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtDisplaySequence', 1, 9999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 0, 20, true, null, '商家编码的长度不能超过20个字符'));
            initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 0.01, 10000000, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0.01, 99999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 0.01, 99999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '货号的长度不能超过20个字符'));
            initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 1, 9999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '必须限制在20个字符以内且只能是英文和中文例:g/元'))
            initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 10, true, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'))
            initValid(new InputValidator('ct100_contentHolder_txtShowSaleCounts', 0, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
            appendValid(new NumberRangeValidator('ct100_contentHolder_txtShowSaleCounts', 0, 9999999, '输入的数值超出了系统表示范围'));
            appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '商品简介必须限制在20个字符以内'));
        }
        $(document).ready(function () { InitValidators(); });
    </script>

</asp:Content>
