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
    public class TransportData 
    {
        private readonly IUnitOfWork unitOfWork;
        public TransportData()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public List<TransportCharge> GetTransportCharge()
        {
            var TransportCharge = unitOfWork.TransportRepository.Get(orderBy:x=>x.OrderByDescending(q => q.TId));
            return TransportCharge;
        }

        public bool Post(TransportCharge Transport)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            
            if (Transport != null)
            {
                unitOfWork.TransportRepository.Insert(Transport);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        //update Class
        [HttpPut]
        public bool Put(int Tid, TransportCharge Transport)
        {
            if (Transport == null)
            {
                return false;
            }
            if (Tid != Transport.TId)
            {
                return false;
            }
            try
            {
                unitOfWork.TransportRepository.Update(Transport);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        //delete Class by id
        public TransportCharge Delete(int Tid)
        {
            TransportCharge Transport = unitOfWork.TransportRepository.GetById(Tid);
            if (Transport == null)
            {
                return Transport;
            }
            
            try
            {
                unitOfWork.TransportRepository.Delete(Transport);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Transport;
            }
            return Transport;
        }
    }
}
