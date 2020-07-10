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
    public class NotificationHelper : INotificationHelper
    {
        readonly NotificationData data = null;
        public NotificationHelper(string CS_Name)
        {
            data = new NotificationData(CS_Name);
        }
        public async Task<List<Model.Notification>> GetNotifications()
        {
            
            var NotificationList = data.GetNotifications();
            var notification = Mapper.Map<List<Data.Notification>, List<Model.Notification>>(NotificationList);
            return notification;
        }
        public async Task<Model.Notification> GetNotification(int Id)
        {
            var NotificationList = data.Get(Id);
            var notification = Mapper.Map<Data.Notification, Model.Notification>(NotificationList);
            return notification;
        }
        public async Task<string> InsertNotification(Model.Notification _notification)
        {
            string Status = string.Empty;
            var notification = (Data.Notification)Mapper.Map<Model.Notification, Data.Notification>(_notification);
            var result = data.Post(notification);
            Status = result ? "Notification successfully saved." : "Failed to saved Notification";
            return Status;
        }
        public async Task<string> UpdateNotification(int Id, Model.Notification _notification)
        {
            string Status = string.Empty;
            var notification = Mapper.Map<Model.Notification, Data.Notification>(_notification);
            var result = data.Put(Id,notification);
            Status = result ? "Notification updated successfully." : "Failed to update Notification";
            return Status;
        }
        public async Task<string> DeleteNotification(int Id)
        {
            string Status = string.Empty;
            var result = data.Delete(Id);
            Status = result != null ? "Notification deleted successfully." : "Failed to delete Notification";
            return Status;
        }
    }
}
