using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RefactorThis.GraphDiff;

namespace HH.Meeting.Internal.Repositories
{
    public interface IMeetingRepository
    {
    }

    public class MeetingRepository : IMeetingRepository
    {
        private readonly Context _context;

        public MeetingRepository(Context context)
        {
            _context = context;
        }

        public Models.Meeting GetMeetingById(int id)
        {
            return _context.Meeting.SingleOrDefault(x => x.Id == id);
        }

        public IList<Models.Meeting> GetMeetingsByIds(List<int> ids)
        {
            return _context.Meeting.Where(x => ids.Contains(x.Id)).ToList();
        }

        public Models.Meeting InsertOrUpdateMeeting(Models.Meeting meeting)
        {
            if (_context.Entry(meeting).State == EntityState.Detached)
            {
                // Update object graph using GraphDiff
                meeting = _context.UpdateGraph(meeting, map => map
                    .OwnedEntity(entity => entity.AnonymousMeetingPart,
                        with => with.AssociatedEntity(c => c.AnonymousMeeting))
                    .AssociatedCollection(g => g.Genres)
                    .AssociatedCollection(t => t.Tags)
                );
            }

            _context.SaveChanges();

            return GetMeetingById(meeting.Id);
        }

        public void DeleteMeeting(int id)
        {
            var meeting = _context.Meeting.Find(id);

            if (meeting == null)
            {
                return;
            }

            _context.Meeting.Remove(meeting);
            _context.SaveChanges();
        }
    }
}