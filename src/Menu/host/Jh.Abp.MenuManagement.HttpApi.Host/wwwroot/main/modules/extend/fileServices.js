layui.define(function (exports) {
    var root = {
        //上传文件至服务器  file类型表单 changeEvent img表单事件参数元素  提交的url地址  回调函数      dataForm 序列化后的表单值 {name="",value=""}
        postFile: function (_options) {
            var opt = {
                ServerUrl: '',
                callBack: function (rjson) { },
                data: undefined,//json格式 
                files: {},
                FormData: new FormData()
            };
            $.extend(opt, _options);
            //var fileObj = document.getElementById(elementidstr).files; // 获取文件对象 
            //var fileObj = changeEvent.target.files;//这样用可以屏蔽 页面中存在相同ID的情况 
            //var form = new FormData();
            var form = opt.FormData;
            $.each(opt.files, function (k, _f) {//向FormData中添加文件对象
                form.append("files", _f);                           // 文件对象
            });
            //form.append("author", "hooyes");                        // 可以增加表单数据
            // XMLHttpRequest 对象
            var xhr = new XMLHttpRequest();
            xhr.open("post", opt.ServerUrl, true);
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {//当请求结束且没有报错时 执行
                    var txt = xhr.responseText;
                    var rjson = $.parseJSON(txt);//解析后的对象
                    //返回为json对象
                    opt.callBack(rjson);
                }
            };
            xhr.send(form);
        },
    };
    exports('fileServices', root);
});