//oidc
layui.define(['layer'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer;

    let userobj = {
        //获取授权令牌
        getAuthorizeToken: function (login_info, callback) {
            let _the = this;
            ajaxobj.request({
                url: '/AccessToken',
                type: 'Post',
                data: login_info,
                isloading: false,
                success: function (response) {
                    _the.setAuthorizeInfo(response);
                    if (callback) {
                        callback(response);
                    }
                }
            });
        },
        //请求成功保存Token信息
        setAuthorizeInfo: function (response) {
            console.log(response);
            var optDefault = {
                headers: {
                    "Authorization": response.tokenType + " " + response.accessToken
                },
                dateValueOf: (new Date()).valueOf(), //主要是这里 查看有没有更新为当前的时间
                expires_in: response.expiresIn
            };
            cookie.set("token", JSON.stringify(optDefault));
            //cookie.set("pkey", JSON.stringify(response.Result.PKey));
        },
        //验证是否登录了
        validateLoginJwt: function () {
            var myTokenStr = cookie.get("token");
            if (myTokenStr === 'undefined') {
                top.location.href = "/login.html";
                return false;
            }
            return JSON.parse(myTokenStr);
        },
        validateLoginOidc: function (_fun) {
            oidcManager.getUser(function(user){
                if (user) {
                    var headers = {
                        "Authorization": "Bearer " + user.access_token
                    };
                    _fun(headers);
                }
            });
        },
        //获取登录用户信息
        getUserInfo: function (callback) {
            oidcManager.getUser(function(user){
                if (user) {
                    callback(user);
                }
            });
        }
    };

    let ajaxobj = {
        ajaxArray: function (_opts, callback) {
            var _the = this;
            var result_list = [];
            _opts.forEach(function (_el) {
                _the.requestAuthorize({
                    url: _el.url,
                    type: _el.type,
                    data: _el.data,
                    success: function (response) {
                        result_list.push(response);
                        _el.success(response);//调用回调并传递数据
                        if (result_list.length == _opts.length) {
                            if (callback) {
                                callback(); //等待全部任务完成调用回调
                            }
                        }
                    }
                });
            });
        },
        ajax: function (opts) {
            let _the=this;
            //上传文件使用 FormData ,提交后台表单不能从FormBody中读取
            let loading;
            let optDefault = {
                url: '',
                data: {},
                type: 'Get',
                //dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                isloading: true,
                beforeSend: function () {
                    let _the = this;
                    if (_the.isloading) {
                        loading = layer.load(2, { time: 10 * 1000 });
                        // loading = layer.msg('加载数据中...', {
                        //     time: 20000,
                        //     icon: 16,
                        //     shade: 0.06
                        // });
                    }
                },
                error: function () { },
                success: function () { },
                always: function () { },
                jsonpCallback: 'jsonp' + (new Date()).valueOf().toString().substr(-4),
                headers: {}, //需要每次携带当前用户的信息 否则不知道权限
                identityServer: false
            };
            Object.assign(optDefault, opts);
            if (optDefault.identityServer) {
                optDefault.url = identityServerApi + optDefault.url;
            }
            if (optDefault.url.indexOf('http') < 0 && optDefault.url.indexOf('/api/') < 0) {
                optDefault.url = baseUrl + optDefault.url;
            }
            if (optDefault.type !== 'Get' && optDefault.contentType !== false) { //post 必须要转为json字符串  get 必须不能转
                optDefault.data = JSON.stringify(optDefault.data);
            }
            $.ajax(optDefault)
                .done(function (response) {
                    console.log('done');
                })
                .fail(function (responseData) {
                    var response = _the.handlerResponse(responseData);
                    optDefault.error(response);
                    console.log('fail');
                })
                .always(function (response) {
                    console.log('always');
                    layer.close(loading);
                    optDefault.always(response);
                });
        },
        //不携带token信息
        request: function (opts) {
            let optDefault = {
                error: function (responseData) {
                    var response = responseData.responseJSON;
                    var error = response.error;
                    top.layer.msg(error.message, { icon: 5, time: 5000 });
                }
            };
            Object.assign(optDefault, opts);
            this.ajax(optDefault);
        },
        handlerResponse(response) {
            switch (response.status) {
                case 400:
                case 401:
                case 402:
                case 403:
                    {
                        if (!response.responseJSON) {
                            response = {
                                responseJSON: {
                                    error: {
                                        message: '对不起没有该请求权限'
                                    }
                                }
                            };
                        }
                    } break;
                case 404:
                    {
                        response = {
                            responseJSON: {
                                error: {
                                    message: '没有找到请求地址'
                                }
                            }
                        };
                    } break;
                default:
                    //不处理
                    break;
            }
            return response;
        },
        //自动携带token信息
        requestAuthorize: function (opts) {
            let _the = this;
            userobj.validateLoginOidc(function (opts_headers) {
                let optDefault = {
                    headers: opts_headers, //默认值 不需要赋值
                    error: function (responseData) {
                        var response = _the.handlerResponse(responseData);
                        console.log(response);
                        var error = response.responseJSON.error;
                        top.layer.msg(error.message, {
                            icon: 5,
                            time: 5000
                        });
                    }
                };
                Object.assign(optDefault, opts);
                _the.ajax(optDefault);
            });
        },
        getAuthorizeToken: function (login_info, callback) {
            userobj.getAuthorizeToken(login_info, callback);
        },
        getUserInfo: function (callback) {
            userobj.getUserInfo(callback);
        }
    };

    exports('ajaxmod', ajaxobj);
});