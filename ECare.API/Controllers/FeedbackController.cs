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
   public class FeedbackController : ApiController
    {
        readonly string LoginStdAdmissionNo = string.Empty;
        readonly IFeedbackHelper FeedbackHelper = null;
        public FeedbackController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
            FeedbackHelper = new FeedbackHelper(ConnectionStringNames.DBEntityName);
        }
        // GET: api/Feedback
        [Route("Feedback")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            Response res = null;
            try
            {
                var Result = await FeedbackHelper.GetFeedbacks();
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

        // GET: api/School/Student/Feedback
        [Route("Student/Feedback")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStudentFeedbacks()
        {
            Response res = null;
            try
            {
                var Result = await FeedbackHelper.GetStudentFeedbacks(LoginStdAdmissionNo);
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
                    Result = ex.ToString()
                };
            }
            return Ok(res);
        }

        // GET: api/Feedback/5
        [Route("Feedback/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int Id)
        {
            Response res = null;
            try
            {
                var Result = await FeedbackHelper.GetFeedback(Id);
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

        // POST: api/Feedback
        [Route("Student/Feedback/Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Feedback Feedback)
        {
            Response res = null;
            try
            {
                Feedback.StuAdmNo = LoginStdAdmissionNo;
                var Result = await FeedbackHelper.InsertFeedback(Feedback);
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

        // PUT: api/Feedback/5
        [Route("Feedback/Update/{Id}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Put(int Id, [FromBody]Feedback Feedback)
        {
            Response res = null;
            try
            {
                var Result = await FeedbackHelper.UpdateFeedback(Id, Feedback);
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

        // DELETE: api/Feedback/5
        [Route("Feedback/Delete/{Id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                var Result = await FeedbackHelper.DeleteFeedback(Id);
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
