(function() {
    angular.module('app')
        .factory('todoService', ['$http', 'WebAPIHost', function ($http, WebAPIHost) {
            var method = {
                //取得待辦清單
	            getTodoList : function() {
	                return $http.get('api/api/Todo/GetTodo')
	            },
                //新增待辦
	            addTodo: function (todo, needTomato) {
	                return $http.post(WebAPIHost + '/Todo/Add', {
	                    "Title": todo,
	                    "NeedTomato": needTomato,
                        "DoneTomato": '0'
	                }).
                      then(function (response) {
                          console.log(response);
                          return response;
                      }, function (response) {
                          // called asynchronously if an error occurs
                          // or server returns response with an error status.
                      });
	            },
                //刪除待辦
	            delTodo: function () {

	            },
                //完成待辦
	            finishTodo: function (id) {
	                return $http.post(WebAPIHost + '/Todo/FinishTodo', JSON.stringify(id))
	                .then(function (response) {
                          console.log(response);
                          return response;
	                }, function (response) {
	                    // called asynchronously if an error occurs
	                    // or server returns response with an error status.
	                });
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
