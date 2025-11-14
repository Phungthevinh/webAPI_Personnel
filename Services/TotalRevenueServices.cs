using WebAPI.DTOs.KOC;
using WebAPI.DTOs.used_discount_code;
using WebAPI.models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class TotalRevenueServices:ITotalRevenue
    {
        private readonly dbContext _dbContext;
        public TotalRevenueServices(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TotalRevenueDto> GetTotalRevenueByMonth(DateTime dateTime)
        {
            var totalRevenue = from discount_code in _dbContext.discount_codes
                               join used_discount_code in _dbContext.used_discount_codes on discount_code.code equals used_discount_code.code
                               where used_discount_code.used_at >= dateTime.ToUniversalTime() && used_discount_code.used_at < dateTime.AddMonths(1).ToUniversalTime()
                               group used_discount_code by discount_code.kol_id into g
                               select new TotalRevenueDto
                               {
                                   discount_amount_applied = g.Sum(g => g.discount_amount_applied),
                                   order_value = g.Sum(g => g.order_value),
                                   total_order = g.Count(),
                                   dateTimeFirstDayMonth = dateTime.ToUniversalTime(),
                                   dateTimeLastDayMonth = dateTime.AddMonths(1).ToUniversalTime(),
                                   KOCId = g.Select(g => g.discount_Codes.kol_id).FirstOrDefault(),
                                   KOCName = g.Select(g => g.discount_Codes.user.full_name).FirstOrDefault()
                               };
            var
            
            return totalRevenue.ToList();
        }
    }
}
