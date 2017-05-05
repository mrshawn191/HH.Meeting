using System.Linq;
using HH.Meeting.Internal.Models;

namespace HH.Meeting.Internal.Repositories
{
    public interface ISmsMessageRepository
    {
        /// <summary>
        /// Gets smsMessage with a specific id
        /// </summary>
        SmsMessage GetSmsMessageById(int id);

        /// <summary>
        /// Gets smsMessage with matching from
        /// </summary>
        SmsMessage FindSmsMessageByFrom(int from);

        /// <summary>
        /// Create or update smsMessage
        /// </summary>
        void CreateOrUpdateEdition(SmsMessage smsMessage);

        /// <summary>
        /// Deletes smsMessage with a specific id
        /// </summary>
        void DeleteSmsMessage(int id);
    }

    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly Context _context;

        public SmsMessageRepository(Context context)
        {
            _context = context;
        }

        public SmsMessage GetSmsMessageById(int id)
        {
            return _context.SmsMessage.SingleOrDefault(x => x.Id == id);
        }

        public SmsMessage FindSmsMessageByFrom(int from)
        {
            return _context.SmsMessage.AsNoTracking().FirstOrDefault(x => x.From == from);
        }

        public void CreateOrUpdateEdition(SmsMessage smsMessage)
        {
            var foundSms = GetSmsMessageById(smsMessage.Id);

            if (foundSms != null)
            {
                foundSms.Update(smsMessage);
            }
            else
            {
                _context.SmsMessage.Add(smsMessage);
            }

            _context.SaveChanges();
        }

        public void DeleteSmsMessage(int id)
        {
            var smsMessage = _context.SmsMessage.Find(id);

            if (smsMessage == null)
            {
                return;
            }

            _context.SmsMessage.Remove(smsMessage);
            _context.SaveChanges();
        }
    }
}