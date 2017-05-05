using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;

namespace HH.Meeting.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceBus _serviceBus;

        public UserController(IUserRepository userRepository, IServiceBus serviceBus)
        {
            _userRepository = userRepository;
            _serviceBus = serviceBus;
        }

    }
}