using System;

namespace HH.Meeting.Internal.Exceptions
{
    public class SmsMessageUpdateException : Exception
    {
        public SmsMessageUpdateException(string message) : base(message)
        {
        }
    }
}