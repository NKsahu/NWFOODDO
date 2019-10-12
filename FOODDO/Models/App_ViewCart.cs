namespace FOODDO.Models
{
    public class App_CartItem
    {
        public string MID{ get; set; }
        public string Mess_Name{ get; set; }
        public string FID { get; set; }
        public string Food_Name { get; set; }
        public string Food_Image { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string Count { get; set; }
    }
    public class ViewCartItems
    {
        public string TotalPrice { get; set; }
        public System.Collections.Generic.List<App_CartItem> ListApp_ViewCart{ get; set; }
    }
}