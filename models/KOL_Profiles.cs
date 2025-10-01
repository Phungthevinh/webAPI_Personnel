using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{
    [Table("kol_profiles")]
    public class KOL_Profiles
    {
        [Key]
        public long id { get; set; }
        public long user_id { get; set; }
        private string bio;
        private string social_media_link;
        private string niche;
        private int follower_count;
        private decimal ratting;
        public string phone { get; set; }

        [ForeignKey("user_id")]
        public Users users { get; set; }
    }
}
