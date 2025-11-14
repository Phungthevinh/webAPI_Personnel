using WebAPI.DTOs.used_discount_code;
using WebAPI.models;

namespace WebAPI.Services.Interfaces
{
    public interface IDiscountCodeUsageRepository
    {
        //kiểm tra mã đó có người sử dụng hay chưa
        Task<bool> HasUserUsedCodeAsync(usedDiscountCode usedDiscountCode);
        //kiểm tra thời hạn của mã giảm giá
        Task<bool> checkCouponCodeExpirationDate(usedDiscountCode usedDiscountCode);
    }
}
