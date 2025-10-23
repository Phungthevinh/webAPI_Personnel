using WebAPI.models;

namespace WebAPI.Services.Interfaces
{
    public interface IDiscountCodeUsageRepository
    {
        Task<bool> HasUserUsedCodeAsync(Used_Discount_Codes used_Discount_codes);
    }
}
