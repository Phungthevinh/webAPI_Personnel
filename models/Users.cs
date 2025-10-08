

using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI.models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string username { get; set; }
        public string? email { get; set; }
        public string password_hash { get; set; }
        public string full_name { get; set; }
        public bool? is_active { get; set; } = false;
        public DateTime? created_at { get; set; }
        public List<ai_prompts> prompts { get; set; } = new();
        public List<KOL_Profiles> KOL_Profile { get; set; } = new();
        public List<user_roles> user_roles { get; set; } = new();
        public List<discount_codes> discount_Codes { get; set; } = new();

    }
    //đăng nhập
    public class DangNhap
    {
        private string username;
        private string password_hash;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password_hash
        {
            get { return password_hash; }
            set { password_hash = value; }
        }
    }
    public class KOI : Users
    {
        private bool is_KOI;
        public bool IsKOI
        {
            get { return is_KOI; }
        }
    }
    //đăng ký chức vụ hoặc vai trò với người dùng
    public class role_user: Users
    {
        public long role_id { get; set; }
    }
}
