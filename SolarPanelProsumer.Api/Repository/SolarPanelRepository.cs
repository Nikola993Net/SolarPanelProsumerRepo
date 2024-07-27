using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;

namespace SolarPanelProsumer.Api.Repository
{
    public class SolarPanelRepository : ISolarPanelRepository
    {
        private readonly ILogger<SolarPanelRepository> _logger;

        public SolarPanelRepository(ILogger<SolarPanelRepository> logger)
        {
                _logger = logger;
        }

        public void Add(SolarPanel solarPanel)
        {
            _logger.LogInformation(" Addint repository method is staring");
            solarPanels.Add(solarPanel);
            _logger.LogInformation(" Addint repository method is finished");
        }

        public void Delete(SolarPanel solarPanel)
        {
            _logger.LogInformation(" Deleting repository method is staring");
            solarPanels.RemoveAll(x => x.ID == solarPanel.ID);
        }

        public IQueryable<SolarPanel> GetAll()
        {
            _logger.LogInformation("Gettling all prosumer from base");
            return solarPanels.AsQueryable();  
        }

        public SolarPanel GetById(int id)
        {
            _logger.LogInformation($"Get the prosumer with Id {id}");
            return solarPanels.FirstOrDefault(sp => sp.ID == id) is SolarPanel panel? panel : new SolarPanel();
        }

        public void Update(SolarPanel solarPanel)
        {
            _logger.LogInformation($"Prosumer {solarPanel.ID} is updating");
            var panel = solarPanels.FirstOrDefault(sp => sp.ID == solarPanel.ID);
            if (panel != null)
                panel = solarPanel;
        }

        static List<SolarPanel> solarPanels = new List<SolarPanel>
        {
            new SolarPanel() {ID = 1, PowerPerHour = (decimal)0.78, YearInstalled = 2020, ProsumerID = 1 },
            new SolarPanel() { ID = 2, PowerPerHour = (decimal)0.99, YearInstalled = 2020, ProsumerID = 2 },
            new SolarPanel() { ID = 3, PowerPerHour = (decimal)1.78, YearInstalled = 2020, ProsumerID = 3 },
            new SolarPanel() { ID = 4, PowerPerHour = (decimal)1.68, YearInstalled = 2020, ProsumerID = 4 },
            new SolarPanel() { ID = 5, PowerPerHour = (decimal)0.60, YearInstalled = 2020, ProsumerID = 1 },
            new SolarPanel() { ID = 6, PowerPerHour = (decimal)1.65, YearInstalled = 2020, ProsumerID = 2 },
            new SolarPanel() { ID = 7, PowerPerHour = (decimal)2.00, YearInstalled = 2020, ProsumerID = 3 },
            new SolarPanel() { ID = 8, PowerPerHour = (decimal)1.18, YearInstalled = 2020, ProsumerID = 4 },
            new SolarPanel() { ID = 9, PowerPerHour = (decimal)0.99, YearInstalled = 2020, ProsumerID = 1 }
        };
    }
}
