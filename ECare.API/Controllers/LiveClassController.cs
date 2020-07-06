using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECare.API.Controllers
{
    [RoutePrefix("api/school")]
    [Authorize]
    public class LiveClassController : ApiController
    {
        // GET: api/LiveClass
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LiveClass/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LiveClass
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LiveClass/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LiveClass/5
        public void Delete(int id)
        {
        }
    }
}
