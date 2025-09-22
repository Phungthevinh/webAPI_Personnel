

using System.Reflection.Metadata.Ecma335;

namespace WebAPI.models
{
    public class Users
    {
        private int id;
        private string username;
        private string email;
        private string password_hash;
        private string full_name;
        private bool is_active;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string PasswordHash
        {
            get { return password_hash; }
            set { password_hash = value; }
        }
        public string FullName
        {
            get { return full_name; }
            set { full_name = value; }
        }
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }

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
