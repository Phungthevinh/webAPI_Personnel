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
    }
}
