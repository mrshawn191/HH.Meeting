using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HH.Meeting.Public.RequestDto
{
    public class CreateChatroomRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Limit { get; set; }

        public string ImageUrl { get; set; }

        public Location Location { get; set; }

        public User Owner { get; set; }

        public List<User> JoinedUsers { get; set; }
    }

    public class User : IdentityUser
    {
    }

    public class Location
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}