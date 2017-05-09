using System.Threading.Tasks;
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
        Task<Chatroom> CreateOrUpdate(CreateChatroomRequest request);
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

        public async Task<Chatroom> CreateOrUpdate(CreateChatroomRequest request)
        {
            var chatroom = new Chatroom
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Limit = request.Limit,
                Location = CreateLocationFromRequest(request)
            };

            return await CreateOrUpdate(chatroom);
        }

        private async Task<Chatroom> CreateOrUpdate(Chatroom chatroom)
        {
            try
            {
                _chatroomRepository.CreateOrUpdateChatroom(chatroom);
            }
            catch (ChatroomUpdateException)
            {
                _chatroomRepository.CreateOrUpdateChatroom(chatroom);
            }

            await SendCreateChatroomMessage(chatroom);

            return _chatroomRepository.GetChatroomById(chatroom.Id);
        }

        private async Task SendCreateChatroomMessage(Chatroom chatroom)
        {
            await _serviceBus.SendAsync(new CreateChatroomMessage
            {
                Title = chatroom.Title,
                Description = chatroom.Description,
                ImageUrl = chatroom.ImageUrl,
                Limit = chatroom.Limit
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