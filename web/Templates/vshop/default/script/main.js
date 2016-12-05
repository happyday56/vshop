//隐藏右上角菜单
document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
    WeixinJSBridge.call('showOptionMenu');
});
var selectProdcut = ($.cookie("SelectProcutId") == null || $.cookie("SelectProcutId")=="") ? {} : JSON.parse($.cookie("SelectProcutId"));

$(document).ready(function () {
    $("footer .glyphicon-refresh").click(function () {
        location.reload();
    })

    $("footer .glyphicon-arrow-left").click(function () {
        history.go(-1);
    })

    /*friends-circle*/
    var Width = $('.firends-circle .content div').width();
    $('.firends-circle .content div').height(Width);
/*index*/
 var ImgWidth = $('.index-content img').width();
 $('.index-content img').height(ImgWidth);
    
    
    /*fill-information*/
    var Width1 = $('.distributor .content div').width();
   $('.distributor .content div').height(0.5 * Width1);
   $('.distributor .content div.disactive').height((0.5 * Width1)-4);
    $('.distributor .content div').click(function () {
        if ($(this).hasClass("disactive")) {
            $(this).removeClass("disactive");

        } else {
            $(this).addClass("disactive");
            $(this).find('span').show();
            $(this).siblings().removeClass('disactive');
        }
    });

    /*my-goods*/
    $("input[type='checkbox']").iCheck({
        checkboxClass: 'icheckbox_flat-red',
        radioClass: 'iradio_flat-red'
    });

    $('.search_img').click(function () {
        $(this).toggleClass('color');
    })

    $("input[type='checkbox'][name='CheckAll']:eq(0)").on('ifChecked', function (a) {
        $("input[name='CheckGroup']").iCheck('check');
    });
    $("input[type='checkbox'][name='CheckAll']:eq(0)").on('ifUnchecked', function (a) {
        $("input[name='CheckGroup']").iCheck('uncheck');
        $("input[type='checkbox'][name='CheckAll']:eq(0)").attr("checked", false);
        $("input[type='checkbox'][name='CheckAll']:eq(0)").parent("div").removeClass("checked");
    });
    $("input[name='CheckGroup']").on('ifChecked', function (event) {
        selectProdcut["CheckGroup" + $(this).val()] = $(this).val();
        UpdateCookieProductId();
  


        var real_h;
        var right_H = $(this).parent().parent().prev().height();
        
        $(this).parent().parent().height(right_H);
        var right_W = $(this).parent().parent().prev().width();
        $(this).parent().parent().width(right_W);
        real_h = (right_H - $(this).parent().height()) * 0.5;
        $(this).parent().css({ position: 'absolute', top: real_h, left: right_W/2 - 10 });



        $(this).parent("div").parent("div").css("display", "block");
    });
    $("input[name='CheckGroup']").on('ifUnchecked', function (event) {
        $(this).parent("div").parent("div").css({ display: 'none' });
        delete selectProdcut["CheckGroup" + $(this).val()];
        UpdateCookieProductId();
    });

    $(".index-content").click(function () {
        $(this).next().find("input[type='checkbox']").iCheck('check');
    });
    $(".right").click(function () {
        $(this).css("display", "none");
        $(this).find("input[type='checkbox']").iCheck('uncheck');
    });

});
setTimeout("CheckShow()",150);
function CheckShow() {
    if (selectProdcut != null) {
        $.each(selectProdcut, function (index, items) {
   
       
            $("input[type='checkbox'][name='CheckGroup'][value='" + items + "']").iCheck('check');
        });
    }
 
}

function UpdateCookieProductId() {
    var valstr = JSON.stringify(selectProdcut);
    $.cookie("SelectProcutId", valstr);
}

function goUrl(url) {
    window.location.href = url;
}

//检查申请提现
function RequestCommissions(obj) {
    if (obj==1 && $("#VRequestCommissions_txtaccount").val().replace(/\s/g, "") == "") {
        alert_h('请输入银行卡号');
        return false;
    }
    if (obj == 1 && $("#VRequestCommissions_txtbankname").val().replace(/\s/g, "") == "") {
        alert_h('请输入提现银行');
        return false;
    }
    if (obj == 1 && $("#VRequestCommissions_txtbankaddress").val().replace(/\s/g, "") == "") {
        alert_h('请输入开户行');
        return false;
    }
    if (obj == 1 && $("#VRequestCommissions_txtaccountname").val().replace(/\s/g, "") == "") {
        alert_h('请输入持卡人姓名');
        return false;
    }
    if (obj == 1 && $("#region").val().replace(/\s/g, "") == "") {
        alert_h('请选择开户行所在地');
        return false;
    }

    var requesttype = obj;
    var account = $("#VRequestCommissions_txtaccount").val();
    var bankname = $("#VRequestCommissions_txtbankname").val();
    var bankaddress = $("#VRequestCommissions_txtbankaddress").val();
    var accountname = $("#VRequestCommissions_txtaccountname").val();
    var mentionnowmoney = $("#VRequestCommissions_txtmentionnowmoney").val();
    var regionaddress = $("#region").val();
    
    var money = 0;
    if (requesttype == 0) {
        account = "";
      
        if ($("#VRequestCommissions_txtmoneyweixin").val().replace(/\s/g, "") == "") {
            alert_h('请输入提现金额');
            return false;
        }
        if (isNaN($("#VRequestCommissions_txtmoneyweixin").val().replace(/\s/g, ""))
  || parseFloat($("#VRequestCommissions_txtmoneyweixin").val().replace(/\s/g, "")) <= 0
 ) {
            alert_h('请输入大于等于纯数字类型');
            return false;
        }
        if ($.trim($("#VRequestCommissions_txtmoneyweixin").val()).search("^[0-9]*[1-9][0-9]*$") != 0) {
            alert_h("请输入一个整数!");
            return false;
        }
        var commissions = $("#maxmoney").text();
        if (parseFloat($("#VRequestCommissions_txtmoneyweixin").val()) > parseFloat(commissions)) {
            alert_h('提现金额不允许超过现有佣金金额');
            return false;
        }

        var placehoder = $("#VRequestCommissions_txtmoneyweixin").val();
        if (placehoder != "" && placehoder != "undefined") {
            if (placehoder % $("#VRequestCommissions_txtmentionnowmoney").val() != 0) {
                alert_h('请输入大于等于' + $("#VRequestCommissions_txtmentionnowmoney").val() + '元的整数倍金额！');
                return false;
            }
            if (parseFloat($("#VRequestCommissions_txtmoneyweixin").val()) < parseFloat(placehoder)) {
                alert_h('请输入大于等于' + placehoder + '元的金额！');
                return false;
            }
        }
        money = $("#VRequestCommissions_txtmoneyweixin").val();
    }
    else {

        if ($("#VRequestCommissions_txtmoney").val().replace(/\s/g, "") == "") {
            alert_h('请输入提现金额');
            return false;
        }
        if (isNaN($("#VRequestCommissions_txtmoney").val().replace(/\s/g, ""))
  || parseFloat($("#VRequestCommissions_txtmoney").val().replace(/\s/g, "")) <= 0
 ) {
            alert_h('请输入大于等于纯数字类型');
            return false;
        }
        if ($.trim($("#VRequestCommissions_txtmoney").val()).search("^[0-9]*[1-9][0-9]*$") != 0) {
            alert_h("请输入一个整数!");
            return false;
        }
        var commissions = $("#maxmoney").text();
        if (parseFloat($("#VRequestCommissions_txtmoney").val()) > parseFloat(commissions)) {
            alert_h('提现金额不允许超过现有佣金金额');
            return false;
        }

        var placehoder = $("#VRequestCommissions_txtmoney").val();
        if (placehoder != "" && placehoder != "undefined") {
            if (placehoder % $("#VRequestCommissions_txtmentionnowmoney").val() != 0) {
                alert_h('请输入大于等于' + $("#VRequestCommissions_txtmentionnowmoney").val() + '元的整数倍金额！');
                return false;
            }
            if (parseFloat($("#VRequestCommissions_txtmoney").val()) < parseFloat(placehoder)) {
                alert_h('请输入大于等于' + placehoder + '元的金额！');
                return false;
            }
        }
        money = $("#VRequestCommissions_txtmoney").val();
    }
    
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: {
            requesttype: requesttype,
            action: "AddCommissions",
            account: account,
            bankname: bankname,
            bankaddress: bankaddress,
            accountname: accountname,
            commissionmoney: money,
            regionaddress: regionaddress
        },
        success: function (resultData) {
            if (resultData.success) {
                alert_h(resultData.msg, function () {
                    location.href = "DistributorCenter.aspx";
                });
            }
            else {
                alert_h(resultData.msg);
            }
        }
    });

}


function myConfirm(title, content, ensureText, ensuredCallback) {
    var myConfirmCode = '<div class="modal fade" id="myConfirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">\
                  <div class="modal-dialog">\
                    <div class="modal-content">\
                      <div class="modal-header">\
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>\
                        <h4 class="modal-title" id="myModalLabel">' + title + '</h4>\
                      </div>\
                      <div class="modal-body">\
                        ' + content + '\
                      </div>\
                      <div class="modal-footer">\
                        <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>\
                        <button type="button" class="btn btn-danger">' + ensureText + '</button>\
                      </div>\
                    </div>\
                  </div>\
                </div>';
    if ($("#myConfirm").length == 0) {
        $("body").append(myConfirmCode);
    }
    $('#myConfirm').modal();
    $('#myConfirm button.btn-danger').unbind("click", "");
    $('#myConfirm button.btn-danger').click(function (event) {
        if (ensuredCallback)
            ensuredCallback();
        $('#myConfirm').modal('hide')
    });
}



function alert_h(content, ensuredCallback) {
    var myConfirmCode = '<div class="modal fade" id="alert_h" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">\
                  <div class="modal-dialog">\
                    <div class="modal-content">\
                      <div class="modal-header">\
                        <h5 class="modal-title" id="myModalLabel">操作提示</h5>\
                      </div>\
                      <div class="modal-body" style="font-size:14px;">\
                        ' + content + '\
                      </div>\
                      <div class="modal-footer">\
                        <button type="button" class="btn btn-danger" data-dismiss="modal">确认</button>\
                      </div>\
                    </div>\
                  </div>\
                </div>';

    if ($("#alert_h").length != 0) {
        $('#alert_h').remove();
    }
    $("body").append(myConfirmCode);
    $('#alert_h').modal();

    $('#alert_h').off('hide.bs.modal').on('hide.bs.modal', function (e) {
        if (ensuredCallback)
            ensuredCallback();
    });
}

function ShowDialog(title, ensureText, content_id, ensuredCallback) {
    var clonecontent = $('#' + content_id).css({display:'block'}).clone();
    $('#' + content_id).replaceWith('<div id="msgtable"></div>');
    $("#myConfirm").remove();
    var myConfirmCode = '<div class="modal fade" id="myConfirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">\
                  <div class="modal-dialog">\
                    <div class="modal-content">\
                      <div class="modal-header">\
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>\
                        <h4 class="modal-title" id="myModalLabel">' + title + '</h4>\
                      </div>\
                      <div class="modal-body">\
                        ' + clonecontent[0].outerHTML + '\
                      </div>\
                      <div class="modal-footer">\
                        <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>\
                        <button type="button" class="btn btn-danger">' + ensureText + '</button>\
                      </div>\
                    </div>\
                  </div>\
                </div>';
    
    $("body").append(myConfirmCode);
    $('#myConfirm').modal();
    $('#myConfirm button.btn-danger,#myConfirm button.btn-default').unbind("click", "");
    $('#myConfirm button.btn-default').click(function () {
        $("#" + content_id).remove();
        $("#msgtable").replaceWith($(clonecontent).css({display:'none'})[0].outerHTML);
    });
    $('#myConfirm button.btn-danger').click(function (event) {
        if (ensuredCallback) {
            if (ensuredCallback()) {
                $('#myConfirm').modal('hide');
                $("#" + content_id).remove();
                $("#msgtable").replaceWith(clonecontent[0].outerHTML);
            }
        }
    });
}


var pageLoadTime;
var passedSeconds = 0;

function GetRTime() {
    var d;
    var h;
    var m;
    var s;

    var startVal = document.getElementById("startTime").value;
    var endVal = document.getElementById("endTime").value;
    var startTime = new Date(startVal);
    var endTime = new Date(endVal); //截止时间 前端路上 http://www.51xuediannao.com/qd63/
    var nowTime = new Date($('#nowTime').val());
    nowTime.setSeconds(nowTime.getSeconds() + passedSeconds);
    passedSeconds++;
    var now_startTime = nowTime.getTime() - startTime.getTime();    //当前时间 减去开始时间
    var s_nTime = startTime.getTime() - nowTime.getTime();          //开始时间减去当前时间
    var start_endTime = endTime.getTime() - startTime.getTime();    //结束时间减去开始时间
    var now_endTime = endTime.getTime() - nowTime.getTime();     //结束时间减去当前时间
    var now_pTime = nowTime.getTime() - pageLoadTime;               //当前时间减去页面刷新时间
    var p_sTime = startTime.getTime() - pageLoadTime;               //开始时间减去页面刷新时间
    var wid = now_startTime / start_endTime * 100;                    //开始后离结束的时间比
    var wid1 = now_pTime / p_sTime * 100;                             //未开始离开始的时间比
    var tuan_button = document.getElementById("buyButton");
    var progress = document.getElementById("progress");
    var tuan_time = document.getElementById("tuan_time");
    function docu() {
        document.getElementById("t_d").innerHTML = d + "天";
        document.getElementById("t_h").innerHTML = h + "时";
        document.getElementById("t_m").innerHTML = m + "分";
        document.getElementById("t_s").innerHTML = s + "秒";
    }
    if (pageLoadTime == null) {
        pageLoadTime = nowTime;
    }
    if (100 >= wid1 >= 0 && wid < 0) {
        d = Math.floor(Math.abs(now_startTime) / 1000 / 60 / 60 / 24);
        h = Math.floor(Math.abs(now_startTime) / 1000 / 60 / 60 % 24);
        m = Math.floor(Math.abs(now_startTime) / 1000 / 60 % 60);
        s = Math.floor(Math.abs(now_startTime) / 1000 % 60);
        docu();
        tuan_time.innerHTML = "团购开始时间：";
        progress.style.width = wid1 + "%";
        tuan_button.disabled = true;
    }
    if (wid1 > 100 || wid1 < 0) {
        if (wid >= 0 && wid < 70) {
            d = Math.floor(now_endTime / 1000 / 60 / 60 / 24);
            h = Math.floor(now_endTime / 1000 / 60 / 60 % 24);
            m = Math.floor(now_endTime / 1000 / 60 % 60);
            s = Math.floor(now_endTime / 1000 % 60);
            docu();
            tuan_time.innerHTML = "团购结束时间：";
            progress.style.width = (100 - wid) + "%";
            tuan_button.disabled = false;
        } else if (wid >= 70 && wid < 90) {
            d = Math.floor(now_endTime / 1000 / 60 / 60 / 24);
            h = Math.floor(now_endTime / 1000 / 60 / 60 % 24);
            m = Math.floor(now_endTime / 1000 / 60 % 60);
            s = Math.floor(now_endTime / 1000 % 60);
            docu();
            tuan_time.innerHTML = "团购结束时间：";
            progress.className = "progress-bar progress-bar-warning";
            progress.style.width = (100 - wid) + "%";
            tuan_button.disabled = false;
        } else if (wid >= 90 && wid <= 100) {
            d = Math.floor(now_endTime / 1000 / 60 / 60 / 24);
            h = Math.floor(now_endTime / 1000 / 60 / 60 % 24);
            m = Math.floor(now_endTime / 1000 / 60 % 60);
            s = Math.floor(now_endTime / 1000 % 60);
            docu();
            tuan_time.innerHTML = "团购结束时间：";
            progress.style.width = (100 - wid) + "%";
            progress.className = "progress-bar progress-bar-danger";
            tuan_button.disabled = false;
        }

        if (wid > 100) {
            tuan_time.innerHTML = "团购已结束!";
            progress.style.width = 0;
            tuan_button.disabled = true;
        }
    }

}

 
function getParam(paramName) {
    paramValue = "";
    isFound = false;
    paramName = paramName.toLowerCase();
    var arrSource = this.location.search.substring(1, this.location.search.length).split("&");
    if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
        if (paramName == "returnurl") {
            var retIndex = this.location.search.toLowerCase().indexOf('returnurl=');
            if (retIndex > -1) {
                var returnUrl = unescape(this.location.search.substring(retIndex + 10, this.location.search.length));
                if ((returnUrl.indexOf("http") != 0) && returnUrl != "" && returnUrl.indexOf(location.host.toLowerCase()) == 0) returnUrl = "http://" + returnUrl;
                return returnUrl;
            }
        }
        i = 0;
        while (i < arrSource.length && !isFound) {
            if (arrSource[i].indexOf("=") > 0) {
                if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                    paramValue = arrSource[i].toLowerCase().split(paramName + "=")[1];
                    paramValue = arrSource[i].substr(paramName.length + 1, paramValue.length);
                    isFound = true;
                }
            }
            i++;
        }
    }
    return paramValue;
}
//页面遮罩层
function maskayer(obj)
{
    if (obj == 0) {
        $("#BgDiv1").css({ display: "block", height: $(document).height() });
        var yscroll = document.documentElement.scrollTop;
        var screenx = $(window).width();
        var screeny = $(window).height();
        $(".DialogDiv").css("display", "block");
        $(".DialogDiv").css("top", yscroll + "px");
        var DialogDiv_width = $(".DialogDiv").width();
        var DialogDiv_height = $(".DialogDiv").height();
        $(".DialogDiv").css("left", (screenx / 2 - DialogDiv_width / 2) + "px")
        $(".DialogDiv").css("top", (screeny / 2 - DialogDiv_height / 2) + "px")
    }
    else {
        
        $(".DialogDiv").css("display", "none");
        $("#BgDiv1").css( "display", "none");
    }
  
}
$(function(){
    $(".mid-nav .mid-nav-tags li").width($(".mid-nav").width());
    $(".mid-nav .mid-nav-tags").width(($(".mid-nav .mid-nav-tags li").width()*$(".mid-nav .mid-nav-tags li").length)+$(".mid-nav").width()*0.1);

    $(".right-tags li").height($(".right-tags li").width());


    var num = $(".mid-nav .mid-nav-tags li").length-4;
    var i = 1;

    $(".mid-nav").bind("swipeleft",function(){
        if( i > num ){
            i = num;
        }
        $(this).children(".mid-nav-tags").animate({"left":"-" + ($(".mid-nav").width()*0.33)*i + "px"},200);
        i++;
    });

    $(".mid-nav").bind("swiperight",function(){
        i--;
        if( i < 0 ){
            i = 0;
        }
        $(this).children(".mid-nav-tags").animate({"left":"-" + ($(".mid-nav").width()*0.33)*i + "px"},200);
    });
})
