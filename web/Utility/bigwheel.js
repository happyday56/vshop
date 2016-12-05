
window.onload = function () {
    init();
}
  
  var startAngle = 0;
  var arc = Math.PI / 6;
  var spinTimeout = null;
  
  var spinArcStart = 10;
  var spinTime = 0;
  var spinTimeTotal = 0;
  
  var ctx;
  var isRunning = false;
  var u = navigator.userAgent,mobile = '';

  function init() {
      var canvas = document.getElementById("wheelcanvas");
      if (u.indexOf('iPhone') > -1) mobile = 'iphone';
      if (u.indexOf('Android') > -1 || u.indexOf('Linux') > -1) mobile = 'Android';

      canvas.addEventListener("touchstart", function (e) { //touchstart  mousedown
          e.preventDefault();
          if (isRunning)
              return;
          var _x, _y;
          if (mobile == 'Android') {
              _x = e.changedTouches[0].pageX - this.offsetLeft;
              _y = e.changedTouches[0].pageY - this.offsetTop;
          } else {
              _x = e.pageX - this.offsetLeft;
              _y = e.pageY - this.offsetTop;
          }
          var num = Math.sqrt((_x - 130) * (_x - 130) + (_y - 130) * (_y - 130));
          if (num <= 40) {
              setTimeout(function () {
                  spin();

              }, 300);
          }
      }, false);
      drawRouletteWheel();
  }
  
  function drawRouletteWheel() {
    var canvas = document.getElementById("wheelcanvas");
    var bgImg = document.getElementById("vBigWheel_bgimg");
    var flagImg = document.getElementById("flagimg");
    ctx = canvas.getContext("2d");
    var textRadius = 110;

    if (canvas.getContext) {
		ctx.clearRect(0,0,canvas.width,canvas.height);
		ctx.save();
		ctx.translate(130,130);
		ctx.rotate(startAngle);
		ctx.drawImage(bgImg,-bgImg.width/2,-bgImg.height/2);

		ctx.restore();

        ctx.save();
		ctx.translate(130,130);
		ctx.drawImage(flagImg,-flagImg.width/2,-flagImg.height/2-14);
        ctx.restore();
	}
  }

  function spin() {
      var no = parseInt(getPrize());
      if (no == -1) {
          alert_h("您已经达到抽奖次数上限");
          
      }
      else if (no == -2) {
          alert_h("您未登录或者，登录超时，请重新从微信进入！");
          
      }
      else if (no == -3) {
          alert_h("对不起，活动还未开始，或者已经结束！", function () {
              location.href = "/vshop/default.aspx";
          });
      }
      else {
          var r = Math.random() * 100;
          if (r > 70)
              r = (r % 70) / 100;
          else
              r = r / 100;
          r = 0.35 - r;
          spinAngleStart = 8.744 * 1 + 8.744 / 12 * (11 - ((2 + no) % 12)) + r;
          spinTime = 0;
          startAngle = 0;
          spinTimeTotal = 5000;
          isRunning = true;
          rotateWheel();
      
      }

  }
  
  function rotateWheel() {
    spinTime += 30;
    if(spinTime >= spinTimeTotal) {
      stopRotateWheel();
      return;
    }
    var spinAngle = spinAngleStart - easeOut(spinTime, 0, spinAngleStart, spinTimeTotal);
    startAngle += (spinAngle * Math.PI / 180);
    drawRouletteWheel();
    spinTimeout = setTimeout('rotateWheel()', 30);
  }

  function stopRotateWheel() {
      clearTimeout(spinTimeout);
      var degrees = startAngle * 180 / Math.PI + 90 - 15;
      var arcd = arc * 180 / Math.PI;
      var index = Math.floor((360 - degrees % 360) / arcd);
      switch (index) {
          case 1:
              alert_h("三等奖", function () { gotoResult(); });
              break;
          case 3:
              alert_h("四等奖", function () { gotoResult(); });
              break;
          case 5:
              alert_h("五等奖", function () { gotoResult(); });
              break;
          case 7:
              alert_h("六等奖", function () { gotoResult(); });
              break;
          case 9:
              alert_h("一等奖", function () { gotoResult(); });
              break;
          case 11:
              alert_h("二等奖", function () { gotoResult(); });
              break;
          default:
              alert_h("继续努力", function () { location.reload(); });
              break;
      }
      isRunning = false;

  }

  function gotoResult() {
      window.location.href = "/Vshop/WinningResults.aspx?activityid=" + GetActivityid();
  
  }





  function easeOut(t, b, c, d) {
      var ts = (t /= d) * t;
      var tc = ts * t;
      return b + c * (tc + -3 * ts + 3 * t);
  }


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
        data: { action: "GetPrize", "activityid": activityid },
        async: false,
        success: function (resultData) {
            no = resultData.No;
        }
    });
    return no;
}
