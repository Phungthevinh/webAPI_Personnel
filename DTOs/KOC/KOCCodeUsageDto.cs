namespace WebAPI.DTOs.KOC
{
    public class KOCCodeUsageDto
    {
        public string ?code { get; set; } //mã giảm giá
        public int UsageCount { get; set; } //số lần sử dụng
        public decimal TotalOrderValue { get; set; } //tổng giá trị được tạo ra từ mã này

    }
}
