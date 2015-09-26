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
   public class TodoService
    {
        private IRepository<Todo> db;
        private IRepository<Tomato> tomatoDb;

        public TodoService() 
        {
            db = new GenericRepository<Todo>();
            tomatoDb = new GenericRepository<Tomato>();
        }


        /// <summary>取得所有待辦資料</summary>
        /// <returns></returns>
        public List<TodoViewModel> Get()
        {
            var DbResult = db.Get()
                    .Where(t => t.IsFinish != true)
                    .OrderByDescending(t => t.CreateTime)
                    .ToList();

            Mapper.CreateMap<Todo, TodoViewModel>();
            return Mapper.Map<List<Todo>, List<TodoViewModel>>(DbResult);
        }

        /// <summary>取得所有待辦資料(分頁)</summary>
        /// <returns></returns>
        public IQueryable<TodoViewModel> Get(int CurrPage, int PageSize, out int TotalRow)
        {
            // 取得所有筆數
            TotalRow = db.Get().ToList().Count();
            // 使用Linq篩選分頁
            var DbResult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            // Mapping到ViewModel
            Mapper.CreateMap<Todo, TodoViewModel>();
            return Mapper.Map<List<Todo>, List<TodoViewModel>>(DbResult).AsQueryable();
        }

        /// <summary>取得待辦資訊</summary>
        /// <param name="TodoID"></param>
        /// <returns></returns>
        public TodoViewModel GetById(string TodoID)
        {
            var DbResult = db.Get().Where(c => c.TodoID.Trim() == TodoID.Trim()).FirstOrDefault();
            Mapper.CreateMap<Todo, TodoViewModel>();
            return Mapper.Map<Todo, TodoViewModel>(DbResult);
        }

        /// <summary>取得待辦目前完成狀況</summary>
        /// <param name="TodoID"></param>
        /// <returns></returns>
        public string GetEventState(string TodoID)
        {
            var DbResult = db.Get().Where(c => c.TodoID.Trim() == TodoID.Trim()).SingleOrDefault();
            string result = string.Format("({0}/{1})", DbResult.DoneTomato, DbResult.NeedTomato);
            return result;
        }

        /// <summary>新增待辦資料</summary>
        /// <returns></returns>
        public TodoViewModel AddTodo(TodoViewModel models)
        {
            models.TodoID = Guid.NewGuid().ToString();
            models.CreateTime = DateTime.Now;
            models.Creator = "system";

            Mapper.CreateMap<TodoViewModel, Todo>();
            var todo = Mapper.Map<TodoViewModel, Todo>(models);
            db.Insert(todo);

            //TODO 存入標籤
            return models;
        }

        /// <summary>儲存待辦資訊</summary>
        /// <param name="models"></param>
        public void UpdateTodo(TodoViewModel models)
        {
            // 建立Mapping邏輯，只更新
            Mapper.CreateMap<TodoViewModel, Todo>()
                .ForMember(x => x.Updator, y => y.UseValue("system"))
                .ForMember(x => x.UpdateTime, y => y.UseValue(DateTime.Now))
                .ForMember(x => x.TodoID, y => y.Ignore())
                .ForMember(x => x.CreateTime, y => y.Ignore())
                .ForMember(x => x.Creator, y => y.Ignore())
                .ForMember(x => x.IsFinish, y => y.Ignore())
                .ForMember(x => x.NeedTomato, y => y.Ignore())
                .ForMember(x => x.DoneTomato, y => y.Ignore());

            Todo todo = db.GetByID(models.TodoID); 
   
            // 只更新ViewModel的部分到Entity  
            Mapper.Map(models, todo);  
               
            db.Update(todo);
        }

        /// <summary>完成待辦資訊</summary>
        /// <param name="models"></param>
        public void FinishTodo(string TodoID, bool done=true)
        {
            Todo todo = db.GetByID(TodoID);
            todo.IsFinish = done;
            todo.UpdateTime = DateTime.Now;
            todo.Updator = "system";

            db.Update(todo);
        }


        /// <summary>刪除待辦資訊</summary>
        /// <param name="TodoID"></param>
        public void Delete(string TodoID)
        {
            //var todo = GetById(TodoID);
            db.Delete(TodoID);
        }
    }
}

