using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ECare.API.Services
{
    public class ConnectionStringNames
    {
        public static JsonConnectionString Collections { get; set; }
        public static string DBEntityName { get; set; }
      //  public static string DBIdentityName { get; set; }
        public static void SetConnectionNameList()
        {
            string allText = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/") + "Connectionnames.json");
            Collections = JsonConvert.DeserializeObject<JsonConnectionString>(allText);
        }
        public void GetConnectionStringName(string SchoolCode)
        {
            School ConnectionStringData = Collections.schools.Where(x => x.Code.Equals(SchoolCode)).FirstOrDefault();
            if (ConnectionStringData != null)
            {
                DBEntityName = ConnectionStringData.DBConnectionString;
              //  DBIdentityName = ConnectionStringData.IdentityConnectionString;
            }
            else
            {
                DBEntityName = "";
         //       DBIdentityName = "";
            }
        }
    }

    public class School
    {
        public string Code { get; set; }
        public string DBConnectionString { get; set; }
        public string IdentityConnectionString { get; set; }

    }
    public class JsonConnectionString
    {
        public List<School> schools { get; set; }

    }
}