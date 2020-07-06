using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ECare.Data.DAL
{
    public class LeaveData
    {
        private readonly IUnitOfWork unitOfWork;
        public LeaveData()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public List<Leave> GetLeaves()
        {
            var LeaveDetails = unitOfWork.LeaveRepository.Get();
            return LeaveDetails;
        }

        public Leave GetLeave(int Id)
        {
            var LeaveDetails = unitOfWork.LeaveRepository.GetById(Id);
            return LeaveDetails;
        }

        public bool Post(Leave _Leave)
        {
            bool result = false;
            if (_Leave != null)
            {
                unitOfWork.LeaveRepository.Insert(_Leave);
                unitOfWork.Save();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //update Leave
        public bool Put(int Id, Leave _Leave)
        {
            var LeaveDetail = unitOfWork.LeaveRepository.GetById(Id);
            if (LeaveDetail == null)
            {
                return false;
            }
            else
            {
                if (Id != LeaveDetail.Id)
                {
                    return false;
                }
                else
                {
                    LeaveDetail.StuAdmNo = string.IsNullOrEmpty(_Leave.StuAdmNo)? LeaveDetail.StuAdmNo: _Leave.StuAdmNo;
                    LeaveDetail.Description = string.IsNullOrEmpty(_Leave.Description) ? LeaveDetail.Description : _Leave.Description;
                    LeaveDetail.Status = _Leave.Status?? LeaveDetail.Status;
                    LeaveDetail.Class = _Leave.Class ?? LeaveDetail.Class;
                    LeaveDetail.FromDate = _Leave.FromDate?? LeaveDetail.FromDate;
                    LeaveDetail.ToDate = _Leave.ToDate?? LeaveDetail.ToDate;
                    LeaveDetail.Type = _Leave.Type?? LeaveDetail.Type;
                    LeaveDetail.Date = _Leave.Date?? LeaveDetail.Date;
                }
            }
            
            try
            {
                unitOfWork.LeaveRepository.Update(LeaveDetail);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            return true;
        }

        //delete Leave by id
        public Leave Delete(int Id)
        {
            Leave _Leave = unitOfWork.LeaveRepository.GetById(Id);
            if (_Leave == null)
            {
                return _Leave;
            }
            try
            {
                unitOfWork.LeaveRepository.Delete(_Leave?.Id);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            return _Leave;
        }
    }
}