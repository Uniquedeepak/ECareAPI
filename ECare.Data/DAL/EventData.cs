using ECare.Data.BAL;
using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ECare.Data.DAL
{
    public class EventData : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public EventData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<Event> GetEvents()
        {
            var Events = unitOfWork.EventRepository.Query().OrderBy(x => x.Id).ToList();
            return Events;
        }

        public Event Get(int id)
        {
            Event objEvent = unitOfWork.EventRepository.GetById(id);
            if (objEvent == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return objEvent;
        }

        public bool Post(Event _Event)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            if (ModelState.IsValid)
            {
                unitOfWork.EventRepository.Insert(_Event);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(int Id, Event _Event)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            if (Id != _Event.Id)
            {
                return false;
            }

            try
            {
                unitOfWork.EventRepository.Update(_Event);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public Event Delete(int Id)
        {
            Event _Event = unitOfWork.EventRepository.GetById(Id);
            if (_Event == null)
            {
                return _Event;
            }

            try
            {
                unitOfWork.EventRepository.Delete(Id);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return _Event;
            }
            return _Event;
        }
    }

}
