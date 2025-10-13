using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{
    interface set_commission_values
    {
        void set_commission_value(String value);
    }
    public class campaigns 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? discount_type { get; set; }
        public float? discount_value { get; set; }
        public string? commission_type { get; set; }
        public float? commission_value { get; set; }
        public bool status { get; set; } = true;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set ; }
       


    }
}
