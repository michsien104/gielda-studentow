using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Education
{
    [Authorize]
    [RoutePrefix("api/Faculty")]
    public class FacultyController : BaseController
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IAuthenticatonService authenticatonService, IFacultyService facultyService) : base(authenticatonService)
        {
            _facultyService = facultyService;
        }


        [HttpGet]
        [Route("{id}")]
        public Faculty Get(long id)
        {
            return _facultyService.GetFacultyById(id);
        }

        [HttpGet]
        [Route("ByCourse/{courseId}")]
        public IHttpActionResult GetCourseFaculty(long courseId)
        {
            return Content(HttpStatusCode.OK, _facultyService.GetCourseFaculty(courseId));
        }

        [HttpGet]
        [Route("{facultyId}/Courses")]
        public IHttpActionResult GetFacultyCourses(long facultyId)
        {
            return Content(HttpStatusCode.OK, _facultyService.GetFacultyCourses(facultyId));
        }

        [HttpPost]
        [Route("University/{universityId}")]
        public IHttpActionResult AddFaculty([FromBody] Faculty faculty, long universityId)
        {
            _facultyService.AddFaculty(faculty,universityId,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
