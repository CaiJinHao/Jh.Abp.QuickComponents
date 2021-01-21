layui.use(['jquery', 'form', 'layer', 'ajaxmod', 'formvalidate'], function () {
    var $ = layui.jquery,
        form = layui.form,
        layer = layui.layer,
        ajaxmod = layui.ajaxmod;


    //获取二维码和tiker
    var optDefault = {
        url: '/QrCodeAuth',
        isApi: true,
        type: 'PUT',
        isDecrypt: false,
        isStatic: true, //是否静默状态
        success: function (_json) {
            if (_json.Code === 0) {
                var imgbase64 = 'data:image/png;base64,' + _json.data.qrcode.FileContents;
                $('#qrcode-img').attr('src', imgbase64);
                LoginHandler(_json.data.ticket);
            }
        }
    };
    ajaxmod.ajax(optDefault);

    var loading = undefined;
    function LoginHandler(ticket) {
        //监听提交
        var interval = setInterval(function () {
            
            ajaxmod.QrCodeGetAuthorizeToken({ Key: ticket},function (_json) {
                if (_json.Code === 0) {
                    $('#login-msg').html("登录成功");
                    //layer.close(loading);
                    layer.msg("登录成功", { time: 5000 });
                    window.location.href = "./main/index.html";
                    clearInterval(interval);
                }
                else if (_json.Code === 10003)
                {
                    $('#login-msg').html(_json.Msg);
                    //loading = layer.msg('已扫码，登录中...', {
                    //    time: 20000,
                    //    icon: 16,
                    //    shade: 0.06
                    //});
                }
            });
        }, 1000);
    }
});