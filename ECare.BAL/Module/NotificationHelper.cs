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
        public async Task<List<Model.Notification>> GetNotifications()
        {
            NotificationData obj = new NotificationData();
            var NotificationList = obj.GetNotifications();
            var notification = Mapper.Map<List<Data.Notification>, List<Model.Notification>>(NotificationList);
            return notification;
        }
        public async Task<Model.Notification> GetNotification(int Id)
        {
            NotificationData obj = new NotificationData();
            var NotificationList = obj.Get(Id);
            var notification = Mapper.Map<Data.Notification, Model.Notification>(NotificationList);
            return notification;
        }
        public async Task<string> InsertNotification(Model.Notification _notification)
        {
            string Status = string.Empty;
            NotificationData obj = new NotificationData();
            var notification = (Data.Notification)Mapper.Map<Model.Notification, Data.Notification>(_notification);
            var result =  obj.Post(notification);
            Status = result ? "Notification successfully saved." : "Failed to saved Notification";
            return Status;
        }
        public async Task<string> UpdateNotification(int Id, Model.Notification _notification)
        {
            string Status = string.Empty;
            NotificationData obj = new NotificationData();
            var notification = Mapper.Map<Model.Notification, Data.Notification>(_notification);
            var result = obj.Put(Id,notification);
            Status = result ? "Notification updated successfully." : "Failed to update Notification";
            return Status;
        }
        public async Task<string> DeleteNotification(int Id)
        {
            string Status = string.Empty;
            NotificationData obj = new NotificationData();
            var result = obj.Delete(Id);
            Status = result != null ? "Notification deleted successfully." : "Failed to delete Notification";
            return Status;
        }
    }
}
