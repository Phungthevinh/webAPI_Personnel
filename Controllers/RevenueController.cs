using Sprache;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class RevenueController
    {
        private readonly dbContext _dbContext;
        public RevenueController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /**
         * tính toán doanh thu 1 tháng 1 KOC mang về cho tổng đơn hàng
        **/
        public async Task<IResult> TotalRevenue(DateTime dateTime)
        {
            try
            {
                TotalRevenueServices totalRevenueServices = new TotalRevenueServices(_dbContext);

                return Results.Ok(totalRevenueServices.GetTotalRevenueByMonth(dateTime));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }


    }
}
