using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;

namespace HH.Meeting.Public.Messages
{
    public class CreateChatroomMessage : IMessage
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

    public class Location
    {
    }
}