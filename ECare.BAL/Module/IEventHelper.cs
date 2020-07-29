using ECare.BAL.Model;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface IEventHelper
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEvent(int Id);
        Task<string> InsertEvent(Event _Event);
        Task<string> UpdateEvent(int Id, Event _Event);
        Task<string> DeleteEvent(int Id);
    }
}
