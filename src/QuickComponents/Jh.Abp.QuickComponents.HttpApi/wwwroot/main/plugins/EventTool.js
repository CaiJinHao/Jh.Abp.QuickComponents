var topics = {},
    subUid = -1;
window.tool = {
    publish: function (topic, args) {//发布
        if (!topics[topic]) { return; }
        var subs = topics[topic],
            len = subs.length;
        while (len--) {
            return subs[len].func(args); //将执行的func 结果返回
        }
        //if (topics[topic]) {//调用过一次就会删除  需要将订阅事件 放到列表的点击事件中
        //    topics[topic].splice(0, 1);//删除
        //}
        return this;
    },
    //要保证 名称唯一
    subscribe: function (topic, func) {//订阅
        //判断是否存在 存在则删除
        if (topics[topic]) {
            topics[topic].splice(0, 1);//删除
        }
        topics[topic] = topics[topic] ? topics[topic] : [];
        var token = (++subUid).toString();
        topics[topic].push({
            token: token,//索引
            func: func      
        });
        return token;
    },
};

//这个方法  layui 有内置的 可以使用layui的
//layui 不能返回外面参数的 result 的结果


/**格式化时间 */
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

/**格式化字符串 */
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        } else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({[" + i + "]})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    //调用方式 var result2=template2.format({name:"loogn",age:22});
    return result;
}
