using System.Text.Json;

namespace WebAPI.DTOs.payment_method
{
    public class PaymentMethodCreateDto
    {
        public JsonDocument details { get; set; }
        public string method_type { get; set; }
    }
}
