(function() {
    angular.module('app')
        .factory('todoService', ['$http', function($http) {
            var method = {
                //取得待辦清單
	            getTodoList : function() {
	                return $http.get('api/api/Todo/GetTodo')
	            },
                //新增待辦
	            addTodo: function () {

	            },
                //刪除待辦
	            delTodo: function () {

	            },
                //完成待辦
	            finishTodo: function () {

	            },
                //更新待辦
	            updateTodo: function () {

	            },
                //取得完成番茄清單
	            getDoneList: function () {

	            },
                //開始番茄計時
	            startCount: function () {

	            },
                //暫停番茄計時
	            pauseCount: function () {

	            },
                //取消番茄計時
	            cancelCount: function () {

	            },
                //完成番茄計時
	            finishCount: function () {

	            },
	        }
            return method;

        }]);
})()
