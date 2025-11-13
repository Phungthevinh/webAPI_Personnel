using WebAPI.DTOs.KOC;
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
            TotalRevenueDto totalRevenueDto = new TotalRevenueDto();
            return new List<TotalRevenueDto> { totalRevenueDto };
        }
    }
}
