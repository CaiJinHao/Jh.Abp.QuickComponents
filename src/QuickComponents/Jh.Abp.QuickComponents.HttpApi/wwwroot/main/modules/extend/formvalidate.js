layui.define(['form','jquery'], function (exports) {
    var form = layui.form,
        $ = layui.jquery;

    var formvalidate = {

    };

//		layui本生提供
//		required（必填项）
//		phone（手机号）
//		email（邮箱）
//		url（网址）
//		number（数字）
//		date（日期）
//		identity（身份证）
//		自定义值
    // 账号
    form.verify({
        Password: [/^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,20}$/, '密码必须6到20位,必须由大字母、小写字母、数字、字符组成'],
        Account: [/(.+){6,20}$/, '账号必须6到20位'],
        Captcha: function (value) {
            if (value.length != 4) {
                return '验证码必须为4位';
            }
        },
        //验证字符串
        Code: function (val, item) {
            var _b = /'(^[a-zA-Z0-9]+$)|[a-z]|[0-9]|[A-Z]'/.test(val);
            if (!_b) {
                return "只能由字母和数字组成";
            }
        },
         //验证纯汉字
        GBK: function (val, item) {//汉字
            if (!new RegExp('^[\u4e00-\u9fa5]+$').test(val)) {
                return "只能由汉字组成";
            }
        },
        //验证纯数字
        Num: function (val, item) {
            var _b = /^\d+$|^\d+\.\d+$/.test(val);//最好用这种方式 比较正规些
            if (!_b) {
                return "请输入正确的数值";
            }
        },
        //身份证
        IDCard: function (val, item) {
            var reg = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
            var _b = reg.test(val);
            if (!_b) {
                return "请输入正确的身份证号";
            }
        },
        //车牌号
        VehicleNo: function (val, item) {
            var reg = /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$/;
            var _b = reg.test(val);
            if (!_b) {
                return "请输入正确的车牌号";
            }
        },
        //手机号
        MyPhone: function (val, item) {
            var reg = /^1[3|4|5|7|8][0-9]{9}$/;
            var _b = reg.test(val);
            if (!_b) {
                return "请输入正确的手机号码";
            }
        },
        myemail:function(val,item){
            var reg = /^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$/;
            var _b = reg.test(val);
            if (!_b) {
                return "请输入正确的邮箱地址";
            }
        }
    });

    exports('formvalidate', formvalidate);

});