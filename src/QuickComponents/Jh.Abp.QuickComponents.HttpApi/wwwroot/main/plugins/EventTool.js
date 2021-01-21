var topics = {},
    subUid = -1;
window.tool = {
    publish: function (topic, args) {//发布
        if (!topics[topic]) { return; }
        var subs = topics[topic],
            len = subs.length;
        while (len--) {
           return subs[len].func(topic, args);//将执行的func 结果返回
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