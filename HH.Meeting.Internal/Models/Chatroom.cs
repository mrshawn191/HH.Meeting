using System.Collections.Generic;

namespace HH.Meeting.Internal.Models
{
    public class Chatroom
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Location Location { get; set; }

        public User Owner { get; set; }

        public List<User> JoinedUsers { get; set; }

        public void Update(Chatroom chatroom)
        {
            Title = chatroom.Title;
            Description = chatroom.Description;
            ImageUrl = chatroom.ImageUrl;
            Location = chatroom.Location;
            Owner = chatroom.Owner;
            JoinedUsers = chatroom.JoinedUsers;
        }
    }
}