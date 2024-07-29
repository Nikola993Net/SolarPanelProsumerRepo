using SolarPanelProsumer.Api.Model;
using RabbitMQ.Client;
using System.Text.Json;
using Serilog;


namespace SolarPanelProsumer.Api.Integrations
{
    public class SunnyHoursProcessingNotification : ISunnyHoursProcessingNotification, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "sunnyupdater.sender";

        public SunnyHoursProcessingNotification(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.GetValue<string>("RabbitMqHost")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SunnyHoursSend(Prosumer prosumer, decimal sunnyHours)
        {
            var message = new UpdateSunnyHoursSendMessage { Prosumer = prosumer, NewSunnyHours = sunnyHours };
            var messageBytes = JsonSerializer.SerializeToUtf8Bytes(message);
            _channel.BasicPublish("", routingKey: queueName, basicProperties: null, body: messageBytes);
            Log.ForContext("Body", message, true)
                .Information("Published sunny hours updater notification");
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }

    
}
