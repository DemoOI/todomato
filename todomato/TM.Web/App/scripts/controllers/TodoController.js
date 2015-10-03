(function () {

    angular.module('app')
        .controller('todo', MainCtrl);

    function MainCtrl($scope, $interval, todoService) {
        var vm = this;
        vm.todolist = [];
        vm.tomatolistofday = [];
        vm.needTomato = 1;
        vm.timerSeconds = 0;
        vm.timeDisplay = '番茄計時器';
        

        //todo 應該合併一起拿
        todoService.getTodoList().then(function (todos) {
            console.log('取得待辦列表:');
            console.log(todos.data)
            vm.todolist = todos.data;
        });

        todoService.getDoneList().then(function (tomatos) {
            console.log('取得完成番茄列表:');
            console.log(tomatos.data)
            vm.tomatolistofday = tomatos.data;
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
            startTodo: function (todo) {
                //隱藏開btn
                vm.IsStart = true;

                // 新增番茄service 
                var tomatoID;
                todoService.startCount(todo).then(function (obj) {
                    tomatoID = obj.data;
                    console.log('新增番茄service done');
                    console.log(obj);
                });
                
                // 計時開始
                vm.timeDisplay = '25:00';
                var max = 1500;
                var setMaxTime = 1500;
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
		            if (vm.timerSeconds == setMaxTime) {
		                $interval.cancel(interval);

		                //完成番茄 service
		                todoService.finishCount(tomatoID).then(function (tomatos) {
		                    console.log('完成番茄 service done');
		                    console.log(tomatos.data);
		                    vm.tomatolistofday = tomatos.data;

		                    //reset
		                    todo.DoneTomato += 1;
		                    vm.timerSeconds = 0;
		                    vm.timeDisplay = '番茄計時器'
		                    vm.IsStart = false;
		                    
		                    alert("完成了一顆番茄,請讓眼睛休息五分鐘唷!!!");
		                    notifyMe();
		                });

                       
		            }
		        }, 1000);
            }
        }


    }

    function notifyMe() {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notification");
        }

        else if (Notification.permission === "granted") {
            var notification = new Notification("完成一顆番茄!");
        }

            // Otherwise, we need to ask the user for permission
            // Note, Chrome does not implement the permission static property
            // So we have to check for NOT 'denied' instead of 'default'
        else if (Notification.permission !== 'denied') {
            Notification.requestPermission(function (permission) {

                // Whatever the user answers, we make sure we store the information
                if (!('permission' in Notification)) {
                    Notification.permission = permission;
                }

                // If the user is okay, let's create a notification
                if (permission === "granted") {
                    var notification = new Notification("完成一顆番茄!");
                }
            });
        }

        // At last, if the user already denied any notification, and you
        // want to be respectful there is no need to bother him any more.
    }

})()
