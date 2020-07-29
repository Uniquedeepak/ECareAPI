using ECare.API.Filter;
using ECare.API.Infrastructure;
using ECare.API.Models;
using ECare.API.Services;
using ECare.BAL.Model;
using ECare.BAL.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ECare.API.Controllers
{
    [RoutePrefix("api/school")]
    [Authorize]
   public class EventController : ApiController
    {
        readonly string LoginStdAdmissionNo = string.Empty;
        readonly IEventHelper EventHelper = null;
        public EventController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
            EventHelper = new EventHelper(ConnectionStringNames.DBEntityName);
        }
        // GET: api/Event
        [Route("Event")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            Response res = null;
            try
            {
                var Result = await EventHelper.GetEvents();
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }

        // GET: api/Event/5
        [Route("Event/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int Id)
        {
            Response res = null;
            try
            {
                var Result = await EventHelper.GetEvent(Id);
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }

        // POST: api/Event
        [Route("Event/Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Event Event)
        {
            Response res = null;
            try
            {
                var Result = await EventHelper.InsertEvent(Event);
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }

        // PUT: api/Event/5
        [Route("Event/Update/{Id}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Put(int Id, [FromBody]Event Event)
        {
            Response res = null;
            try
            {
                var Result = await EventHelper.UpdateEvent(Id, Event);
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }

        // DELETE: api/Event/5
        [Route("Event/Delete/{Id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                var Result = await EventHelper.DeleteEvent(Id);
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }
    }
}
