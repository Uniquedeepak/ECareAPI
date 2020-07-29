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
    public class FeedbackHelper : IFeedbackHelper
    {
        readonly FeedbackData data = null;
        public FeedbackHelper(string CS_Name)
        {
            data = new FeedbackData(CS_Name);
        }
        public async Task<List<Model.Feedback>> GetFeedbacks()
        {
            
            var FeedbackList = data.GetFeedbacks();
            var Feedback = Mapper.Map<List<Data.Feedback>, List<Model.Feedback>>(FeedbackList);
            return Feedback;
        }

        public async Task<List<Model.Feedback>> GetStudentFeedbacks(string AdmNo)
        {

            var FeedbackList = data.GetStudentFeedbacks(AdmNo);
            var Feedback = Mapper.Map<List<Data.Feedback>, List<Model.Feedback>>(FeedbackList);
            return Feedback;
        }
        public async Task<Model.Feedback> GetFeedback(int Id)
        {
            var FeedbackList = data.Get(Id);
            var Feedback = Mapper.Map<Data.Feedback, Model.Feedback>(FeedbackList);
            return Feedback;
        }
        public async Task<string> InsertFeedback(Model.Feedback _Feedback)
        {
            string Status = string.Empty;
            var Feedback = Mapper.Map<Model.Feedback, Data.Feedback>(_Feedback);
            var result = data.Post(Feedback);
            Status = result ? "Feedback submitted successfully." : "Failed to submit Feedback";
            return Status;
        }
        public async Task<string> UpdateFeedback(int Id, Model.Feedback _Feedback)
        {
            string Status = string.Empty;
            var Feedback = Mapper.Map<Model.Feedback, Data.Feedback>(_Feedback);
            var result = data.Put(Id,Feedback);
            Status = result ? "Feedback updated successfully." : "Failed to update Feedback";
            return Status;
        }
        public async Task<string> DeleteFeedback(int Id)
        {
            string Status = string.Empty;
            var result = data.Delete(Id);
            Status = result != null ? "Feedback deleted successfully." : "Failed to delete Feedback";
            return Status;
        }
    }
}
