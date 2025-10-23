using WebAPI.DTOs.used_discount_code;
using WebAPI.models;

namespace WebAPI.Services.Interfaces
{
    public interface IDiscountCodeUsageRepository
    {
        Task<bool> HasUserUsedCodeAsync(usedDiscountCode usedDiscountCode);
    }
}
