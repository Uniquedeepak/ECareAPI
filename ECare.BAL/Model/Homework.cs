using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public partial class Homework
    {
        public int id { get; set; }
        public string @class { get; set; }
        public string month { get; set; }
        public string date { get; set; }
        public string desciption { get; set; }
        public string name { get; set; }
        public string contenttype { get; set; }
        public byte[] data { get; set; }
        public string section { get; set; }
    }
}
