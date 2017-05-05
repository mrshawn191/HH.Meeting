using System.Data.Entity;
using HH.Meeting.Internal.Models;

namespace HH.Meeting.Internal.Repositories
{
    public class Context : DbContext
    {
        public virtual DbSet<Models.Meeting> Meeting { get; set; }

        public virtual DbSet<Event> Event { get; set; }

        public virtual DbSet<Chatroom> Chatroom { get; set; }

        public virtual DbSet<SmsMessage> SmsMessage { get; set; }

        public virtual DbSet<Video> Video { get; set; }

        public virtual DbSet<Membership> Membership { get; set; }
    }
}