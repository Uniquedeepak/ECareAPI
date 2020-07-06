using ECare.API.Models;
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
    public class LeaveController : ApiController
    {
        ILeaveHelper LeaveHelper = null;
        public LeaveController()
        {
            LeaveHelper = new LeaveHelper();
        }
        // GET: api/Leave
        [Route("Leave")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            Response res = null;
            try
            {
                var Result = await LeaveHelper.GetLeaves();
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

        // GET: api/Leave/5
        [Route("Leave/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int Id)
        {
            Response res = null;
            try
            {
                var Result = await LeaveHelper.GetLeave(Id);
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

        // POST: api/Leave
        [Route("Leave/Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Leave Leave)
        {
            Response res = null;
            try
            {
                var Result = await LeaveHelper.InsertLeave(Leave);
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

        // PUT: api/Leave/5
        [Route("Leave/Update/{Id}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Put(int Id, [FromBody]Leave Leave)
        {
            Response res = null;
            try
            {
                var Result = await LeaveHelper.UpdateLeave(Id, Leave);
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

        // DELETE: api/Leave/5
        [Route("Leave/Delete/{Id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                var Result = await LeaveHelper.DeleteLeave(Id);
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
