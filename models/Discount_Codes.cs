namespace WebAPI.models
{

    interface set_discount_codes
    {
        void Activate();
        void Deactivate();
    }
    public class Discount_Codes : set_discount_codes
    {
        private int id;
        private string code;
        private int kol_id;
        private float discount_value;
        private DateTime valid_from;
        private DateTime valid_until;
        private bool is_active;

        public void Activate()
        {
            is_active = true;
        }
        public void Deactivate()
        {
            is_active = false;
        }
        public bool Is_active
        {
            get { return is_active;}
        }
        public int Kol_id
        {
            get { return kol_id; }
            set { kol_id = value; }
        }
        public float Discount_value
        {
            get { return discount_value; }
            set
            {
                if (value < 0) Console.WriteLine("vui lòng nhập giá trị lớn hơn 0");
                if (value > 49) Console.WriteLine("giá trị phải nhỏ hơn 50%");
                else discount_value = value;
            }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
