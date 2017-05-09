using System;
using System.Collections.Generic;
using HH.Meeting.Internal.Models;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HH.Meeting.Internal.Services
{
    public interface ITwilioService
    {
        MessageResource SendSmsMessage(SmsMessage smsMessage);
    }

    public class TwilioService
    {
        private readonly TwilioClient _twilioClient;
        private readonly ILogger _logger;

        public TwilioService(TwilioClient twilioClient, ILogger logger, string accountSid, string authToken)
        {
            _twilioClient = twilioClient;
            _logger = logger;
            TwilioClient.Init(accountSid, authToken);
        }

        public MessageResource SendSmsMessage(SmsMessage smsMessage)
        {
            var sender = new PhoneNumber(smsMessage.From);
            var recipient = new PhoneNumber(smsMessage.To);
            var mediaUrl = new List<Uri>()
            {
                new Uri(smsMessage.UrlToDashboard)
            };

            var message = MessageResource.Create(
                recipient,
                from: sender,
                body: smsMessage.Message,
                mediaUrl: mediaUrl
            );

            if (!IsSentSuccessfully(message.Status))
            {
                _logger.Error("Failed to send smsMessage {message}", message);
            }

            return message;
        }

        private static bool IsSentSuccessfully(MessageResource.StatusEnum status)
        {
            if (Equals(status, MessageResource.StatusEnum.Failed) ||
                Equals(status, MessageResource.StatusEnum.Undelivered))
            {
                return false;
            }

            return true;
        }
    }
}