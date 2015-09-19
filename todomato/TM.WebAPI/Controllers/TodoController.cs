using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TM.BLL.Services;
using TM.Domain.ViewModel;

namespace TM.WebAPI.Controllers
{
    public class TodoController : ApiController
    {
        private TodoService service;

        //TODO 9/19 API 項目
        /*
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
         
         */

        public TodoController()
        {
            service = new TodoService();
        }

        // GET: api/Todo
        [HttpGet]
        public HttpResponseMessage GetTodo()
        {
            try
            {
                // 取得待辦資料
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message.ToString());
            }
           
        }

        [HttpGet]
        public HttpResponseMessage Get(int CurrPage, int PageSize)
        {
            try
            {
                // 總數量
                int TotalRow = 0;
                // 向BLL取得資料
                var datas = service.Get(CurrPage, PageSize, out TotalRow);
                // 回傳一個JSON Object
                var Rvl = new { Total = TotalRow, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            
        }

        [HttpGet]
        public HttpResponseMessage Get(int TodoID)
        {
            try
            {
                var data = service.GetById(TodoID.ToString());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
 
        // POST: api/Todo
        public HttpResponseMessage Add(TodoViewModel models)
        {
            try
            {
                service.AddTodo(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // PUT: api/Todo/5
        public HttpResponseMessage Update(TodoViewModel models)
        {
            try
            {
                service.UpdateTodo(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // DELETE: api/Todo/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                service.Delete(id.ToString());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
