namespace FOODDO.Models
{
    public class App_GetOrders
    {
        public string Order_No { get; set; }
        public string Date { get; set; }
        public System.Collections.Generic.List<App_GetOrderItem> ListGetOI { get; set; }
        public string Status { get; set; }
        public string Payable_Ammount { get; set; }

    }
}