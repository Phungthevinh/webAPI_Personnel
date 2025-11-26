using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebAPI.DTOs.KOC;
using WebAPI.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Services
{
    public class KocPerformanceService:IKocCouponService
    {
        private readonly dbContext _dbContext;
        public KocPerformanceService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<KOCReportDto> getCouponUsageSummary(ClaimsPrincipal claims, DateTime date)
        {
            KOCReportDto kOCReportDto = new KOCReportDto();
            var userName = claims.Identity.Name;
            var targetMonth = date.ToUniversalTime();

            var userID = _dbContext.users
                .Where(c => c.username == userName)
                .FirstOrDefault();
           
            var Revenues = await (
                    from code in _dbContext.used_discount_codes
                    where code.used_at >= targetMonth && code.used_at < targetMonth.AddMonths(1)
                    group code by code.discount_Codes.kol_id into c
                    where c.Key == userID.id
                    select new KOCRevenueDto
                    {
                        TotalOrders = c.Select(c => c.code).Count(),
                        KOCId = c.Key,
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
