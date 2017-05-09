using HH.Meeting.Internal.Exceptions;
using HH.Meeting.Internal.Models;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;
using HH.Meeting.Public.Messages;
using HH.Meeting.Public.RequestDto;
using HH.Meeting.Public.Requests;
using Location = HH.Meeting.Internal.Models.Location;

namespace HH.Meeting.Internal.Factories
{
    public interface IChatroomFactory
    {
    }

    public class ChatroomFactory : IChatroomFactory
    {
        private readonly IChatroomRepository _chatroomRepository;
        private readonly IServiceBus _serviceBus;

        public ChatroomFactory(IChatroomRepository chatroomRepository, IServiceBus serviceBus)
        {
            _chatroomRepository = chatroomRepository;
            _serviceBus = serviceBus;
        }

        public Chatroom CreateOrUpdate(CreateChatroomRequest request)
        {
            var chatroom = new Chatroom
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Limit = request.Limit,
                Location = CreateLocationFromRequest(request)
            };
        }

        public async Task CreateOrUpdate(Chatroom chatroom)
        {
            try
            {
                _chatroomRepository.CreateOrUpdateChatroom(chatroom);
            }
            catch (ChatroomUpdateException)
            {
                _chatroomRepository.CreateOrUpdateChatroom(chatroom);
            }


        }

        private async Task SendCreateChatroomMessage()
        {
            await _serviceBus.SendAsync(new CreateChatroomMessage
            {

            });
        }

        private Location CreateLocationFromRequest(CreateChatroomRequest request)
        {
            return new Location
            {
                Latitude = request.Location.Latitude,
                Longitude = request.Location.Longitude
            };
        }
    }
}