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
        public async Task<IResult> commissionForKoc(ClaimsPrincipal claims)
        {
            try
            {
                var targetMonth = new DateTime(2025, 10, 01).ToUniversalTime();
                var nextMonth = targetMonth.AddMonths(1).ToUniversalTime();
                var commissionKoc = from commission in _dbContext.used_discount_codes
                                    where commission.used_at >= targetMonth && commission.used_at < nextMonth
                                    select commission;

                return Results.Ok(commissionKoc);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
