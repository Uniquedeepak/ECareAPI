using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ECare.Data.DAL
{
    public class SchoolData
    {
        private readonly IUnitOfWork unitOfWork;
        public SchoolData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<School> GetSchool()
        {
            var SchoolDetails = unitOfWork.SchoolRepository.Get(orderBy: q=>q.OrderByDescending(x => x.ID));
            return SchoolDetails;
        }

        public List<Session> GetSessions()
        {
            var session = unitOfWork.SessionRepository.Get();
            return session;
        }

        public string GetCurrentSession()
        {
            string session = unitOfWork.SessionRepository.GetFirstOrDefault(x=>x.IsActive==true).Session1;
            return session;
        }

        public bool Post(School school)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            
            if (school != null)
            {
                unitOfWork.SchoolRepository.Insert(school);
                unitOfWork.Save();
               return true;
            }
            else
            {
                return false;
            }
        }

        public bool Put(int ID, School school)
        {
            if (school == null)
            {
                return false;
            }
            if (ID != school.ID)
            {
                return false;
            }
            try
            {
                unitOfWork.SchoolRepository.Update(school);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public School Delete(int ID)
        {
            School school = unitOfWork.SchoolRepository.GetById(ID);
            if (school == null)
            {
                return school;
            }
            try
            {
                unitOfWork.SchoolRepository.Delete(ID);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return school;
            }
            return school;
        }
    }
}
