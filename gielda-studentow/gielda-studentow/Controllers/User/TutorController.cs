using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Models;
using gielda_studentow.Service.Interface;

namespace gielda_studentow.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/Tutor")]
    public class TutorController : BaseController
    {
        private readonly ITutorService _tutorService;

        public TutorController(IAuthenticatonService authenticatonService, ITutorService tutorService) : base(authenticatonService)
        {
            _tutorService = tutorService;
        }

        [HttpGet]
        [Route("{tutorId}")]
        public IHttpActionResult Get(string tutorId)
        {
            return Content(HttpStatusCode.OK, _tutorService.GeTutorById(tutorId));
        }

        [HttpPost]
        [Route("Me/Course/{courseId}")]
        public IHttpActionResult PostCourse(long courseId)
        {
            _tutorService.AddTutorCourse(GetCurrentUserId(), courseId);
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Me/Names")]
        public IHttpActionResult PostNames([FromBody] FirstLastNameModel model)
        {
            _tutorService.AddTutorNames(GetCurrentUserId(), model);
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpGet]
        [Route("MyProfile")]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, _tutorService.GeTutorById(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("MyCourses")]
        public IHttpActionResult GetCourses()
        {
            return Content(HttpStatusCode.OK, _tutorService.GetCourses(GetCurrentUserId()));
        }
    }
}
