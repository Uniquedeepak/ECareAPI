﻿using ECare.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Hosting;
using ECare.Data.BAL;
using System.Data.Entity;

namespace ECare.Data.DAL
{
    public class AttendanceData : ApiController
    {
        private wisdomDBEntities SchoolDB = null;
        private string SchoolSession { get; set; }
        public AttendanceData()
        {
            SchoolDB = new wisdomDBEntities();
            SchoolSession = PropertiesConfiguration.ActiveSession;
        }

        public List<StAttendance> GetAttendanceCharge()
        {
            var AttendanceCharge = SchoolDB.StAttendances.Where(x=>x.Session==SchoolSession).OrderByDescending(x => x.ID).ToList();
            ClassData _class = new ClassData();
            AttendanceCharge.ForEach(x=>x.StClass=_class.GetClassName(x.StClass));
            return AttendanceCharge;
        }

        public HttpResponseMessage Post(List<StAttendance> Attendance)
        {
            HttpRequestMessage Request = new HttpRequestMessage();
            Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            ClassData _class = new ClassData();
            if (ModelState.IsValid)
            {
                foreach (var item in Attendance)
                {
                    item.Session = SchoolSession;
                    SchoolDB.StAttendances.Add(item);
                    SchoolDB.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, Attendance);
                return response;   
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        public bool IsAttendanceMarked(List<StAttendance> Attendance)
        {
            ClassData _class = new ClassData();
            Attendance.ForEach(x=>x.StClass=_class.GetClassID(x.StClass).ToString());
            var SelectedClass = Attendance.GroupBy(
                                p => p.StClass,
                                (key, g) => new { stClass = key }).FirstOrDefault();
            var list = SchoolDB.StAttendances.ToList().Where(x => x.StClass == SelectedClass.stClass && x.Session == SchoolSession && x.Date.Value.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy")).ToList();
            return list.Any();
        }

        public IEnumerable<MonthAttendanceReport> GetMonthlyAttendance(string ClassVal)
        {
            var ReturnVal = from bs in SchoolDB.StAttendances.Where(x => x.Session == SchoolSession).ToList()
                        where Convert.ToDateTime(bs.Date).Month == DateTime.Now.Month 
                        group bs by new
                        {
                            bs.StAdmNo,
                            bs.StName,
                            bs.StClass
                        }
                        into g
                        select new MonthAttendanceReport
                        {
                            AdmissionNo = g.Key.StAdmNo,
                            Name = g.Key.StName,
                            Class = g.Key.StClass,
                            Present = g.Sum(x => x.Attendance.ToUpper() == "PRESENT" ? 1 : 0),
                            Absent = g.Sum(x => x.Attendance.ToUpper() == "ABSENT" ? 1 : 0),
                            Leave = g.Sum(x => x.Attendance.ToUpper() == "LEAVE" ? 1 : 0),
                            TotalDays = DateTime.Now.Day

                        };
            ClassData _class = new ClassData();
            List<MonthAttendanceReport> ReportList = new List<MonthAttendanceReport>();
            foreach (var item in ReturnVal)
            {
                item.Class = _class.GetClassName(item.Class);
                ReportList.Add(item);
            }
            return ReportList;

        }

        //update Class
        [HttpPut]
        public bool Put(int Tid, StAttendance Attendance)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            if (Tid != Attendance.ID)
            {
                return false;
            }
            Attendance.Session = SchoolSession;
            SchoolDB.Entry(Attendance).State = EntityState.Modified;
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

        //delete Class by id
        public StAttendance Delete(int Tid)
        {
            StAttendance Attendance = SchoolDB.StAttendances.Find(Tid);
            if (Attendance == null)
            {
                return Attendance;
            }
            SchoolDB.StAttendances.Remove(Attendance);
            try
            {
                SchoolDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Attendance;
            }
            return Attendance;
        }


        //prevent memory leak
        protected override void Dispose(bool disposing)
        {
            SchoolDB.Dispose();
            base.Dispose(disposing);
        }
    }
}
