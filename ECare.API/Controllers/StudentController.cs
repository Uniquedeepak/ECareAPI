using ECare.API.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.IO;
using System.Net.Http.Headers;
using ECare.Data.DAL;
using ECare.Data;

namespace ECare.API.Controllers
{
    [RoutePrefix("api/school")]
    public class StudentController : ApiController
    {
        string LoginStdAdmissionNo = string.Empty;
        public StudentController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
        }
        [Authorize]
        [Route("Student")]
        public async Task<IHttpActionResult> GetStudent()
        {
            StudentData obj = new StudentData();
            var result = obj.Get(LoginStdAdmissionNo);
            Response res = new Response()
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Result = result
            };
            return Ok(res);
        }

        [Authorize]
        [Route("fee")]
        public async Task<IHttpActionResult> GetStudentFee()
        {
            FeesData obj = new FeesData();
            var Result = obj.GetStudentFeeDetail(LoginStdAdmissionNo);
            Response res = new Response()
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Result = Result
            };
            return Ok(res);
        }

        [Authorize]
        [Route("homework")]
        public async Task<IHttpActionResult> GetHomework()
        {
            HomeworkData obj = new HomeworkData();
            StudentData std = new StudentData();
            string ClassId = std.Get(LoginStdAdmissionNo).Class;
            List<tbl_homework> homeworkList = obj.GetHomeworkByClass(ClassId);
            List<dynamic> Result = new List<dynamic>();
            foreach (var item in homeworkList)
            {
                var homework = new
                {
                    Id = item.id,
                    item.month,
                    item.name,
                    item.@class,
                    item.contenttype,
                    item.date,
                    item.desciption,
                    data = "Call API - homework/file/{id}"
                };
                Result.Add(homework);
            }

            Response res = new Response()
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Result = Result,
            };
            return Ok(res);
        }

        [Authorize]
        [Route("homework/file/{id}")]
        public async Task<IHttpActionResult> GetFile(int id)
        {

            HomeworkData obj = new HomeworkData();
            tbl_homework homework = obj.Get(id);

            MemoryStream stream = new MemoryStream(homework.data);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = homework.month
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(homework.contenttype);
            ResponseMessageResult responseMessageResult = ResponseMessage(httpResponseMessage);
            return responseMessageResult;
        }

        [Authorize]
        [Route("notification")]
        public async Task<IHttpActionResult> GetNotification()
        {
            NotificationData obj = new NotificationData();
            StudentData std = new StudentData();
            string ClassId = std.Get(LoginStdAdmissionNo).Class;
            var Result = obj.GetNotificationByClass(ClassId);
            Response res = new Response()
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Result = Result
            };
            return Ok(res);
        }

    }
}
