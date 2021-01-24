var NOTICEINDEX = 0;
layui.define(['jquery'], function (exports) {
    var $ = layui.jquery;
    layui.link(layui.cache.base + 'extend/notice/notice.css');
    var noticeObj = {
        default: {
            type: "",
            calssName: "",
            title: "This is a notice",
            radius: "",
            icon: "",
            style: "",
            align: "",
            autoClose: true,
            time: 3000,
            click: true,
            end: null
        },
        init: function ($data) {
            var options = $.extend(this.default, $data);
            var _classN = "layui-bg-blue";
            switch (options.type) {
                case 'warm':
                    _classN = "layui-bg-yellow";
                    break;
                case 'danger':
                    _classN = "layui-bg-red";
                    break;
                case 'custom':
                    _classN = options.calssName;
                    break;
                default:

                    break;
            };

            var noticeObjHtml = '<div class="layui-anim layui-anim-up layui-notice box-' + options.align+'"><div class="notice ' + _classN + ' has-radius">';

            var noticeClass = 'layui-notice-' + NOTICEINDEX;

            noticeObjHtml += '<div class="txt-left ' + noticeClass + '">';

            if (options.icon) {
                noticeObjHtml += '  <i class="layui-icon ' + options.icon + '"></i>';
            }
            noticeObjHtml += '<span class="box-txt">' + options.title +'</span>';
            if (options.click) {
                noticeObjHtml +=
                    '<a href="javascript:;" class="pull-right"><i class="layui-icon layui-icon-close notice-close"></i></a>';
            }
            // 结束
            noticeObjHtml += '</div></div></div>';
            $("body").append(noticeObjHtml);

            if (options.autoClose) {
                window.setTimeout(function () {
                    $("." + noticeClass).parents(".layui-notice").addClass("layui-anim-fadeout").remove();
                }, options.time);
            }

            if (options.click) {
                $(".notice-close").click(function () {
                    $(this).parents(".layui-notice").addClass("layui-anim-fadeout").remove();
                });
            }

            this.end = function (callback) {
                callback();
            };
            NOTICEINDEX++;
        },
        close:function (){
            var noticeClass = 'layui-notice-' + NOTICEINDEX;
            $("." + noticeClass).parents(".layui-notice").addClass("layui-anim-fadeout").remove();
        },
        closeAll:function (){
            $(".notice-close").parents(".layui-notice").addClass("layui-anim-fadeout").remove();
        }
    };
    exports('notice', noticeObj);
});