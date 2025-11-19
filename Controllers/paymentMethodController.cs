using System.Security.Claims;
using WebAPI.DTOs.payment_method;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class paymentMethodController
    {
        private readonly dbContext _dbContext;
        public paymentMethodController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // thêm tài khoản ngân hàng
        public async Task<IResult> savePaymentMethod(PaymentMethodCreateDto paymentMethodCreateDto, ClaimsPrincipal claims)
        {
            payment_method_services payment_Method_Services = new payment_method_services(_dbContext);
            var paymentMethod = await payment_Method_Services.savePaymentMethod(paymentMethodCreateDto, claims);
            return paymentMethod;
        }
    }
}
