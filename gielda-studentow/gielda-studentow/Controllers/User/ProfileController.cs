using System;
using System.Net;
using System.Web.Http;
using gielda_studentow.Controllers.Misc;
using gielda_studentow.Models;
using gielda_studentow.Service.Interface;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/Profile")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IAuthenticatonService authenticatonService, IProfileService profileService) : base(authenticatonService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var myProfile = _profileService.GetUserById(GetCurrentUserId());
            if (myProfile == null)
                return Content(HttpStatusCode.Unauthorized, "User is not logged in");
            return Content(HttpStatusCode.OK, myProfile);
        }

        [HttpGet]
        [Route("isStudent")]
        public IHttpActionResult GetIsStudent()
        {
            return Content(HttpStatusCode.OK, _profileService.IsStudent(GetCurrentUserId()));
        }

        [HttpGet]
        [Route("isTutor")]
        public IHttpActionResult GetIsTutor()
        {
            return Content(HttpStatusCode.OK, _profileService.IsTutor(GetCurrentUserId()));
        }

        [HttpPut]
        [Route("Avatar")]
        public IHttpActionResult ChangeUserAvatar([FromBody] ItemImage itemImage)
        {
            _profileService.ChangeUserAvatar(itemImage.Url,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }

        [HttpPut]
        [Route("Username")]
        public IHttpActionResult ChangeUsername(string newUsername)
        {
            try
            {
                _profileService.ChangeUsername(newUsername, GetCurrentUserId());
                return Content(HttpStatusCode.NoContent, "");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        [Route("Password")]
        public IHttpActionResult ChangePassword([FromBody] StringWrapper password)
        {
            _profileService.ChangePassword(password.Field,GetCurrentUserId());
            return Content(HttpStatusCode.NoContent, "");
        }
    }
}
