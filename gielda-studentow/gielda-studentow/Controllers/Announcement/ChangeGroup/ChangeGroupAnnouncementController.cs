using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Announcement.ChangeGroup
{
    [Authorize]
    [RoutePrefix("api/ChangeGroupAnnouncement")]
    public class ChangeGroupAnnouncementController : BaseController
    {
        private readonly IAnnouncementService _announcementService;

        public ChangeGroupAnnouncementController(IAuthenticatonService authenticatonService, IAnnouncementService announcementService) : base(authenticatonService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public IHttpActionResult GetCurrentUserChangeGroupAnnouncements()
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetChangeGroupAnnouncementsByReceiverId(GetCurrentUserId()));
        }

        [HttpPost]
        public IHttpActionResult PostChangeGroupAnnouncement([FromBody] ChangeGroupAnnouncement announcement)
        {
            _announcementService.AddChangeGroupAnnouncement(announcement, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetChangeGroupAnnouncementById(long id)
        {
            return Content(HttpStatusCode.OK, _announcementService.GetChangeGroupAnnouncementById(id));
        }
    }
}
