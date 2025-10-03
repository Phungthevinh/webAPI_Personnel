using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.models
{

    public class Roles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id {  get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public DateTime created_at { get; set; }
        public List<user_roles> user_Roles { get; set; }
    }

    
    public class user_roles
    {
        [ForeignKey(nameof(Users))]
        [Column("user_id")]
        public long user_id { get; set; }

        [ForeignKey(nameof(Roles))]
        [Column("role_id")]
        public long role_id { get; set; }

        public Users Users { get; set; }
        public Roles Roles { get; set; }

    }
}
