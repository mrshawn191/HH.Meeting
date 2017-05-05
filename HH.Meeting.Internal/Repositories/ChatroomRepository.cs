using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using HH.Meeting.Internal.Exceptions;
using HH.Meeting.Internal.Models;
using Serilog;

namespace HH.Meeting.Internal.Repositories
{
    public interface IChatroomRepository
    {
        /// <summary>
        /// Gets chatroom with a specific id
        /// </summary>
        Chatroom GetChatroomById(int id);

        /// <summary>
        /// Gets chatroom with matching title
        /// </summary>
        Chatroom FindChatroomByTitle(string title);

        /// <summary>
        /// Creates or update chatroom
        /// </summary>
        Chatroom CreateOrUpdateChatroom(Chatroom chatroom);

        /// <summary>
        /// Deletes a chatroom with a specific id
        /// </summary>
        void DeleteChatroom(int id);
    }

    public class ChatroomRepository : IChatroomRepository
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public ChatroomRepository(Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Chatroom GetChatroomById(int id)
        {
            return _context.Chatroom.SingleOrDefault(x => x.Id == id);
        }

        public Chatroom FindChatroomByTitle(string title)
        {
            return _context.Chatroom.AsNoTracking().FirstOrDefault(x => x.Title == title);
        }

        public Chatroom CreateOrUpdateChatroom(Chatroom chatroom)
        {
            var foundChatroom = GetChatroomById(chatroom.Id);
            var rowsAffected = 0;

            if (foundChatroom != null)
            {
                foundChatroom.Update(chatroom);
            }
            else
            {
                _context.Chatroom.Add(chatroom);
            }

            try
            {
                rowsAffected += _context.SaveChanges();
            }
            catch (SqlException e)
            {
                _logger.Error("Error adding {chatroom}", chatroom);

                if (e.InnerException is UpdateException)
                {
                    throw new ChatroomUpdateException("Failed to insert chatroom");
                }
                throw;
            }

            _logger.Information("Saved chatroom changes for {chatroom} {rowsAffected}", chatroom, rowsAffected);
            return GetChatroomById(chatroom.Id);
        }

        public void DeleteChatroom(int id)
        {
            var chatroom = _context.Chatroom.Find(id);

            if (chatroom == null)
            {
                return;
            }

            _context.Chatroom.Remove(chatroom);
            _context.SaveChanges();
        }
    }
}