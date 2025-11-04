namespace WebAPI.DTOs.KOC
{
    public class KOCCommissionDto:KOCDto
    {
        public decimal TotalRevenue { get; set; }     // Tổng doanh thu làm cơ sở tính hoa hồng
        public decimal CommissionRate { get; set; }   // % chiết khấu (VD: 0.10 = 10%)
        public decimal CommissionAmount { get; set; } // Số tiền thực nhận
    }
}
