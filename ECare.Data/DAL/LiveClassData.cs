using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ECare.Data.DAL
{
    public class LiveClassData
    {
        private readonly IUnitOfWork unitOfWork;
        public LiveClassData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<LiveClass> GetLiveClasss()
        {
            var LiveClassDetails = unitOfWork.LiveClassRepository.Get();
            return LiveClassDetails;
        }

        public LiveClass GetLiveClass(int Id)
        {
            var LiveClassDetails = unitOfWork.LiveClassRepository.GetById(Id);
            return LiveClassDetails;
        }

        public bool Post(LiveClass _LiveClass)
        {
            bool result = false;
            if (_LiveClass != null)
            {
                unitOfWork.LiveClassRepository.Insert(_LiveClass);
                unitOfWork.Save();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //update LiveClass
        public bool Put(int Id, LiveClass _LiveClass)
        {
            var LiveClassDetail = unitOfWork.LiveClassRepository.GetById(Id);
            if (LiveClassDetail == null)
            {
                return false;
            }
            else
            {
                if (Id != LiveClassDetail.Id)
                {
                    return false;
                }
                else
                {
                    LiveClassDetail.Name = string.IsNullOrEmpty(_LiveClass.Name)? LiveClassDetail.Name: _LiveClass.Name;
                    LiveClassDetail.Link = string.IsNullOrEmpty(_LiveClass.Link) ? LiveClassDetail.Link : _LiveClass.Link;
                    LiveClassDetail.Active = _LiveClass.Active?? LiveClassDetail.Active;
                    LiveClassDetail.Class = _LiveClass.Class;
                    LiveClassDetail.StartTime = _LiveClass.StartTime?? LiveClassDetail.StartTime;
                    LiveClassDetail.EndTime = _LiveClass.EndTime?? LiveClassDetail.EndTime;
                    LiveClassDetail.Date = _LiveClass.Date?? LiveClassDetail.Date;
                }
            }
            
            try
            {
                unitOfWork.LiveClassRepository.Update(LiveClassDetail);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            return true;
        }

        //delete LiveClass by id
        public LiveClass Delete(int Id)
        {
            LiveClass _LiveClass = unitOfWork.LiveClassRepository.GetById(Id);
            if (_LiveClass == null)
            {
                return _LiveClass;
            }
            try
            {
                unitOfWork.LiveClassRepository.Delete(_LiveClass?.Id);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            return _LiveClass;
        }
    }
}