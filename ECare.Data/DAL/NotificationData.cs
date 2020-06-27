﻿using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ECare.Data.DAL
{
    public class NotificationData
    {
        private readonly IUnitOfWork unitOfWork;

        public NotificationData()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public List<Notification> GetNotifications()
        {
            var Notifications = unitOfWork.NotificationRepository.Query().OrderBy(x => x.Id).ToList();
            return Notifications;
        }

        public Notification Get(int id)
        {
            Notification objNotification = unitOfWork.NotificationRepository.GetById(id);
            return objNotification;
        }
        public List<Notification> GetNotificationByClass(string ClassId)
        {
            int classId = Convert.ToInt32(ClassId);
            List<Notification> objNotification = unitOfWork.NotificationRepository.Get(x => x.Class==classId && x.Status==1).ToList();
            return objNotification;
        }

        public bool Post(Notification _Notification)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            if (_Notification !=null)
            {
                unitOfWork.NotificationRepository.Insert(_Notification);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(int Id, Notification _Notification)
        {
            if (_Notification==null)
            {
                return false;
            }

            if (Id != _Notification.Id)
            {
                return false;
            }
            
            try
            {
                unitOfWork.NotificationRepository.Update(_Notification);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public Notification Delete(int Id)
        {
            Notification _Notification = unitOfWork.NotificationRepository.GetById(Id);
            if (_Notification == null)
            {
                return _Notification;
            }
            
            try
            {
                unitOfWork.NotificationRepository.Delete(Id);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return _Notification;
            }
            return _Notification;
        }
    }
}