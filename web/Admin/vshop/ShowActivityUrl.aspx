<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowActivityUrl.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.ShowActivityUrl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body {
        font-size:12px;font-family:'Microsoft YaHei';
        }
    </style>
    <script type="text/javascript" src="/Utility/swfupload/swfobject.js"></script>
<script type="text/javascript">
    function copySuccess() {
        alert("该链接地址已经复制，你可以使用Ctrl+V 粘贴！");
    }
    var flashvars = {
        content: encodeURIComponent("<%=htmlActivityUrl %>"),
        uri: '/Utility/swfupload/lkcopy.jpg'
    };
    var params = {
        wmode: "transparent",
        allowScriptAccess: "always"
    };
    swfobject.embedSWF("/Utility/swfupload/clipboard.swf", "spanCopy", "34", "17", "9.0.0", null, flashvars, params);
   
</script>
</head>
<body>
     <p><%=htmlActivityUrl %><span style="padding-top: 20px; vertical-align: top;"><span id="spanCopy">复制链接</span></span>
            </p>

            <table>
                <tr>
                    <td>
                        <img id="imgsrc" src="http://s.jiathis.com/qrcode.php?url=<%=Server.UrlEncode(htmlActivityUrl) %>" type="img" width="300px" /></td>
                </tr>
            </table>
</body>
</html>