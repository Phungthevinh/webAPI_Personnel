using System.Security.Claims;
using WebAPI.DTOs.KOC;

namespace WebAPI.Services.Interfaces
{
    public interface IKocCouponService
    {
        // KOC xem số lượng mã giảm giá đã sử dụng của chính bản thân và doanh thu mang về
        public Task<KOCReportDto> getCouponUsageSummary(ClaimsPrincipal claims, DateTime date);
    }
}
