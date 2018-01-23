using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Education
{
    [Authorize]
    [RoutePrefix("api/Group")]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IAuthenticatonService authenticatonService, IGroupService groupService) : base(authenticatonService)
        {
            _groupService = groupService;
        }


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            return Content(HttpStatusCode.OK,_groupService.GetGroupById(id));
        }

        [HttpGet]
        [Route("{groupId}/Members")]
        public IHttpActionResult GetGroupMembers(long groupId)
        {
            return Content(HttpStatusCode.OK, _groupService.GetGroupMembers(groupId));
        }

        [HttpPut]
        [Route("{groupId}/Join")]
        public IHttpActionResult JoinGroup(long groupId)
        {
            _groupService.AddStudentToGroup(GetCurrentUserId(),groupId);
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPut]
        [Route("{groupId}/Leave")]
        public IHttpActionResult LeaveGroup(long groupId)
        {
            _groupService.RemoveStudentFromGroup(GetCurrentUserId(),groupId);
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpGet]
        [Route("ByCourse/{courseId}/NotMember/{studentId}")]
        public IHttpActionResult GetCourseGroupsWhereStudentIsNotAssigned(long courseId, string studentId)
        {
            return Content(HttpStatusCode.OK,
                _groupService.GetCourseGroupsWhereStudentIsNotAssigned(courseId, studentId));
        }

        [HttpGet]
        [Route("ByCourse/{courseId}/NotMember/Me")]
        public IHttpActionResult GetCourseGroupsWhereIAmNotAssigned(long courseId)
        {
            return Content(HttpStatusCode.OK,
                _groupService.GetCourseGroupsWhereStudentIsNotAssigned(courseId, GetCurrentUserId()));
        }

        [HttpPost]
        [Route("CourseOfStudy/{courseId}")]
        public IHttpActionResult AddGroup([FromBody] Group group, long courseId)
        {
            _groupService.AddGroup(group, courseId, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
