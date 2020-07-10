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
    public class LiveClassHelper : ILiveClassHelper
    {
        readonly LiveClassData data = null;
        public LiveClassHelper(string CS_Name)
        {
            data = new LiveClassData(CS_Name);
        }
        public async Task<List<Model.LiveClass>> GetLiveClasss()
        {
            
            var LiveClassList = data.GetLiveClasss();
            var LiveClass = Mapper.Map<List<Data.LiveClass>, List<Model.LiveClass>>(LiveClassList);
            return LiveClass;
        }
        public async Task<Model.LiveClass> GetLiveClass(int Id)
        {
            var LiveClassList = data.GetLiveClass(Id);
            var LiveClass = Mapper.Map<Data.LiveClass, Model.LiveClass>(LiveClassList);
            return LiveClass;
        }
        public async Task<string> InsertLiveClass(Model.LiveClass _LiveClass)
        {
            string Status = string.Empty;
            var LiveClass = Mapper.Map<Model.LiveClass, Data.LiveClass>(_LiveClass);
            var result = data.Post(LiveClass);
            Status = result ? "LiveClass submitted successfully." : "Failed to submit LiveClass";
            return Status;
        }
        public async Task<string> UpdateLiveClass(int Id, Model.LiveClass _LiveClass)
        {
            string Status = string.Empty;
            var LiveClass = Mapper.Map<Model.LiveClass, Data.LiveClass>(_LiveClass);
            var result = data.Put(Id,LiveClass);
            Status = result ? "LiveClass updated successfully." : "Failed to update LiveClass";
            return Status;
        }
        public async Task<string> DeleteLiveClass(int Id)
        {
            string Status = string.Empty;
            var result = data.Delete(Id);
            Status = result != null ? "LiveClass deleted successfully." : "Failed to delete LiveClass";
            return Status;
        }
    }
}
