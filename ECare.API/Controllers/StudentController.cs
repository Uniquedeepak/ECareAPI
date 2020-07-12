using ECare.API.Models;
using ECare.API.Services;
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
        readonly string LoginStdAdmissionNo = string.Empty;
        readonly IStudentHelper StudentHelper = null;
        public StudentController()
        {
            LoginStdAdmissionNo = RequestContext.Principal.Identity.Name;
            StudentHelper = new StudentHelper(ConnectionStringNames.DBEntityName);
        }

        [Authorize]
        [Route("School")]
        public async Task<IHttpActionResult> GetSchool()
        {
            var result = await StudentHelper.GetSchool();
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
            var result =await StudentHelper.GetStudent(LoginStdAdmissionNo);
            Response res = new Response()
            {
                ResponseCode = result != null? ((int)HttpStatusCode.OK).ToString() : ((int)HttpStatusCode.NotFound).ToString(),
                ResponseMessage = "Success",
                Result = result
            };
            return Ok(res);
        }

        [Authorize]
        [Route("attendance")]
        public async Task<IHttpActionResult> GetStAttendence()
        {
            Response res;
            var Result = await StudentHelper.GetStudentAttendance(LoginStdAdmissionNo);
            if (Result.Count < 1)
            {
                res = new Response()
                {
                    ResponseCode = ((int)HttpStatusCode.NotFound).ToString(),
                    ResponseMessage = "No Record Found.",
                    Result = Result
                };
            }
            else
            {
                res = new Response()
                {
                    ResponseCode = ((int)HttpStatusCode.OK).ToString(),
                    ResponseMessage = "Success",
                    Result = Result
                };
            }
            
            return Ok(res);
        }

        [Authorize]
        [Route("fee")]
        public async Task<IHttpActionResult> GetStudentFee()
        {
            var Result =await StudentHelper.GetStudentFee(LoginStdAdmissionNo);
            Response res = new Response()
            {
                ResponseCode = Result.Count < 1? ((int)HttpStatusCode.NotFound).ToString() : ((int)HttpStatusCode.OK).ToString(),
                ResponseMessage = "Success",
                Result = Result
            };
            return Ok(res);
        }

        [Authorize]
        [Route("homework")]
        public async Task<IHttpActionResult> GetHomework()
        {
            Response res;
            var homeworkList = await StudentHelper.GetHomeworkByClass(LoginStdAdmissionNo);
            if (homeworkList.Count < 1)
            {
                res = new Response()
                {
                    ResponseCode = ((int)HttpStatusCode.NotFound).ToString(),
                    ResponseMessage = "No Record Found.",
                    Result = "No Record Found",
                };
            }
            else
            {
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
                res = new Response()
                {
                    ResponseCode = ((int)HttpStatusCode.OK).ToString(),
                    ResponseMessage = "Success",
                    Result = Result,
                };
            }

            return Ok(res);
        }

        [Authorize]
        [Route("homework/file/{id}")]
        public async Task<IHttpActionResult> GetFile(int id)
        {

            var homework = await StudentHelper.GetHomeworkById(id);
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
    }
}
