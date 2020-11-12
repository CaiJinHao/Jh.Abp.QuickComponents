(function init() {
    var remember_me_key='remember-me-key';
    var page={
        prompt:function(title,msg,time){
            $('.prompt .prompt-title').html(title);
            $('.prompt .prompt-msg').html(msg);
            $('.prompt').fadeIn();
            setTimeout(function(){
                $('.prompt').fadeOut();
            },time);
        },
        remember:function(){
            var login_info_str=localStorage.getItem(remember_me_key);
            if(login_info_str!=null)
            {
                var login_info=JSON.parse(login_info_str);
                $('#UserName').val(login_info.Key);
                $('#Password').val(login_info.Secret);
                $('#remember-me').prop('checked',true);
            }
        }
    };

    page.remember();

    $('#login-form').submit(function (event) {
        event.preventDefault();
        var remember_me=$('#remember-me').prop('checked');
        var login_info = {
            UserNameOrEmailAddress: $('#UserName').val(),
            Password: $('#Password').val()
        };
        if(remember_me)
        {
            localStorage.setItem(remember_me_key,JSON.stringify(login_info));
        }
        $.ajax({
            url: '/api/AccessToken/Swagger',
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
            }
        });
    });
}());