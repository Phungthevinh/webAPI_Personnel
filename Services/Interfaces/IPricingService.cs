using WebAPI.models;

namespace WebAPI.Services.Interfaces
{
    public interface IPricingService
    {
        decimal CalculateFinalAmount(Used_Discount_Codes used_Discount_Codes);
    }
}
