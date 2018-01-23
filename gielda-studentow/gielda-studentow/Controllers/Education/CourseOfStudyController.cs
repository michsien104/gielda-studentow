using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Education
{
    [Authorize]
    [RoutePrefix("api/CourseOfStudy")]
    public class CourseOfStudyController : BaseController
    {
        private readonly ICourseOfStudyService _courseOfStudyService;

        public CourseOfStudyController(IAuthenticatonService authenticatonService, ICourseOfStudyService courseOfStudyService) : base(authenticatonService)
        {
            _courseOfStudyService = courseOfStudyService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            return Content(HttpStatusCode.OK,_courseOfStudyService.GetCourseById(id));
        }

        [HttpGet]
        [Route("ByGroup/{groupId}")]
        public IHttpActionResult GetGroupCourse(long groupId)
        {
            return Content(HttpStatusCode.OK, _courseOfStudyService.GetGroupCourse(groupId));
        }

        [HttpGet]
        [Route("{courseId}/Groups")]
        public IHttpActionResult GetCourseGroups(long courseId)
        {
            return Content(HttpStatusCode.OK, _courseOfStudyService.GetCourseGroups(courseId));
        }

        [HttpPost]
        [Route("Faculty/{facultyId}")]
        public IHttpActionResult AddCourseOfStudy([FromBody] CourseOfStudy courseOfStudy, long facultyId)
        {
            _courseOfStudyService.AddCourseOfStudy(courseOfStudy, facultyId, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
