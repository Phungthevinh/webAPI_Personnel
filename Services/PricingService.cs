using WebAPI.models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class PricingService : IPricingService
    {
        private readonly dbContext _dbContext;
        public PricingService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public decimal CalculateFinalAmount(Used_Discount_Codes used_Discount_Codes)
        {
            var discount_value = (from code in _dbContext.discount_codes
                                 where used_Discount_Codes.code == code.code
                                 join campaign in _dbContext.campaigns on code.campaign_id equals campaign.id
                                 select  campaign.discount_value ).FirstOrDefault();

            decimal valueDiscount = (decimal)(discount_value);
            decimal soTienGiam = used_Discount_Codes.order_value * (decimal)(valueDiscount / 100);
            used_Discount_Codes.discount_amount_applied = used_Discount_Codes.order_value - soTienGiam;
            return used_Discount_Codes.discount_amount_applied;
        }
    }

}
