using System.Collections.Generic;

namespace HH.Meeting.Internal.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<User> JoinedUser { get; set; }

        public Location Location { get; set; }

        public string PlaceId { get; set; }

        public virtual AnonymousMeetingPart AnonymousMeetingPart { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public void Update(Meeting meeting)
        {
            Title = meeting.Title;
            JoinedUser = meeting.JoinedUser;
            Location = meeting.Location;
            PlaceId = meeting.PlaceId;
            Genres = meeting.Genres;
            Tags = meeting.Tags;
        }
    }
}