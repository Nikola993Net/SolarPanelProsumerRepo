using SolarPanelProsumer.Api.Model;

namespace SolarPanelProsumer.Api.Interafaces
{
    public interface ISolarPanelRepository
    {
        IQueryable<SolarPanel> GetAll();
        //IQueryable<SolarPanel> GetAllByProsumerId(int ProsumerId);
        //IQueryable<SolarPanel> GetAllByYearInstalled(int YearInstalled);
        SolarPanel GetById(int id);
        void Add(SolarPanel solarPanel);
        void Update(SolarPanel solarPanel);
        void Delete(SolarPanel solarPanel);
    }
}
