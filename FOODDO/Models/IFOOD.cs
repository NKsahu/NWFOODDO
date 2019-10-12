using System.Collections.Generic;
using FOODDO.Models.FooddoModels;
namespace FOODDO.Models
{
    public interface IFOOD
    {
        //Users registration
         string Signup_Customer(string Name, string Birthday, string Email, string Mobile, string Password);

        //User login
        App_Customer Login_Customer(string Mobile, string Password);

        // Post Address 
        string PostAddress(string CID, string Address1="", string Landmark="", string Hub="", string Type="" ,string Latitude="",string Longitude="",string Description="");
        // Get Customer Address
        IList<App_Address> GetAddress(string CID);

        // Get List Food Category
        List<App_Category> GetCategory();


        // Get List of Food and Categories
        IList<App_Mess> GetMessOfFoodAndCategories(string SearchTerm = "", string CID = "",string FoodType="");


        //Get List of Mess with all item.
        List<App_MessItemList> GetMessItem(string SearchTerm = "", string CID = "", string FoodType = "", string UID = "" ,string MealsType="");

        // Food, Category, Mess Name search
        IList<App_Search> SearchMessFood(string SearchTerm);

        // Add item in Cart
        string AddCart(string CID, string FID, string Cnt, string MessID);

        // Get View Cart
        ViewCartItems GetCart(string CID);

        // Post Order
        string PostOrder(string CID, string CSVMessId, string Type,int HubId);

        // Get Orders
        List<App_GetOrders> GetOrder(string CID);

        // Get Total Cart Item Count only
        string HaveItemInCart(string CID);

        // Change Password
        string ChangePass(string CID, string OldPass, string NewPass);

        // ReOrder
        string ReOrder(string OrderID);

        // My VOLET
        string CheckBalance(string CID);

        // Add balance in VOLET
        string AddBalance(string CID,string AMT);

        // post Review in food
        string PostReview(string FID, string CID, string Rating, string comment="");

        // Item details
        List<App_Review> GetReview(string FID);

        //Customer Gmail Login
        string GmailLogin_Customer(string Name, string Gmail);

        // Delivery Boy Login
        string DBLogin(string Mobile, string Password);


        //============= START MESS APP CODE HERE==============////
        //1) mess user login
        Mess MessLogin(string UserName, string Password);

        //2) mess order item Description=========

        List<MessOrdersApi> MessOrder(string MessId, string Status);

        //3) mess order item count
        string MessOrderCount(string Messid);

        //4 OrderItemStatus as packed status 1
        string MarkOrderItemPacked(int OrderItemID);



        //========admin login =======

        Users AdminLogin(string UserName, string Password);

        List<Mess> CollectionFilter();
        List<OrderItem> ListOfOredItemCollect(int MessId, int FID);

       CollectOrder PostCollectionItem(int OIID, int ElementCode);
        List<Routes> HubList();
    }

}