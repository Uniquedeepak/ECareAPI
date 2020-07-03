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
    public class StudentHelper : IStudentHelper
    {
        public async Task<Homework> GetHomeworkById(int Id)
        {
            HomeworkData obj = new HomeworkData();
            var homework = obj.Get(Id);
            var _Homework = Mapper.Map<Data.tbl_homework, Homework>(homework);
            return _Homework;
        }
        public async Task<IList<Homework>> GetHomeworkByClass(string AdmissionNo)
        {
            HomeworkData obj = new HomeworkData();
            StudentData std = new StudentData();
            string ClassId = std.Get(AdmissionNo).Class;
            List<Data.tbl_homework> homeworkList = obj.GetHomeworkByClass(ClassId);
            var HomeworkList = Mapper.Map<IList<Data.tbl_homework>, IList<Homework>>(homeworkList);
            return HomeworkList;
        }
        public async Task<IList<Model.Notification>> GetNotificationByClass(string AdmissionNo)
        {
            NotificationData obj = new NotificationData();
            StudentData std = new StudentData();
            string ClassId = std.Get(AdmissionNo).Class;
            List<Data.Notification> Result = obj.GetNotificationByClass(ClassId);
            var NotificationList = Mapper.Map<IList<Data.Notification>, IList<Model.Notification>>(Result);
            return NotificationList;
        }
        public async Task<IList<Model.School>> GetSchool()
        {
            SchoolData obj = new SchoolData();
            var Result = obj.GetSchool();
            var SchoolList = Mapper.Map<IList<Data.School>, IList<Model.School>>(Result);
            return SchoolList;
        }
        public async Task<Student> GetStudent(string AdmissionNo)
        {
            StudentData obj = new StudentData();
            var Result = obj.Get(AdmissionNo);
            var Students = Mapper.Map<AdmissionForm, Student>(Result);
            return Students;
        }
        public async Task<IList<StudentFee>> GetStudentFee(string AdmissionNo)
        {
            FeesData obj = new FeesData();
            var Result = obj.GetStudentFeeDetail(AdmissionNo);
            var SchoolFeeList = Mapper.Map<IList<Data.StudentFeeDetail>, IList<Model.StudentFee>>(Result);
            return SchoolFeeList;
        }
    }
}
