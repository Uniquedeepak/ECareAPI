using ECare.BAL.Model;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface IFeedbackHelper
    {
        Task<List<Feedback>> GetFeedbacks();
        Task<List<Feedback>> GetStudentFeedbacks(string AdmissionNo);
        Task<Feedback> GetFeedback(int Id);
        Task<string> InsertFeedback(Feedback _Feedback);
        Task<string> UpdateFeedback(int Id, Feedback _Feedback);
        Task<string> DeleteFeedback(int Id);
    }
}
