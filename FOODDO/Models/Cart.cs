namespace FOODDO.Models
{
    public class Cart
    {
        public System.Int64 CID { get; set; }
        public System.Int64 FID { get; set; }
        public int Count { get; set; }
        public System.Int64 MessID { get; set; }
        public static System.Collections.Generic.List<Cart> List { get; set; }
        

    }
}
