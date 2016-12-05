<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"  CodeBehind="AddProduct.aspx.cs" Inherits="Hidistro.UI.Web.Admin.AddProduct" %>
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
	  <div class="title  m_none td_bottom"> <em><img src="../images/01.gif" width="32" height="32" /></em>
	    <h1>添加商品</h1>
      </div>
	  <div class="datafrom">
	    <div class="formitem validator1">
	      <ul>
          <li><h2 class="colorE">基本信息</h2></li>
	        <li>
	            <span class="formitemtitle Pw_198">所属商品分类：</span>
                <span class="colorE float fonts"><asp:Literal runat="server" ID="litCategoryName"></asp:Literal></span>
                [<asp:HyperLink runat="server" ID="lnkEditCategory" CssClass="a" Text="编辑"></asp:HyperLink>]
            </li>
	        <li>
	            <span class="formitemtitle Pw_198">商品类型：</span>
                <abbr class="formselect"><Hi:ProductTypeDownList runat="server" CssClass="productType" ID="dropProductTypes" NullToDisplay="--请选择--" /></abbr>
	            品牌：<abbr class="formselect"><Hi:BrandCategoriesDropDownList  runat="server" ID="dropBrandCategories" NullToDisplay="--请选择--" /></abbr>
            </li>
	        <li class=" clearfix"> <span class="formitemtitle Pw_198"><em >*</em>商品名称：</span>
	          <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductName" Width="350px"/>
              <p id="ctl00_contentHolder_txtProductNameTip">限定在60个字符</p>
	        </li>
	        <li><span class="formitemtitle Pw_198">商家编码：</span>
                <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtProductCode" />
                <p id="ctl00_contentHolder_txtProductCodeTip">长度不能超过20个字符</p>
            </li>
	        <li><span class="formitemtitle Pw_198">计量单位：</span>
                <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtUnit" />
                <p id="ctl00_contentHolder_txtUnitTip">必须限制在20个字符以内且只能是英文和中文例:g/元</p>
            </li>
            <li><span class="formitemtitle Pw_198">市场价：<em >*</em></span>
	            <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMarketPrice" />&nbsp;元
                <p id="ctl00_contentHolder_txtMarketPriceTip">本站会员所看到的商品市场价</p>
	        </li>
	        <li><h2 class="colorE">扩展属性</h2></li>
	        <li id="attributeRow" style="display:none;"><span class="formitemtitle Pw_198">商品属性：</span>
	        <div class="attributeContent" id="attributeContent"></div>
            <Hi:TrimTextBox runat="server" ID="txtAttributes" TextMode="MultiLine" style="display:none;"></Hi:TrimTextBox>
            </li>
	        <li id="skuCodeRow"><span class="formitemtitle Pw_198">货号：</span>
                <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSku" />
                <p id="ctl00_contentHolder_txtSkuTip">限定在20个字符</p>
            </li>
	        <li id="salePriceRow"><span class="formitemtitle Pw_198">一口价：<em >*</em></span>
              <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtSalePrice" />&nbsp;元<input type="button" onclick="editProductMemberPrice();" value="编辑会员价" style="margin-left:10px;" />
              <Hi:TrimTextBox runat="server" ID="txtMemberPrices" TextMode="MultiLine" style="display:none;"></Hi:TrimTextBox>
              <p id="ctl00_contentHolder_txtSalePriceTip">本站会员所看到的商品零售价</p>
	        </li>
	        <li id="costPriceRow"><span class="formitemtitle Pw_198">成本价：<em >*</em></span>
	            <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtCostPrice" />&nbsp;元
	            <p id="ctl00_contentHolder_txtCostPriceTip">商品的成本价</p>
	        </li>
              <li id="costPriceRow"><span class="formitemtitle Pw_198"><asp:Literal runat="server" ID="litVPName"></asp:Literal>使用率：<em >*</em></span>
	            <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtVirtualPointRate" />&nbsp;%
	            <p id="ctl00_contentHolder_txtVirtualPointRateTip"></p>
	        </li>
            <li id="qtyRow"><span class="formitemtitle Pw_198"><em >*</em>商品库存：</span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtStock" /></li>
            <li id="weightRow"><span class="formitemtitle Pw_198">商品重量：</span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtWeight" />&nbsp;克</li>
              <li><span class="formitemtitle Pw_198">累计销量：</span><Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtShowSaleCounts" /></li>
            <li id="skuTitle" style="display:none;"><h2 class="colorE">商品规格</h2></li>
            <li id="enableSkuRow" style="display:none;"><span class="formitemtitle Pw_198">规格：</span><input id="btnEnableSku" type="button" value="开启规格" /> 
                开启规格前先填写以上信息，可自动复制信息到每个规格</li>
            <li id="skuRow"  style="display:none;">
                <p id="skuContent">
                    <input type="button" id="btnshowSkuValue" value="生成部分规格" />
                    <input type="button" id="btnAddItem" value="增加一个规格" />
                    <input type="button" id="btnCloseSku" value="关闭规格" />
                    <input type="button" id="btnGenerateAll" value="生成所有规格" />
                </p>
                <p id="skuFieldHolder" style="margin:0px auto;display:none;"></p>
                <div id="skuTableHolder">
                </div>
                <Hi:TrimTextBox runat="server" ID="txtSkus" TextMode="MultiLine" style="display:none;" ></Hi:TrimTextBox>
                <asp:CheckBox runat="server" ID="chkSkuEnabled" style="display:none;" />
            </li>
            <li><h2 class="colorE">图片和描述</h2></li>
           <li><span class="formitemtitle Pw_100">商品首页图：</span>
                  <asp:FileUpload ID="homePicUrlFileUpload" CssClass="forminput" runat="server" />
                </li>
              <li style="height: 126px;"><span class="formitemtitle Pw_198">商品图片：</span><Hi:ProductFlashUpload ID="ucFlashUpload1" runat="server" />                 
                  </li>
              <li><p class="Pa_198 clearfix m_none" style="padding-left:200px;">支持多图上传,最多5个,每个图应小于120k,jpg,gif,png格式。建议为500x500像素</p></li>
            <li class="clearfix"><span class="formitemtitle Pw_198">商品简介：</span>
                <Hi:TrimTextBox runat="server" Rows="6" Height="100px" Columns="76" ID="txtShortDescription" TextMode="MultiLine" />
                <p class="Pa_198">限定在300个字符以内</p>
            </li>
            <li class="clearfix"><span class="formitemtitle Pw_198">商品描述：</span>
               <Kindeditor:KindeditorControl ID="editDescription" runat="server" Height="300"  />
               <p style="color:Red;"><asp:CheckBox runat="server" ID="ckbIsDownPic" Text="是否下载商品描述外站图片" /></p>
                <p class="Pa_198">如果勾选此选项时，商品里面有外站的图片则会下载到本地，相反则不会，由于要下载图片，如果图片过多或图片很大，需要下载的时间就多，请慎重选择。</p>
				<p class="Pa_198">单张图片尺寸建议不要超过：320px * 580px</p>
            </li>
            
	       <li><h2 class="colorE clear">相关设置</h2></li>
	        <li>
			  <span class="formitemtitle Pw_198">商品销售状态：</span>
				 <asp:RadioButton runat="server" ID="radOnSales" GroupName="SaleStatus" Text="出售中"></asp:RadioButton>
                <asp:RadioButton runat="server" ID="radUnSales" GroupName="SaleStatus"  Text="下架区" Visible="false"></asp:RadioButton>
                <%--这里默认设置入库，不直接出售，保存后需在商品列表批量处理出售，才会对外出售--%>
                <asp:RadioButton runat="server" ID="radInStock" GroupName="SaleStatus" Checked="true"  Text="仓库中"></asp:RadioButton>
 			</li>
              <li>
			  <span class="formitemtitle Pw_198">是否首页显示：</span>
				 <asp:RadioButton runat="server" ID="radOnHome" GroupName="DisplayHomeStatus" Text="显示" Checked="true"></asp:RadioButton>
                <asp:RadioButton runat="server" ID="radUnHome" GroupName="DisplayHomeStatus"  Text="不显示" ></asp:RadioButton>
 			</li>
              <li>
			  <span class="formitemtitle Pw_198">跨境商品：</span>
				 <asp:RadioButton runat="server" ID="radOnCross" GroupName="CrossStatus" Text="是"></asp:RadioButton>
                <asp:RadioButton runat="server" ID="radUnCross" GroupName="CrossStatus"  Text="不是"  Checked="true"></asp:RadioButton>
 			</li>
              <li>
                  <span class="formitemtitle Pw_198">跨境商品最大购买数：</span>
                  <Hi:TrimTextBox runat="server" CssClass="forminput" ID="txtMaxCross" Text="1" />
              </li>
            <li>
			  <span class="formitemtitle Pw_198">商品包邮：
                </span>
				<asp:CheckBox ID="ChkisfreeShipping" 
                    runat="server" />
 			&nbsp;</li>
             <li class="clearfix" id="l_tags" runat="server" style="display:none;">
			   <span class="formitemtitle Pw_198">商品标签定义：<br /><a id="a_addtag" href="javascript:void(0)" onclick="javascript:AddTags()" class="add">添加</a></span>
			   
			   <div id="div_tags"> <Hi:ProductTagsLiteral ID="litralProductTag" runat="server"></Hi:ProductTagsLiteral></div>
			   <div id="div_addtag" style="display:none"><input type="text" id="txtaddtag" /><input type="button" value="保存" onclick="return AddAjaxTags()" /></div>
			     <Hi:TrimTextBox runat="server" ID="txtProductTag" TextMode="MultiLine" style="display:none;"></Hi:TrimTextBox> 
            </li>    
	      </ul>
	      <ul class="btntf Pa_198 clear">
	        <asp:Button runat="server" ID="btnAdd" Text="保 存" OnClientClick="return doSubmit();" CssClass="submit_DAqueding inbnt" />
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
        <div style="margin-top:10px;text-align:center;">
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
        <div id="skuItems" >
        </div>
        <div style="margin-top:10px;text-align:center;">
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
           initValid(new InputValidator('ctl00_contentHolder_txtProductCode', 0, 20, true, null, '商家编码的长度不能超过20个字符'));
           initValid(new InputValidator('ctl00_contentHolder_txtSalePrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
           appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtSalePrice', 0.01, 10000000, '输入的数值超出了系统表示范围'));                    
           initValid(new InputValidator('ctl00_contentHolder_txtCostPrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
           appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtCostPrice', 0.01, 99999999, '输入的数值超出了系统表示范围'));
           initValid(new InputValidator('ctl00_contentHolder_txtMarketPrice', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
           appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtMarketPrice', 0.01, 99999999, '输入的数值超出了系统表示范围'));
           initValid(new InputValidator('ctl00_contentHolder_txtSku', 0, 20, true, null, '货号的长度不能超过20个字符'));
           initValid(new InputValidator('ctl00_contentHolder_txtStock', 1, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'));
           appendValid(new NumberRangeValidator('ctl00_contentHolder_txtStock', 1, 9999999, '输入的数值超出了系统表示范围'));
           initValid(new InputValidator('ctl00_contentHolder_txtVirtualPointRate', 1, 10, false, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
           appendValid(new MoneyRangeValidator('ctl00_contentHolder_txtVirtualPointRate', 0.00, 100, '输入的数值超出了系统表示范围'));
           initValid(new InputValidator('ctl00_contentHolder_txtUnit', 1, 20, true, '[a-zA-Z\/\u4e00-\u9fa5]*$', '必须限制在20个字符以内且只能是英文和中文例:g/元'));
           initValid(new InputValidator('ctl00_contentHolder_txtWeight', 0, 10, true, '(0|(0+(\\.[0-9]{1,2}))|[1-9]\\d*(\\.\\d{1,2})?)', '数据类型错误，只能输入实数型数值'));
           appendValid(new NumberRangeValidator('ctl00_contentHolder_txtWeight', 1, 9999999, '输入的数值超出了系统表示范围'));
           initValid(new InputValidator('ct100_contentHolder_txtShowSaleCounts', 0, 10, false, '-?[0-9]\\d*', '数据类型错误，只能输入整数型数值'))
           appendValid(new NumberRangeValidator('ct100_contentHolder_txtShowSaleCounts', 0, 9999999, '输入的数值超出了系统表示范围'));
            initValid(new InputValidator('ctl00_contentHolder_txtShortDescription', 0, 300, true, null, '商品简介必须限制在20个字符以内'));            
           }
           $(document).ready(function() { InitValidators(); });
  </script>     
</asp:Content>
