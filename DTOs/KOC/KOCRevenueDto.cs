namespace WebAPI.DTOs.KOC
{
    public class KOCRevenueDto : KOCDto
    {
        public int TotalOrders { get; set; }         // Tổng số đơn hàng
        public decimal TotalRevenue { get; set; }    // Tổng doanh thu (sau giảm giá)
        public decimal AverageOrderValue { get; set; } // Giá trị trung bình mỗi đơn
    }
}
