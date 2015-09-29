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

        // 取得完成番茄清單
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


        // 開始番茄計時
        [HttpPost]
        public HttpResponseMessage StartTomato(TomatoViewModel models)
        {
            try
            {
                string tomatoID = service.AddTomato(models);
                return Request.CreateResponse(HttpStatusCode.OK, tomatoID);
            }
            catch (Exception ex)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // 暫停番茄計時
        [HttpPost]
        public HttpResponseMessage PauseTomato(TomatoViewModel models)
        {
            try
            {
                service.PauseTomato(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // 取消番茄計時
        [HttpPost]
        public HttpResponseMessage CancelTomato([FromBody]string tomatoID)
        {
            try
            {
                //將番茄標記為刪除
                service.Delete(tomatoID);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // 完成番茄計時
        [HttpPost]
        public HttpResponseMessage FinishTomato([FromBody]string tomatoID)
        {
            try
            {
                //將番茄標記為刪除
                service.FinishTomato(tomatoID);
                // 取得番茄資料
                var datas = service.GetWeekListByDay(); 
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

    }
}
