<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="branchAddDistributors.aspx.cs" Inherits="Hidistro.UI.Web.Admin.branchAddDistributors" %>

<%@ Import Namespace="Hidistro.Core" %>
<%@ Import Namespace="Hidistro.Entities.Sales" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
  <div class="dataarea mainwidth databody">
    <div class="title m_none td_bottom"> 
      <em><img src="../images/06.gif" width="32" height="32" /></em>
      <h1>批量生成分销商账号</h1>
      <span>可以按不同规则批量制作一批分销商账号,生成的账号默认密码统一为"888888"</span>
    </div>
    <div class="datafrom">
    <div class="formitem">
    <ul>
    <li><span class="formitemtitle Pw_100">生成规则：</span><input type="radio" name="radioadd" value="1" onclick="selecttype(1)" checked="true"  id="radionumber" runat="server" />指定数量生成<input type="radio" name="radioadd" value="2" onclick="selecttype(2)" id="radioaccount" runat="server"  />指定账号生成</li>
    </ul>
     <ul>
    <li><span class="formitemtitle Pw_100">推荐分销商：</span><input type="text" id="txtslsdistributors" class="forminput" name="txtslsdistributors" runat="server" autocomplete="off" /></li>
    </ul>
    <ul>
    <li><span class="formitemtitle Pw_100">输入生成数量：</span><input type="text" class="forminput" name="txtnumber" id="txtnumber" runat="server" />
     <p class="Pa_100">请输入1~999的正整数</p>
    </li>
    </ul>
      <ul>
        <li> <span class="formitemtitle Pw_100">指定账号生成：</span>
        <textarea id="txtdistributornames" name="txtdistributornames" rows="8" wrap="off" cols="50" runat="server"></textarea>
          <p class="Pa_100">每个分销商账号在2~10个英文字符，一行一个</p>
        </li>
        </ul>
                <ul class="btntf Pa_100"><li><asp:Button runat="server" ID="batchCreate" Text="生 成" class="submit_DAqueding inbnt" OnClientClick="return vailidbatch()" /><asp:Button runat="server" ID="btnExport" Visible="false" Text="导出分销商" class="submit_DAqueding inbnt"/></li>      
          </ul>
      </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
<Hi:Script runat="server" Src="/utility/jquery.bigautocomplete.js"></Hi:Script>
<hi:style ID="Style1" runat="server" href="/utility/jquery.bigautocomplete.css" media="screen" />
<script>
    function selecttype(typeselect) {
        if (typeselect == 1) {
            $("#ctl00_contentHolder_txtnumber").attr("disabled", false);
            $("#ctl00_contentHolder_txtdistributornames").attr("disabled", true);
        } else {
            $("#ctl00_contentHolder_txtnumber").attr("disabled", true);
            $("#ctl00_contentHolder_txtdistributornames").attr("disabled", false);
        }
    }

    $(function () {
        selecttype($("input[type='radio']:checked").val());
        var searchajax = "?action=SearchKey";
        $("#ctl00_contentHolder_txtslsdistributors").bigAutocomplete({ url: searchajax, width: 443});

        $("#ctl00_contentHolder_txtslsdistributors").keypress(function (e) {
            if (e.which == 13) {
                return false;
            }
        });
    });

    function vailidbatch(){
        if ($("input[type='radio']:checked").val() == "1") {//输入数量
            if ($("#ctl00_contentHolder_txtnumber").val().replace(/\s/g, "") == ""
             || isNaN($("#ctl00_contentHolder_txtnumber").val().replace(/\s/g, ""))
             || parseInt($("#ctl00_contentHolder_txtnumber").val().replace(/\s/g, ""))<=0
             || parseInt($("#ctl00_contentHolder_txtnumber").val().replace(/\s/g, ""))>999) {
                alert('请输入1~999的正整数！');
                return false;
            }
        } else { //输入指定账号
            if ($("#ctl00_contentHolder_txtdistributornames").val().replace(/\s/g, "") == "") {
                alert('请输入要生成的分销账号！');
                return false;
            }
            var tag = true;
            var regA = /[\w\d]/,
                regB = /(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)/;
            var arr=$("#ctl00_contentHolder_txtdistributornames").val().split('\n');
            $.each(arr, function (i, e) {
                var is = regA.test(e) || regB.test(e),
                    len = e.length;
                var a = (len >= 2) && (len <= 10) ? true : false;
                if (!is || !a) {
                    alert('出现非法字符或长度不符合,请检查！');
                    tag = false;
                    return false;
                }
            });
            return tag
        }
    }


</script>
</asp:Content>
