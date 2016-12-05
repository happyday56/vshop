<%@ Page Language="C#"  MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="DistributorLogo.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorLogo" %>

 
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <script src="../../Utility/swfupload/swfupload.js" type="text/javascript"  charset="gbk"></script>
    <script src="../../Utility/swfupload/DisLogoupload.js" type="text/javascript"  charset="gbk"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <asp:HiddenField ID="hidpic" runat="server" />
     <asp:HiddenField ID="hidpicdel" runat="server" />
    <div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/03.gif" width="32" height="32" /></em>
            <h1>添加分销商Logo图片</h1>
            <span class="font">管理员可为分销商上传1张Logo图片</span>
      </div>
          <div class="formitem validator2">
            <ul>
              <li><span class="formitemtitle Pw_100">图片上传：</span>
				        <span id="spanButtonPlaceholder"></span>
		            <div id="divFileProgressContainer"  > 
		           
	            </div>图片上传最多1张
                </li>
            <li><span class="formitemtitle Pw_100">显示上传图片：</span>
               建议上传80*80的图片
              <p id="imgall" style="height:100px"></p>
           
              </li>
            </ul>
              <ul class="btn Pa_100 clearfix">
               <asp:Button ID="btnSave" runat="server"   Text="保存"  CssClass="submit_DAqueding float" />
         </ul>
          </div>
  </div>
        
  </div>
    
    <script type="text/javascript">
        var swfu;
        $(function () {
            function loader(url) {
                var settings = {
                    upload_url: "DistributorLogoUpload.aspx",
                    post_params: {

                        imgurl: url
                    },
                    use_query_string: true,
                    file_size_limit: "2 MB",
                    file_types: "*.jpg;*.gif;*.png;*.jpeg",
                    file_types_description: "JPG Images",
                    file_upload_limit: "0",

                    file_queue_error_handler: fileQueueError,
                    file_dialog_complete_handler: fileDialogComplete,
                    upload_progress_handler: uploadProgress,
                    upload_start_handler: uploadStart,
                    upload_error_handler: uploadError,
                    upload_success_handler: uploadSuccess,
                    upload_complete_handler: uploadComplete,
                    button_image_url: "/Admin/images/swfupload_uploadBtn.png",
                    button_placeholder_id: "spanButtonPlaceholder",
                    button_width: 160,
                    button_height: 22,
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,
                    flash_url: "/Utility/swfupload/swfupload.swf",
                    custom_settings: {
                        upload_target: "divFileProgressContainer"
                    },
                    debug: false
                };
                swfu = new SWFUpload(settings);
            };

            if ($("#ctl00_contentHolder_hidpic").val() != "") {
                var pic = $("#ctl00_contentHolder_hidpic").val();
                var imgarry = pic.split("|");
                imgnum = imgarry.length - 1;
                loader(imgnum);
                for (var i = 0; i < imgarry.length; i++) {
                    if (imgarry[i] != "")
                        $("#imgall").append("<span id='span" + i + "' style='margin-right:10px'><img src='" + imgarry[i] + "' width='180' height='120'/></br><a style=\"text-align:center;cursor: pointer;\" onclick=EditDelImg('" + imgarry[i] + "|','span" + i + "')><span style=\"width: 100%; font-size:12px;\">删除</span></a></span>");
                }
            }
            else {
                loader(0);
            }
        });
                function EditDelImg(obj, spanid) {

                    var alldelpic = $("#ctl00_contentHolder_hidpicdel").val();
                    alldelpic += obj;
                    $("#ctl00_contentHolder_hidpicdel").val(alldelpic);
                    var pic = $("#ctl00_contentHolder_hidpic").val();
                    var delimg = pic.replace(obj, "");
                    $("#ctl00_contentHolder_hidpic").val(delimg);
                    $("#" + spanid).remove();
                    imgnum--;
                    swfu.setPostParams({ "imgurl": imgnum });
                }
        
    </script>
</asp:Content>
