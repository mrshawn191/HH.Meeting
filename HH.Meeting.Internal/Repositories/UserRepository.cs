using System.Linq;
using HH.Meeting.Internal.Models;
using Serilog;

namespace HH.Meeting.Internal.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets user with a specific id
        /// </summary>
        User GetUserById(string id);

        /// <summary>
        /// Creates user
        /// </summary>
        User CreateUser(User user);

        /// <summary>
        /// Updates user
        /// </summary>
        User UpdateUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public UserRepository(Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public User GetUserById(string id)
        {
            return _context.User.SingleOrDefault(x => x.Id == id);
        }

        public User CreateUser(User user)
        {
            var existingUser = _context.User.SingleOrDefault(x => x.Email == user.Email);
            if (existingUser != null)
            {
                return existingUser;
            }

            _context.User.Add(user);
            _context.SaveChanges();

            return GetUserById(user.Id);
        }

        public User UpdateUser(User user)
        {
            _context.SaveChanges();
            return GetUserById(user.Id);
        }
    }
}