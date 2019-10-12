using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FOODDO.Models.FooddoModels;
namespace FOODDO.Models
{
    public class IIFOOD
    {
        public IIFOOD() { }

        string Host = System.Configuration.ConfigurationManager.AppSettings["Host"];

        //===== User Signup ==========
        public string Signup_Customer(string Name, string Birthday, string Email, string Mobile, string Password)
        {
            try
            {
                if (Customer.List.Find(x => x.Mobile.Equals(Mobile)) != null)
                    return "NO";
                Customer ObjCustomer = new Customer()
                {
                    Name = Name,
                    Birthday = System.Convert.ToDateTime(Birthday),
                    Email = Email,
                    Mobile = Mobile,
                    Password = Password
                };
                System.Int64 CID = ObjCustomer.Save();
                if (CID < 1)
                    return "ERROR";
                return "" + CID;
            }
            catch (System.Exception Ex) { return Ex.ToString(); }
        }

        //========== Login Customer ===========
        public App_Customer Login_Customer(string Mobile, string Password)
        {
            Customer ObjCustomer = Customer.List.Find(x => x.Mobile.Equals(Mobile) && x.Password.Equals(Password));
            App_Customer ObjApp_Customer = new App_Customer();
            if (ObjCustomer != null)
            {
                ObjApp_Customer.CID = ObjCustomer.CID.ToString();
                ObjApp_Customer.Name = ObjCustomer.Name;
                ObjApp_Customer.Birthday = ObjCustomer.Birthday.ToString("dd-mm-yyyy");
                ObjApp_Customer.Email = ObjCustomer.Email;
                ObjApp_Customer.Mobile = ObjCustomer.Mobile;
            }
            return ObjApp_Customer;
        }
        //===== Post Address =============

        public string PostAddress(string CID, string Address1 = "", string Landmark = "", string Hub = "", string Type = "", string Latitude = "", string Longitude = "", string Description = "")
        {
            Address ObjAdd = Address.List.Find(x => x.CID == System.Convert.ToInt64(CID) && x.Type.ToLower().Equals(Type.ToLower()));
            try
            {
                if (ObjAdd != null)
                {
                    ObjAdd.Address1 = Address1;
                    ObjAdd.Landmark = Landmark;
                    ObjAdd.Hub = Hub;
                    ObjAdd.Latitude = Latitude;
                    ObjAdd.Longitude = Longitude;
                    ObjAdd.Description = Description;
                    if (ObjAdd.Update() > 0)
                        return "YES";
                }
                else
                {
                    Address ObjCustomer = new Address()
                    {
                        CID = System.Convert.ToInt64(CID),
                        Address1 = Address1,
                        Landmark = Landmark,
                        Hub = Hub,
                        Type = Type,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Description = Description
                    };
                    if (ObjCustomer.Insert() > 0)
                        return "YES";
                }

                return "NO";
            }
            catch (System.Exception ex) { return ex.ToString(); }
        }

        //Get Customer Address
        public IList<App_Address> GetAddress(string CID)
        {
            List<Address> ListAddress = Address.List.FindAll(x => x.CID == System.Convert.ToInt64(CID));
            IList<App_Address> ListAppAddress = new List<App_Address>();
            List<Routes> HubList = new Routes().RouteList();
            foreach (var ObjTmp in ListAddress)
            {
                Routes ObjRoute = HubList.Find(x => x.HubID ==int.Parse(ObjTmp.Hub));
                string HubName = ObjRoute != null ? ObjRoute.HubName : "None";
                App_Address ObjCusAdd = new App_Address()
                {
                    AddressId = ObjTmp.AddressId,
                    Address1 = ObjTmp.Address1,
                    Landmark = ObjTmp.Landmark,
                    HubId = ObjTmp.Hub,
                    HubName = HubName,
                    Type = ObjTmp.Type,
                    Latitude = ObjTmp.Latitude,
                    Longitued = ObjTmp.Longitude,
                    Description = ObjTmp.Description
                };
                ListAppAddress.Add(ObjCusAdd);
            }
            return ListAppAddress;

        }
        // Get Food Category
        public List<App_Category> GetCategory()
        {
            List<App_Category> App_Category = new List<App_Category>();
            IList<Category> IListCategory = Category.List;
            foreach (Category objCate in IListCategory)
            {
                App_Category TmpObj = new App_Category();
                TmpObj.ID = objCate.Category_ID.ToString();
                TmpObj.Name = objCate.Name;
                App_Category.Add(TmpObj);
            }
            return App_Category;
        }

        //// Get Mess of Food
        //public IList<App_Mess> GetMessOfFoodAndCategories(string SearchTerm = "", string CID = "",string FoodType="")
        //{

        //    IList<App_Mess> IAppListMess = new List<App_Mess>();
        //    List<Food> ListFood = Food.List;
        //    if (!SearchTerm.Equals(""))
        //    {
        //        ListFood = ListFood.FindAll(x => x.Food_Name.ToLower().Contains(SearchTerm.ToLower()));
        //    }
        //    else if (!CID.Equals(""))
        //    {
        //        ListFood = ListFood.FindAll(x => x.Category_ID.ToString()==CID);
        //    }
        //    foreach (Food ObjFood in ListFood)
        //    {
        //        Mess ObjMess = Mess.List.Find(x => x.MID == ObjFood.MID);
        //        App_Mess TmpObj = new App_Mess()
        //        {

        //            MID = ObjFood.MID.ToString(),
        //            FID = ObjFood.FID.ToString(),
        //            MessImage = Host + ObjMess.Image,
        //            Mess_Name = ObjMess.Mess_Name,
        //            Item_Name = ObjFood.Food_Name,
        //            MessAddress = ObjMess.Address,
        //            MessType = ObjMess.MessType,
        //            FoodType=ObjFood.FoodType,
        //            FoodImg = Host + ObjFood.Food_Image
        //        };
        //        IAppListMess.Add(TmpObj);
        //    }


        //    return IAppListMess;
        //}

        // Get List Mess Item
        public List<App_MessItemList> GetMessItem(string SearchTerm = "", string CID = "", string FoodType = "",string UID="",string MealsType="")
        {
            List<App_MessItemList> ListMessItemList = new List<App_MessItemList>();

            List<Food> ListFood = Food.List;

            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == System.Convert.ToInt64(UID));
           
        
            if (!SearchTerm.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.Food_Name.ToLower().Contains(SearchTerm.ToLower()));
            }
            else if (!CID.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.Category_ID.ToString() == CID);
            }
           else if (!FoodType.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.FoodType == FoodType);
            }
            else if (!MealsType.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.MealsType == MealsType);
            }
            foreach (Food ObjFood in ListFood)
            {
                Mess ObjMess = Mess.List.Find(x => x.MID == ObjFood.MID);
                App_MessItemList TmpObj = new App_MessItemList();
                //mess details
                TmpObj.MID = ObjMess.MID.ToString();
                TmpObj.MessName = ObjMess.Mess_Name;
                //food details
                TmpObj.FID = ObjFood.FID.ToString();
                TmpObj.ItemImage = Host + ObjFood.Food_Image;
                TmpObj.ItemName = ObjFood.Food_Name;
                TmpObj.ItemQuntity = ObjFood.Qty;
                TmpObj.ItemPrice = ObjFood.Price.ToString();
                TmpObj.ItemDescription = ObjFood.Description;
                TmpObj.Rating = ObjFood.Rating;
                TmpObj.FoodType = ObjFood.FoodType;
                TmpObj.MealsType = ObjFood.MealsType;
                Cart ObjCart = ListCart.Find(x => x.FID == ObjFood.FID);
                if (ObjCart != null)
                {
                    TmpObj.Count = ObjCart.Count.ToString();
               
                }
                else
                    TmpObj.Count = "0";

                ListMessItemList.Add(TmpObj);
            }
            
            return ListMessItemList;
        }

        // Food, Category, Mess Name search
        public IList<App_Search> SearchMessFood(string SearchTerm)
        {
            List<App_Search> ListMessFood = new List<App_Search>();
            // food filter
            List<Food> ListFood = new List<Food>();
            ListFood = Food.List.FindAll(x => x.Food_Name.ToLower().Contains(SearchTerm.ToLower())).GroupBy(y => y.Food_Name).Select(g => g.First()).ToList();
            if (ListFood.Count > 0)
            {
                foreach (Food ObjFood in ListFood)
                {
                    App_Search TmpObj = new App_Search()
                    {
                        FID = ObjFood.FID.ToString(),
                        Name = ObjFood.Food_Name,
                        MID = ObjFood.MID.ToString(),
                    };
                    ListMessFood.Add(TmpObj);
                }
            }
            return ListMessFood;
        }

        // Add item in Cart
        public string AddCart(string CID, string FID, string Cnt,string MessID)
        {
            System.Int64 CusID = System.Convert.ToInt64(CID);
            System.Int64 FoodID = System.Convert.ToInt64(FID);
            System.Int64 Mess_ID = System.Convert.ToInt64(MessID);
            Cart ObjCart = Cart.List.Find(x => x.CID == CusID && x.FID == FoodID);
            if (ObjCart != null)
            {
                ObjCart.Count = System.Convert.ToInt32(Cnt);
                Cart.List.RemoveAll(x => x.CID == CusID && x.FID == FoodID);
                if (ObjCart.Count != 0)
                    Cart.List.Add(ObjCart);
            }
            else
                Cart.List.Add(new Cart() { CID = CusID, FID = FoodID, Count = System.Convert.ToInt32(Cnt),MessID=Mess_ID });

            double Amt = 0;
            int Count = 0;


            foreach (Cart Obj in Cart.List.FindAll(x => x.CID == CusID))
            {
                Food ObjFood = Food.List.Find(x => x.FID == Obj.FID);
                Amt += Obj.Count * ObjFood.Price;
                Count += Obj.Count;
            }

            if (Cnt.Equals("0"))
                return Count + "," + Amt + "," + FID;
            return Count + "," + Amt + "," + "0";
        }

        // Get View Cart
        public ViewCartItems GetCart(string CID)
        {
            ViewCartItems ObjCartItem = new ViewCartItems();
            double TotalPrice = 0;
            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == System.Convert.ToInt64(CID));
            List<App_CartItem> ListApp_ViewCarts = new List<App_CartItem>();
            App_CartItem Objpp_ViewCarts = new App_CartItem();

            foreach (Cart Obj in ListCart)
            {
                Food ObjFood = Food.List.Find(x => x.FID == Obj.FID);
                Mess ObjMess = Mess.List.Find(x => x.MID == ObjFood.MID);
                Objpp_ViewCarts = new App_CartItem();
                Objpp_ViewCarts.MID = ObjMess.MID.ToString();
                Objpp_ViewCarts.Mess_Name = ObjMess.Mess_Name;
                Objpp_ViewCarts.FID = ObjFood.FID.ToString();
                Objpp_ViewCarts.Food_Name = ObjFood.Food_Name;
                Objpp_ViewCarts.Food_Image = Host + ObjFood.Food_Image;
                Objpp_ViewCarts.Price = ObjFood.Price.ToString();
                Objpp_ViewCarts.Qty = ObjFood.Qty;
                Objpp_ViewCarts.Count = Obj.Count.ToString();
                TotalPrice += Obj.Count * ObjFood.Price;
                ListApp_ViewCarts.Add(Objpp_ViewCarts);
            }
            ObjCartItem.TotalPrice = TotalPrice.ToString();
            ObjCartItem.ListApp_ViewCart = ListApp_ViewCarts;
            return ObjCartItem;
        }

        // Post Order
        public string PostOrder(string CID,string CSVMessId ,string Type,int HubId)
        {
            
            System.TimeSpan OpeningTime = new System.TimeSpan(6,0,0);
            System.TimeSpan ClosingTime = new System.TimeSpan(22,0,0);
            Settings OrderOpeningTimeObj = Settings.List.Find(x => x.KeyName == "OrderOpeningTime");
            Settings OrderClosingTimeObj = Settings.List.Find(x => x.KeyName == "OrderClosingTime");
            if (OrderOpeningTimeObj != null)
            {
                string[] ArrayOpeningTime = OrderOpeningTimeObj.KeyValue.Split(',');
                OpeningTime = new System.TimeSpan(int.Parse(ArrayOpeningTime[0]),int.Parse(ArrayOpeningTime[1]),int.Parse(ArrayOpeningTime[2]));
            }
            if (OrderClosingTimeObj != null)
            {
                string[] ArrayClosingTime = OrderClosingTimeObj.KeyValue.Split(',');
                ClosingTime = new System.TimeSpan(int.Parse(ArrayClosingTime[0]), int.Parse(ArrayClosingTime[1]), int.Parse(ArrayClosingTime[2]));
            }

            if (System.DateTime.Now.Date.Add(OpeningTime)>System.DateTime.Now && System.DateTime.Now.Add(ClosingTime) < System.DateTime.Now) { }
            else
            {
                return "Cannot Order Between :" + System.DateTime.Now.Date.Add(OpeningTime).ToString("dd/MM/yyyy hh:mm:ss tt") + "To " + System.DateTime.Now.Add(ClosingTime).ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            
            System.Int64 CusID = System.Convert.ToInt64(CID);
            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == CusID);
            if (ListCart.Count <= 0)
            {
                return "Order Not Complete Because No Item Added";
            }
            //check Balance
            double PayableAmt = 0;
            foreach (Cart Obj in ListCart)
            {
                Food ObjFood = Food.List.Find(x => x.FID == Obj.FID);
                PayableAmt += Obj.Count * ObjFood.Price;
            }
            double Bal = Ledger.List.FindAll(x => x.CID == CusID).Select(y => y.Credit - y.Debit).Sum();
            if (Bal < PayableAmt)
                return "No Bal";

            Orders ObjOrders = new Orders()
            {
                Create_By = CusID,
                Create_Date = System.DateTime.Now,
                CID = CusID,
                MessIDs= CSVMessId,
                Status = "Order-Placed",
                Type= Type,
                HubId=HubId

            };
            System.Int64 NewOID = ObjOrders.Save();

            if (NewOID > 0)
            {
                foreach (Cart Obj in ListCart)
                {
                    Food ObjFood = Food.List.Find(x => x.FID == Obj.FID);
                    OrderItem ObjID = new OrderItem()
                    {
                        FID = Obj.FID,
                        Price = ObjFood.Price,
                        Count = Obj.Count,
                        Qty = ObjFood.Qty,
                        OID = NewOID,
                        MessID= Obj.MessID,
                        Status=0
                        
                    };
                    ObjID.Save();
                }
                Cart.List.RemoveAll(x => x.CID == CusID);

                Ledger ObjLedger = new Ledger()
                {
                    CID = CusID,
                    Debit = PayableAmt,
                    Description = "Deducted For Order NO :" + NewOID,
                    LedgerType="CUSTOMER"
                };
                if (ObjLedger.Save() == 0)
                    return "Order Saved But Balance Not Deduct";

                return "" + NewOID;
            }
            return "Order Not Complete";

        }

        // Get Orders
        public List<App_GetOrders> GetOrder(string CID)
        {
            List<App_GetOrders> List_AppOrder = new List<App_GetOrders>();
            List<Orders> ListOreders = Orders.List.FindAll(x => x.CID == System.Convert.ToInt64(CID));
            System.Int64[] OIDArray = ListOreders.Select(x => x.OID).ToArray();
            List<OrderItem> ListOI = OrderItem.List.FindAll(x => OIDArray.Contains(x.OID));
            double PayAmt = 0;
            foreach (Orders Obj in ListOreders)
            {
                List<App_GetOrderItem> ListGetOI = new List<App_GetOrderItem>();
                App_GetOrders AppObj = new App_GetOrders();
                AppObj.Order_No = Obj.OID.ToString();
                AppObj.Date = Obj.Create_Date.ToString("dd-MM-yyyy");
                AppObj.Status = Obj.Status;
                List<OrderItem> ListTmpOI = ListOI.FindAll(x => x.OID == Obj.OID);
                foreach (OrderItem TmpOI in ListTmpOI)
                {
                    App_GetOrderItem ObjGetOI = new App_GetOrderItem()
                    {
                        Item_Name = Food.List.Where(x => x.FID == TmpOI.FID).Select(y => y.Food_Name).FirstOrDefault(),
                        Price = TmpOI.Price.ToString(),
                        Count = TmpOI.Count.ToString(),
                        Qty = TmpOI.Qty,
                        TotalAmount = (TmpOI.Count * TmpOI.Price).ToString()
                    };
                    ListGetOI.Add(ObjGetOI);
                    PayAmt += TmpOI.Count * TmpOI.Price;
                }
                AppObj.ListGetOI = ListGetOI;
                AppObj.Payable_Ammount = PayAmt.ToString();
                List_AppOrder.Add(AppObj);
            }

            return List_AppOrder;
        }
        // Get Total Cart Item Count only
        public string HaveItemInCart(string CID)
        {
            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == System.Convert.ToInt64(CID));
            int counts = 0;
            foreach (Cart ObjCart in ListCart)
            {
                counts += ObjCart.Count;
            }
            return "" + counts;
        }

        // Change Password

        public string ChangePass(string CID, string OldPass, string NewPass)
        {
            Customer ObjCustomer = Customer.List.Find(x => x.CID == System.Convert.ToInt64(CID) && x.Password.Equals(OldPass));
            if (ObjCustomer != null)
            {
                ObjCustomer.Password = NewPass;
                ObjCustomer.Save();
                return "YES";
            }
            return "NO";
        }

        // ReOrder
        public string ReOrder(string OrderID)
        {
            Orders ObjOrder = Orders.List.Find(x => x.OID == System.Convert.ToInt64(OrderID));
            List<OrderItem> ListOrderItem = OrderItem.List.FindAll(x => x.OID == System.Convert.ToInt64(OrderID));
            foreach (OrderItem Obj in ListOrderItem)
            {
                Cart ObjCart = new Cart()
                {
                    CID = ObjOrder.CID,
                    FID = Obj.FID,
                    Count = Obj.Count
                };
                Cart.List.Add(ObjCart);
            }
            return "YES";
        }

        // My VOLET
        public string CheckBalance(string CID)
        {
            double Bal = Ledger.List.FindAll(x => x.CID == System.Convert.ToInt64(CID)).Select(y => y.Credit - y.Debit).Sum();
            return Bal.ToString();
        }

        // Add balance in VOLET
        public string AddBalance(string CID, string AMT)
        {
            Ledger Obj = new Ledger()
            {
                CID = System.Convert.ToInt64(CID),
                Credit = System.Convert.ToInt32(AMT),
                Description = "Self Credit"
            };
            if (Obj.Save() != 0)
                return "YES";
            return "NO";

        }

        // post Review in food
        public string PostReview(string FID, string CID, string Rating, string comment = "")
        {
            System.Int64 FOODID = System.Convert.ToInt64(FID);
            System.Int64 CusID = System.Convert.ToInt64(CID);

            FoodReview ObjFoodReview = FoodReview.List.Find(x => x.FID == FOODID && x.CID == CusID);
            if (ObjFoodReview != null)
            {
                ObjFoodReview.Rating = System.Convert.ToInt32(Rating);
                ObjFoodReview.Comment = ObjFoodReview.Comment + " " + comment;
                if (ObjFoodReview.Update() == 0)
                    return "Error in Update Revivew";
            }
            else
            {
                ObjFoodReview = new FoodReview()
                {
                    FID = FOODID,
                    CID = CusID,
                    Rating = System.Convert.ToInt32(Rating),
                    Comment = comment
                };
                if (ObjFoodReview.Insert() == 0)
                    return "Error in PostRating";
            }

            // update Rating of food
            int RN1 = FoodReview.List.FindAll(x => x.FID == FOODID && x.Rating == 1).Count();
            int RN2 = FoodReview.List.FindAll(x => x.FID == FOODID && x.Rating == 2).Count();
            int RN3 = FoodReview.List.FindAll(x => x.FID == FOODID && x.Rating == 3).Count();
            int RN4 = FoodReview.List.FindAll(x => x.FID == FOODID && x.Rating == 4).Count();
            int RN5 = FoodReview.List.FindAll(x => x.FID == FOODID && x.Rating == 5).Count();

            Food ObjFood = Food.List.Find(x => x.FID == FOODID);
            int RN1_1 = -RN1;
            int RN2_1 = -2 * (RN2);
            int RN4_1 = RN4;
            int RN5_1 = 2 * RN5;
            ObjFood.Rating = (System.Convert.ToDouble((RN1_1 + RN2_1 + RN4_1 + RN5_1) / (RN1 + RN2 + RN3 + RN4 + RN5))).ToString();
            if (ObjFood.Save() != 0)
                return "YES";
            return "ERROR IN RATING";

        }

        // Item details
        public List<App_Review> GetReview(string FID)
        {
            List<FoodReview> ListFoodReview = FoodReview.List.FindAll(x => x.FID == System.Convert.ToInt64(FID));
            List<App_Review> ListComt = new List<App_Review>();
            foreach (FoodReview Obj in ListFoodReview)
            {
                Customer ObjCus = Customer.List.Find(x => x.CID == Obj.CID);
                FoodReview ObjFoodReview = ListFoodReview.Find(x => x.CID == Obj.CID);
                App_Review ObjComt = new App_Review()
                {
                    Rating = ObjFoodReview.Rating.ToString(),
                    Time = Obj.Date_Time.ToString("dd-MM-yyyy hh:MM tt"),
                    CustomerName = ObjCus.Name,
                    Comment = ObjFoodReview.Comment
                };
                ListComt.Add(ObjComt);
            }
            return ListComt;
        }

        //Customer Gmail Login
        public string GmailLogin_Customer(string Name, string Gmail)
        {
            Customer ObjCus = Customer.List.Find(x => x.Email.ToLower().Equals(Gmail.ToLower()));
            if (ObjCus != null)
                return "" + ObjCus.CID;
            else
            {
                ObjCus = new Customer()
                {
                    Name = Name,
                    Email = Gmail,
                    Mobile = "",
                    Password = "ByGmail"
                };
                System.Int64 NewCID = ObjCus.Save();
                if (NewCID != 0)
                    return NewCID + "";
            }
            return "NO";
        }

        // hub list
        public List<Routes> HubList()
        {
            Routes routes = new Routes();
            return routes.RouteList();
        }

        // Delivery Boy Login
        public string DBLogin(string Mobile, string Password)
        {
            DeliveryBoy ObjDBoy = DeliveryBoy.List.Find(x => x.Mobile.Equals(Mobile) && x.Password.Equals(Password));
            if (ObjDBoy != null)
                return ObjDBoy.DBID + ","+ ObjDBoy.Name;
                return "NO";
        }


        public Mess MessLogin(string UserName, string Password)
        {
            Mess mess = new Mess();
            Users User = Users.List.Find(x => (x.User_Name == UserName) && (x.Password == Password));
            if (User != null)
            {
                Mess ObjMess = Mess.List.Find(x => x.MID == User.MESSID);
                if (ObjMess != null)
                    return ObjMess;
                else
                    return mess;
                
            }
            else
                return mess;
          
          
        }
        // status for filters today,yesterday,completed
        public List<MessOrdersApi> MessOrder(string MessId,string Status)
        {
            int MyMessId = System.Int32.Parse(MessId);
            List<MessOrdersApi> MessOrderList = new List<MessOrdersApi>();
            List<Food> MyMesssFood = Food.List.FindAll(x => x.MID == MyMessId);
            List<OrderItem> orderItems = OrderItem.List.FindAll(x => x.MessID == MyMessId);
            List<Orders> OrderList = Orders.List.FindAll(x=>x.MessIDs.Contains(MessId));
            if (Status == "Today")
            {
                orderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date && x.Status==0);
            }
            else if (Status=="Yesterday")
            {
                orderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date.AddDays(-1) && x.Status == 0);
            }
            else if (Status == "Completed")
            {
                orderItems = orderItems.FindAll(x => x.Status != 0);
            }

                for (int i = 0; i < orderItems.Count; i++)
            {
                MessOrdersApi messOrdersApi = new MessOrdersApi();
                Orders MessOrder = OrderList.Find(x => x.OID == orderItems[i].OID);
                Food MessFood = MyMesssFood.Find(x => x.FID == orderItems[i].FID);
                if (MessOrder != null&& MessFood!=null)
                {
                    messOrdersApi.FoodName = MessFood.Food_Name;
                    messOrdersApi.FoodImg = MessFood.Food_Image;
                    messOrdersApi.TotalItems = orderItems[i].Count;
                    messOrdersApi.Status = orderItems[i].Status;
                    messOrdersApi.Type = MessOrder.Type;
                    messOrdersApi.OrderItemId = orderItems[i].OIID;
                    MessOrderList.Add(messOrdersApi);
                }
            }
            return MessOrderList;

        }
        public string MessOrderCount(string Messid)
        {
            int MyMessId = System.Int32.Parse(Messid);
            string Result = "0,0,0";// today order yesterday order completed order
            List<OrderItem> orderItems = OrderItem.List.FindAll(x => x.MessID == MyMessId);
             int   TodayorderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date && x.Status == 0).Count;
            int  YesterdayorderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date.AddDays(-1) && x.Status == 0).Count;
            int  CompletedorderItems = orderItems.FindAll(x => x.Status != 0).Count;
            Result =TodayorderItems.ToString() + "," + YesterdayorderItems.ToString() + "," + CompletedorderItems.ToString();
            return Result;
        }


      public  string MarkOrderItemPacked(int OrderItemID)
        {
            string staus = "0";
            OrderItem orderItem = OrderItem.List.Find(x => x.OIID==OrderItemID);
            if (orderItem != null)
            {
                orderItem.Status = 1;
                if (orderItem.Save() > 0)
                    return "1";
            }
            return staus;
        }



        // admin login 

        public Users AdminLogin(string UserName, string Password)
        {
            //Users ObjUser = new Users();
            Users User = Users.List.Find(x => (x.User_Name == UserName) && (x.Password == Password));
            if (User != null)
            {
                return User;
            }
            else
            {
                return new Users();
            }
            
        }
        public List<Mess> CollectionFilter()
        {

            List<Mess> TempMessList = Mess.List;
            List<Food> food_list = Food.List;
            for(int i = 0; i < TempMessList.Count; i++)
            {
                TempMessList[i].FoodList = food_list.FindAll(x => x.MID == TempMessList[i].MID);

            }

            return TempMessList;

        }

        public List<OrderItem> ListOfOredItemCollect(int MessId,int FID)
        {
            List<OrderItem> OrderItemList = new List<OrderItem>();
                 OrderItemList = OrderItem.List.FindAll(x => x.MessID == MessId && x.FID == FID);

            foreach(OrderItem objOI in OrderItemList)
            {

            }

            return OrderItemList;
        }
       public CollectOrder  PostCollectionItem(int OIID, int ElementCode)
        {
            CollectOrder CollOrdObj = new CollectOrder();
            CollOrdObj.status = 0;
            CollOrdObj.msg = "Server Error";
            int NumberOfRackInOrderItem = 0;
            int OrderItemID = OIID;
            int ElementRackID = ElementCode;
                TifinRackMaster ObjElementRack = TifinRackMaster.List.Values.ToList().Find(x => x.TifinRackId == ElementRackID);
                OrderItem ObjOrderItem = OrderItem.List.Find(x => x.OIID == OrderItemID);
                if (ObjOrderItem != null)
                {
                    if (ObjElementRack != null && ObjElementRack.TifinRackStatus != ("Used"))
                    {
                        int TotalItems = ObjOrderItem.Count;

                        if (ObjOrderItem.TifinRackIds.Contains(","))
                        {
                        string[] TifinRackIds = Regex.Split(ObjOrderItem.TifinRackIds, ",").Where(x => x != string.Empty).ToArray();
                            NumberOfRackInOrderItem = TifinRackIds.Length;
                        }
                        if (NumberOfRackInOrderItem > TotalItems)
                        {
                        CollOrdObj.status = 0;
                        CollOrdObj.msg = "Element Racks Cannot More Than Total Item in Order Item. Total Rack Element in Item is going to" + NumberOfRackInOrderItem;
                        return CollOrdObj;
                        }
                    ObjOrderItem.Status = 2;// 2 for approved by foodoo admin
                    if (ObjOrderItem.TifinRackIds != null && ObjOrderItem.TifinRackIds != "")
                        {
                            ObjOrderItem.TifinRackIds = ObjOrderItem.TifinRackIds + ElementRackID.ToString() + ",";
                        }
                        else
                        {
                            ObjOrderItem.TifinRackIds = ElementRackID.ToString() + ",";
                        }
                            ObjOrderItem.UpdatedBy = 0;
                        ObjOrderItem.UpdationDate = System.DateTime.Now;
                    if (ObjOrderItem.Save() > 0)
                    {
                        CollOrdObj.status = ObjOrderItem.Status;
                        // split logc here 
                        string[] TifinRackIdsArray =Regex.Split(ObjOrderItem.TifinRackIds, ",").Where(x => x != string.Empty).ToArray();
                        CollOrdObj.msg = string.Join(",", TifinRackIdsArray);
                        //
                        NumberOfRackInOrderItem += 1;// increament number of rack in as per OID count
                        ObjElementRack.TifinRackStatus = "Used";
                        ObjElementRack.Save();
                        List<OrderItem> TotalOrderItemList = OrderItem.List.FindAll(x => x.OID == ObjOrderItem.OID);
                        List<OrderItem> TotalOrderCompleted = TotalOrderItemList.FindAll(x => x.Status == 2);
                        if (TotalOrderCompleted.Count == TotalOrderItemList.Count)
                        {
                            Orders ObjOrder = Orders.List.Find(x => x.OID == ObjOrderItem.OID);
                            if (ObjOrder != null)
                            {
                                ObjOrder.Status = "Order-Collected";
                                if (ObjOrder.Save() <= 0) { }

                            }
                        }
                    }
                    else
                    {
                        CollOrdObj.status = 0;
                        CollOrdObj.msg = "Can't collect Item Server is Too Busy";
                        return CollOrdObj;
                    }
                    }
                    else if (ObjElementRack != null)
                    {
                    CollOrdObj.status = 0;
                    CollOrdObj.msg = "This Element Is Aready Used";
                    }
                    else
                    {
                    CollOrdObj.status = 0;
                    CollOrdObj.msg =" Element Not Found Plese Check Element Code";
                    }

                }
            return CollOrdObj;
        }

        
    }
}