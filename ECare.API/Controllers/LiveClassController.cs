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
    public class LiveClassController : ApiController
    {
        ILiveClassHelper LiveClassHelper = null;
        private readonly string LoginStdAdmissionNo = string.Empty;
        public LiveClassController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
            LiveClassHelper = new LiveClassHelper(ConnectionStringNames.DBEntityName);
        }
        // GET: api/LiveClass
        [Route("LiveClass")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.GetLiveClasss();
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

        // GET: api/LiveClass
        [Route("Student/LiveClass")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStudentLiveClass()
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.GetStudentLiveClasss(LoginStdAdmissionNo);
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

        // GET: api/LiveClass/5
        [Route("LiveClass/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int Id)
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.GetLiveClass(Id);
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

        // POST: api/LiveClass
        [Route("LiveClass/Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]LiveClass LiveClass)
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.InsertLiveClass(LiveClass);
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

        // PUT: api/LiveClass/5
        [Route("LiveClass/Update/{Id}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Put(int Id, [FromBody]LiveClass LiveClass)
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.UpdateLiveClass(Id, LiveClass);
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

        // DELETE: api/LiveClass/5
        [Route("LiveClass/Delete/{Id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                var Result = await LiveClassHelper.DeleteLiveClass(Id);
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
