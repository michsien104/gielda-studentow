using System.Web.Http;
using gielda_studentow.Service.Interface;
using Microsoft.AspNet.Identity;

namespace gielda_studentow.Controllers.Misc
{
    public abstract class BaseController : ApiController
    {
        protected readonly IAuthenticatonService AuthenticatonService;

        protected BaseController(IAuthenticatonService authenticatonService)
        {
            AuthenticatonService = authenticatonService;
        }

        protected string GetCurrentUserId()
        {
            var userName = RequestContext.Principal.Identity.GetUserName();
            var receiverId = AuthenticatonService.GetUserIdByUsername(userName);
            return receiverId;
        }
    }
}
