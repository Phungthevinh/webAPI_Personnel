namespace WebAPI.models
{
    public class KOL_Profiles : Users
    {
        private int id;
        private int user_id;
        private string bio;
        private string social_media_link;
        private string niche;
        private int follower_count;
        private decimal ratting;
        private string phone;

        public int User_id
        {
            get { return user_id; }
            set
            {
                try
                {
                    if (value < 0)
                    {
                        Console.WriteLine("vui lòng nhập lớn hơn 0");
                    }
                    else if (value.GetType() == typeof(string))
                    {
                        Console.WriteLine("value cần phải là số");
                    }
                    else if (value == null)
                    {
                        Console.WriteLine("vui lòng nhập vào trường này");
                    }
                    else
                    {
                        user_id = value;
                    }

                }
                catch(AggregateException ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
        }

        public string Phone
        {
            get { return phone; }
            set { 
                if (value.GetType() == typeof(int)) 
                 {
                    Console.WriteLine("vui lòng nhập là chuỗi"); 
                 } else if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("vui lòng nhập vào và không bỏ trống");
                }
                else { phone = value; }
            }
        }
    }
}
