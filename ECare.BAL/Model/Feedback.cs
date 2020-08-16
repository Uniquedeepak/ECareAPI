using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public class Feedback
    {
        public int ID { get; set; }
        public string StuAdmNo { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
