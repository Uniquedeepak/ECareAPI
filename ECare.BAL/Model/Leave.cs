using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public class Leave
    {
        public int Id { get; set; }
        public string StuAdmNo { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public string Description { get; set; }
        public LeaveStatus Status { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
       
    }

    public enum LeaveStatus
    {
        Request=1,
        Approve,
        Cancel
    }
}
