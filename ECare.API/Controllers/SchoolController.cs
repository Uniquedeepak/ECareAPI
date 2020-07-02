using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECare.API.Controllers
{
    [RoutePrefix("")]
    public class SchoolController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("ECare API Started");
        }

    }
}
