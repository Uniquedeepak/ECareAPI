using ECare.BAL.Model;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface ILeaveHelper
    {
        Task<List<Leave>> GetLeaves();
        Task<Leave> GetLeave(int Id);
        Task<string> InsertLeave(Leave _Leave);
        Task<string> UpdateLeave(int Id, Leave _Leave);
        Task<string> DeleteLeave(int Id);
    }
}
