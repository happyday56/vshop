<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddMultiArticle.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.AddMultiArticle" %>
    <%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <script src="../js/swfupload/swfupload.js" type="text/javascript"></script>
    <script src="../js/swfupload/handlers.js" type="text/javascript"></script>
    <link href="../css/MutiArticle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var auth = "<%=(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value) %>";
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server" ClientIDMode="Static">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                添加多图文</h1>
            <span>添加多图文信息</span>
         </div>
         <div class="datafrom">
            <div class="tw_body">
                <div class="tw_box box_left">
                    <div id="sbox1" class="body">
                        <div class="img_fm" onmousemove="sBoxzzShow('1')">
                            <div id="fm1" style="display: block;" class="gy_bg fmImg">
                                封面图片</div>
                            <img id="img1" class="fmImg" style="display: none" src="" />
                            <p class="abstractVal">
                                <span id="title1" style="margin-left: 4px;">摘要</span></p>
                            <div id="zz_sbox1" class="zzc" onmouseout="sBoxzzHide('1')" style="line-height: 178px;">
                                <a href="javascript:void(0)" onclick="editTW(1);">修改</a></div>
                        </div>
                    </div>
                    <div id="sbox2" class="baseBorder twSBox" onmousemove="sBoxzzShow(2)" style="position: relative;">
                        <div class="body">
                            <div id="title2" class="info">
                                <p>
                                    标题</p>
                            </div>
                            <div class="simg">
                                <div style="width: 100px; height: 100px; background-color: rgb(236,236,236); line-height: 100px;
                                    color: #c0c0c0; font-weight: bold; text-align: center;">
                                    预览图</div>
                                <img id="img2" src="" class="fmImg" style="display: none" />
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div id="zz_sbox2" onmouseout="sBoxzzHide(2)" class="zzc" style="line-height: 121px;
                            height: 121px;">
                            <a href="javascript:void(0)" onclick="editTW(2);">修改</a> <a href="javascript:void(0)"
                                onclick="sBoxDel(2);">删除</a></div>
                    </div>
                    <span id="addSBoxInfoHere"></span>
                    <div class="baseBorder twSBox boxAdd">
                        <div style="width: 360px;">
                            <a href="javascript:void(0)" onclick="addSBox()">添加一个图文</a>
                        </div>
                    </div>
                </div>
                <div id="box_move" class="tw_box box_left box_body">
                    <div class="cont_body">
                        <div class="fgroup">
                            <span><em>*</em>标 题：</span>
                            <input id="title" type="text" onkeyup="syncTitle(this.value)" />
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
                        <div class="fgroup" id="w_url">
                            <span>自定义链接：</span> http://
                            <input id="urlData" style="width: 192px;" type="text" value="" />(可不填，若填写则优先跳转)
                        </div>
                        <div id="msg" style="display: none; color: red; font-size: 14px;">
                        </div>
                        <div>
                            <Kindeditor:KindeditorControl id="fkContent" runat="server" Width="700px"  height="200px" />
                        </div>
                    </div>
                    <i class="arrow arrow_out" style="margin-top: 0px;"></i><i class="arrow arrow_in"
                        style="margin-top: 0px;"></i>
                </div>
                <div id="nextTW">
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div id="modelSBox" style="display: none;">
                <div id="sboxrpcode1366" class="baseBorder twSBox" onmousemove="sBoxzzShow(rpcode1366)"
                    style="position: relative;">
                    <div class="body">
                        <div id="titlerpcode1366" class="info">
                            <p>
                                标题</p>
                        </div>
                        <div class="simg">
                            <div style="width: 100%; height: 100%; background-color: rgb(236,236,236); line-height: 100px;
                                color: #c0c0c0; font-weight: bold; text-align: center;">
                                预览图</div>
                            <img id="imgrpcode1366" src="pig1.png" style="width: 100%; height: 100%; display: none;
                                position: absolute; top: 0; left: 0;" />
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div id="zz_sboxrpcode1366" onmouseout="sBoxzzHide(rpcode1366)" class="zzc" style="line-height: 121px;
                        height: 121px;">
                        <a href="javascript:void(0)" onclick="editTW(rpcode1366)">修改</a> <a href="javascript:void(0)"
                            onclick="sBoxDel(rpcode1366);">删除</a></div>
                </div>
            </div>
            <div class="clearfix">
            </div>

			<hr style="border: 1px solid #ccc; border-bottom: 0; margin: 20px;">
<div class="formitem validator2">
        <ul>
          <li> <span class="formitemtitle Pw_100">回复类型：</span>
              <asp:CheckBox ID="chkKeys" runat="server" Text="关键字回复" />
              <asp:CheckBox ID="chkSub" runat="server" Text="关注时回复" />
              <asp:CheckBox ID="chkNo" runat="server" Text="无匹配回复" />
          </li>
          <li class="likey"> <span class="formitemtitle Pw_100"><em >*</em>关键字：</span>
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
        <input id="Articlejson" name="Articlejson" type="hidden" />

        </div>
        
        <div class="btn Pa_110">
            <input type="button" onclick="return checkJson();" class="submit_DAqueding" style="float: left;"
                value="添 加" />
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script src="../js/swfupload/uploadMult.js" type="text/javascript"></script>
    <script src="../js/MultiBox.js" type="text/javascript"></script>
    <script type="text/javascript">
        var SessionID='<%=Session.SessionID %>';
        edit = false; //定义当前JS执行为添加状态
        function AddMultArticles() {
            if ($("#chkKeys:checked").length > 0) {
                if ($("#txtKeys").val() == "") {
                    alert("你选择了，关键字回复，请填写关键字！");
                    return;
                }
            }
            if ($("#chkKeys:checked,#chkSub:checked,#chkNo:checked").length == 0) {
                alert("请选择回复类型");
                return;
            }

            $.ajax({
                url: "./AddMultiArticle.aspx?cmd=add",
                type: "POST",
                dataType: "text",
                data: { "MultiArticle": $("#Articlejson").val()
                    , "Keys": $("#txtKeys").val()
                    , "chkKeys": $("#chkKeys:checked").length == 1
                    , "chkSub": $("#chkSub:checked").length == 1
                    , "chkNo": $("#chkNo:checked").length == 1
                    , "radMatch": $("#radMatch_0:checked").length == 1
                    , "radDisable": $("#radDisable_0:checked").length == 1
                },
                success: function (msg) {
                    if (msg == "true") {
                        alert("添加成功！");
                        window.location.href = "ReplyOnKey.aspx";
                    }
                    else if (msg == "key") {
                        alert("关键字重复，请重新填写！");
                        $("#txtKeys").focus();
                    }
                    else {
                        alert("添加失败！");
                    }
                },
                error: function (xmlHttpRequest, error) {
                   // alert(error);
                }
            });
        }
    </script>
    <script src="../js/ReplyOnKey.js" type="text/javascript"></script>
    <script src="../js/jquery-json-2.4.js" type="text/javascript"></script>
</asp:Content>
