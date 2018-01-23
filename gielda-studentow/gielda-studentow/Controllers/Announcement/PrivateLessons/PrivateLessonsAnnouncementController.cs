using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Announcement.PrivateLessons
{
    [Authorize]
    [RoutePrefix("api/PrivateLessons")]
    public class PrivateLessonsAnnouncementController : BaseController
    {
        private readonly IAnnouncementService _announcementService;

        public PrivateLessonsAnnouncementController(IAuthenticatonService authenticatonService, IAnnouncementService announcementService) : base(authenticatonService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        [Route("Take")]
        public IHttpActionResult GetTakePrivateLessonsAnnouncementsByCurrentUserId()
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetTakePrivateLessonsAnnouncementsByReceiverId(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Take/{id}")]
        public IHttpActionResult GetTakePrivateLessonsAnnouncementId(long id)
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetTakePrivateLessonsAnnouncementById(id));
        }

        [HttpGet]
        [Route("Give")]
        public IHttpActionResult GetGivePrivateLessonsAnnouncementsByCurrentUserId()
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetGivePrivateLessonsAnnouncementsByReceiverId(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Give/{id}")]
        public IHttpActionResult GetGivePrivateLessonsAnnouncementId(long id)
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetGivePrivateLessonsAnnouncementById(id));
        }

        [HttpPost]
        [Route("Take")]
        public IHttpActionResult PostTake([FromBody] TakePrivateLessonsAnnouncement announcement)
        {
            _announcementService.AddTakePrivateLessonsAnnouncement(announcement,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Give")]
        public IHttpActionResult PostGive([FromBody] GivePrivateLessonsAnnouncement announcement)
        {
            _announcementService.AddGivePrivateLessonsAnnouncement(announcement,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
