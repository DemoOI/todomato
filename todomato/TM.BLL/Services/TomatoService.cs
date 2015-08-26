using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.DLL.Interface;
using TM.DLL.Repository;
using TM.Domain;
using TM.Domain.ViewModel;

namespace TM.BLL.Services
{
    public class TomatoService
    {
        private IRepository<Tomato> db;

        public TomatoService() 
        {
            db = new GenericRepository<Tomato>();
        }


        /// <summary>取得所有番茄資料</summary>
        /// <returns></returns>
        public List<TomatoViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            Mapper.CreateMap<Tomato, TomatoViewModel>();
            return Mapper.Map<List<Tomato>, List<TomatoViewModel>>(DbResult);
        }

        /// <summary>取得所有番茄資料(分頁)</summary>
        /// <returns></returns>
        public IQueryable<TomatoViewModel> Get(int CurrPage, int PageSize, out int TotalRow)
        {
            // 取得所有筆數
            TotalRow = db.Get().ToList().Count();
            // 使用Linq篩選分頁
            var DbResult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            // Mapping到ViewModel
            Mapper.CreateMap<Tomato, TomatoViewModel>();
            return Mapper.Map<List<Tomato>, List<TomatoViewModel>>(DbResult).AsQueryable();
        }

        /// <summary>取得番茄資訊</summary>
        /// <param name="TomatoID"></param>
        /// <returns></returns>
        public TomatoViewModel Get(string TomatoID)
        {
            var DbResult = db.Get().Where(c => c.TomatoID.Trim() == TomatoID.Trim()).FirstOrDefault();
            Mapper.CreateMap<Tomato, TomatoViewModel>();
            return Mapper.Map<Tomato, TomatoViewModel>(DbResult);
        }

        /// <summary>新增番茄資料</summary>
        /// <returns></returns>
        public void AddTomato(TomatoViewModel models)
        {
            models.TomatoID = Guid.NewGuid().ToString();

            Mapper.CreateMap<TomatoViewModel, Tomato>();
            var Tomato = Mapper.Map<TomatoViewModel, Tomato>(models);
            db.Insert(Tomato);
        }

        /// <summary>儲存番茄資訊</summary>
        /// <param name="models"></param>
        public void SaveTomato(TomatoViewModel models)
        {
            // 建立Mapping邏輯，只更新
            Mapper.CreateMap<TomatoViewModel, Tomato>()
                .ForMember(x => x.TomatoID, y => y.Ignore());

            Tomato Tomato = db.GetByID(models.TomatoID); 
   
            // 只更新ViewModel的部分到Entity  
            Mapper.Map(models, Tomato);  
               
            db.Update(Tomato);
        }

        /// <summary>刪除番茄資訊</summary>
        /// <param name="TomatoID"></param>
        public void Delete(string TomatoID)
        {
            var Tomato = db.GetByID(TomatoID);
            db.Delete(Tomato);
        }
    }
}
