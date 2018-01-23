using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Models;
using gielda_studentow.Service.Interface;

namespace gielda_studentow.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/Student")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ICourseOfStudyService _courseOfStudyService;
        private readonly IGroupService _groupService;

        public StudentController(IAuthenticatonService authenticatonService, 
            IStudentService studentService, 
            ICourseOfStudyService courseOfStudyService, 
            IGroupService groupService) : base(authenticatonService)
        {
            _studentService = studentService;
            _courseOfStudyService = courseOfStudyService;
            _groupService = groupService;
        }

        [HttpGet]
        [Route("MyProfile")]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK,_studentService.GetStudentById(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("studentGroups")]
        public IHttpActionResult GetStudentGroups()
        {
            return Content(HttpStatusCode.OK, _studentService.GetStudentGroups(GetCurrentUserId()));
        }

        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            return Content(HttpStatusCode.OK,_studentService.GetStudentById(id));
        }

        [HttpGet]
        [Route("{studentId}/Groups")]
        public IHttpActionResult GetStudentGroups(string studentId)
        {
            return Content(HttpStatusCode.OK, _studentService.GetStudentGroups(studentId));
        }

        [HttpGet]
        [Route("{studentId}/Courses")]
        public IHttpActionResult GetStudentCourses(string studentId)
        {
            return Content(HttpStatusCode.OK, _courseOfStudyService.GetStudentCourses(studentId));
        }

        [HttpGet]
        [Route("Me/Courses/Faculty/{facultyId}/Leftover")]
        public IHttpActionResult GetFacultyStudentLeftoverCourses(long facultyId)
        {
            return Content(HttpStatusCode.OK, _courseOfStudyService.GetLeftoverStudentCourses(facultyId,GetCurrentUserId()));
        }

        [HttpGet]
        [Route("{studentId}/Groups/ByCourse/{courseId}")]
        public IHttpActionResult GetStudentCourseGroups(string studentId, long courseId)
        {
            return Content(HttpStatusCode.OK, _groupService.GetStudentCourseGroups(studentId,courseId));
        }

        [HttpGet]
        [Route("MyGroups/ByCourse/{courseId}")]
        public IHttpActionResult GetMyCourseGroups(long courseId)
        {
            return Content(HttpStatusCode.OK, _groupService.GetStudentCourseGroups(GetCurrentUserId(),courseId));
        }

        [HttpPost]
        [Route("Me/Names")]
        public IHttpActionResult PostNames([FromBody] FirstLastNameModel model)
        {
            _studentService.AddStudentNames(GetCurrentUserId(), model);
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpGet]
        [Route("Me/Admin/Groups")]
        public IHttpActionResult GetAdministratedGroups()
        {
            return Content(HttpStatusCode.OK, _studentService.GetAdministratedGroups(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Me/Admin/Courses")]
        public IHttpActionResult GetAdministratedCourses()
        {
            return Content(HttpStatusCode.OK, _studentService.GetAdministratedCourses(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Me/Admin/Faculties")]
        public IHttpActionResult GetAdministratedFaculties()
        {
            return Content(HttpStatusCode.OK, _studentService.GetAdministratedFaculties(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Me/Admin/Universities")]
        public IHttpActionResult GetAdministratedUniversities()
        {
            return Content(HttpStatusCode.OK, _studentService.GetAdministratedUniversities(GetCurrentUserId()));
        }
    }
}
