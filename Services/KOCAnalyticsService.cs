using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.KOC;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class KOCAnalyticsService:IKOCAnalyticsService
    {
        private readonly dbContext _dbContext;
        public KOCAnalyticsService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // lấy ra báo cáo của KOC và chiến dịch
        public async Task<KOCReportDto> GetKOCReportAsync()
        {
            KOCReportDto kOCReportDto = new KOCReportDto();

            var codeUsages = await (from codes in _dbContext.used_discount_codes
                                    group codes by codes.code into c
                                    select new KOCCodeUsageDto
                                    {
                                        code = c.Key,
                                        UsageCount = c.Select(x => x.phone).Distinct().Count(),
                                        TotalOrderValue = c.Sum(x => x.discount_amount_applied)
                                    }).ToArrayAsync();

            var Revenues = await (from codes in _dbContext.used_discount_codes
                                  group codes by codes.discount_Codes.kol_id into c
                                  select new KOCRevenueDto
                                  {
                                      TotalOrders = c.Select(x => x.code).Count(),

                                      TotalRevenue = (
                                        from TotalRevenue in c
                                        select TotalRevenue.discount_amount_applied
                                      ).Sum(),  

                                      AverageOrderValue = (
                                        from TotalRevenue in c
                                        select TotalRevenue.discount_amount_applied
                                      ).Average(),

                                      KOCName = c.Select(x => x.discount_Codes.user.full_name).FirstOrDefault(),

                                      KOCId = c.Select(x => x.discount_Codes.user.id).FirstOrDefault()
                                      
                                  }
                                 ).ToArrayAsync();

            kOCReportDto.CodeUsages = codeUsages;
            kOCReportDto.Revenues = Revenues;
            return kOCReportDto;
        }
    }
}
