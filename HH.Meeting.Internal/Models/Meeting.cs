using System.Collections.Generic;

namespace HH.Meeting.Internal.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        public virtual AnonymousMeetingPart AnonymousMeetingPart { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}