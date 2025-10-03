using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{
    public class Permissions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
