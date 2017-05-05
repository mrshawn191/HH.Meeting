using System.Collections.Generic;

namespace HH.Meeting.Internal.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<User> JoinedUser { get; set; }

        public Location Location { get; set; }

        public string PlaceId { get; set; }
    }
}