using ECare.BAL.Model;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface ILiveClassHelper
    {
        Task<List<LiveClass>> GetLiveClasss();
        Task<LiveClass> GetLiveClass(int Id);
        Task<string> InsertLiveClass(LiveClass _LiveClass);
        Task<string> UpdateLiveClass(int Id, LiveClass _LiveClass);
        Task<string> DeleteLiveClass(int Id);
    }
}
