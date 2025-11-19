using Sprache;
using System.Security.Claims;
using WebAPI.DTOs.payment_method;
using WebAPI.models;

namespace WebAPI.Services
{
    public class payment_method_services
    {
        private readonly dbContext _dbContext;
        public payment_method_services(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // lưu số tài khoản dùng để thanh toán
        public async Task<IResult> savePaymentMethod(PaymentMethodCreateDto paymentMethodCreateDto, ClaimsPrincipal claims)
        {
            try
            {
                var username = claims.Identity.Name;
               
                //_dbContext.Add(new payment_Methods
                //{
                //    method_type = paymentMethodCreateDto.method_type,
                //    details = paymentMethodCreateDto.details,
                //    is_primary = false,
                //    created_at = DateTime.Now
                //});

                //await _dbContext.SaveChangesAsync();
                return Results.Ok(200);
            }catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
