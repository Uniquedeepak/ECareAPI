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
        public LeaveHelper(string CS_Name)
        {
            data = new LeaveData(CS_Name);
        }
        public async Task<List<Model.Leave>> GetLeaves()
        {
            
            var LeaveList = data.GetLeaves();
            var Leave = Mapper.Map<List<Data.Leave>, List<Model.Leave>>(LeaveList);
            return Leave;
        }
        public async Task<Model.Leave> GetLeave(int Id)
        {
            var LeaveList = data.GetLeave(Id);
            var Leave = Mapper.Map<Data.Leave, Model.Leave>(LeaveList);
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
