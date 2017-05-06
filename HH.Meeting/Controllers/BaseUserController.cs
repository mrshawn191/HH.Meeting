using System.Web.Http;
using HH.Meeting.Internal.Factories;
using HH.Meeting.Internal.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HH.Meeting.Controllers
{
    public class BaseUserController : ApiController
    {
        private UserFactory _userFactory;
        private ApplicationUserManager _applicationUserManager = null;

        protected ApplicationUserManager AppUserManager => _applicationUserManager ??
                                                           Request.GetOwinContext()
                                                               .GetUserManager<ApplicationUserManager>();

        protected UserFactory BaseUserFactory
        {
            get
            {
                if (_userFactory == null)
                {
                    _userFactory = new UserFactory(this._applicationUserManager);
                }
                return _userFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
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