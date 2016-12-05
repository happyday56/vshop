<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="EditSingleArticle.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.EditSingleArticle" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <script type="text/javascript">
    var auth = "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value) %>";
</script>
    <script src="../js/swfupload/swfupload.js" type="text/javascript"></script>
    <script src="../js/swfupload/handlers.js" type="text/javascript"></script>
    <link href="../css/MutiArticle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" ClientIDMode="Static"
    runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                编辑单图文</h1>
            <span>编辑单图文信息</span></div>
        <div class="datafrom">
           <div class="tw_body">
	        <div class="tw_box box_left">
    	        <div class="body">        
    		        <asp:Label ID="LbimgTitle" runat="server"  Text="标题"></asp:Label>
                    <div class="img_fm">
            	        <div id="img_default" style="display:block" class="gy_bg">封面图片</div>
                       <img id="uploadpic" runat="server" class="fmImg" width="300"  />
                    </div>            
    		        <asp:Label ID="Lbmsgdesc" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div id="box_move" class="tw_box box_left box_body">
                <div class="cont_body">
                    <div class="fgroup">
                        <span><em>*</em>标题：</span>
                        <asp:TextBox ID="Tbtitle" runat="server" Width="282px" onkeyup="syncSingleTitle(this.value)"></asp:TextBox>
                    </div>
                    <div class="fgroup">
                            <div style="width: 100%; height: 28px;">
                                <span><em>*</em>封 面：</span> <span id="swfu_container"><span>
                                <span id="spanButtonPlaceholder"></span>
                                </span><span id="divFileProgressContainer"></span></span>
								<div>建议尺寸：360*200</div>
                            </div>
                            <div id="smallpic" style="display: none; margin-left: 100px;">
                            </div>
                             <!--封面上传后，返回的图片地址，填充下面的input对象。-->
                            <input id="fmSrc" type="text" value="" style="display: none;" />
                        </div>

                    <div class="fgroup">
                        <span><em>*</em>摘要：</span>
                        <asp:TextBox ID="Tbdescription" runat="server" TextMode="MultiLine" onkeyup="syncAbstract(this.value)" ></asp:TextBox>
                    </div>
                    <div class="fgroup">
                        <span>自定义链接：</span>
                        <asp:TextBox ID="TbUrl" runat="server" Width="228px"></asp:TextBox>(可不填，若填写则优先跳转)
                    </div>            
                    <div>
                       <Kindeditor:KindeditorControl id="fkContent" runat="server" Width="530px"  height="200px" />
                    </div>
                </div>
                <i class="arrow arrow_out" style="margin-top: 0px;"></i>
                <i class="arrow arrow_in" style="margin-top: 0px;"></i>
            </div>        
             <div id="nextTW"></div>        
        </div>
            <asp:HiddenField ID="hdpic" runat="server" />
            <div class="clear">
            </div>
            <hr style="border: 1px solid #ccc; border-bottom: 0; margin: 20px;">
            <div class="formitem validator2">
        <ul>
          <li> <span class="formitemtitle Pw_100">回复类型：</span>
              <asp:CheckBox ID="chkKeys" runat="server" Text="关键字回复" />
              <asp:CheckBox ID="chkSub" runat="server" Text="关注时回复" />
              <asp:CheckBox ID="chkNo" runat="server" Text="无匹配回复" />
          </li>
         <li class="likey"><span class="formitemtitle Pw_100"><em >*</em>关键字：</span>
            <asp:TextBox ID="txtKeys" runat="server" CssClass="forminput"></asp:TextBox>
            <p id="ctl00_contentHolder_txtKeysTip">用户可通过该关键字搜到到这个内容</p>
          </li>          
          <li class="likey"> <span class="formitemtitle Pw_100">匹配模式：</span>
            <Hi:YesNoRadioButtonList ID="radMatch" runat="server" RepeatLayout="Flow" YesText="模糊匹配" NoText="精确匹配" />
          </li>
          <li> <span class="formitemtitle Pw_100">状态：</span>
            <Hi:YesNoRadioButtonList ID="radDisable" runat="server" RepeatLayout="Flow" YesText="启用" NoText="禁用" />
          </li>
      </ul>
      </div>
        </div>
              
        <div class="btn Pa_110">
                <asp:Button ID="btnCreate" runat="server" Text="保 存" CssClass="submit_DAqueding"
                    Style="float: left;" OnClientClick="return CheckKey();" OnClick="btnCreate_Click" />
            </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script src="../js/swfupload/upload.js" type="text/javascript"></script>
    <script src="../js/MultiBox.js" type="text/javascript"></script>
    <script src="../js/ReplyOnKey.js" type="text/javascript"></script>
</asp:Content>
