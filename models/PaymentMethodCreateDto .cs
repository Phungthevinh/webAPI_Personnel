using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace WebAPI.models
{
    public class payment_Methods
    {
        public long id { get; set; }
        public long employee_id { get; set; }
        public string method_type { get; set; }
        public JsonDocument details { get; set; }
        public bool is_primary { get; set; }
        public DateTime created_at { get; set; } 

        //khóa ngoại
        
        public Users Users { get; set; }
    }
}
