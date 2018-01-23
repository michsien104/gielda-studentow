using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Education
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessageController(IAuthenticatonService authenticatonService, IMessageService messageService) : base(authenticatonService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IHttpActionResult GetAllCurrentStudentMessages()
        {
            return Content(HttpStatusCode.OK,_messageService.GetAllStudentMessages(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Order/Newest")]
        public IHttpActionResult GetAllCurrentStudentMessagesNewestFirst()
        {
            return Content(HttpStatusCode.OK, _messageService.SortMessagesNewestFirst(_messageService.GetAllStudentMessages(GetCurrentUserId())));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            return Content(HttpStatusCode.OK,_messageService.GetMessageById(id));
        }

        [HttpPost]
        [Route("University/{id}")]
        public IHttpActionResult NewUniversityMessage([FromBody] Message message, long id)
        {
            _messageService.NewUniversityMessage(message, id, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Faculty/{id}")]
        public IHttpActionResult NewFacultyMessage([FromBody] Message message, long id)
        {
            _messageService.NewFacultyMessage(message, id, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Course/{id}")]
        public IHttpActionResult NewCourseOfStudyMessage([FromBody] Message message, long id)
        {
            _messageService.NewCourseOfStudyMessage(message, id, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Group/{id}")]
        public IHttpActionResult NewGroupMessage([FromBody] Message message, long id)
        {
            _messageService.NewGroupMessage(message, id, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
