using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HH.Meeting.Internal.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string ProfileImage { get; set; }

        public Country Country { get; set; }

        public DateTimeOffset? JoinedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }
    }
}