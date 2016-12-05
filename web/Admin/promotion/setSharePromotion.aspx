<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="setSharePromotion.aspx.cs" Inherits="Hidistro.UI.Web.Admin.setSharePromotion" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>分享推广设置</h1>
            <span>通过设置来分享到朋友、朋友圈、QQ、微博</span>
        </div>
        <div class="datafrom">
            <div class="formitem validator4">
                <ul>
                    <h2 class="colorE">商品详细</h2>
                    <li>

                        <span class="formitemtitle Pw_110">商品图片：</span>
                        <div class="uploadimages">
                            <Hi:UpImg runat="server" ID="upgoods" IsNeedThumbnail="false"  UploadType="SharpPic"/>
                        </div>
                    </li>
                    <li><span class="formitemtitle Pw_110">商品名称：</span>
                        <asp:TextBox ID="txtGoodsName" runat="server" CssClass="forminput" Width="300"></asp:TextBox>
                         <p id="ctl00_contentHolder_txtGoodsNameTip" >长度限制在40字符以内</p>
                    </li>
                     <li><span class="formitemtitle Pw_110">商品描述：</span><asp:TextBox ID="txtGoodsDec" runat="server" CssClass="forminput" Width="400"></asp:TextBox>
                         <p id="ctl00_contentHolder_txtGoodsDecTip">长度限制在40字符以内</p>
                          </li>
                     <h2 class="colorE">店铺首页</h2>
                    <li>

                        <span class="formitemtitle Pw_110">店铺首页图片：</span>
                        <div class="uploadimages">
                            <Hi:UpImg runat="server" ID="upshop" IsNeedThumbnail="false" UploadType="SharpPic"/>
                        </div>
                    </li>
                    <li><span class="formitemtitle Pw_110">店铺首页名称：</span><asp:TextBox ID="txtShopName" runat="server" CssClass="forminput" Width="300"></asp:TextBox>
                     <p id="ctl00_contentHolder_txtShopNameTip" >长度限制在40字符以内</p>
                         </li>
                     <li><span class="formitemtitle Pw_110">店铺首页描述：</span><asp:TextBox ID="txtShopDec" runat="server" CssClass="forminput" Width="400"></asp:TextBox>
                      <p id="ctl00_contentHolder_txtShopDecTip" >长度限制在40字符以内</p>
                     </li>
                     <h2 class="colorE">店铺推广码</h2>
                    <li>

                        <span class="formitemtitle Pw_110">推广图片：</span>
                        <div class="uploadimages">
                            <Hi:UpImg runat="server" ID="upSharp" IsNeedThumbnail="false" UploadType="SharpPic"/>
                        </div>
                    </li>
                    <li><span class="formitemtitle Pw_110">推广名称：</span><asp:TextBox ID="txtSharpName" runat="server" CssClass="forminput" Width="300"></asp:TextBox>
                     <p id="ctl00_contentHolder_txtSharpNameTip" >长度限制在40字符以内</p>
                    </li>
                     <li><span class="formitemtitle Pw_110">推广描述：</span><asp:TextBox ID="txtSharpDec" runat="server" CssClass="forminput" Width="400"></asp:TextBox>
                     <p id="ctl00_contentHolder_txtSharpDecTip" >长度限制在40字符以内</p>
                     </li>
                </ul>
                <ul class="btntf Pa_198">
                    <asp:Button ID="btnOK" runat="server" Text="提 交" CssClass="submit_DAqueding inbnt" OnClientClick="return PageIsValid();" />
                </ul>
            </div>
        </div>
    </div>
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="validateHolder" Runat="Server">
       <script type="text/javascript" language="javascript">
           function InitValidators() {
               initValid(new InputValidator('ctl00_contentHolder_txtGoodsName', 1, 40, true, null, '长度限制在40个字符以内'));
               initValid(new InputValidator('ctl00_contentHolder_txtShopDec', 1, 40, true, null, '长度限制在40个字符以内'));
               initValid(new InputValidator('ctl00_contentHolder_txtSharpDec', 1, 40, true, null, '长度限制在40个字符以内'));
               initValid(new InputValidator('ctl00_contentHolder_txtSharpName', 1, 40, true, null, '长度限制在40个字符以内'));
               initValid(new InputValidator('ctl00_contentHolder_txtShopName', 1, 40, true, null, '长度限制在40个字符以内'));
               initValid(new InputValidator('ctl00_contentHolder_txtGoodsDec', 1, 40, true, null, '长度限制在40个字符以内'));
           }
           $(document).ready(function () { InitValidators(); });
    </script>
 </asp:Content>

