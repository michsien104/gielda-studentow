using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Education
{
    [Authorize]
    [RoutePrefix("api/University")]
    public class UniversityController : BaseController
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IAuthenticatonService authenticatonService, IUniversityService universityService) : base(authenticatonService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            return Content(HttpStatusCode.OK,_universityService.GetUniversityById(id));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, _universityService.GetAll());
        }

        [HttpGet]
        [Route("ByFaculty/{facultyId}")]
        public IHttpActionResult GetFacultyUniversity(long facultyId)
        {
            return Content(HttpStatusCode.OK, _universityService.GetFacultyUniversity(facultyId));
        }

        [HttpGet]
        [Route("{universityId}/Faculties")]
        public IHttpActionResult GetUniversityFaculties(long universityId)
        {
            return Content(HttpStatusCode.OK, _universityService.GetUniversityFaculties(universityId));
        }

        [HttpPost]
        public IHttpActionResult AddUniversity([FromBody] University university)
        {
            _universityService.AddUniversity(university,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
