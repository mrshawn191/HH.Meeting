using System;

namespace HH.Meeting.Internal.Exceptions
{
    public class MeetingUpdateException : Exception
    {
        public MeetingUpdateException(string message) : base(message)
        {
        }
    }
}