<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="True"
    CodeBehind="ImageData.aspx.cs" Inherits="Hidistro.UI.Web.Admin.ImageData" EnableSessionState="True" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <Hi:Script ID="Script1" runat="server" Src="/Utility/swfupload/swfupload.js"></Hi:Script>
    <Hi:Script ID="Script2" runat="server" Src="/Utility/swfupload/handlers.js"></Hi:Script>
    
    <script type="text/javascript" src="/Utility/swfupload/swfobject.js"></script>
    <script type="text/javascript">
        function copySuccess() {
            alert("该地址已经复制，你可以使用Ctrl+V 粘贴！");
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <!--面包屑-->
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/01.gif" width="32" height="32" /></em>
            <h1>
                <strong>图片库管理</strong></h1>
            <span>查看服务器上所有图片进行管理和操作 </span>
        </div>
        <div class="datalist">
            <div class="functionHandleArea">
                <!--分页功能-->
                <div class="searcharea clearfix br_search">
                    <ul class="a_none_left">
                        <li><span>图片数量：</span><span><a class="selectthis"><asp:Label ID="lblImageData" runat="server"
                            Text=""></asp:Label></a></span></li>
                        <li>
                            <asp:TextBox ID="txtWordName" Width="110" runat="server" CssClass="forminput" /></li>
                        <li>
                            <asp:Button ID="btnImagetSearch" runat="server" Text="查询" CssClass="searchbutton" /></li>
                    </ul>
                </div>
                <!--结束-->
                <div class="batchHandleArea">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="77%">
                                <ul>
                                    <li class="batchHandleButton"><span class="signicon"></span><span class="allSelect">
                                        <a href="javascript:void(0)" onclick="CheckClickAll()">全选</a></span> <span class="reverseSelect">
                                            <a href="javascript:void(0)" onclick="CheckReverse()">反选</a></span> <span class="moveSelect">
                                                <a href="javascript:void(0)" onclick="MoveImg()">移动到</a></span> <span class="delete">
                                                    <Hi:ImageLinkButton ID="btnDelete1" runat="server" Text="删除" IsShow="true" /></span>
                                        <span id="swfu_container"><span><span id="spanButtonPlaceholder"></span></span><span
                                            id="divFileProgressContainer"></span></span></li>
                                </ul>
                            </td>
                            <td width="20%" nowrap="nowrap">
                                请选择上传图片的分类：<Hi:ImageDataGradeDropDownList ID="dropImageFtp2" runat="server" NullToDisplay="请选择上传图片的路径" />
                                排序:
                                <Hi:ImageOrderDropDownList AutoPostBack="true" runat="server" ID="ImageOrder" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="imageDataRight">
                <div class="borderthin">
                    <ul class="RightHead">
                        图片分类:</ul>
                    <Hi:ImageTypeLabel runat="server" ID="ImageTypeID" />
                    <ul class="pad10">
                        <a href="<%= Globals.GetAdminAbsolutePath("/store/ImageType.aspx")%>" class="submit_queding"
                            style="display: block; text-align: center;">分类管理</a></ul>
                </div>
            </div>
            <div class="imageDataLeft" id="ImageDataList">
                <!--图片列表begin-->
                <asp:DataList ID="photoDataList" runat="server" RepeatColumns="4" ShowFooter="False"
                    ShowHeader="False" DataKeyField="PhotoId" CellPadding="0" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <div class="imageItem imageLink">
                            <dl>
                                <dd>
                                    <a href='<%=GlobalsPath%><%# Eval("PhotoPath")%>' target="_blank" title="<%# Eval("PhotoName")%>">
                                        <img src='<%=GlobalsPath%>/Admin/PicRar.aspx?P=<%# Eval("PhotoPath")%>&W=140&H=110' />
                                        <asp:HiddenField ID="HiddenFieldImag" Value='<%# DataBinder.Eval(Container.DataItem, "PhotoPath")%> '
                                            runat="server" />
                                    </a>
                                </dd>
                            </dl>
                            <ul>
                                
                                    <%# TruncStr(DataBinder.Eval(Container.DataItem, "PhotoName").ToString(), 20)%><br />
                                <label>
                                    <asp:CheckBox ID="checkboxCol" runat="server" />选择</label>&nbsp;
                                 
                                <script> bindFlashCopyButton("<%# DataBinder.Eval(Container.DataItem, "PhotoPath")%>", 'spcopy<%#Container.ItemIndex+1 %>');</script>
                               
                                <em>
                                   <span style="padding: 6px 0 0 10px; float: left;"><span id="spcopy<%#Container.ItemIndex+1 %>">复制</span></span><a href="javascript:RePlaceImg('<%# DataBinder.Eval(Container.DataItem, "PhotoPath")%>','<%# DataBinder.Eval(Container.DataItem, "PhotoId")%>')" style="margin:0;">
                                        替换</a> <a href="javascript:ReImgName('<%# DataBinder.Eval(Container.DataItem, "PhotoName")%>','<%# DataBinder.Eval(Container.DataItem, "PhotoId")%>')" style="margin:0;">
                                            改名</a>
                                    <!--<a href="javascript:DelImg('<%# DataBinder.Eval(Container.DataItem, "PhotoPath")%>','<%# DataBinder.Eval(Container.DataItem, "PhotoId")%>')">删除</a>-->
                                    <Hi:ImageLinkButton ID="btnDelPhoto" runat="server" Text="删除" IsShow="true" />
                                </em>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <!--图片列表-->
            </div>
            <div class="batchHandleArea">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="77%">
                            <ul>
                                <li class="batchHandleButton"><span class="bottomSignicon"></span><span class="allSelect">
                                    <a href="javascript:void(0)" onclick="CheckClickAll()">全选</a></span> <span class="reverseSelect">
                                        <a href="javascript:void(0)" onclick="CheckReverse()">反选</a></span> <span class="moveSelect">
                                            <a href="javascript:void(0)" onclick="MoveImg()">移动到</a></span> <span class="delete">
                                                <Hi:ImageLinkButton ID="btnDelete2" runat="server" Text="删除" IsShow="true" /></span>
                                </li>
                            </ul>
                        </td>
                        <td width="20%" nowrap="nowrap">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--翻页页码-->
        <div class="page">
            <div class="bottomPageNumber clearfix">
                <div class="pageNumber">
                    <div class="pagination">
                        <UI:Pager runat="server" ShowTotalPages="true" ID="pager" DefaultPageSize="20" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <asp:Button ID="btnSaveImageDataName" runat="server" Text="更换图片" CssClass="submit_sure" />
        <asp:Button ID="btnMoveImageData" runat="server" Text="文件移动" CssClass="submit_sure" />
    </div>
    <!--更改图片名称-->
    <div id="ImageDataWindowName" style="display: none;">
        <div class="frame-content">
            <asp:HiddenField ID="ReImageDataNameId" Value='' runat="server" />
            <p>
                <span class="frame-span frame-input90">图片名称：<em>*</em></span>
                <asp:TextBox name="ReImageDataName" runat="server" Text='' CssClass="forminput" ID="ReImageDataName"
                    Width="250"></asp:TextBox></p>
            <b id="ctl00_contentHolder_ReImageDataNameTip">图片名称不能为空长度限制在30个字符以内</b>
        </div>
    </div>
    <!--图片路径替换-->
    <div id="ImageDataWindowFtp" style="display: none">
        <div class="frame-content">
            <asp:HiddenField ID="RePlaceImg" Value='' runat="server" />
            <asp:HiddenField ID="RePlaceId" Value='' runat="server" />
            <p>
                <span class="frame-span frame-input90">上传图片：<em>*</em></span>
                <asp:FileUpload ID="FileUpload" runat="server" onchange="FileExtChecking(this)" /></p>
        </div>
    </div>
    <!--文件移动-->
    <div id="ImageDataWindowMove" style="display: none">
        <div class="frame-content">
            <p>
                <span class="frame-span frame-input90">选择分类：</span>
                <Hi:ImageDataGradeDropDownList ID="dropImageFtp" runat="server" />
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" language="javascript">

        var formtype = "change";

        function validatorForm() {
            var imgsrc = "", imgid = "";
            switch (formtype) {
                case "change":
                    imgsrc = $("#ctl00_contentHolder_ReImageDataName").val().replace(/\s/g, "");
                    if (imgsrc.length <= 0) {
                        alert("图片名称不允许为空！");
                        return false;
                    }
                    break;
                case "remove":
                    if (!confirm("您确定要移动选中的图片吗？")) {
                        return false;
                    }
                    setArryText('ctl00_contentHolder_dropImageFtp', $("#ctl00_contentHolder_dropImageFtp").val());
                    break;
            };
            return true;
        }
        $(document).ready(function () {
            $("#ImageDataList table td div").mouseover(function () {
                var className = $(this).attr("class");
                if (className.indexOf("imageLink")) {
                    $(this).attr("class", "imageItem imageOver");
                }
            }).mouseout(function () {
                $(this).attr("class", "imageItem imageLink");
            });

        });

        //文件移动
        function MoveImg() {
            formtype = "remove";
            var frm = document.aspnetForm;
            var isFlag = false;
            for (i = 0; i < frm.length; i++) {
                var e = frm.elements[i];
                if (e.checked) {
                    isFlag = true;
                    break;
                }
            }
            if (isFlag) {
                arrytext = null;
                DialogShow("移动图片管理", "imagecmp", 'ImageDataWindowMove', 'ctl00_contentHolder_btnMoveImageData');
            }

            else
                alert("请选择需要移动的图片！");
        }
        //替换
        function RePlaceImg(imgSrc, imgId) {
            DialogFrame("store/ImageReplace.aspx?imgsrc=" + imgSrc + "&imgId=" + imgId, '图片替换', 335, 140);
        }


        //改名
        function ReImgName(imgName, imgId) {
            arrytext = null;
            formtype = "change";
            setArryText('ctl00_contentHolder_ReImageDataName', imgName);
            setArryText('ctl00_contentHolder_ReImageDataNameId', imgId);
            DialogShow('文件名称更改', 'imagecmp', 'ImageDataWindowName', 'ctl00_contentHolder_btnSaveImageDataName');
        }

        //复制
        //function CopyImgUrl(txt) {
        //    var myHerf = window.location.host;
        //    var txt = "http://" + myHerf + applicationPath + txt;
        //    if (window.clipboardData) {
        //        window.clipboardData.clearData();
        //        window.clipboardData.setData("Text", txt);
        //        alert("复制成功！")
        //    }
        //    else if (navigator.userAgent.indexOf("Opera") != -1) {
        //        window.location = txt;
        //    }
        //    else if (window.netscape) {
        //        try {
        //            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        //        }
        //        catch (e) {
        //            alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将 'signed.applets.codebase_principal_support'设置为'true'");
        //        }
        //        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        //        if (!clip)
        //            return;
        //        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        //        if (!trans)
        //            return;
        //        trans.addDataFlavor('text/unicode');
        //        var str = new Object();
        //        var len = new Object();
        //        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        //        var copytext = txt;
        //        str.data = copytext;
        //        trans.setTransferData("text/unicode", str, copytext.length * 2);
        //        var clipid = Components.interfaces.nsIClipboard;
        //        if (!clip)
        //            return false;
        //        clip.setData(trans, null, clipid.kGlobalClipboard);
        //        alert("复制成功！")
        //    }
        //}




        //反选
        function CheckReverse() {
            var frm = document.aspnetForm;
            for (i = 0; i < frm.length; i++) {
                e = frm.elements[i];
                if (e.type == 'checkbox' && e.name.indexOf('checkboxCol') != -1) {
                    if (e.checked == false)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

        //全选
        function CheckClickAll() {
            var frm = document.aspnetForm;
            for (i = 0; i < frm.length; i++) {
                e = frm.elements[i];
                if (e.type == 'checkbox' && e.name.indexOf('checkboxCol') != -1) {
                    e.checked = true;
                }
                if (e.type == 'checkbox' && e.name.indexOf('checkboxHead') != -1)
                    e.checked = false;
            }
        }
        var queueErrorArray;
        var swfu;
        $(function () {

            function loader(thing) {
                var settings = {
                    // Backend Settings
                    upload_url: "ImageData.aspx",
                    use_query_string: false,
                    post_params: {
                        iscallback: "true",
                        typeId: thing,
                        "ASPSESSID": "<%=Session.SessionID %>"                        
                    },
                    // File Upload Settings
                    file_size_limit: "501",
                    file_types: "*.jpg;*.gif;*.png;*.jpeg",
                    file_types_description: "JPG Images",
                    file_upload_limit: "0",    // Zero means unlimited

                    // Event Handler Settings - these functions as defined in Handlers.js
                    // The handlers are not part of SWFUpload but are part of my website and control how
                    // my website reacts to the SWFUpload events.
                    file_queue_error_handler: fileQueueError,
                    file_dialog_complete_handler: fileDialogComplete,
                    upload_progress_handler: uploadProgress,
                    upload_error_handler: uploadError,
                    upload_success_handler: uploadSuccess,
                    upload_complete_handler: uploadComplete,

                    // Button settings
                    button_image_url: "/DialogTemplates/images/swfupload_uploadBtn2.png",
                    button_placeholder_id: "spanButtonPlaceholder",
                    button_width: 63,
                    button_height: 22,

                    default_preview: "/DialogTemplates/images/07.png",

                    // Flash Settings
                    flash_url: "/DialogTemplates/swfupload/swfupload.swf", // Relative to this file
                    custom_settings: { upload_target: "divFileProgressContainer" }
                };
                swfu = new SWFUpload(settings);
                
            };
            $("#ctl00_contentHolder_dropImageFtp2").change(function () {
                swfu.setPostParams({ "typeId": this.value,"iscallback":"true" });
            });
            loader(0);
        });
    </script>
</asp:Content>
