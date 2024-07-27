using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Model;
using System.Net;

namespace SolarPanelProsumer.Api.Repository
{
    public class ProsumerRepository : IProsumerRepository
    {
        public void Add(Prosumer prosumer)
        {
            AllProducts.Add(prosumer);
        }

        public void Delete(Prosumer prosumer)
        {
            if(AllProducts.Contains(prosumer))
                AllProducts.Remove(prosumer);
        }

        public IQueryable<Prosumer> GetAll()
        {
            return AllProducts.AsQueryable();
        }

        public Prosumer GetById(int id)
        {
            var prosumer = AllProducts.FirstOrDefault(x => x.ID == id);
            if (prosumer != null)
            {
                return prosumer;
            }
            return new Prosumer();
        }

        public void Update(Prosumer prosumer)
        {
            var prosumerF = AllProducts.FirstOrDefault(x => x.ID == prosumer.ID);
            if (prosumerF != null)
                prosumerF = prosumer;
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
