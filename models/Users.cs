

using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI.models
{
    public class Users
    {
        public long id { get; set; }
        public string username { get; set; }
        public string? email { get; set; }
        public string password_hash { get; set; }
        public string full_name { get; set; }
        public bool? is_active { get; set; } = false;
        public List<ai_prompts> prompts { get; set; } = new();
        public List<KOL_Profiles> KOL_Profile { get; set; } = new();

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
}
