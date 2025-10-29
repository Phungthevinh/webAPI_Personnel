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
                                    }).ToListAsync();
            kOCReportDto.CodeUsages = codeUsages;
            return kOCReportDto;
        }
    }
}
