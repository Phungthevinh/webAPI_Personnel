using WebAPI.DTOs.KOC;

namespace WebAPI.Services.Interfaces
{
    public interface IKOCAnalyticsService
    {
        Task<KOCReportDto> GetKOCReportAsync();
    }
}
