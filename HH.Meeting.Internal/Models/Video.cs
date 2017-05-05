using System;

namespace HH.Meeting.Internal.Models
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public User UploadedBy { get; set; }

        public string Thumbnail { get; set; }

        public double Grade { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public void Update(Video video)
        {
            Title = video.Title;
            Duration = video.Duration;
            UploadedBy = video.UploadedBy;
            Thumbnail = video.Thumbnail;
            Grade = video.Grade;
        }
    }
}