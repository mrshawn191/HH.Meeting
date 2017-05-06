using System;
using System.Threading.Tasks;
using System.Web.Http;
using HH.Meeting.Internal.Models;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public.RequestDto;
using Context = System.Runtime.Remoting.Contexts.Context;

namespace HH.Meeting.Controllers
{
    public class RegistrationController : BaseUserController
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository;

        public RegistrationController(Context context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateUser(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JoinedAt = DateTimeOffset.UtcNow
            };

            var createUserResult = await this.AppUserManager.CreateAsync(user, request.Password);

            if (!createUserResult.Succeeded)
            {
                return GetErrorResult(createUserResult);
            }

            // Generate email confirmation token
            string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = user.Id, code = code}));

            await this.AppUserManager.SendEmailAsync(user.Id,
                "Confirm your account",
                "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            var locationHeader = new Uri(Url.Link("GetUserById", new {id = user.Id}));

            return Created(locationHeader, BaseUserFactory.Create(user));
        }
    }
}