using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using Context = System.Runtime.Remoting.Contexts.Context;

namespace HH.Meeting.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository;

        public RegistrationController(Context context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }


    }
}