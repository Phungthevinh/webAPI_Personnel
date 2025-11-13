namespace WebAPI.DTOs.KOC
{
    //doanh thu đem về của KOC
    public class TotalRevenueDto:KOCDto
    {
        public decimal discount_amount_applied { get; set; } // tính tổng doanh thu mà KOC đó mang về sau khi khấu trừ giảm giá
        public decimal order_value { get; set; } // tổng doanh thu nà koc đó mang về khi chưa khấu trừ giảm giá
        public int total_order { get; set; } //tổng số lượng đơn hàng mà koc đó mang về
        public int month { get; set; } // tháng hiện tại của doanh thu
    }
}
