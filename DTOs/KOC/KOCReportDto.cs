namespace WebAPI.DTOs.KOC
{
    public class KOCReportDto
    {
        // Lấy danh sách mã và số lượng sử dụng
        public IEnumerable<KOCCodeUsageDto> ?CodeUsages { get; set; }
        public IEnumerable<KOCRevenueDto> ?Revenues { get; set; }
        public IEnumerable<KOCCommissionDto> ?Commissions { get; set; }
    }
}
