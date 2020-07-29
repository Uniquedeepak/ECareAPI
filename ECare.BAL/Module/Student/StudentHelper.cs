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
        private string csName {get;set;}
        public StudentHelper(string CS_Name)
        {
            csName = CS_Name;
        }
        public async Task<Homework> GetHomeworkById(int Id)
        {
            HomeworkData obj = new HomeworkData(csName);
            var homework = obj.Get(Id);
            var _Homework = Mapper.Map<Data.tbl_homework, Homework>(homework);
            return _Homework;
        }
        public async Task<IList<Homework>> GetHomeworkByClass(string AdmissionNo)
        {
            HomeworkData obj = new HomeworkData(csName);
            StudentData std = new StudentData(csName);
            string ClassId = std.Get(AdmissionNo).Class;
            List<Data.tbl_homework> homeworkList = obj.GetHomeworkByClass(ClassId);
            var HomeworkList = Mapper.Map<IList<Data.tbl_homework>, IList<Homework>>(homeworkList);
            return HomeworkList;
        }
        public async Task<IList<Model.Notification>> GetNotificationByClass(string AdmissionNo)
        {
            NotificationData obj = new NotificationData(csName);
            StudentData std = new StudentData(csName);
            string ClassId = std.Get(AdmissionNo).Class;
            List<Data.Notification> Result = obj.GetNotificationByClass(ClassId);
            var NotificationList = Mapper.Map<IList<Data.Notification>, IList<Model.Notification>>(Result);
            return NotificationList;
        }
        public async Task<IList<Model.School>> GetSchool()
        {
            SchoolData obj = new SchoolData(csName);
            var Result = obj.GetSchool();
            var SchoolList = Mapper.Map<IList<Data.School>, IList<Model.School>>(Result);
            return SchoolList;
        }
        public async Task<Student> GetStudent(string AdmissionNo)
        {
            StudentData obj = new StudentData(csName);
            var Result = obj.Get(AdmissionNo);
            var Students = Mapper.Map<AdmissionForm, Student>(Result);
            if (Students != null)
            {
                Students.Class = new ClassData(csName).GetClassName(Students?.Class);
            }
            return Students;
        }

        public async Task<List<StuAttendance>> GetStudentAttendance(string AdmissionNo, int MonthId)
        {
            AttendanceData obj = new AttendanceData(csName);
            var Result = obj.GetStMonthlyAttendance(AdmissionNo, MonthId).ToList();
            var Students = Mapper.Map<List<StAttendance>, List<StuAttendance>>(Result);
            return Students;
        }
        public async Task<IList<StudentFee>> GetStudentFee(string AdmissionNo)
        {
            FeesData obj = new FeesData(csName);
            var Result = obj.GetStudentFeeDetail(AdmissionNo);
            var SchoolFeeList = Mapper.Map<IList<Data.StudentFeeDetail>, IList<Model.StudentFee>>(Result);
            return SchoolFeeList;
        }
    }
}
