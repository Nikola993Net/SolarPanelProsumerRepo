using SolarPanelProsumer.Api.Model;

namespace SolarPanelProsumer.Api.Integrations
{
    public interface ISunnyHoursProcessingNotification
    {
        void SunnyHoursSend(Prosumer prosumer, decimal sunnyHours);
    }
}
