using System;

namespace HH.Meeting.Internal.Exceptions
{
    public class VideoUpdateException : Exception
    {
        public VideoUpdateException(string message) : base(message)
        {
        }
    }
}