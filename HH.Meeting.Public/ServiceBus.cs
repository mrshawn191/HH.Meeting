using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;


namespace HH.Meeting.Public
{
    public interface IServiceBus
    {
        Task SendAsync<T>(T message) where T : IMessage;
    }

    public class ServiceBus : IServiceBus
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private ConcurrentDictionary<Type, QueueClient> QueueClients { get; }

        private MessagingFactory Factory => _factory ?? (_factory = MessagingFactory.CreateFromConnectionString(_config.ServiceBusConnectionString));
        private NamespaceManager Manager => _manager ?? (_manager = NamespaceManager.CreateFromConnectionString(_config.ServiceBusConnectionString));


        public async Task SendAsync<T>(T message) where T : IMessage
        {
            var client = await GetQueueClient<T>();
            var brokedMessage = CreateMessage(message);

            await client.SendAsync(brokedMessage);
        }


        private async Task<QueueClient> GetQueueClient<T>() where T : IMessage
        {
            QueueClient client;
            if (QueueClients.TryGetValue(typeof(T), out client))
            {
                return client;
            }

            try
            {
                await _semaphore.WaitAsync();

                if (QueueClients.TryGetValue(typeof(T), out client))
                {
                    return client;
                }

                client = await CreateQueueClient<T>();
                return QueueClients.GetOrAdd(typeof(T), client);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<QueueClient> CreateQueueClient<T>() where T : IMessage
        {
            var queueName = _messageMapper.GetQueueName<T>();

            if (!await Manager.QueueExistsAsync(queueName))
            {
                var description = new QueueDescription(queueName) {EnablePartitioning = true};
                await Manager.CreateQueueAsync(description);
            }

            return Factory.CreateQueueClient(queueName);
        }
    }
}