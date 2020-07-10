using GenericAPI.UnitOfWork;
using ECare.Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace ECare.Data.Scheduler
{
    public class SentBirthdaySms
    {
        private readonly System.Timers.Timer aTimer = null;
        private readonly IUnitOfWork unitOfWork;
        public SentBirthdaySms(string csName)
        {
            unitOfWork = new UnitOfWork(csName);
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 * 60 * 30 ; //30 Minutes
            aTimer.Elapsed += OnTimerElapsed;
            aTimer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            DateTime dt = DateTime.Now;

            if (dt.Hour == 10 && dt.Minute <= 30)
            {
                SendStudentBirthdaySms();
            }
        }

        private void SendStudentBirthdaySms()
        {
            var StudentList = (from emp in unitOfWork.AdmissionFormRepository.Get()
                         where emp.DOB.HasValue == true
                                    && emp.DOB.Value.Day == DateTime.Now.Day && emp.DOB.Value.Month == DateTime.Now.Month
                        select new {
                            StFirstName = emp.StFirstName,
                            Contact = emp.Contact
                                 }).ToList();
            var schoolName = unitOfWork.SchoolRepository.Get().FirstOrDefault().SchoolName;
            foreach (var item in StudentList)
            {
                if (!string.IsNullOrEmpty(item.Contact))
                {
                    SMS.SendSMSApi($"{schoolName} Family wishes you a very Happy Birthday {item.StFirstName }.", item.Contact);
                }
            }
        }
    }
}