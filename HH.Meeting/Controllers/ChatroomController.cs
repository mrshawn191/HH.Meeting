﻿using System.Web.Http;
using System.Web.Services.Protocols;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;
using HH.Meeting.Public.Requests;

namespace HH.Meeting.Controllers
{
    public class ChatroomController : ApiController
    {
        private readonly IChatroomRepository _chatroomRepository;
        private readonly IServiceBus _serviceBus;

        public ChatroomController(IChatroomRepository chatroomRepository, IServiceBus serviceBus)
        {
            _chatroomRepository = chatroomRepository;
            _serviceBus = serviceBus;
        }

        [HttpGet, Route("api/chatroom/{id}")]
        public IHttpActionResult GetChatroom([FromUri] int id)
        {
            var chatroom = _chatroomRepository.GetChatroomById(id);

            return Ok(chatroom);
        }

        [HttpPost, Route("api/chatroom/{id}")]
        public IHttpActionResult CreateOrUpdateChatroom([FromUri] int id, [FromBody] CreateChatroomRequest request)
        {

        }
    }
}