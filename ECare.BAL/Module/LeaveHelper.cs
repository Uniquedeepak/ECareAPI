using AutoMapper;
using ECare.BAL.Model;
using ECare.Data;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public class LeaveHelper : ILeaveHelper
    {
        readonly LeaveData data = null;
        readonly StudentData studentData = null;
        public LeaveHelper(string CS_Name)
        {
            data = new LeaveData(CS_Name);
            studentData = new StudentData(CS_Name);

        }
        public async Task<List<Model.Leave>> GetLeaves()
        {
            
            var LeaveList = data.GetLeaves();
            var leave = Mapper.Map<List<Data.Leave>, List<Model.Leave>>(LeaveList);
            foreach (var item in leave)
            {
                string ClassId = string.Empty;
                if (string.IsNullOrEmpty(item.Class))
                {
                    ClassId = studentData.Get(item.StuAdmNo).Class;
                }
                else
                {
                    ClassId = item.Class;
                }
                item.Class = studentData.GetClassName(ClassId);
                item.Name = studentData.GetNameByAdmissionNo(item.StuAdmNo);
            }
            return leave;
        }
        public async Task<Model.Leave> GetLeave(int Id)
        {
            var LeaveList = data.GetLeave(Id);
            var Leave = Mapper.Map<Data.Leave, Model.Leave>(LeaveList);
            Leave.Class = studentData.GetClassName(Leave.Class);
            Leave.Name = studentData.GetNameByAdmissionNo(Leave.Name);
            return Leave;
        }
        public async Task<string> InsertLeave(Model.Leave _Leave)
        {
            string Status = string.Empty;
            var Leave = Mapper.Map<Model.Leave, Data.Leave>(_Leave);
            var result = data.Post(Leave);
            Status = result ? "Leave submitted successfully." : "Failed to submit Leave";
            return Status;
        }
        public async Task<string> UpdateLeave(int Id, Model.Leave _Leave)
        {
            string Status = string.Empty;
            var Leave = Mapper.Map<Model.Leave, Data.Leave>(_Leave);
            var result = data.Put(Id,Leave);
            Status = result ? "Leave updated successfully." : "Failed to update Leave";
            return Status;
        }
        public async Task<string> DeleteLeave(int Id)
        {
            string Status = string.Empty;
            var result = data.Delete(Id);
            Status = result != null ? "Leave deleted successfully." : "Failed to delete Leave";
            return Status;
        }
    }
}
