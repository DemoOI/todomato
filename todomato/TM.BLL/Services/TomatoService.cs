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
        private IRepository<Todo> todoDb;

        public TomatoService() 
        {
            db = new GenericRepository<Tomato>();
            todoDb = new GenericRepository<Todo>();
        }

        /// <summary>
        /// 取得番茄資訊,以天為單位
        /// </summary>
        /// <returns></returns>
        public List<TomatoListByDayViewModel> GetWeekListByDay()
        {
            //取得一周已經完成的番茄
            var DbResult = db.Get().Where(x => x.IsCompleted == true).Where(x => x.FinishTime > DateTime.Now.AddDays(-7)).ToList();
            var result = new List<TomatoListByDayViewModel>();
            var tList = new List<Tomato>();


            //組裝以天為單位的番茄list
            var tomatoLists = DbResult.GroupBy(u => String.Format("{0:MM/dd/yyyy}", u.FinishTime))
                                    .Select(g => tList = g.ToList())
                                    .ToList();
            tomatoLists.ForEach(x => result.Add(new TomatoListByDayViewModel(x)));

            return result;
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
        public string AddTomato(TomatoViewModel models)
        {
            models.TomatoID = Guid.NewGuid().ToString();
            models.CreateTime = DateTime.Now;
            models.IsCompleted = false;

            Mapper.CreateMap<TomatoViewModel, Tomato>()
                    .ForMember(x => x.SpentTime, y => y.Ignore())
                    .ForMember(x => x.FinishTime, y => y.Ignore());
            var Tomato = Mapper.Map<TomatoViewModel, Tomato>(models);
            db.Insert(Tomato);

            return models.TomatoID;
        }

        /// <summary>完成番茄</summary>
        /// <returns></returns>
        public void FinishTomato(string tomatoID)
        {
            //TODO 加入Transaction

            Tomato tomato = db.GetByID(tomatoID);
            tomato.IsCompleted = true;
            tomato.FinishTime = DateTime.Now;
            db.Update(tomato);

            Todo todo = todoDb.GetByID(tomato.TodoID);
            todo.DoneTomato = (todo.DoneTomato == null) ? 1 : todo.DoneTomato + 1;
            todo.UpdateTime = DateTime.Now;
            todo.Updator = "system";
            todoDb.Update(todo);
        }

        /// <summary>番茄暫停</summary>
        /// <param name="models"></param>
        public void PauseTomato(TomatoViewModel models)
        {
            var tomato = db.GetByID(models.TomatoID);
            tomato.PauseCount = tomato.PauseCount + 1;

            db.Update(tomato);
        }

        /// <summary>標示刪除番茄資訊</summary>
        /// <param name="TomatoID"></param>
        public void Delete(string TomatoID)
        {
            var tomato = db.GetByID(TomatoID);
            tomato.IsDeleted = true;
            tomato.FinishTime = DateTime.Now;

            db.Update(tomato);
        }

       
    }
}
