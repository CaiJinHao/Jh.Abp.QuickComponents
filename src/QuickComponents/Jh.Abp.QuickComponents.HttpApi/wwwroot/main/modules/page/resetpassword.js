var vm = new Vue({
    el: "#resetpassword",
    data: {
    },
    created() {
        layui.use(['jquery', 'form', 'ajaxmod', 'layer', 'formvalidate'], function () {
            var $ = layui.jquery,
                form = layui.form,
                layer = layui.layer,
                ajaxmod = layui.ajaxmod,
                formvalidate = layui.formvalidate;

            form.verify({
                MyPassword: [/^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,20}$/, '密码必须6到20位,必须由大字母、小写字母、数字、字符组成。'],
                ConfirmPassword: function (val, item) {
                    var _b = /^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,20}$/.test(val);
                    if (!_b) {
                        return "只能由字母或数字组成";
                    }
                    if (val !== $('#newPassword').val()) {
                        return "两次输入的密码不一致，请确认密码";
                    }
                }
            });
            //监听提交
            form.on('submit(btn_resetpassword)', function (data) {
                //验证原密码是否正确
                var pwdInfo = {
                    newPassword: data.field.newPassword,
                    currentPassword: data.field.currentPassword
                };
                ajaxmod.requestAuthorize({
                    url: '/User/change-password',
                    type: 'Post',
                    data: pwdInfo,
                    success: function () {
                        top.layer.msg('修改密码成功', {icon: 1});
                    }
                });
                return false;
            });
        });
    },
});
