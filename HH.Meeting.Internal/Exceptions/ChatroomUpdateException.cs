using System;

namespace HH.Meeting.Internal.Exceptions
{
    public class ChatroomUpdateException : Exception
    {
        public ChatroomUpdateException(string message) : base(message)
        {
        }
    }
}