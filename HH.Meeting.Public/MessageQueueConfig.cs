using System.Configuration;

namespace HH.Meeting.Public
{
    public interface IMessageQueueConfig
    {
        string ServiceBusConnectionString { get; }
    }

    public class MessageQueueConfig : IMessageQueueConfig
    {
        public string ServiceBusConnectionString => ConfigurationManager.AppSettings["ServiceBus.ConnectionString"];
    }

}