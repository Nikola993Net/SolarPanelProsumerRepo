using SolarPanelProsumer.Api.Model;

namespace SolarPanelProsumer.Api.Interafaces
{
    public interface IProsumerRepository
    {
        IQueryable<Prosumer> GetAll();
        Prosumer GetById(int id);
        void Add(Prosumer prosumer);
        void Update(Prosumer prosumer);
        void Delete(Prosumer prosumer);
        void UpdateSunnyHours(int id, float sunnyHours);
    }
}
