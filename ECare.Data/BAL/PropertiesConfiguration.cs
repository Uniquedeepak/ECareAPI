﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECare.Data.BAL
{
    public class PropertiesConfiguration
    {
        public static string  ActiveSession { get; set; }
        public static string SMSApiHost { get; set; }
        public static string SMSApiUser { get; set; }
        public static string SMSApiPassword { get; set; }
        public static string AdminNumber { get; set; }
        public static bool IsSMSEnable { get; set; }
        
        static PropertiesConfiguration()
        {
            FillProperties();
        }
        private static void FillProperties()
        {
            ActiveSession = "2020-2021";// SchoolDB.Sessions.Where(x => x.IsActive == true).Select(s => s.Session1).FirstOrDefault();
            SMSApiHost = System.Configuration.ConfigurationManager.AppSettings["SMSApiHost"].ToString();
            SMSApiUser = System.Configuration.ConfigurationManager.AppSettings["SMSApiUser"].ToString();
            SMSApiPassword = System.Configuration.ConfigurationManager.AppSettings["SMSApiPassword"].ToString();
            AdminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminNumber"].ToString();
            IsSMSEnable = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsSMSEnable"]);
        }

    }
}