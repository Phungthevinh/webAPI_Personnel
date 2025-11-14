using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.used_discount_code;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class DiscountUsageChecker : IDiscountCodeUsageRepository
    {
        private readonly dbContext _dbContext;
        public DiscountUsageChecker(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> HasUserUsedCodeAsync(usedDiscountCode udc)
        {
            bool checkCodeUsed = await _dbContext.used_discount_codes
                                 .AnyAsync(u => u.phone == udc.phone && u.code == udc.code);
            return checkCodeUsed;
        }

        public async Task<bool> checkCouponCodeExpirationDate(usedDiscountCode usedDiscountCode)
        {
            bool checkTimeCode = await (from discount_code in _dbContext.discount_codes
                                        join campaign in _dbContext.campaigns on discount_code.campaign_id equals campaign.id
                                        where discount_code.code == usedDiscountCode.code &&
                                        campaign.start_date < DateTimeOffset.UtcNow &&
                                        campaign.end_date > DateTimeOffset.UtcNow
                                        select discount_code).AnyAsync();

            Console.WriteLine(checkTimeCode);
            return checkTimeCode;
        }
    }
}
