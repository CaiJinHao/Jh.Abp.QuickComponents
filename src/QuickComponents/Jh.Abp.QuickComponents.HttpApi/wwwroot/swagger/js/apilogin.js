(function init() {
    var remember_me_key='remember-me-key';
    var page = {
        loadding:false,
        prompt: function (title, msg, time) {
            let _the = this;
            $('.prompt .prompt-title').html(title);
            $('.prompt .prompt-content').html(msg);
            $('.prompt').fadeIn();
            if (time) {
                setTimeout(function () { _the.promptHide(); }, time);
            }
        },
        promptHide: function () {
            let _the = this;
            _the.loadding = false;
            $('.prompt').fadeOut();
        },
        loaddingAjax: function (title,msg) {
            let _the = this;
            _the.loadding = true;
            _the.prompt(title, msg);
        },
        remember:function(){
            var login_info_str = localStorage.getItem(remember_me_key);
            if (login_info_str)
            {
                var login_info =JSON.parse(login_info_str);
                $('#UserName').val(login_info.UserNameOrEmailAddress);
                $('#Password').val(login_info.Password);
                $('#remember-me').prop('checked',true);
            }
        }
    };

    page.remember();

    $('#login-form').submit(function (event) {
        event.preventDefault();
        var remember_me = $('#remember-me').prop('checked');
        var login_info = {
            UserNameOrEmailAddress: $('#UserName').val(),
            Password: $('#Password').val()
        };
        if (remember_me) {
            localStorage.setItem(remember_me_key, JSON.stringify(login_info));
        } else {
            localStorage.removeItem(remember_me_key);
        }
        if (page.loadding) {
            return;
        }
        page.loaddingAjax("登录提示","正在登录中...");
        $.ajax({
            url: '/api/v1/SwaggerAccessToken',
            type: 'POST',
            data: JSON.stringify(login_info),
            contentType: 'application/json;charset=utf-8',
            success: function (api_result) {
                sessionStorage.setItem('restapi_session_key', JSON.stringify(api_result.data));
                location.href = '/swagger/';
            },
            error: function (XMLHttpRequest) {
                var api_result = XMLHttpRequest.responseJSON;
                page.prompt("登录提示", api_result.error.message, 3000);
            },
            complete: function (jqXHR, textStatus) {
            }
        });
    });
}());