using Serilog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using SolarPanelProsumer.App.Models;
using SolarPanelProsumer.App.Reflection;

namespace SolarPanelProsumer.App
{
    public class Worker : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "sunnyupdater.sender";
        private EventingBasicConsumer _consumer;

        public Worker(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.GetValue<string>("RabbitMqHost")
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(60*1000, stoppingToken);
                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer);
            }
            return Task.CompletedTask;
        }

        private void SunnyHourUpdater(object sender, BasicDeliverEventArgs eventArgs)
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
