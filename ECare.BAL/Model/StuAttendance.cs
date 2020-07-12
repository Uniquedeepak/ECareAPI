using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public class StuAttendance
    {
        public int ID { get; set; }
        public Nullable<int> StAdmId { get; set; }
        public string StAdmNo { get; set; }
        public string StName { get; set; }
        public string StClass { get; set; }
        public string StNumber { get; set; }
        public string Attendance { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Session { get; set; }
    }
}
