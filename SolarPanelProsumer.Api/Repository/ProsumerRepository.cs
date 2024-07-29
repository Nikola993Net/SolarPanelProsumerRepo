using SolarPanelProsumer.Api.Integrations;
using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;
using System.Net;

namespace SolarPanelProsumer.Api.Repository
{
    public class ProsumerRepository : IProsumerRepository
    {
        private readonly ILogger<IProsumerRepository> _logger;
        private readonly ISunnyHoursProcessingNotification _sunnyHoursNotification;

        public ProsumerRepository(ILogger<IProsumerRepository> logger, ISunnyHoursProcessingNotification sunnyRepository)
        {
            _logger = logger;
            _sunnyHoursNotification = sunnyRepository;
        }

        public void Add(Prosumer prosumer)
        {
            _logger.LogInformation(" Addint repository method is staring");
            AllProducts.Add(prosumer);
            _logger.LogInformation(" Addint repository method is finished");
        }

        public void Delete(Prosumer prosumer)
        {
            _logger.LogInformation(" Deleting repository method is staring");
            if (AllProducts.Contains(prosumer))
            {
                AllProducts.Remove(prosumer);
                _logger.LogInformation(" Deleting repository method is finished");
            }
            else
                _logger.LogCritical("The prosmer is not found in base");
        }

        public IQueryable<Prosumer> GetAll()
        {
            _logger.LogInformation("Gettling all prosumer from base");
            return AllProducts.AsQueryable();
        }

        public Prosumer GetById(int id)
        {
            var prosumer = AllProducts.FirstOrDefault(x => x.ID == id);
            if (prosumer != null)
            {
                _logger.LogInformation($"Get the prosumer with Id {id}");
                return prosumer;
            }
            _logger.LogCritical("The prosmer is not found in base");
            return new Prosumer();
        }

        public void Update(Prosumer prosumer)
        {
            var prosumerF = AllProducts.FirstOrDefault(x => x.ID == prosumer.ID);
            if (prosumerF != null)
            {
                prosumerF = prosumer;
                _logger.LogInformation($"Prosumer {prosumer.ID} is updated");
            }
            else
                _logger.LogCritical("The prosmer is not found in base");

        }

        public void UpdateSunnyHours(int id, float sunnyHours)
        {
            var prosumer = AllProducts.FirstOrDefault(x => x.ID == id);
            if (prosumer != null)
            {
                _logger.LogInformation($"Prosumer {prosumer} will be updated");
                _sunnyHoursNotification.SunnyHoursSend(prosumer, (decimal) sunnyHours);
            }
            else
            {
                _logger.LogCritical($"Prosumer with {id} is not found");
            }
            
        }

        static List<Prosumer> AllProducts =  new List<Prosumer>
            {
                new Prosumer() { ID = 1, Name = "Nikola", Surname = "Nikolic", DalyPowerConsuption = (decimal)35.62, SunnyHourPerDay = (decimal)10.0, GeneratePower = (decimal)10.0, PulledPower = (decimal)25.62 },
                new Prosumer() { ID = 2, Name = "Pera", Surname = "Peric", DalyPowerConsuption = (decimal)45.62, SunnyHourPerDay = (decimal)10.0, GeneratePower = (decimal)15.0, PulledPower = (decimal)30.62 },
                new Prosumer() { ID = 3, Name = "Nikola", Surname = "Nikolic", DalyPowerConsuption = (decimal)35.62, SunnyHourPerDay = (decimal)10.0, GeneratePower = (decimal)10.0, PulledPower = (decimal)25.62 },
                new Prosumer() { ID = 4, Name = "Jovan", Surname = "Ilic", DalyPowerConsuption = (decimal)35.62, SunnyHourPerDay = (decimal)10.0, GeneratePower = (decimal)10.0, PulledPower = (decimal)25.62 }
            };
    }
}
