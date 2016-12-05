function CountDown(settings){
	this.id=settings.id;//倒计时绑定的对象
	this.timer=settings.timer||60;//倒计时多少秒,默认参数与新参数替换
	this.duringTime=settings.duringTime||null;//多少秒后执行
	this.duringEvent=settings.duringEvent||null;//多少秒后执行的事件
	this.clicking=settings.clicking||function(){};//倒计时点击时的回调函数
	this.callback=settings.callback||function(){};//倒计时完成后的回调函数
	this.normalClass=settings.normalClass||'countdown';//正常样式
	this.disabledClass=settings.disabledClass||'disabled';//禁用样式
	this.clickFont=settings.clickFont||"秒后重发";
	this.afterFont=settings.afterFont||"重新发送";
	this.flag=false;
	this.isfirst=true;
	this.countTime=0;
	this.cookieName="countdown";//设置cookie名字
	this.cookieTime=settings.cookieTime||1;//分钟
	this.init();//初始化
}
CountDown.prototype={
	init:function(){
		var isExist=this.isExistCookie(this.cookieName);
		if(isExist){
			var val=this.getCookieValue(this.cookieName);
			this.timer=val;
			this.startCount();
		}
	},
	startCount:function(){
		//开始计时
		var that=this;
		if(!this.flag){
			this.countTime=that.timer;
			this.flag=true;
		}
		var o=document.getElementById(that.id);
		if (this.countTime == 0) {
			o.removeAttribute("disabled");         
			o.value=this.afterFont;
			this.countTime =this.timer;
			this.callback();//倒计时完成后的回调函数
			o.className=this.normalClass;
		}
		else {
			if(this.isfirst){
				this.clicking();//倒计时点击时的回调函数
				o.className=this.disabledClass;
			}
			if(this.duringTime!=null){
				if(this.duringTime==this.countTime&&this.duringFlag==undefined){
					if(typeof this.duringEvent=="function"){
						this.duringFlag=true;
						this.duringEvent();	
					}
				}
			}
			this.isfirst=false;
			o.setAttribute("disabled", true);
			o.value=this.countTime+this.clickFont;
			this.countTime--;
			this.setCookie(this.countTime);
			this.timeId=setTimeout(function(){that.startCount()},1000);
		}
	},
	clearTimeId:function(){
		clearTimeout(this.timeId);
		this.clearCookie();
	},
	setCookie:function(num){
		var isExist=this.isExistCookie(this.cookieName);
		var time=new Date();
		time.setMinutes(time.getMinutes()+this.cookieTime);
		if(!isExist){
			document.cookie=this.cookieName+"="+num+";path=/;expires="+time.toUTCString();
		}
		document.cookie=this.cookieName+"="+num+";path=/;expires="+time.toUTCString();
		var cookieValue=this.getCookieValue(this.cookieName);
		if(cookieValue==0){
			this.clearCookie();
		}
	},
	isExistCookie:function(name){
		return document.cookie.indexOf(name)==-1?false:true;
	},
	getCookieValue:function(name){
	  	var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
	    if(arr=document.cookie.match(reg))
	        return unescape(arr[2]); //返回cookie值
	    else 
	        return null; 
	},
	clearCookie:function(){
		if(this.isExistCookie(this.cookieName)){
			var exp = new Date();  
    		exp.setTime(exp.getTime()- 1);
			document.cookie=this.cookieName+"="+this.getCookieValue(this.cookieName)+";path=/;expires="+exp.toGMTString();
		}
	}
}