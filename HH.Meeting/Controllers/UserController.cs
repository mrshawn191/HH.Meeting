using System.Threading.Tasks;
using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;

namespace HH.Meeting.Controllers
{
    public class UserController : BaseUserController
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceBus _serviceBus;

        public UserController(IUserRepository userRepository, IServiceBus serviceBus)
        {
            _userRepository = userRepository;
            _serviceBus = serviceBus;
        }

        [HttpGet, Route("", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await this.AppUserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(this.BaseUserFactory.Create(user));
        }


    }
}