(function () {

    angular.module('app')
        .controller('todo', MainCtrl);

    function MainCtrl($scope, $interval, todoService) {
        var vm = this;
        vm.todolist = [];
        vm.tomatolist = [];
        vm.needTomato = 1;
        vm.timerSeconds = 0;
        vm.timeDisplay = '番茄計時器';
        
        todoService.getTodoList().then(function (obj) {
            console.log('取得待辦列表:');
            console.log(obj.data)
            vm.todolist = obj.data;
        });

        // 觸發事件
        vm.clickEvent = {
            // 新增待辦
            addTodo: function () {
                var newary = [];

                todoService.addTodo(vm.todo, vm.needTomato).then(function (obj) {
                    var t = obj.data;
                    newary.push({
                        TodoID: t.TodoID,
                        Title: t.Title,
                        NeedTomato: t.NeedTomato,
                        DoneTomato: t.DoneTomato
                    });

                    angular.forEach(vm.todolist, function (item, idx) {
                        newary.push(item);
                    });
                    vm.todolist = newary;

                    //初始化
                    vm.todo = "";
                });
            },
            // 完成待辦
            finishTodo: function (todo) {
                todoService.finishTodo(todo.TodoID).then(function (obj) {
                    console.log(obj);
                });
            },
            // 開始待辦計時
            startTodo: function(todo){
                console.log('start');
                console.log(todo);
                // 計時開始
                vm.timeDisplay = '25:00';
                var newary = {};
            	var max = 300;
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
		            if (vm.timerSeconds == max) {
		                $interval.cancel(interval);

		            	todo.finishTomato += 1;
		            	vm.timerSeconds = 0;
		            	newary.todo = todo.todo
		            	newary.startmin = startmin;
		            	newary.starthour = starthour;
		            	vm.tomatolist.push(newary);
		            	vm.timeDisplay = '番茄計時器'
		            }
		        }, 1000);
            }
        }


    }

})()
