using GenericAPI.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ECare.Data.DAL
{
    public class HomeworkData
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string ConnectionName;
        public HomeworkData(string CSName)
        {
            ConnectionName = CSName;
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<tbl_homework> GetHomeworks()
        {
           ClassData _class = new ClassData(ConnectionName);
            var Homeworks = unitOfWork.HomeworkRepository.Get(orderBy: q => q.OrderBy(s => s.id));
            Homeworks.ForEach(cc => cc.@class = _class.GetClassName(cc.@class));
            return Homeworks;
        }

        public List<tbl_homework> GetHomeworkByClass(string ClassId)
        {
            List<tbl_homework> objHomework = unitOfWork.HomeworkRepository.Get(x=>x.@class==ClassId).ToList();
            return objHomework;
        }

        public tbl_homework Get(int id)
        {
            tbl_homework objHomework = unitOfWork.HomeworkRepository.GetById(id);
            return objHomework;
        }

        public bool Post(tbl_homework _Homework)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            if (_Homework != null)
            {
                unitOfWork.HomeworkRepository.Insert(_Homework);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(int Id, tbl_homework _Homework)
        {
            if (_Homework == null)
            {
                return false;
            }
            if (Id != _Homework.id)
            {
                return false;
            }
            
            try
            {
                unitOfWork.HomeworkRepository.Update(_Homework);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public tbl_homework Delete(int Id)
        {
            tbl_homework _Homework = unitOfWork.HomeworkRepository.GetById(Id);
            if (_Homework == null)
            {
                return _Homework;
            }
            
            try
            {
                unitOfWork.HomeworkRepository.Delete(_Homework);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return _Homework;
            }
            return _Homework;
        }
    }
}
