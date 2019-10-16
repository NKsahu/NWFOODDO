 
namespace FOODDO.Models
{
    public class App_Review
    {
        public double Rating { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public string Time { get; set; }
        public int NoOfOrderCount { get; set; }
        public int NumberOfRatingCount { get; set; }
        public int OwnRating { get; set; }
    }
}