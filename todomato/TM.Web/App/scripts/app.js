﻿'use strict';
(function () {
    var app = angular.module("app", ['ngRoute', 'ui.bootstrap', 'timer']);
    app.config(['$routeProvider', function ($routeProvider) {
        //路由配置
        var route = $routeProvider;
        route.when("/todo/list", { controller: 'todo as t', templateUrl: 'App/views/todo/list.html' });
        route.when("/", { redirectTo: '/todo/list' });
        route.otherwise({ templateUrl: '/utils-404' });
    }
    ]);

    //app.value('WebAPIHost', 'http://localhost:1535/api/api');
    app.value('WebAPIHost', 'http://posp.brightideas.com.tw/api');
})();
