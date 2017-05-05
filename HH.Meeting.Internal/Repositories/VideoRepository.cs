using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using HH.Meeting.Internal.Exceptions;
using HH.Meeting.Internal.Models;
using Serilog;

namespace HH.Meeting.Internal.Repositories
{
    public interface IVideoRepository
    {
        /// <summary>
        /// Gets video with a specific id
        /// </summary>
        Video GetVideoById(int id);

        /// <summary>
        /// Gets video with matching title
        /// </summary>
        Video FindVideoByTitle(string title);

        /// <summary>
        /// Create or update video
        /// </summary>
        Video CreateOrUpdateVideo(Video video);

        /// <summary>
        /// Deletes video with a specific id
        /// </summary>
        void DeleteVideo(int id);
    }

    public class VideoRepository : IVideoRepository
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public VideoRepository(Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Video GetVideoById(int id)
        {
            return _context.Video.SingleOrDefault(x => x.Id == id);
        }

        public Video FindVideoByTitle(string title)
        {
            return _context.Video.AsNoTracking().FirstOrDefault(x => x.Title == title);
        }

        public Video CreateOrUpdateVideo(Video video)
        {
            var foundVideo = GetVideoById(video.Id);
            var rowsAffected = 0;

            if (foundVideo != null)
            {
                foundVideo.Update(video);
            }
            else
            {
                _context.Video.Add(video);
            }

            try
            {
                rowsAffected += _context.SaveChanges();
            }
            catch (SqlException e)
            {
                _logger.Error("Error adding video {video}", video);

                if (e.InnerException is UpdateException)
                {
                    throw new VideoUpdateException("Failed to insert video");
                }
                throw;
            }

            _logger.Information("Saved video changes for {video} {rowsAffected}", video, rowsAffected);
            return GetVideoById(video.Id);
        }

        public void DeleteVideo(int id)
        {
            var video = _context.Video.Find(id);

            if (video == null)
            {
                return;
            }

            _context.Video.Remove(video);
            _context.SaveChanges();
        }
    }
}