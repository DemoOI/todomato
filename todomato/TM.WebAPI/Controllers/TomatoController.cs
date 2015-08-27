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
    public class TomatoController : ApiController
    {
        private TomatoService service;

        public TomatoController()
        {
            service = new TomatoService();
        }

        // GET: api/Tomato
        [HttpGet]
        public HttpResponseMessage GetTomato()
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

        // 取得一周的番茄資料,含天的資訊
        [HttpGet]
        public HttpResponseMessage GetWeekListByDay()
        {
            try
            {
                // 取得待辦資料
                var datas = service.GetWeekListByDay();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
            
 
        // POST: api/Tomato
        public HttpResponseMessage Add(TomatoViewModel models)
        {
            try
            {
                service.AddTomato(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // PUT: api/Tomato/5
        public HttpResponseMessage Update(TomatoViewModel models)
        {
            try
            {
                service.SaveTomato(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // DELETE: api/Tomato/5
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
