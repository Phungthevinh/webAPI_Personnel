using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.DTOs.KOC;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class KocPerformanceService:IKocCouponService
    {
        private readonly dbContext _dbContext;
        public KocPerformanceService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<KOCReportDto> getCouponUsageSummary(ClaimsPrincipal claims, long id = 6)
        {
            KOCReportDto kOCReportDto = new KOCReportDto();
            var userName = claims.Identity.Name;
            var userId = claims.FindFirst("id");
            Console.WriteLine(userId);

            var Revenues = await (
                    from code in _dbContext.used_discount_codes
                    group code by code.discount_Codes.kol_id into c
                    where c.Key == id
                    select new KOCRevenueDto
                    {
                        TotalOrders = c.Select(c => c.code).Count(),
                        KOCId = id,
                        TotalRevenue = c.Sum(c => c.discount_amount_applied),
                        AverageOrderValue = c.Average(c => c.order_value),
                        KOCName = userName
                    }

                ).ToArrayAsync();
            kOCReportDto.Revenues = Revenues;
            return kOCReportDto;
        }
    }
}
