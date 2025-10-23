namespace WebAPI.DTOs.KOC
{
    public class KOCRevenueDto
    {
        public int TotalOrders { get; set; }         // Tổng số đơn hàng
        public decimal TotalRevenue { get; set; }    // Tổng doanh thu (sau giảm giá)
        public decimal AverageOrderValue { get; set; } // Giá trị trung bình mỗi đơn
        public DateTime ReportPeriodStart { get; set; } // Thời điểm bắt đầu kỳ báo cáo
        public DateTime ReportPeriodEnd { get; set; }   // Thời điểm kết thúc kỳ báo cáo
    }
}
