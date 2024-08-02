using SolarPanelProsumer.App.Repository;

namespace SolarPanelProsumer.App
{
    public class Worker : BackgroundService
    {

        public Worker(IConfiguration config)
        {
            MessageReceiver.Initialize(config);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                MessageReceiver.Instance.BasicConsume();
            }
            return Task.CompletedTask;
        }
    }
}
