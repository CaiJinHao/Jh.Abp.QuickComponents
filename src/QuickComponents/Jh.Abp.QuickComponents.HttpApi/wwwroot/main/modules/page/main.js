//暂时没有用到
var vm = new Vue({
    el: '#page-wrapper',
    data: {
        userinfo: {},
    },
    methods: {
        RenderDom: function(form){
            vm.$forceUpdate();//强制重新渲染 dom
            // DOM 还没有更新
            vm.$nextTick(function () {
                // DOM 现在更新了
                form.render();
            });
        },
    }
});

layui.use(['jquery', 'code','element', 'datatable'], function () {
    var $ = layui.jquery;
    var element = layui.element;
   
});