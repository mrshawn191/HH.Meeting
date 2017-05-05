using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using HH.Meeting.Internal.Exceptions;
using RefactorThis.GraphDiff;
using Serilog;

namespace HH.Meeting.Internal.Repositories
{
    public interface IMeetingRepository
    {
        /// <summary>
        /// Gets meeting with a specific id
        /// </summary>
        Models.Meeting GetMeetingById(int id);

        /// <summary>
        /// Gets meetings by matching ids
        /// </summary>
        IList<Models.Meeting> GetMeetingsByIds(List<int> ids);

        /// <summary>
        /// Create or update meeting with all its associated relations
        /// </summary>
        Models.Meeting InsertOrUpdateMeeting(Models.Meeting meeting);

        /// <summary>
        /// Creates or update meeting
        /// </summary>
        Models.Meeting CreateOrUpdateMeeting(Models.Meeting meeting);

        /// <summary>
        /// Deletes meeting with a specific id
        /// </summary>
        void DeleteMeeting(int id);
    }

    public class MeetingRepository : IMeetingRepository
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public MeetingRepository(Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
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
            var rowsAffected = 0;

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

            try
            {
                rowsAffected += _context.SaveChanges();
            }
            catch (SqlException e)
            {
                _logger.Error("Error adding {meeting}", meeting);

                if (e.InnerException is UpdateException)
                {
                    throw new MeetingUpdateException("Failed to insert meeting");
                }
                throw;
            }

            _logger.Information("Saved meeting changes for {meeting} {rowsAffected}", meeting, rowsAffected;
            return GetMeetingById(meeting.Id);
        }

        public Models.Meeting CreateOrUpdateMeeting(Models.Meeting meeting)
        {
            var foundMeeting = GetMeetingById(meeting.Id);
            var rowsAffected = 0;

            if (foundMeeting != null)
            {
                foundMeeting.Update(meeting);
            }
            else
            {
                _context.Meeting.Add(meeting);
            }

            try
            {
                rowsAffected += _context.SaveChanges();
            }
            catch (SqlException e)
            {
                _logger.Error("Error adding meeting {meeting}", meeting);

                if (e.InnerException is UpdateException)
                {
                    throw new MeetingUpdateException("Failed to insert meeting");
                }
                throw;
            }

            _logger.Information("Saved meeting changes for {meeting} {rowsAffected}", meeting, rowsAffected);
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