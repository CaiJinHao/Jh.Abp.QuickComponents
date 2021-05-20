//加载公共样式 每个页面都有用
layui.link("/main/plugins/font-awesome/css/font-awesome.min.css");
// document.writeln("<script src=\"/main/plugins/rsasign/jsrsasign-all-min.js\"></script>");
document.writeln("<script src=\"/main/plugins/oidc/oidc-client.min.js\"></script>");
document.writeln("<script src=\"/main/plugins/oidc/log.js\"></script>");
document.writeln("<script src=\"/main/plugins/oidc/oidc-client-sample.js\"></script>");

// 公共函数

// 接口域
var baseUrl = window.baseUrl = '/api/v1';
var identityServerApi = "https://localhost:44381/api";
var loginUrl = '/login.html';
var PageSize = 10;
var mainUrl = '/main/main.html';

// config的设置是全局的
layui.config({
	base: '/main/modules/'
}).extend({
    //自定义扩展
    ajaxmod: 'extend/ajaxmod',
    datatable: 'extend/datatable',
    fileServices: 'extend/fileServices',
    formvalidate: 'extend/formvalidate',
    navbar: 'extend/navbar',
    tab: 'extend/tab',

    //插件
    drag:'plugins/drag/drag',
    excel: 'plugins/exceldata/excel',
    FileSaver: 'plugins/exceldata/FileSaver',
    xlsx: 'plugins/exceldata/xlsx',
    rsasign: 'plugins/rsasign/jsrsasign',

    axios: 'plugins/axios/axios',

    backstretch: 'plugins/backstretch',
    formSelects: 'plugins/formSelects/formSelects',
    nprogress: 'plugins/nprogress/nprogress',
    notice: 'plugins/notice/notice',
    qrcode: 'plugins/qrcode/qrcode',


    //页面js
    login: 'page/login',
    loginapi: 'page/loginapi',
    loginqr: 'page/loginqr',
    index: 'page/index',
    main: 'page/main',
    resetpassword: 'page/resetpassword',
});

// 提示更新浏览器
(function(w) {
	if(!("WebSocket" in w && 2 === w.WebSocket.CLOSING)) {
		var d = document.createElement("div");
		d.className = "browsehappy";
		d.innerHTML = '<div style="width:100%;height:100px;font-size:20px;line-height:100px;text-align:center;background-color:#E90D24;color:#fff;margin-bottom:40px;">\u4f60\u7684\u6d4f\u89c8\u5668\u5b9e\u5728<strong>\u592a\u592a\u65e7\u4e86</strong>\uff0c\u592a\u592a\u65e7\u4e86 <a target="_blank" href="http://browsehappy.osfipin.com/" style="background-color:#31b0d5;border-color: #269abc;text-decoration: none;padding: 6px 12px;background-image: none;border: 1px solid transparent;border-radius: 4px;color:#FFEB3B;">\u7acb\u5373\u5347\u7ea7</a></div>';
		var f = function() {
			var s = document.getElementsByTagName("body")[0];
			if("undefined" == typeof(s)) {
				setTimeout(f, 10)
			} else {
				s.insertBefore(d, s.firstChild)
			}
		};
        f();
	}
}(window));

function RenderDateTime(val) {
    if (val === '0001-01-01 00:00:00') {
        return '';
    }
    return val;
}



/*
 * HTTP Cookie:存储会话信息
 * 名称和值传送时必须是经过RUL编码的
 * cookie绑定在指定的域名下，非本域无法共享cookie，但是可以是在主站共享cookie给子站
 * cookie有一些限制：比如IE6 & IE6- 限定在20个；IE7 50个；Opear 30个...所以一般会根据【必须】需求才设定cookie
 * cookie的名称不分大小写；同时建议将cookie URL编码；路径是区分cookie在不同情况下传递的好方式；带安全标志cookie
 *     在SSL情况下发送到服务器端，http则不会。建议针对cookie设置expires、domain、 path；每个cookie小于4KB
 * */
//对cookie的封装，采取getter、setter方式

(function(global){
	//获取cookie对象，以对象表示
	function getCookiesObj(){
		var cookies = {};
		if(document.cookie){
			var objs = document.cookie.split('; ');
			for(var i in objs){
				var index = objs[i].indexOf('='),
					name = objs[i].substr(0, index),
					value = objs[i].substr(index + 1, objs[i].length);	
				cookies[name] = value;
			}
		}
		return cookies;
	}
	//设置cookie
	function set(name, value, opts){
		//opts maxAge, path, domain, secure
		if(name && value){
            var cookie = encodeURIComponent(name) + '=' + encodeURIComponent(value) +';path=/';
			//可选参数
			if(opts){
				if(opts.maxAge){
					cookie += '; max-age=' + opts.maxAge; 
				}
				if(opts.path){
					cookie += '; path=' + opts.path;
				}
				if(opts.domain){
					cookie += '; domain=' + opts.domain;
				}
				if(opts.secure){
					cookie += '; secure';
				}
			}
			document.cookie = cookie;
			return cookie;
		}else{
			return '';
		}
	}
	//获取cookie
	function get(name){
		return decodeURIComponent(getCookiesObj()[name]) || null;
	}
	// 判断cookie存在
	function has(name) {
		return document.cookie.indexOf(name) == -1 ? false : true;
	}
	//清除某个cookie
	function remove(name){
        if (getCookiesObj()[name]) {
            document.cookie = name + '=; max-age=0;path=/';
		}
	}
	//清除所有cookie
    function clear() {
		var cookies = getCookiesObj();
        for (var key in cookies) {
            document.cookie = key + '=; max-age=0;path=/';
		}
	}
	//获取所有cookies
	function getCookies(name){
		return getCookiesObj(name);
	}
	//解决冲突
	function noConflict(name){
		if(name && typeof name === 'string'){
			if(name && window['cookie']){
				window[name] = window['cookie'];
				delete window['cookie'];
				return window[name];
			}
		}else{
			return window['cookie'];
			delete window['cookie'];
		}
	}
	global['cookie'] = {
		'getCookies': getCookies,
		'set': set,
		'has': has,
		'get': get,
		'remove': remove,
		'clear': clear,
		'noConflict': noConflict
	};
})(window);


// 反序列化
function unserialize (data) {
	var obj = {};
	data.split('&').forEach(function (item) {
	    item = item.split('=');
	    var name = item[0],
	        val = item[1];
	    obj[name] = val;
	});
	return obj;
}

// url参数转对象，适用于iframe的参数传递,对象中存在数组处理欠佳
function geturlparam () {
	var searchstr = decodeURI(location.search),
		param = unserialize(searchstr.substr(1,searchstr.length));
	return param;
}

// 节流函数
function throttle(fn,options,delay,mustApplyTime){
	clearTimeout(fn.timer);
	fn._cur=Date.now();  //记录当前时间
	if(!fn._start){ 
	    //若该函数是第一次调用，则直接设置_start,即开始时间，为_cur，即此刻的时间
	    fn._start=fn._cur;
	}
	if(fn._cur-fn._start>mustApplyTime){ 
		//当前时间与上一次函数被执行的时间作差，与mustApplyTime比较，若大于，则必须执行一次函数，若小于，则重新设置计时器
	    	return fn.call(null,options);
     	fn._start=fn._cur;
	}else{
	    fn.timer=setTimeout(function(){
	    	return fn.call(null,options);
	    },delay);
	}
}

var $;
layui.use(['jquery'], function () {
    $ = layui.jquery;
});

function MyCommon() {
    return {
        fields: function (elform) {
            var field = {};
            var fieldElem = elform.find('input,select,textarea');
            $.each(fieldElem, function (_, item) {
                item.name = (item.name || '').replace(/^\s*|\s*&/, '');
                if (!item.name) return;
                //用于支持数组 name
                if (/^.*\[\]$/.test(item.name)) {
                    var key = item.name.match(/^(.*)\[\]$/g)[0];
                    nameIndex[key] = nameIndex[key] | 0;
                    item.name = item.name.replace(/^(.*)\[\]$/, '$1[' + (nameIndex[key]++) + ']');
                }
                if (/^checkbox|radio$/.test(item.type) && !item.checked) return;
                field[item.name] = item.value;
            });
            return field;
        }
    };
}






























