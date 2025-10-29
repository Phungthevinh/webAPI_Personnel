using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class KOCAnalyticsController
    {
        private readonly dbContext _dbContext;
        public KOCAnalyticsController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //báo cáo tổng hợp phân tích doanh số của KOC và chiến dịch
        public async Task<IResult> GetKOCReport()
        {
            try
            {
                KOCAnalyticsService kOCAnalyticsService = new KOCAnalyticsService(_dbContext);

                var KOCReportAsync = await kOCAnalyticsService.GetKOCReportAsync();
      
                return Results.Ok(KOCReportAsync);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
