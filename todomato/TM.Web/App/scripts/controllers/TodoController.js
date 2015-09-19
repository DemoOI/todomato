(function () {

    angular.module('app')
        .controller('todo', MainCtrl);

    function MainCtrl($scope, $interval, todoService) {
        var vm = this;
        vm.todolist = [];
        vm.tomatolist = [];
        vm.needTomato = 1;
        vm.timerSeconds = 0;
        vm.timeDisplay;
        vm.list = todoService.getList();

        vm.clickEvent = {
            addTodo: function () {
				var newary = [];
				newary.push({
					id: "new",
					todo: vm.todo,
					needTomato: vm.needTomato,
					finishTomato: 0
				});
				angular.forEach(vm.todolist, function(item, idx){
				    newary.push(item);
				});
				vm.todolist = newary;
				//初始化
				vm.todo = "";
            },
            finishTodo: function(){
            	console.log('finish');
            },
            startTodo: function(todo){
                console.log('start');
                console.log(todo);
                //計時開始
                var newary = {};
            	var max = 1500;
            	var sec = 60;
            	var date = new Date();
            	var startmin = date.getMinutes();
            	var starthour = date.getHours();

    	        var interval = $interval(function () {
    	        	sec -= 1;
    	        	if (sec < 0) {sec = 60};
    	        	max -= 1;
		            vm.timerSeconds = (vm.timerSeconds + 1);
		            vm.timeDisplay = Math.floor((max / 60)) + ':' + (sec % 60);
	            	console.log(vm.timeDisplay);
		            //時間終了
		            if (vm.timerSeconds == 10) {
		                $interval.cancel(interval);

		            	todo.finishTomato += 1;
		            	vm.timerSeconds = 0;
		            	newary.todo = todo.todo
		            	newary.startmin = startmin;
		            	newary.starthour = starthour;
		                vm.tomatolist.push(newary);
		            }
		        }, 1000);
            }
        }

        
        vm.todolist.push({
        	id: "1",
            todo: "一號事件",
            needTomato: 3,
            finishTomato: 0
        });
         vm.todolist.push({
            id: "2",
            todo: "二號事件",
            needTomato: 3,
            finishTomato: 0
        });
    }

})()
