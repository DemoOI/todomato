(function() {
    angular.module('app')
        .factory('todoService', ['$http', function($http) {
        	var method = {
	            getList : function() {
	                $http.get('api/api/Todo/GetTodo')
	                    .success(function (data) {
	                        console.log(data);
	                    });
	        		
	            }
	        }
            return method;

        }]);
})()
