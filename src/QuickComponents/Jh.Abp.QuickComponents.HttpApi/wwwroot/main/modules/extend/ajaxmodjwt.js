//jwt
layui.define(['layer'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer;

    var ajaxobjold = {
        ajaxArray:function(_opts,callback){
            var _the=this;
            var result_list=[];
            _opts.forEach(function(_el){
                _the.authorizeAjax({
                    url: _el.url,
                    type: _el.type,
                    data: _el.data,
                    success: function (_json) {
                        result_list.push(_json);
                        _el.success(_json);//调用回调并传递数据
                        if (result_list.length==_opts.length) {
                            if (callback) {
                                callback(); //等待全部任务完成调用回调
                            }
                        }
                    }
                });
            });
        },
        request: function (_options) {
            //上传文件使用 FormData ,提交后台表单不能从FormBody中读取
            var optDefault = {
                url: '',
                data: {},
                type: 'Get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                ajaxStatus: true,
                beforeSend: function () { },
                error: function () { },
                success: function () { },
                always: function () { },
                jsonpCallback: 'jsonp' + (new Date()).valueOf().toString().substr(-4),
                headers: {}, //需要每次携带当前用户的信息 否则不知道权限
            };
            Object.assign(optDefault,_options);
            optDefault.url = baseUrl + _options.url;
            /*判断是否可以发送请求*/
            if (!optDefault.ajaxStatus) {
                return false;
            }
            optDefault.ajaxStatus = false; //禁用掉  防止多次点击
            if (optDefault.type !== 'Get' && optDefault.contentType!==false) { //post 必须要转为json字符串  get 必须不能转
                optDefault.data = JSON.stringify(optDefault.data);
            }
            $.ajax(optDefault)
                .done(function (data) { })
                .fail(function (data) { })
                .always(function (data) {
                    optDefault.ajaxStatus = true;//js为单线程
                    optDefault.always();
                });
        },
        ajax: function (_options) {
            var _athe = this;
            var optDefault = {
                url: '',
                data: {},
                alone: true,
                isStatic: false, //true 不弹窗  false 弹窗 是否静默状态
                isloading: true, //默认显示加载条
                isShowError: true,
                isDecrypt: true, //是否解密响应数据
                isApi: true, //是否添加baseurl
                type: 'Get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8', //'application/x-www-form-urlencoded',//
                ajaxStatus: true,
                headers: {}, //需要每次携带当前用户的信息 否则不知道权限
                error: function (data) {
                    console.log(data);
                    var _the = this;
                    layer.closeAll('loading');
                    setTimeout(function () {
                        if (data.status === 400) {
                            top.layer.msg('请求失败，' + data.responseJSON.Msg);
                        } else if (data.status === 404) {
                            top.layer.msg('请求失败，请求未找到');
                        } else if (data.status === 403) {
                            top.layer.msg('您没有权限访问该模块数据');
                        } else if (data.status === 500) {
                            top.layer.msg('请求失败，服务器内部错误');
                        } else {
                            top.layer.msg('请求失败,服务器错误');
                        }
                        _the.ajaxStatus = true; //初始化状态
                    }, 500);
                },
                optSuccess: function (_json) {
                    //暂时不添加数据响应加密
                    //if (optDefault.isDecrypt) {
                    //    console.log(_json);
                    //    _athe.DecryptData(_json.Result);
                    //    console.log(_json);
                    //}
                    if (_json.Code === 0) { //服务器处理成功
                        if (optDefault.type !== 'Get') { //get 是获取数据  其他都是操作数据
                            if (!optDefault.isStatic) {
                                top.layer.msg('操作成功', {
                                    icon: 1
                                });
                            }
                        }
                        _options.success(_json); //传输过去
                    } else if (_json.Code === 200) {
                        _options.success(_json); //传输过去
                    } else {
                        if (optDefault.isShowError) {
                            top.layer.msg(_json.Msg, {
                                icon: 5,
                                time: 5000
                            });
                        } else {
                            _options.success(_json); //传输过去
                        }
                        //服务器处理失败
                        if (optDefault.alone) { //改变ajax提交状态
                            ajaxStatus = true;
                        }
                    }
                }
            };
            $.extend(optDefault, _options);
            /*判断是否可以发送请求*/
            if (!optDefault.ajaxStatus) {
                return false;
            }
            optDefault.ajaxStatus = false; //禁用掉  防止多次点击
            if (!optDefault.alone) {
                setTimeout(function () {
                    optDefault.ajaxStatus = true;
                }, 1000);
            }
            if (optDefault.type !== 'Get'&& optDefault.contentType!==false) { //post 必须要转为json字符串  get 必须不能转
                optDefault.data = JSON.stringify(optDefault.data);
            }
            var _ajaxUrl = "";
            if (optDefault.isApi) {
                _ajaxUrl = baseUrl + optDefault.url; //webapi
            } else {
                _ajaxUrl = optDefault.url;
            }
            var loading;
            $.ajax({
                    //async: async,
                    url: _ajaxUrl,
                    data: optDefault.data,
                    type: optDefault.type,
                    dataType: optDefault.dataType,
                    contentType: optDefault.contentType,
                    success: optDefault.optSuccess,
                    error: optDefault.error,
                    headers: optDefault.headers,
                    jsonpCallback: 'jsonp' + (new Date()).valueOf().toString().substr(-4),
                    beforeSend: function () {
                        if (optDefault.isloading) {
                            loading = layer.msg('加载数据中...', {
                                time: 20000,
                                icon: 16,
                                shade: 0.06
                            });
                        }
                    }
                })
                .done(function (data) {})
                .fail(function (data) {
                    console.log(data);
                })
                .always(function (data) {
                    if (optDefault.isloading) {
                        layer.close(loading);
                    }
                });
        },
        authorizeAjax: function (_options) {
            var _the = this;
            var optDefault = {
                url: '',
                data: {},
                type: 'Get',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                headers: {}, //默认值 不需要赋值
                success: function (_data) {
                    console.log('没有实现函数');
                }
            };
            $.extend(optDefault, _options);
            var myToken = _the.validateLogin();
            var isContinue = _the.AssertTokenExpires(myToken);
            if (!isContinue) {
                return false;
            }
            $.extend(optDefault.headers, myToken.headers);
            if (optDefault.type !== 'Get') {
                var _date = new Date();
                optDefault.data.Time = _date.getTime(); //增加签名时间戳
                //var sign = _the.GetSign(optDefault.data);
                //optDefault.headers.sign = sign;
            }
            _the.ajax(optDefault);
        },
        //验证是否登录了
        validateLogin: function () {
            var myTokenStr = cookie.get("token");
            if (myTokenStr === 'undefined') {
                top.location.href = "/login.html";
                return false;
            }
            return JSON.parse(myTokenStr);
        },
        //诊断Token是否过期
        AssertTokenExpires: function (myToken) {
            var _the = this;
            var _result = true;

            var _dateValueOf = (new Date()).valueOf(); //现在的时间戳
            var _ms = _dateValueOf - myToken.dateValueOf; //时间戳相减 为 毫秒
            var _is_expires_s = (myToken.expires_in * 1000 - _ms) / 1000; //还有多少秒过期
            if (_is_expires_s > 0 && _is_expires_s < 120) { //离过期时间还有5分钟,维护Token
                _the.AutoLogin(function (_json) {
                    if (_json.Code !== 0) //登录失败
                        top.location.href = "/login.html";
                });
            } else if (_is_expires_s < 1) {
                if (_is_expires_s < -1200) { //过期了20分钟了
                    top.location.href = "/login.html";
                    return false;
                }
                top.layer.alert('您的登录已过期，请重新登录', {
                    skin: 'layui-layer-molv',
                    closeBtn: 0
                }, function () {
                    top.location.href = "/login.html";
                });
                _result = false;
            }
            console.log('还有' + (_is_expires_s * 1000) + '毫秒过期' + _is_expires_s + '秒');
            return _result;
        },
        AutoLogin: function (callback) {
            var _the = this;
            var myToken = _the.validateLogin();
            var optDefault = {
                url: '/token',
                isApi: true,
                isDecrypt: false,
                type: 'GET',
                headers: myToken.headers, //默认值 不需要赋值
                isStatic: true, //是否静默状态
                success: function (_json) {
                    if (_json.Code === 0) {
                        _the.SetLoginInfo(_json);
                    }
                    if (callback) {
                        callback(_json);
                    }
                }
            };
            _the.ajax(optDefault);
        },
        //请求成功保存Token信息
        SetLoginInfo: function (_json) {
            var _the = this;
            var optDefault = {
                headers: {
                    "Authorization": _json.Result.TokenType + " " + _json.Result.AccessToken
                },
                dateValueOf: (new Date()).valueOf(), //主要是这里 查看有没有更新为当前的时间
                expires_in: _json.Result.ExpiresIn
            };
            cookie.set("token", JSON.stringify(optDefault));
            cookie.set("pkey", JSON.stringify(_json.Result.PKey));
        },
        //身份验证
        GetAuthorizeToken: function (userinfo, callback) {
            var _the = this;
            var optDefault = {
                url: '/token',
                isApi: true,
                type: 'Post',
                data: userinfo,
                isDecrypt: false,
                isShowError:false,
                isStatic: true, //是否静默状态
                success: function (_json) {
                    if (_json.Code === 0) {
                        _the.SetLoginInfo(_json);
                    }
                    if (callback) {
                        callback(_json);
                    }
                }
            };
            _the.ajax(optDefault);
        },
        QrCodeGetAuthorizeToken: function (data, callback) {
            var _the = this;
            var optDefault = {
                url: '/QrCodeAuth',
                isApi: true,
                type: 'Post',
                data: data,
                isDecrypt: false,
                isloading: false,
                isShowError: false,
                isStatic: true, //是否静默状态
                success: function (_json) {
                    if (_json.Code === 0) {
                        _the.SetLoginInfo(_json);
                    }
                    if (callback) {
                        callback(_json);
                    }
                }
            };
            _the.ajax(optDefault);
        },
        //根据Token 获取用户信息
        GetUserInfo: function (callback) {
            var _the = this;
            var myToken = _the.validateLogin();
            var optDefault = {
                url: '/sysusers',
                isApi: true,
                isDecrypt: false,
                type: 'Get',
                headers: myToken.headers, //默认值 不需要赋值
                isStatic: true, //是否静默状态
                success: function (_json) {
                    if (_json.Code === 0) {
                        // 缓存用户信息 放到index 页面 每次刷新都会执行
                        cookie.set('userinfo', JSON.stringify(_json.Result));
                        if (callback) {
                            callback(_json);
                        }
                    }
                }
            };
            _the.ajax(optDefault);
        },
        GetSign: function (paramObj) {
            var _the = this;
            var queryArr = [];
            for (var key in paramObj) {
                //为了导致对象签名的问题 都转为字符串进行签名
                queryArr.push(key + '=' + JSON.stringify(paramObj[key]));
            }
            //var compare = function (x, y) {
            //    if (x < y) { return -1; }
            //    else if (x > y) { return 1; }
            //    else { return 0; }
            //};
            //queryArr=queryArr.sort(compare);
            //js 和 .net 排序算法不一致去除排序
            var queryStr = queryArr.join('&');
            var _key = _the.GetPk();

            var rsa = new RSAKey();
            rsa.readPrivateKeyFromPEMString(_key);
            var hexSig = rsa.sign(queryStr, 'sha256');
            var signature = hextob64(hexSig);
            return signature;
        },
        GetPk: function () {
            //通过接口获取private key
            //解密获取到private key
            var _key = JSON.parse(cookie.get("pkey"));
            return "-----BEGIN RSA PRIVATE KEY-----\n" + _key + "-----END RSA PRIVATE KEY-----\n";
        },
        //解密数据(使用加密解密数据会慢很多)
        DecryptData: function (encryptedData) {
            var _the = this;
            return;
            var str = JSON.stringify(encryptedData);
            console.log(str);
            var _key = _the.GetPk();
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey(_the.getPuk());
            var encrypted = encrypt.encryptLong2(str);
            console.log(encrypted);
            console.log(encryptedData);

            var decrypt = new JSEncrypt();
            decrypt.setPrivateKey(_key);
            var uncrypted = decrypt.decryptLong2(encrypted);
            console.log(uncrypted);

            //var dec = KJUR.crypto.Cipher.decrypt(encryptedData, _key,"RSAOAEP256");
            //console.log(dec);
            return uncrypted;
        },
        getPuk: function () {
            var strVar = "";
            strVar += "-----BEGIN PUBLIC KEY-----\n";
            strVar += "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2MexpMsm93lfvI0RaqZE\n";
            strVar += "nwDxEVhEaZ1xc6BMm/vArkZZeBrj5Y9W02WxujRpURKcNatyqYoJmuuMgDjh6wTG\n";
            strVar += "x/o2QIP5Bnv660ZfHOWI9LQdsMopFUwt9wdTVam/w4pJYL/OInVGbsTKbW0lZ8YN\n";
            strVar += "MvOrnJV/KSc94KouBEdck9h0dIpby8dBdb5e30pAuuMPQ0VUDCIIdAr89j+E8P0G\n";
            strVar += "0uVJsHT0zXQVRpb8Fesz9LT6thfa3dId5W2aLzPUg95rghfqH0brv1zsCbNPZ4Dt\n";
            strVar += "EFvvMXq5PFIwKcBsjWUSXBxdQORQBI9Q+WYfYvM8IRojK12TJI7xeuZCELe+68I2\n";
            strVar += "BQIDAQAB\n";
            strVar += "-----END PUBLIC KEY-----\n";
            strVar += "\n";
            return strVar;
        },
    };

    let userobj = {
        //获取授权令牌
        getAuthorizeToken: function (login_info, callback) {
            let _the = this;
            ajaxobj.request({
                url: '/AccessToken',
                type: 'Post',
                data: login_info,
                isloading:false,
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
        validateLogin: function () {
            var myTokenStr = cookie.get("token");
            if (myTokenStr === 'undefined') {
                top.location.href = "/login.html";
                return false;
            }
            return JSON.parse(myTokenStr);
        },
        //获取登录用户信息
        getUserInfo: function (callback) {
            var _the = this;
            ajaxobj.requestAuthorize({
                url: '/User/info',
                type: 'Get',
                success: function (response) {
                    callback(response);
                }
            });
            return;
/*
            function getUserId(_fn) {
                ajaxobj.requestAuthorize({
                    url: '/AccessToken/User',
                    data: {type:'sub'},
                    type: 'Get',
                    success: function (response) {
                        _fn(response);
                    }
                });
            };

            getUserId(function (userid) {
                ajaxobj.requestAuthorize({
                    url: identityServerApi + '/identity/users/' + userid,
                    type: 'Get',
                    success: function (response) {
                        if (callback) {
                            callback(response);
                        }
                    }
                });
            });
            */
        }
    };

    let ajaxobj = {
        ajaxArray:function(_opts,callback){
            var _the=this;
            var result_list=[];
            _opts.forEach(function(_el){
                _the.requestAuthorize({
                    url: _el.url,
                    type: _el.type,
                    data: _el.data,
                    success: function (response) {
                        result_list.push(response);
                        _el.success(response);//调用回调并传递数据
                        if (result_list.length==_opts.length) {
                            if (callback) {
                                callback(); //等待全部任务完成调用回调
                            }
                        }
                    }
                });
            });
        },
        ajax: function (opts) {
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
                        loading = layer.load(2, {time: 10 * 1000}); 
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
                identityServer:false
            };
            Object.assign(optDefault, opts);
            if (optDefault.identityServer) {
                optDefault.url = identityServerApi + optDefault.url;
            }
            if (optDefault.url.indexOf('http') <0&&optDefault.url.indexOf('/api/') <0) {
                optDefault.url = baseUrl + optDefault.url;
            }
            if (optDefault.type !== 'Get' && optDefault.contentType !== false) { //post 必须要转为json字符串  get 必须不能转
                optDefault.data = JSON.stringify(optDefault.data);
            }
            $.ajax(optDefault)
                .done(function (response) { 
                    console.log('done');
                })
                .fail(function (response) { 
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
        //自动携带token信息
        requestAuthorize: function (opts) {
            let myToken = userobj.validateLogin();
            let optDefault = {
                headers: myToken.headers, //默认值 不需要赋值
                error: function (responseData) {
                    var response = responseData.responseJSON;
                    var error = response.error;
                    top.layer.msg(error.message, { icon: 5, time: 5000 });
                }
            };
            Object.assign(optDefault, opts);
            this.ajax(optDefault);
        },
        getAuthorizeToken: function (login_info, callback) {
            userobj.getAuthorizeToken(login_info,callback);
        },
        getUserInfo: function (callback) {
            userobj.getUserInfo(callback);
        }
    };

    exports('ajaxmod', ajaxobj);
});