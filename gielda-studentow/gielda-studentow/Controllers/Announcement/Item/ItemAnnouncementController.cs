using System;
using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.Announcement.Item
{
    [Authorize]
    [RoutePrefix("api/ItemAnnouncement")]
    public class ItemAnnouncementController : BaseController
    {
        private readonly IAnnouncementService _announcementService;

        public ItemAnnouncementController(IAuthenticatonService authenticatonService,
            IAnnouncementService announcementService) : base(authenticatonService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, _announcementService.GetItemAnnouncements(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Buy")]
        public IHttpActionResult GetBuyItemAnnouncementsByCurrentUserId()
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetBuyItemAnnouncementsByReceiverId(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Buy/{id}")]
        public IHttpActionResult GetBuyItemAnnouncementId(long id)
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetBuyItemAnnouncementById(id));
        }

        [HttpGet]
        [Route("Sell")]
        public IHttpActionResult GetSellItemAnnouncementsByCurrentUserId()
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetSellItemAnnouncementsByReceiverId(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("Sell/{id}")]
        public IHttpActionResult GetSellItemAnnouncementId(long id)
        {
            return Content(HttpStatusCode.OK,
                _announcementService.GetSellItemAnnouncementById(id));
        }

        [HttpPost]
        [Route("Buy")]
        public IHttpActionResult PostBuy([FromBody] BuyItemAnnouncement announcement)
        {
            _announcementService.AddBuyItemAnnouncement(announcement, GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPost]
        [Route("Sell")]
        public IHttpActionResult PostSell([FromBody] SellItemAnnouncement announcement)
        {
            _announcementService.AddSellItemAnnouncement(announcement,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}