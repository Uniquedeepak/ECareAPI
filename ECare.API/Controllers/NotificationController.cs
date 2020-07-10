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
    public class NotificationController : ApiController
    {
        INotificationHelper NotificationHelper = null;
        public NotificationController()
        {
            NotificationHelper = new NotificationHelper(ConnectionStringNames.DBEntityName);
        }

        // GET: api/Notification
        [HttpGet]
        [Route("Notification")]
        public async Task<IHttpActionResult> Get()
        {
            Response res = null;
            try
            {
                var Result = await NotificationHelper.GetNotifications();
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

        // GET: api/Notification/5
        [HttpGet]
        [Route("Notification/{Id}")]
        public async Task<IHttpActionResult> Get(int Id)
        {
            Response res = null;
            try
            {
                var Result = await NotificationHelper.GetNotification(Id);
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

        // POST: api/Notification
        [HttpPost]
        [Route("Notification/Insert")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post([FromBody]Notification Notification)
        {
            Response res = null;
            try
            {
                var Result = await NotificationHelper.InsertNotification(Notification);
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

        // PUT: api/Notification/5
        [HttpPut]
        [Route("Notification/Update/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Put(int Id,[FromBody] Notification Notification)
        {
            Response res = null;
            try
            {
                var Result = await NotificationHelper.UpdateNotification(Id, Notification);
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

        // DELETE: api/Notification/5
        [HttpDelete]
        [Route("Notification/Delete/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                var Result = await NotificationHelper.DeleteNotification(Id);
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
