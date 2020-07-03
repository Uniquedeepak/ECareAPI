using ECare.API.Models;
using ECare.BAL.Module;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ECare.API.Controllers
{
    [RoutePrefix("api/school")]
    public class StudentController : ApiController
    {
        string LoginStdAdmissionNo = string.Empty;
        IStudentHelper StudentHelper = null;
        public StudentController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
            StudentHelper = new StudentHelper();
        }

        [Authorize]
        [Route("School")]
        public async Task<IHttpActionResult> GetSchool()
        {
            var result = StudentHelper.GetSchool().Result;
            Response res = new Response()
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Result = result
            };
            return Ok(res);
        }

        [Authorize]
        [Route("Student")]
        public async Task<IHttpActionResult> GetStudent()
        {
            var result = StudentHelper.GetStudent(LoginStdAdmissionNo).Result;
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
            var Result = StudentHelper.GetStudentFee(LoginStdAdmissionNo).Result;
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
            var homeworkList = StudentHelper.GetHomeworkByClass(LoginStdAdmissionNo).Result;
            
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
                    data = $"Call API - api/school/homework/file/{item.id}"
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

            var homework = StudentHelper.GetHomeworkById(id).Result;

            if (homework !=null)
            {
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
            return NotFound();
        }

        [Authorize]
        [Route("notification")]
        public async Task<IHttpActionResult> GetNotification()
        {
            var Result = StudentHelper.GetNotificationByClass(LoginStdAdmissionNo).Result;
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
