using ECare.BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface INotificationHelper
    {
        Task<List<Notification>> GetNotifications();
        Task<Notification> GetNotification(int Id);
        Task<string> InsertNotification(Notification _notification);
        Task<string> UpdateNotification(int Id, Notification _notification);
        Task<string> DeleteNotification(int Id);
    }
}
