(function() {
    angular.module('app')
        .factory('todoService', ['$http', function($http) {
        	var method = {
	            getList : function() {
	                return $http.get('api/api/Todo/GetTodo')
	            }
	        }
            return method;

        }]);
})()
