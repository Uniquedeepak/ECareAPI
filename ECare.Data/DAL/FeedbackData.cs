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
    public class FeedbackData : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public FeedbackData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<Feedback> GetFeedbacks()
        {
            var Feedbacks = unitOfWork.FeedbackRepository.Query().OrderBy(x => x.ID).ToList();
            return Feedbacks;
        }

        public List<Feedback> GetStudentFeedbacks(string AdmissionNo)
        {
            var Feedbacks = unitOfWork.FeedbackRepository.Query(x=>x.StuAdmNo== AdmissionNo).OrderBy(x => x.ID).ToList();
            return Feedbacks;
        }

        public Feedback Get(int id)
        {
            Feedback objFeedback = unitOfWork.FeedbackRepository.GetById(id);
            if (objFeedback == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return objFeedback;
        }

        public bool Post(Feedback _Feedback)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            if (ModelState.IsValid)
            {
                var studentdetail = unitOfWork.AdmissionFormRepository.GetFirstOrDefault(x=>x.AdmissionNo == _Feedback.StuAdmNo);
                _Feedback.Name = studentdetail.StFirstName;
                _Feedback.ContactNumber = studentdetail.Contact;
                _Feedback.Email = studentdetail.EmailId;
                unitOfWork.FeedbackRepository.Insert(_Feedback);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(int Id, Feedback _Feedback)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            if (Id != _Feedback.ID)
            {
                return false;
            }

            try
            {
                unitOfWork.FeedbackRepository.Update(_Feedback);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public Feedback Delete(int Id)
        {
            Feedback _Feedback = unitOfWork.FeedbackRepository.GetById(Id);
            if (_Feedback == null)
            {
                return _Feedback;
            }

            try
            {
                unitOfWork.FeedbackRepository.Delete(Id);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return _Feedback;
            }
            return _Feedback;
        }
    }

}
