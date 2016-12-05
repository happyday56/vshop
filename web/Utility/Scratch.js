function dload() {
    /**判断浏览器是否支持canvas**/
    try {
        document.createElement('canvas').getContext('2d');
    } catch (e) {
        var addDiv = document.createElement('div');
        alert_h('您的手机不支持刮刮卡效果哦~!');
    }
};

var IsPrized = false;
function Prized() {
    var index =Number(getPrize());
    switch (index) {
        case 1:
            $(".textScratch").text("三等奖");
            break;
        case 3:
            $(".textScratch").text("四等奖");
            break;
        case 5:
            $(".textScratch").text("五等奖");
            break;
        case 7:
            $(".textScratch").text("六等奖");
            break;
        case 9:
            $(".textScratch").text("一等奖");
            break;
        case 11:
            $(".textScratch").text("二等奖");
            break;
        case -1:
//            alert_h("您已经达到抽奖次数上限", function () {
//                location.href = "/vshop/default.aspx";
//            });
//            
            break;
        case -2:
            alert_h("您未登录或者登录超时，请重新从微信进入！", function () {
                location.href = "/vshop/default.aspx";
            });
            
            break;
        default:
            $(".textScratch").text("继续努力");
            break;
    }
}

$(document).ready(function () { dload(); Prized();});
 
var u = navigator.userAgent, mobile = '';
if (u.indexOf('iPhone') > -1) mobile = 'iphone';
if (u.indexOf('Android') > -1 || u.indexOf('Linux') > -1) mobile = 'Android';

function createCanvas(parent, width, height) {
    var canvas = {};
    canvas.node = document.createElement('canvas');
    canvas.context = canvas.node.getContext('2d');
    canvas.node.width = width || 100;
    canvas.node.height = height || 100;
    parent.appendChild(canvas.node);
    return canvas;
}

var _x, _y;
var canvas;
var ctx;
function reset() {
    ctx.globalCompositeOperation = 'source-over';
    ctx.clearTo("#c8c8c8");
}

function init(container, width, height, fillColor, type) {
    canvas = createCanvas(container, width, height);
    ctx = canvas.context;
    // define a custom fillCircle method 
    ctx.fillCircle = function (x, y, radius, fillColor) {
        this.fillStyle = fillColor;
        this.beginPath();
        this.moveTo(x, y);
        this.arc(x, y, radius, 0, Math.PI * 2, false);
        this.fill();
    };
    ctx.clearTo = function (fillColor) {
        ctx.fillStyle = fillColor;
        ctx.fillRect(0, 0, width, height);
        ctx.fillStyle = "#a0a0a0";
        ctx.font = "14px Georgia";
        ctx.fillText("刮开中大奖!", width / 2-35, height / 2+5);
    };
    ctx.clearTo(fillColor || "#ddd");
    canvas.node.addEventListener("touchstart", function (e) {
        canvas.isDrawing = true;
        e.preventDefault();
        if (type == 'Android') {
            _x = e.changedTouches[0].pageX - this.offsetLeft;
            _y = e.changedTouches[0].pageY - this.offsetTop;
        } else {
            _x = e.pageX - this.offsetLeft;
            _y = e.pageY - this.offsetTop;
        }
    }, false);
    canvas.node.addEventListener("touchend", function (e) {
        e.preventDefault();
        canvas.isDrawing = false;
       

    }, false);
    canvas.node.addEventListener("touchmove", function (e) {
        e.preventDefault();
        if (!canvas.isDrawing) {
            return;
        }
        if (type == 'Android') {
            var x = e.changedTouches[0].pageX - this.offsetLeft;
            var y = e.changedTouches[0].pageY - this.offsetTop;
        } else {
            var x = e.pageX - this.offsetLeft;
            var y = e.pageY - this.offsetTop;
        }
        ctx.globalCompositeOperation = 'destination-out';
        ctx.lineWidth = 10;
        ctx.lineCap = "round";
        ctx.strokeStyle = "#c8c8c8";
        ctx.beginPath();
        ctx.moveTo(_x, _y);
        ctx.lineTo(x, y);
        ctx.stroke();
        _x = x;
        _y = y;
        var _w = parseInt(canvas.node.width / 20);
        var _h = parseInt(canvas.node.height / 20);
        var _c = _w * _h;
        var _c1 = 0;
        for (var i = 0; i < _w; i++) {
            for (var j = 0; j < _h; j++) {
                var imgData = ctx.getImageData(i * 20 + 1, j * 20 + 1, 1, 1);
                var alpha = imgData.data[3];
                if (alpha < 255) {
                    _c1 = _c1 + 1;
                }
            }
        }
        if (_c1 > (_c / 4)) {
            ctx.globalCompositeOperation = 'destination-out';
            ctx.fillStyle = fillColor;
            ctx.fillRect(0, 0, width, height);
            if (!IsPrized) {
                Prizerecord();
                if ($(".textScratch").text() != "继续努力") {
                    alert_h("恭喜你获得" + $(".textScratch").text() + "!", function () {
                        window.location.href = "/Vshop/WinningResults.aspx?activityid=" + GetActivityid();
                    });

                }
                else {
                    alert_h("亲，就差一点点就中奖了，继续努力吧！", function () {
                        window.location.href = window.location.href;
                    });
                    
                }
            }
            else {
                return;
            }

        }
    }, false);
}
var container = document.getElementById('canvasScratch');
init(container, 190, 40, '#c8c8c8', mobile);


function GetActivityid() {
    var activityid = window.location.search.substr(window.location.search.indexOf("=") + 1);
    if (activityid.indexOf("&") > 0)
        activityid = activityid.substr(0, activityid.indexOf("&"));
    return activityid;
}

function getPrize() {
    var no = 0;
    var activityid = GetActivityid();
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "GetPrize", "activityid": activityid,activitytype:"scratch"},
        async: false,
        success: function (resultData) {
            no = resultData.No;
        }
    });
    return no;
}



function Prizerecord() { 
    IsPrized = true;
    $.ajax({
        url: "/API/VshopProcess.ashx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "AddUserPrize", "activityid": GetActivityid(), prize: $(".textScratch").text() },
        async: false,
        success: function (resultData) {

            if (resultData.Status == "OK")
                return true;
        }
    });
    return false;
}