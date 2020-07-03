using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public partial class Student
    {
        public int AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public string StFirstName { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string EmailId { get; set; }
        public string ComAddress { get; set; }
        public string ParAddress { get; set; }
        public string Contact { get; set; }
        public string Class { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Route { get; set; }
        public string Transport_Charge { get; set; }
        public string Status { get; set; }
        public string ESession { get; set; }
        public string Nationality { get; set; }
        public string RTE { get; set; }
        public string EmergencyNo { get; set; }
        public string PreviousDue { get; set; }
        public string Concession { get; set; }
        public string House_Name { get; set; }
        public string MAadhaar { get; set; }
        public string FAadhaar { get; set; }
        public Nullable<int> Hobby { get; set; }
    }
}
