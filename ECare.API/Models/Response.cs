using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECare.API.Models
{
    public class Response
    {
        public object Result { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}