using System.Security.Claims;
using WebAPI.DTOs.KOC;

namespace WebAPI.Services
{
    public class commissionService
    {
        private readonly dbContext _dbContext;
        public commissionService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // hoa hồng của KOC theo từng tháng
        public async Task<IResult> commissionForKoc(ClaimsPrincipal claims, DateTime dateTime)
        {
            try
            {
                var targetMonth = dateTime.ToUniversalTime();
                var nextMonth = targetMonth.AddMonths(1).ToUniversalTime();
                var commissionKoc = from commission in _dbContext.used_discount_codes
                                    where commission.used_at >= targetMonth && commission.used_at < nextMonth
                                    group commission by commission.code into g
                                    select new KOCCommissionDto
                                    {
                                        
                                        TotalRevenue = g.Sum(g => g.order_value),
                                      
                                    };

                return Results.Ok(commissionKoc);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }
    }
}
