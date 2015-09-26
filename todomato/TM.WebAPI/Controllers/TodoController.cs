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

        public TodoController()
        {
            service = new TodoService();
        }

        // 取得待辦清單
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
        public HttpResponseMessage Get([FromBody]string id)
        {
            try
            {
                var data = service.GetById(id.ToString());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
 
        // 新增待辦
        [HttpPost]
        public HttpResponseMessage Add(TodoViewModel models)
        {
            try
            {
                var result = service.AddTodo(models);
                return Request.CreateResponse(result);
            }
            catch (Exception ex)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        //CHECK 更新待辦
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

        // 刪除待辦
        [HttpPost]
        public HttpResponseMessage Delete([FromBody]string id)
        {
            try
            {
                service.Delete(id.ToString());
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // 完成待辦
        [HttpPost]
        public HttpResponseMessage FinishTodo([FromBody]string id)
        {
            try
            {
                service.FinishTodo(id.ToString());
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // 完成待辦
        [HttpPost]
        public HttpResponseMessage CancelFinishTodo([FromBody]string id)
        {
            try
            {
                service.FinishTodo(id.ToString(),false);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


    }
}
