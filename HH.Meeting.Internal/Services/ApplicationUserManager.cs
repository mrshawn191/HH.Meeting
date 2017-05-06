using HH.Meeting.Internal.Models;
using Microsoft.AspNet.Identity;

namespace HH.Meeting.Internal.Services
{
    /// <summary>
    /// Manages instances of the user class.
    /// </summary>
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
        }
    }
}