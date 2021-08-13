// Oidc.Log.logger = console;
// Oidc.Log.level = Oidc.Log.INFO;
var settings = {
    authority: 'http://localhost:6102',
    client_id: 'MenuManagement_Js',
    redirect_uri: window.location.origin + '/ids/callback.html',
    response_type: 'id_token token',
    scope: 'email openid profile role phone address MenuManagement offline_access',
    post_logout_redirect_uri: 'http://localhost:6102/index',

    filterProtocolClaims: false,
    loadUserInfo: false
};

var manager = new Oidc.UserManager(settings);

var oidcManager = {
    getUser: function (_fn) {
        let _the=this;
        manager.getUser().then(function (user) {
            if (user) {
                log('用户已登录', user.profile);
            } else {
                log('用户未登录');
                _the.login();
            }
            _fn(user);
        });
    },
    login: function () {
        manager.signinRedirect();
    },
    logout: function () {
        manager.signoutRedirect();
    }
};