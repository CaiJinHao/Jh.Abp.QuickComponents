// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.



///////////////////////////////
// OidcClient config
///////////////////////////////
Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;
console.log(window.location.origin);
var settings = {
    authority: 'https://localhost:6102',
    client_id: 'MenuManagement_Js',
    redirect_uri:  'https://localhost:6101/main/index.html',
    post_logout_redirect_uri: 'https://localhost:6101/signout-callback-oidc',
    response_type: 'id_token token',
    scope: 'email openid profile role phone address MenuManagement offline_access',

    filterProtocolClaims: false,
    loadUserInfo: false
};
var manager = new Oidc.UserManager(settings);
manager.getUser().then(function (user) {
    console.log(user);
    if (user) {
        log('用户已登录', user.profile);
    } else {
        log('用户未登录');
    }
});
function login() {
    manager.signinRedirect();
}

function api() {
    manager.getUser().then(function (user) {
       console.log(user);
    });
}

function logout() {
    manager.signoutRedirect();
}

var client = new Oidc.OidcClient(settings);

///////////////////////////////
// functions for UI elements
///////////////////////////////
function signin() {
    client.createSigninRequest({ state: { bar: 15 } }).then(function(req) {
        log("signin request", req, "<a href='" + req.url + "'>go signin</a>");
        if (followLinks()) {
            window.location = req.url;
        }
    }).catch(function(err) {
        log(err);
    });
}

var signinResponse;
function processSigninResponse() {
    client.processSigninResponse().then(function(response) {
        signinResponse = response;
        log("signin response", signinResponse);
    }).catch(function(err) {
        log(err);
    });
}

function signinDifferentCallback(){
    client.createSigninRequest({ state: { bar: 15 }, redirect_uri: settings.redirect_uri }).then(function(req) {
        log("signin request", req, "<a href='" + req.url + "'>go signin</a>");
        if (followLinks()) {
            window.location = req.url;
        }
    }).catch(function(err) {
        log(err);
    });
}

function signout() {
    client.createSignoutRequest({ id_token_hint: signinResponse && signinResponse.id_token, state: { foo: 5 } }).then(function(req) {
        log("signout request", req, "<a href='" + req.url + "'>go signout</a>");
        if (followLinks()) {
            window.location = req.url;
        }
    });
}

function processSignoutResponse() {
    client.processSignoutResponse().then(function(response) {
        signinResponse = null;
        log("signout response", response);
    }).catch(function(err) {
        log(err);
    });
}

function toggleLinks() {
    var val = document.getElementById('links').checked;
    localStorage.setItem("follow", val);

    var display = val ? 'none' : '';

}

function followLinks() {
    return localStorage.getItem("follow") === "true";
}

var follow = followLinks();
var display = follow ? 'none' : '';


if (followLinks()) {
    if (window.location.href.indexOf("#") >= 0) {
        processSigninResponse();
    }
    else if (window.location.href.indexOf("?") >= 0) {
        processSignoutResponse();
    }
}