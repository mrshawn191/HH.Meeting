using System.Linq;
using HH.Meeting.Internal.Models;

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
        void CreateOrUpdateVideo(Video video);

        /// <summary>
        /// Deletes video with a specific id
        /// </summary>
        void DeleteVideo(int id);
    }

    public class VideoRepository : IVideoRepository
    {
        private readonly Context _context;

        public VideoRepository(Context context)
        {
            _context = context;
        }

        public Video GetVideoById(int id)
        {
            return _context.Video.SingleOrDefault(x => x.Id == id);
        }

        public Video FindVideoByTitle(string title)
        {
            return _context.Video.AsNoTracking().FirstOrDefault(x => x.Title == title);
        }

        public void CreateOrUpdateVideo(Video video)
        {
            var foundVideo = GetVideoById(video.Id);

            if (foundVideo != null)
            {
                foundVideo.Update(video);
            }
            else
            {
                _context.Video.Add(video);
            }

            _context.SaveChanges();
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