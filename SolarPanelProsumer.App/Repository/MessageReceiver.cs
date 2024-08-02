using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using SolarPanelProsumer.App.Models;
using SolarPanelProsumer.App.Reflection;
using System.Text.Json;

namespace SolarPanelProsumer.App.Repository
{
    /// <summary>
    /// This is singleton class which is 
    /// </summary>
    public sealed class MessageReceiver
    {
        private static IConfiguration? _configuration;
        private static readonly Lazy<MessageReceiver> _lazy = new Lazy<MessageReceiver>(() => new MessageReceiver(_configuration));

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "sunnyupdater.sender";
        private EventingBasicConsumer _consumer;

        public static MessageReceiver Instance
        {
            get
            {
                if (_configuration == null)
                {
                    throw new InvalidOperationException("MessageReceiver has not been initialized. Call Initialize() first.");
                }
                return _lazy.Value;
            }
        }

        private MessageReceiver(IConfiguration? config)
        {
            _configuration = config;
            if (_configuration == null)
            {
                throw new InvalidOperationException("MessageReceiver has not been created.");
            }
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("RabbitMqHost")
            };
            try
            {
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (_connection != null)
                        continue;
                    Thread.Sleep(3000);
                    try { _connection = factory.CreateConnection(); } catch { }
                }
                if (_connection == null) throw;
            }
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += SunnyHourUpdater;
        }

        public static void Initialize(IConfiguration config)
        {
            if (_lazy.IsValueCreated)
            {
                throw new InvalidOperationException("MessageReceiver has already been initialized.");
            }
            _configuration = config;
        }

        public void BasicConsume()
        {
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer);
        }

        private void SunnyHourUpdater(object? sender, BasicDeliverEventArgs eventArgs)
        {
            var updaterInfo = JsonSerializer.Deserialize<UpdateSunnyHoursReceivedMessage>(eventArgs.Body.ToArray());
            Log.ForContext("UpdaterHoursReceived", updaterInfo, true)
                .Information("Received message from SolarPanelProsumer Api project");

            var instaceOfOtherClass =
                ReflectionHelper.GetInstanceOfClass("SolarPanelProsumer.App.Models.Prosumer");
            Log.Information($"The instance of other class is {instaceOfOtherClass}");
            //TODO: At this place should call the DataBase and sned the query for updating sunny hours of the prosmuer 
        }


    }
}
