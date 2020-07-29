using ECare.BAL.Model;
using ECare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface IStudentHelper
    {
        Task<IList<School>> GetSchool();
        Task<Student> GetStudent(string AdmissionNo);
        Task<List<StuAttendance>> GetStudentAttendance(string AdmissionNo, int MonthId);
        Task<IList<StudentFee>> GetStudentFee(string AdmissionNo);
        Task<Homework> GetHomeworkById(int Id);
        Task<IList<Homework>> GetHomeworkByClass(string AdmissionNo);
        Task<IList<Notification>> GetNotificationByClass(string AdmissionNo);

    }
}
