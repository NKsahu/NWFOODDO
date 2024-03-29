﻿using System.Collections.Generic;
using FOODDO.Models.FooddoModels;
using Newtonsoft.Json.Linq;
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
        string AddCart(string CID, string FID, string Cnt, string MessID,string MealType);

        // Get View Cart
        ViewCartItems GetCart(string CID);

        // Post Order
        PostOrderResult PostOrder(string CID, string CSVMessId);

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
        App_Review GetReview(string FID,int CID);
        // Show Hide Rating if Customer not Eat Food
        string ShowHideRating(int Fid, int CustID);
        //Customer Gmail Login
        string GmailLogin_Customer(string Name, string Gmail);

        // Delivery Boy Login
        string DBLogin(string Mobile, string Password);


        //============= START MESS APP CODE HERE==============////
        //1) Common User Login Like D-B ,HubOwner ,Mess user login
        string CommonLogin(string MobileNo, string Password);

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
        List<Routes> HubList();
        List<QRInfo> CusQrInfo(int CustId);
        List<WalletOffer> WalletOffers();
        List<HubWiseTifin> HubWiseTifinList();
        System.Int64 CreateMessFood(JObject food, string imgbytes);
        List<Food> MessFoodList(int MID);
        int HubSignUnsignApproval(string obj);
    }

}