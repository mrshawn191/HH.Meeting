using System.Linq;
using HH.Meeting.Internal.Models;

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
        void CreateOrUpdateChatroom(Chatroom chatroom);

        /// <summary>
        /// Deletes a chatroom with a specific id
        /// </summary>
        void DeleteChatroom(int id);
    }

    public class ChatroomRepository : IChatroomRepository
    {
        private readonly Context _context;

        public ChatroomRepository(Context context)
        {
            _context = context;
        }

        public Chatroom GetChatroomById(int id)
        {
            return _context.Chatroom.SingleOrDefault(x => x.Id == id);
        }

        public Chatroom FindChatroomByTitle(string title)
        {
            return _context.Chatroom.AsNoTracking().FirstOrDefault(x => x.Title == title);
        }

        public void CreateOrUpdateChatroom(Chatroom chatroom)
        {
            var foundChatroom = GetChatroomById(chatroom.Id);

            if (foundChatroom != null)
            {
                foundChatroom.Update(chatroom);
            }
            else
            {
                _context.Chatroom.Add(chatroom);
            }

            _context.SaveChanges();
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