using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{

    interface set_discount_codes
    {
        void Activate();
        void Deactivate();
    }
    public class discount_codes : set_discount_codes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public bool? is_active { get; set; }
        public long kol_id { get; set; }
        public long campaign_id { get; set; }
        [ForeignKey("kol_id")]
        public Users user { get; set; }
        [ForeignKey("campaign_id")]
        public campaigns campaigns { get; set; }

        public void Activate()
        {
            this.is_active = true;
        }
        public void Deactivate()
        {
            this.is_active = false;
        }
    }
}
