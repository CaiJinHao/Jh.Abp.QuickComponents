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
layui.use(['jquery', 'form', 'layer', 'ajaxmod'], function () {
    var $ = layui.jquery,
        form = layui.form,
        layer = layui.layer,
        ajaxmod = layui.ajaxmod

    if (localStorage.getItem('rememberme')) {
        vm.loginUser = JSON.parse(localStorage.getItem('rememberme'));
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
        if (userinfo.rememberMe === '1') {
            localStorage.setItem("rememberme", JSON.stringify(userinfo));
        } else {
            localStorage.removeItem("rememberme");
        }
        var login_info = {
            UserNameOrEmailAddress: userinfo.Key,
            Password: userinfo.Secret
        };
        ajaxmod.getAuthorizeToken(login_info, function (response) {
            layer.close(loading);
            localStorage.setItem("lockcms", false);//取消锁屏
            layer.msg("登录成功", { time: 5000 });
            window.location.href = "./main/index.html";
        });
        return false;
    });
});