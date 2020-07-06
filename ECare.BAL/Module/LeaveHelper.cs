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
        public async Task<List<Model.Leave>> GetLeaves()
        {
            LeaveData obj = new LeaveData();
            var LeaveList = obj.GetLeaves();
            var Leave = Mapper.Map<List<Data.Leave>, List<Model.Leave>>(LeaveList);
            return Leave;
        }
        public async Task<Model.Leave> GetLeave(int Id)
        {
            LeaveData obj = new LeaveData();
            var LeaveList = obj.GetLeave(Id);
            var Leave = Mapper.Map<Data.Leave, Model.Leave>(LeaveList);
            return Leave;
        }
        public async Task<string> InsertLeave(Model.Leave _Leave)
        {
            string Status = string.Empty;
            LeaveData obj = new LeaveData();
            var Leave = Mapper.Map<Model.Leave, Data.Leave>(_Leave);
            var result =  obj.Post(Leave);
            Status = result ? "Leave submitted successfully." : "Failed to submit Leave";
            return Status;
        }
        public async Task<string> UpdateLeave(int Id, Model.Leave _Leave)
        {
            string Status = string.Empty;
            LeaveData obj = new LeaveData();
            var Leave = Mapper.Map<Model.Leave, Data.Leave>(_Leave);
            var result = obj.Put(Id,Leave);
            Status = result ? "Leave updated successfully." : "Failed to update Leave";
            return Status;
        }
        public async Task<string> DeleteLeave(int Id)
        {
            string Status = string.Empty;
            LeaveData obj = new LeaveData();
            var result = obj.Delete(Id);
            Status = result != null ? "Leave deleted successfully." : "Failed to delete Leave";
            return Status;
        }
    }
}
