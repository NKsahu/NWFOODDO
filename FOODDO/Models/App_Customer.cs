namespace FOODDO.Models
{
    public class App_Customer
    {
        public App_Customer()
        {
            this.CID = "0";
        }
        public string CID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}