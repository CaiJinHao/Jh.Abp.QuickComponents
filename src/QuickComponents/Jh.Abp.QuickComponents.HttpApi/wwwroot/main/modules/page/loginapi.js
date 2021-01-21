var vm = new Vue({
    el: '#page-vm',
    data: {
        currPage: 1,
        loginUser: {
            Key: '',
            Secret: '',
            rememberMe: 0
        }
    },
    methods: {
        RenderDom: function (form, callback) {
            vm.$forceUpdate(); //强制重新渲染 dom
            // DOM 还没有更新
            vm.$nextTick(function () {
                // DOM 现在更新了
                form.render();
                if (callback) {
                    callback();
                }
            });
        }
    }
});
layui.use(['jquery', 'form', 'layer', 'ajaxmod', 'formvalidate'], function () {
    var $ = layui.jquery,
        form = layui.form,
        layer = layui.layer;

    if (localStorage.getItem('remembermeapi')) {
        vm.loginUser = JSON.parse(localStorage.getItem('remembermeapi'));
        vm.RenderDom(form);
    }

    //监听提交
    form.on('submit(form-login)', function (data) {
        var userinfo = data.field;
        var loading = layer.msg('登录中...', {
            time: 20000,
            icon: 16,
            shade: 0.06
        });
        $.ajax({
            url: '/api/restadmin',
            type: 'POST',
            data: JSON.stringify(userinfo),
            contentType: 'application/json;charset=utf-8',
            success: function (_json) {
                layer.close(loading);
                if (_json.Code === 0) {
                    localStorage.setItem("restapi_session_key", JSON.stringify(_json.Result));
                    if (userinfo.rememberMe === '1') {
                        localStorage.setItem('remembermeapi', JSON.stringify(userinfo));
                    } else {
                        localStorage.removeItem('remembermeapi');
                    }
                    layer.msg("登录成功", {
                        time: 5000
                    });
                    window.location.href = "/restapi/";
                } else {
                    layer.msg(_json.Msg, {
                        icon: 5,
                        time: 5000
                    });
                }
            }
        });
        return false;
    });
});