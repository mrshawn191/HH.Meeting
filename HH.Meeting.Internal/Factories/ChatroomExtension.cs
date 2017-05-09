using HH.Meeting.Internal.Models;
using HH.Meeting.Public.ResponseDto;

namespace HH.Meeting.Internal.Factories
{
    public static class ChatroomExtension
    {
        public static ChatroomResponse ToResponse(this Chatroom chatroom)
        {
            if (chatroom == null)
            {
                return null;
            }

            return new ChatroomResponse
            {

            };
        }
    }
}