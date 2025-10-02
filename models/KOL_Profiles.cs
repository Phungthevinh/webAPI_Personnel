using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{
    [Table("kol_profiles")]
    public class KOL_Profiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long user_id { get; set; }
        public string? bio { get; set; }
        public string? social_media_links { get; set; }
        public string? niche { get; set; }
        public int? follower_count { get; set; }
        public decimal? ratting { get; set; }
        public string? phone { get; set; }

        [ForeignKey("user_id")]
        public Users users { get; set; }
    }
}
