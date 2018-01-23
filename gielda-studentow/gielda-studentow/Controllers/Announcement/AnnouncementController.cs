using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Announcement
{
    [Authorize]
    [RoutePrefix("api/Announcement")]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAuthenticatonService authenticatonService, IAnnouncementService announcementService) : base(authenticatonService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            return Content(HttpStatusCode.OK,_announcementService.GetAnnouncementById(id,GetCurrentUserId()));
        }

        [HttpGet]
        [Route("senderAnnouncements")]
        public IHttpActionResult GetAllSenderAnnouncements()
        {
            return Content(HttpStatusCode.OK, _announcementService.GetAllSenderAnnouncements(GetCurrentUserId()));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, _announcementService.GetAllAnnouncements(GetCurrentUserId()));
        }

        [HttpPut]
        [Route("changeStatus/{announcementId}")]
        public IHttpActionResult ChangeStatus(long announcementId)
        {
            try
            {
                _announcementService.ChangeStatus(announcementId);
                return Content(HttpStatusCode.NoContent, "");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Order/IssueDate/Newest")]
        public IHttpActionResult GetAllSortByIssueDateNewestFirst()
        {
            return Content(HttpStatusCode.OK, _announcementService.SortAnnouncementsByIssueDateNewestFirst(_announcementService.GetAllAnnouncements(GetCurrentUserId())));
        }

        [HttpGet]
        [Route("{id}/images")]
        public IHttpActionResult GetAnnouncemenntImages(long id)
        {
            return Content(HttpStatusCode.OK, _announcementService.GetAnnouncementImages(id));
        }

        [HttpPut]
        [Route("{announcementId}/images/add")]
        public IHttpActionResult AddAnnouncementImage([FromBody] ItemImage image, long announcementId)
        {
            try
            {
                _announcementService.AddAnnouncementImage(image, announcementId);
                return Content(HttpStatusCode.NoContent,"");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
