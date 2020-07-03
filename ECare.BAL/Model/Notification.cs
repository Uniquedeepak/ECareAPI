using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
