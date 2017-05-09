using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;


namespace HH.Meeting.Public
{
    public interface IServiceBus
    {
        Task SendAsync<T>(T message) where T : IMessage;
    }

    public class ServiceBus : IServiceBus
    {
        /// <summary>
        /// As you can see, the client object reference is assigned to a field of the class,
        /// which is done here intentionally to signal that applications shall hold on to
        /// any Service Bus client objects for as long as possible and preferably for the lifetime of the application.
        /// </summary>
        QueueClient sendClient;


        public ServiceBus()
        {
            this.sendClient =
                QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings["ServiceBus.ConnectionString"],
                    ConfigurationManager.AppSettings["ServiceBus.QueueName"]);
        }

        public async Task SendAsync<T>(T message) where T : IMessage
        {
            var brokedMessage = CreateBrokeredMessagePayload(message);
            await sendClient.SendAsync(brokedMessage);
        }

        /// <summary>
        /// Creates brokeredMessage with the payload as json serialized
        /// </summary>
        private static BrokeredMessage CreateBrokeredMessagePayload<T>(T body)
        {
            var message =
                new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))))
                {
                    ContentType = "application/json",
                    TimeToLive = TimeSpan.FromMinutes(2)
                };

            return message;
        }
    }
}