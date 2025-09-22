namespace WebAPI.models
{
    public class Discount_Codes
    {
        private int id;
        private string code;
        private int kol_id;
        private float discount_value;
        private DateTime valid_from;
        private DateTime valid_until;
        private bool is_active;
    }
}
