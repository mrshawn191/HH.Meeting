using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using HH.Meeting.Internal.Exceptions;
using HH.Meeting.Internal.Models;
using Serilog;

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
        SmsMessage CreateOrUpdateSmsMessage(SmsMessage smsMessage);

        /// <summary>
        /// Deletes smsMessage with a specific id
        /// </summary>
        void DeleteSmsMessage(int id);
    }

    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public SmsMessageRepository(Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public SmsMessage GetSmsMessageById(int id)
        {
            return _context.SmsMessage.SingleOrDefault(x => x.Id == id);
        }

        public SmsMessage FindSmsMessageByFrom(int from)
        {
            return _context.SmsMessage.AsNoTracking().FirstOrDefault(x => x.From == from);
        }

        public SmsMessage CreateOrUpdateSmsMessage(SmsMessage smsMessage)
        {
            var foundSms = GetSmsMessageById(smsMessage.Id);
            var rowsAffected = 0;

            if (foundSms != null)
            {
                foundSms.Update(smsMessage);
            }
            else
            {
                _context.SmsMessage.Add(smsMessage);
            }

            try
            {
                rowsAffected += _context.SaveChanges();
            }
            catch (SqlException e)
            {
                _logger.Error("Error adding smsmessage {smsmessage}", smsMessage);

                if (e.InnerException is UpdateException)
                {
                    throw new SmsMessageUpdateException("Failed to insert smsmessage");
                }
                throw;
            }

            _logger.Information("Saved smsmessage changes for {smsmessage} {rowsAffected}", smsMessage, rowsAffected);
            return GetSmsMessageById(smsMessage.Id);
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