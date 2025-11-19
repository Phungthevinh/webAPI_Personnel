using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebAPI.Controllers;
using WebAPI.DTOs.payment_method;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.routers
{
    public class paymentMethodRouter
    {
        public paymentMethodRouter(WebApplication app)
        {
            app.MapPost("them-thong-tin-tai-khoan-ngan-hang",[Authorize] (PaymentMethodCreateDto paymentMethodCreateDto, dbContext dbContext, ClaimsPrincipal claimsPrincipal) =>
            {
                //Console.WriteLine(claimsPrincipal.Identity.Name);
                paymentMethodController paymentMethodController = new paymentMethodController(dbContext);

                return paymentMethodController.savePaymentMethod(paymentMethodCreateDto, claimsPrincipal);

            });
        }
    }
}
