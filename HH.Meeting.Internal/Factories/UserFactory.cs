using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using HH.Meeting.Internal.Models;
using HH.Meeting.Internal.Services;
using System.Web.Http;

using HH.Meeting.Public.ResponseDto;

namespace HH.Meeting.Internal.Factories
{
    public class UserFactory
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly UrlHelper _urlHelper;

        public UserFactory(ApplicationUserManager applicationUserManager)
        {
            _urlHelper = new UrlHelper(new HttpControllerContext());
            _applicationUserManager = applicationUserManager;
        }

        public CreateUserResponse Create(User user)
        {
            return new CreateUserResponse
            {
                Url = _urlHelper.Link("GetUserById", new {id = user.Id}),
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                JoinDate = user.JoinedAt
            };
        }
    }
}