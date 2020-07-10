using ECare.Data.BAL;
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
using System.Xml;

namespace ECare.Data.DAL
{
    public class StudentData
    {
        private wisdomDBEntities SchoolDB = null;
        readonly ClassData _class = null;
        private readonly string SchoolSession = string.Empty;
        private readonly string connectionName = string.Empty;
        public StudentData(string csName)
        {
            SchoolDB = new wisdomDBEntities(csName);
            _class = new ClassData(csName);
            SchoolSession = PropertiesConfiguration.ActiveSession;
        }

        // GET api/school/5
        public List<AdmissionForm> GetStudentDetails()
        {
            
            var studentDetails = SchoolDB.AdmissionForms.Where(x => x.ESession.Contains(SchoolSession)).OrderBy(x => x.Class).ThenBy(z => z.StFirstName).ToList();
            studentDetails.ForEach(cc => cc.Class = _class.GetClassName(cc.Class));
            return studentDetails;
        }
        public List<AdmissionForm> GetStudentDetailByClass(string ClassID)
        {
            List<AdmissionForm> studentDetails;
            if (ClassID != "0")
            {
                studentDetails = SchoolDB.AdmissionForms.Where(x => x.ESession.Contains(SchoolSession) && x.Class == ClassID).OrderBy(x => x.Class).ThenBy(z => z.StFirstName).ToList();
            }
            else
            {
                studentDetails = SchoolDB.AdmissionForms.Where(x => x.ESession.Contains(SchoolSession)).OrderBy(x => x.Class).ThenBy(z => z.StFirstName).ToList();
            }
            
            studentDetails.ForEach(cc => cc.Class = _class.GetClassName(cc.Class));
            return studentDetails;
        }
        public List<AdmissionForm> GetMultiChildParent()
        {
            var studentDetails = SchoolDB.AdmissionForms.Where(x => x.ESession.Contains(SchoolSession)).OrderBy(x => x.Class).ToList();
            studentDetails.ForEach(cc => cc.Class = _class.GetClassName(cc.Class));
            studentDetails = studentDetails.GroupBy(item => item.FatherName)
                 .Where(group => group.Count() > 1)
                 .SelectMany(group => group)
                 .ToList();
            return studentDetails;
        }
        public string GetNextAdmNo(int ClassId)
        {
            bool IsPrimaryClass = SchoolDB.Classes.Any(x => x.CID==ClassId && x.Prefix.Equals("P"));
            var students = SchoolDB.AdmissionForms.OrderByDescending(x => x.AdmissionId).FirstOrDefault();
            string Admission = string.Empty;
            Admission = students.AdmissionNo.Contains("P") ? students.AdmissionNo.Remove(0,1) : students.AdmissionNo;
            Admission =Convert.ToString(Convert.ToInt32(Admission) + 1);
            return IsPrimaryClass?  "P" + Admission.ToString(): Admission.ToString();
        }
        //get all student
        [HttpGet]
        public IEnumerable<AdmissionForm> Get()
        {
            return SchoolDB.AdmissionForms.Where(x => x.ESession.Contains(SchoolSession)).AsEnumerable();
        }
        //get student by id
        public AdmissionForm Get(string AdmissionNo)
        {
            AdmissionForm student = SchoolDB.AdmissionForms.Where(x => x.AdmissionNo.Equals(AdmissionNo)).OrderBy(x=>x.AdmissionId).FirstOrDefault();
            if (student == null)
            {
                return null;
            }
            return student;
        }
        //insert student
        public int InserStudent(AdmissionForm student)
        {
            int result = 0;
            if (student != null)
            {
                SchoolDB.AdmissionForms.Add(student);
                result = SchoolDB.SaveChanges();
            }
            return result;
        }
        //update customer
        [HttpPut]
        public bool Put(int AdmissionId, AdmissionForm student)
        {
            if (student == null)
            {
                return false;
            }
            if (AdmissionId != student.AdmissionId)
            {
                return false;
            }

            try
            {
                Convert.ToInt32(student.Class);
            }
            catch (Exception ex)
            {
                student.Class = _class.GetClassID(student.Class).ToString();
            }
            
            SchoolDB.Entry(student).State = EntityState.Modified;
            try
            {
                SchoolDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }
        //delete customer by id
        public AdmissionForm Delete(int AdmissionId)
        {
            AdmissionForm student = SchoolDB.AdmissionForms.Find(AdmissionId);
            if (student == null)
            {
                return student;
            }
            SchoolDB.AdmissionForms.Remove(student);
            try
            {
                SchoolDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return student;
            }
            return student;
        }
    }
}
