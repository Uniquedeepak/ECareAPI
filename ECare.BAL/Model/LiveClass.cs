using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public class LiveClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string Link { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
