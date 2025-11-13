using System.Security.Claims;
using WebAPI.DTOs.KOC;
using WebAPI.models;

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
                var commissionKoc = from commission in _dbContext.discount_codes
                                    join used_discount_codes in _dbContext.used_discount_codes on commission.code equals used_discount_codes.code
                                    where used_discount_codes.used_at >= targetMonth && used_discount_codes.used_at < nextMonth
                                    group used_discount_codes by commission.kol_id into g
                                    select new KOCCommissionDto
                                    {
                                        KOCId = g.Key,
                                        TotalRevenue = g.Sum(g => g.order_value),
                                        KOCName = g.Select(g => g.discount_Codes.user.full_name).FirstOrDefault()
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
