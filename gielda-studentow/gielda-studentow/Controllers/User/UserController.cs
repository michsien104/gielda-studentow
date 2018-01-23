using System.Threading.Tasks;
using System.Web.Http;
using gielda_studentow.Repository;
using Microsoft.AspNet.Identity;
using StudentExchangeDataAccess.Entity;

namespace gielda_studentow.Controllers.User
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly AuthRepository _authRepository = null;


        public UserController()
        {
            _authRepository = new AuthRepository();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _authRepository.RegisterUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }
            return Ok();
        }

        [AllowAnonymous]
        [Route("Register/Tutor")]
        public async Task<IHttpActionResult> RegisterTutor(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _authRepository.RegisterTutor(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }
            return Ok();
        }

        [AllowAnonymous]
        [Route("Register/Student")]
        public async Task<IHttpActionResult> RegisterStudent(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _authRepository.RegisterStudent(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authRepository.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
