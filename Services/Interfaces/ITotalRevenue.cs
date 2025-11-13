using WebAPI.DTOs.KOC;

namespace WebAPI.Services.Interfaces
{
    // doah thu mà koc đó đã đem về
    public interface ITotalRevenue
    {
        //lấy doanh thu mà koc đem về trong 1 tháng
        public List<TotalRevenueDto> GetTotalRevenueByMonth(DateTime dateTime);
    }
}   
