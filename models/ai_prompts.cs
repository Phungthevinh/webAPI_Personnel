using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{
    [Table("ai_prompts")]
    public class ai_prompts
    {
        public long id { get; set; }
        public string? prompt_name { get; set; }
        public string? prompt_text { get; set; }
        public string? category { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public long created_by_user_id { get; set; }

        [ForeignKey("created_by_user_id")]
        public Users users { get; set; }
    }

    public class chinh_sua_promts
    {
        public long id { get; set; }
        public string prompt_name { get; set; }
        public string prompt_text { get; set; }
        public string category { get; set; }
    }
}
