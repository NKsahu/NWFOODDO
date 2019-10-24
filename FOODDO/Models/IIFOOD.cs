using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FOODDO.Models.FooddoModels;
using Newtonsoft.Json.Linq;
using FOODDO.Models.COMMON;
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
                Routes ObjRoute = HubList.Find(x => x.HubID == int.Parse(ObjTmp.Hub));
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

        // Get List Mess Item
        public List<App_MessItemList> GetMessItem(string SearchTerm = "", string CID = "", string FoodType = "", string UID = "", string MealsType = "")
        {
            List<App_MessItemList> ListMessItemList = new List<App_MessItemList>();

            List<Food> ListFood = Food.List;

            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == System.Convert.ToInt64(UID));


            if (!SearchTerm.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.Food_Name.ToLower().Contains(SearchTerm.ToLower()));
            }
            if (!CID.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.Category_ID.ToString() == CID);
            }
            if (!FoodType.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.FoodType.Contains(FoodType));
            }
            if (!MealsType.Equals(""))
            {
                ListFood = ListFood.FindAll(x => x.MealsType.Contains(MealsType));
            }
            List<FoodReview> FoodReviewList = FoodReview.List.FindAll(x => x.CID == System.Int64.Parse(UID));
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
                TmpObj.MealsType = MealsType;
                Cart ObjCart = ListCart.Find(x => x.FID == ObjFood.FID && x.MealType == MealsType);
                if (ObjCart != null)
                {
                    TmpObj.Count = ObjCart.Count.ToString();

                }
                else
                    TmpObj.Count = "0";

                FoodReview FoodNoteInfoObj = FoodReviewList.Find(x => x.FID == ObjFood.FID);
                if (FoodNoteInfoObj != null)
                {
                    TmpObj.NoteInfo = FoodNoteInfoObj.Comment;
                }
                else
                {
                    TmpObj.NoteInfo = "";
                }
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
        public string AddCart(string CID, string FID, string Cnt, string MessID, string MealType = "")
        {
            System.Int64 CusID = System.Convert.ToInt64(CID);
            System.Int64 FoodID = System.Convert.ToInt64(FID);
            System.Int64 Mess_ID = System.Convert.ToInt64(MessID);
            Cart ObjCart = Cart.List.Find(x => x.CID == CusID && x.FID == FoodID && x.MealType == MealType);
            if (ObjCart != null)
            {
                ObjCart.Count = System.Convert.ToInt32(Cnt);
                Cart.List.RemoveAll(x => x.CID == CusID && x.FID == FoodID && x.MealType == MealType);
                if (ObjCart.Count != 0)
                    Cart.List.Add(ObjCart);
            }
            else
                Cart.List.Add(new Cart() { CID = CusID, FID = FoodID, Count = System.Convert.ToInt32(Cnt), MessID = Mess_ID, MealType = MealType });

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
                Objpp_ViewCarts.MealsType = Obj.MealType;
                TotalPrice += Obj.Count * ObjFood.Price;

                ListApp_ViewCarts.Add(Objpp_ViewCarts);
            }
            ObjCartItem.TotalPrice = TotalPrice.ToString();
            ObjCartItem.ListApp_ViewCart = ListApp_ViewCarts;
            return ObjCartItem;
        }

        // Post Order
        public PostOrderResult PostOrder(string CID, string CSVMessId)
        {
            PostOrderResult ReturnResult = new PostOrderResult();
            ReturnResult.Status = 0;
            System.Int64 CusID = System.Convert.ToInt64(CID);
            List<Address> CustomerAddressList = Address.List.FindAll(x => x.CID == CusID);
            Customer ObjCustomer = Customer.List.Find(x => x.CID == CusID);
            if (ObjCustomer == null)
            {
                ReturnResult.Msg = "Please Login Again Your Id Not Found";
                return ReturnResult;
            }
            System.TimeSpan OpeningTime = new System.TimeSpan(6, 0, 0);
            System.TimeSpan ClosingTime = new System.TimeSpan(22, 0, 0);
            Settings OrderOpeningTimeObj = Settings.List.Find(x => x.KeyName == "OrderOpeningTime");
            Settings OrderClosingTimeObj = Settings.List.Find(x => x.KeyName == "OrderClosingTime");
            if (OrderOpeningTimeObj != null)
            {
                string[] ArrayOpeningTime = OrderOpeningTimeObj.KeyValue.Split(',');
                OpeningTime = new System.TimeSpan(int.Parse(ArrayOpeningTime[0]), int.Parse(ArrayOpeningTime[1]), int.Parse(ArrayOpeningTime[2]));
            }
            if (OrderClosingTimeObj != null)
            {
                string[] ArrayClosingTime = OrderClosingTimeObj.KeyValue.Split(',');
                ClosingTime = new System.TimeSpan(int.Parse(ArrayClosingTime[0]), int.Parse(ArrayClosingTime[1]), int.Parse(ArrayClosingTime[2]));
            }
            System.DateTime OpeningDT = System.DateTime.Now.Date.Add(OpeningTime);
            System.DateTime ClosingDT = System.DateTime.Parse(System.DateTime.Now.Date.Add(ClosingTime).ToString("dd/MM/yyyy hh:mm:ss tt"));
            if (System.DateTime.Now > OpeningDT && ClosingDT > System.DateTime.Now) { }
            else
            {
                ReturnResult.Msg = "Cannot Order Before :" + System.DateTime.Now.Date.Add(OpeningTime).ToString("hh:mm tt") + " & After " + System.DateTime.Now.Date.Add(ClosingTime).ToString("hh:mm tt");
                return ReturnResult;
            }


            List<Cart> ListCart = Cart.List.FindAll(x => x.CID == CusID);
            if (ListCart.Count <= 0)
            {
                ReturnResult.Status = 101;
                ReturnResult.Msg = "Order Not Complete Because No Item Added";
                return ReturnResult;
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
            {
                ReturnResult.Status = 102;
                ReturnResult.Msg = "No Balance In Wallet";
                return ReturnResult;
            }


            IEnumerable<IGrouping<string, Cart>> CartItemByMealType = ListCart.GroupBy(x => x.MealType).ToList();
            string[] MealTypes = CartItemByMealType.Select(x => x.Key).ToArray();

            // check Address Type For Lunch Dinner and Breakfast
            foreach (string Key in MealTypes)
            {
                Address CustAddress = CustomerAddressList.Find(x => x.Type == Key);
                if (CustAddress == null)
                {
                    ReturnResult.Status = 103;
                    ReturnResult.Msg = Key;
                    return ReturnResult;
                }
            }
            string OrderIds = "";
            foreach (var MealTypeList in CartItemByMealType)
            {
                string MealTypeKey = MealTypeList.Key;
                Orders ObjOrders = new Orders()
                {
                    Create_By = CusID,
                    Create_Date = System.DateTime.Now,
                    CID = CusID,
                    MessIDs = CSVMessId,
                    Status = "1",
                    Type = MealTypeKey,
                    HubId = int.Parse(CustomerAddressList.Find(x => x.Type == MealTypeKey).Hub)

                };
                System.Int64 NewOID = ObjOrders.Save();
                if (NewOID > 0)
                {
                    ReturnResult.Status = 1;
                    OrderIds += NewOID.ToString() + ",";
                    foreach (Cart Obj in MealTypeList.ToList())
                    {
                        Food ObjFood = Food.List.Find(x => x.FID == Obj.FID);
                        OrderItem ObjID = new OrderItem()
                        {
                            FID = Obj.FID,
                            Price = ObjFood.Price,
                            Count = Obj.Count,
                            Qty = ObjFood.Qty,
                            OID = NewOID,
                            MessID = Obj.MessID,
                            Status = 0

                        };
                        if (ObjID.Save() <= 0)
                        {
                            Orders order = new Orders();
                            order.DeleteOrderAndOrderItem(NewOID);
                            ReturnResult.Status = 0;
                            ReturnResult.Msg = "Unable To Place Order-Items at this time";
                            return ReturnResult;
                        }
                    }
                }
                else
                {
                    ReturnResult.Msg = "Unable To Place Order at this time";
                    return ReturnResult;
                }

            }
            var OrderidsArray = Regex.Split(OrderIds, ",").Where(x => x != string.Empty).ToArray();
            OrderIds = string.Join(",", OrderidsArray);
            ReturnResult.Msg = OrderIds;
            Cart.List.RemoveAll(x => x.CID == CusID);
            Ledger ObjLedger = new Ledger()
            {
                CID = CusID,
                Debit = PayableAmt,
                Description = OrderIds,
                LedgerType = "1"// 1 FOR CUSTOMER TYPE LEDGER;
            };
            if (ObjLedger.Save() == 0)
            {
                foreach (string OID in OrderidsArray)
                {
                    Orders order = new Orders();
                    order.DeleteOrderAndOrderItem(System.Int64.Parse(OID));
                }
                ReturnResult.Msg = "Unable To Deduct balance For Order";

            }
            return ReturnResult;
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
            double FoodRating = (RN1 + RN2 + RN3 + RN4 + RN5) / 5;
            ObjFood.Rating = FoodRating.ToString("0.00");
            //int RN1_1 = -RN1;
            //int RN2_1 = -2 * (RN2);
            //int RN4_1 = RN4;
            //int RN5_1 = 2 * RN5;
            /// ObjFood.Rating = (System.Convert.ToDouble((RN1_1 + RN2_1 + RN4_1 + RN5_1) / (RN1 + RN2 + RN3 + RN4 + RN5))).ToString();
            if (ObjFood.Save() != 0)
                return "YES";
            return "ERROR IN RATING";

        }

        // Show Hide Rating if Customer not Eat Food
        public string ShowHideRating(int Fid, int CustID)
        {
            List<Orders> OrderList = Orders.List.FindAll(x => x.CID == CustID && x.Status == "Order-Completed");
            HashSet<System.Int64> OID = new HashSet<System.Int64>(OrderList.Select(x => x.OID).ToArray());
            OrderItem Obj = OrderItem.List.Find(x => OID.Contains(x.OID) && x.FID == Fid);
            if (Obj != null) return "YES";
            else
                return "NO";
        }
        // Item details
        public App_Review GetReview(string FID, int CID)
        {
            List<FoodReview> ListFoodReview = FoodReview.List.FindAll(x => x.FID == System.Convert.ToInt64(FID));
            FoodReview OwnRatingObj = ListFoodReview.Find(x => x.CID == CID);
            App_Review App_Review = new App_Review();
            App_Review.NumberOfRatingCount = ListFoodReview.Count;
            App_Review.Rating = ListFoodReview.Sum(x => x.Rating) / 5;
            App_Review.OwnRating = (OwnRatingObj != null) ? OwnRatingObj.Rating : 0;
            App_Review.NoOfOrderCount = OrderItem.List.FindAll(x => x.FID == System.Convert.ToInt64(FID)).Count;
            return App_Review;
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
                return ObjDBoy.DBID + "," + ObjDBoy.Name;
            return "NO";
        }


        public string CommonLogin(string MobileNo, string Password)
        {
            string result = "";
            Mess ObjMess = Mess.List.Find(x => x.Mobile == MobileNo && x.Password == Password);
            if (ObjMess != null)
            {
                result = "MESS," + ObjMess.Mess_Name + "," + ObjMess.MID;
                return result;
            }
            DeliveryBoy ObjDB = DeliveryBoy.List.Find(x => x.Mobile == MobileNo && x.Password == Password);
            if (ObjDB != null)
            {
                result = "DELIVER-BOY," + ObjDB.Name + "," + ObjDB.DBID;
                return result;
            }

            Routes ObjHub = new Routes().RouteList().Find(x => x.MobileNo == MobileNo && x.Password == Password);
            if (ObjHub != null)
            {
                result = "HUBOWNER," + ObjHub.HubName + "," + ObjHub.HubID;
                return result;
            }
            return result;
        }
        // status for filters today,yesterday,completed
        public List<MessOrdersApi> MessOrder(string MessId, string Status)
        {
            int MyMessId = System.Int32.Parse(MessId);
            List<MessOrdersApi> MessOrderList = new List<MessOrdersApi>();
            List<Food> MyMesssFood = Food.List.FindAll(x => x.MID == MyMessId);
            List<OrderItem> orderItems = OrderItem.List.FindAll(x => x.MessID == MyMessId);
            List<Orders> OrderList = Orders.List.FindAll(x => x.MessIDs.Contains(MessId));
            if (Status == "Today")
            {
                orderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date && x.Status == 0);
            }
            else if (Status == "Yesterday")
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
                if (MessOrder != null && MessFood != null)
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
            int TodayorderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date && x.Status == 0).Count;
            int YesterdayorderItems = orderItems.FindAll(x => x.OrderDate.Date == System.DateTime.Now.Date.AddDays(-1) && x.Status == 0).Count;
            int CompletedorderItems = orderItems.FindAll(x => x.Status != 0).Count;
            Result = TodayorderItems.ToString() + "," + YesterdayorderItems.ToString() + "," + CompletedorderItems.ToString();
            return Result;
        }


        public string MarkOrderItemPacked(int OrderItemID)
        {
            string staus = "0";
            OrderItem orderItem = OrderItem.List.Find(x => x.OIID == OrderItemID);
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
            for (int i = 0; i < TempMessList.Count; i++)
            {
                TempMessList[i].FoodList = food_list.FindAll(x => x.MID == TempMessList[i].MID);

            }

            return TempMessList;

        }

        public List<OrderItem> ListOfOredItemCollect(int MessId, int FID)
        {
            List<OrderItem> OrderItemList = new List<OrderItem>();
            OrderItemList = OrderItem.List.FindAll(x => x.MessID == MessId && x.FID == FID);
            return OrderItemList;
        }

        public List<QRInfo> CusQrInfo(int CustId)
        {
            List<Routes> HubList = new Routes().RouteList();
            List<QRInfo> CusQrInfoTemp = new List<QRInfo>();
            List<Orders> CustOrderList = Orders.List.FindAll(x => x.CID == CustId);
            for (int i = 0; i < CustOrderList.Count; i++)
            {
                QRInfo TempObj = new QRInfo();
                TempObj.CustID = CustOrderList[i].CID;
                TempObj.OrderID = CustOrderList[i].OID;
                var tifinno = Regex.Split(CustOrderList[i].TifinIds, ",").Where(x => x != string.Empty).ToArray();
                TempObj.TifinCodes = string.Join(",", tifinno);
                TempObj.OrderType = CustOrderList[i].Type;
                TempObj.HubName = "";
                Routes ObjHub = HubList.Find(x => x.HubID == CustOrderList[i].HubId);
                if (ObjHub != null)
                {
                    TempObj.HubName = ObjHub.HubName + "( " + ObjHub.MobileNo.ToString() + " )";
                }
                CusQrInfoTemp.Add(TempObj);
            }
            return CusQrInfoTemp;
        }
        public List<WalletOffer> WalletOffers()
        {
            List<WalletOffer> walletOffersList = new List<WalletOffer>();
            //List<Offers> WithouCashBack=Offers.List.fin
            WalletOffer CashBackOffer = new WalletOffer();
            CashBackOffer.OfferTitle = "CASHBACK";
            CashBackOffer.Discription = "";
            List<WalletOffer.CashBacktemList> cashbackList = new List<WalletOffer.CashBacktemList>();
            foreach (var offerobj in COMMON.Offers.List)
            {
                if (offerobj.Title.Equals("cashback", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    WalletOffer.CashBacktemList cashback = new WalletOffer.CashBacktemList();
                    cashback.FromAmt = offerobj.FromAmount;
                    cashback.ToAmt = offerobj.ToAmount;
                    cashback.BonusAmt = offerobj.Bonus;
                    cashbackList.Add(cashback);
                }
                else
                {
                    WalletOffer walletOffer = new WalletOffer();
                    walletOffer.OfferTitle = offerobj.Title;
                    walletOffer.Discription = offerobj.Discription;
                    walletOffer.CashBackList = null;
                    walletOffersList.Add(walletOffer);
                }
            }
            CashBackOffer.CashBackList = cashbackList.OrderBy(X => X.FromAmt).ToList();
            walletOffersList.Add(CashBackOffer);
            return walletOffersList;
        }

        public List<HubWiseTifin> HubWiseTifinList()
        {
            List<Routes> HubList = new Routes().RouteList();
            List<HubWiseTifin> hubwiselist = new List<HubWiseTifin>();
            List<Orders> YesterDayOrderList = Orders.List.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date).ToList();
            IEnumerable<IGrouping<int, Orders>> OrdersGroupByHub = YesterDayOrderList.GroupBy(x => x.HubId);
            foreach (var OrderList in OrdersGroupByHub)
            {
                List<HubWiseTifin.HubTifins> tifins = new List<HubWiseTifin.HubTifins>();
                int TotalTifin = 0;
                foreach (Orders order in OrderList.ToList())
                {
                    HubWiseTifin.HubTifins hubTifins = new HubWiseTifin.HubTifins();
                    hubTifins.OID = order.OID;
                    var TifinsArray = Regex.Split(order.TifinIds, ",").Where(x => x != string.Empty).ToArray();
                    hubTifins.TifinNo = string.Join(",", TifinsArray);
                    TotalTifin += TifinsArray.Length;
                    tifins.Add(hubTifins);
                }
                Routes ObjHub = HubList.Find(x => x.HubID == OrderList.Key);
                if (ObjHub != null)
                {
                    HubWiseTifin hubWiseTifin = new HubWiseTifin();
                    hubWiseTifin.HubId = ObjHub.HubID;
                    hubWiseTifin.HubCode = ObjHub.HubCode;
                    hubWiseTifin.HubName = ObjHub.HubName;
                    hubWiseTifin.MobileNo = ObjHub.MobileNo;
                    hubWiseTifin.TifinCounts = TotalTifin;
                    hubWiseTifin.TifinList = tifins;
                    hubwiselist.Add(hubWiseTifin);
                }
            }
            hubwiselist = hubwiselist.OrderBy(x => x.HubCode).ToList();
            return hubwiselist;
        }

        public System.Int64 CreateMessFood(JObject food, string imgbytes)
        {
            string Sfood = Newtonsoft.Json.JsonConvert.SerializeObject(food);
            Food foodObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Food>(Sfood);
            Food FoodExist = Food.List.Find(x => x.Food_Name.ToUpper().Equals(foodObj.Food_Name.ToUpper()) && x.MID == foodObj.MID && x.FID != foodObj.FID);
            if (FoodExist != null) return 1;// food name already exist
            Settings ObjSetting = Settings.List.Find(x => x.KeyName.ToUpper().Equals("MARGINPRICE"));
            foodObj.Price = foodObj.CostPrice + (double.Parse(ObjSetting.KeyValue) / 100) * foodObj.CostPrice;
            if (foodObj.Save() > 0)
            {
                if (imgbytes != null && imgbytes != "")
                {
                    foodObj.Food_Image = "/FoodImages/" + foodObj.FID + ".jpg";
                    foodObj.Save();///UPDATE IMAGE
                    byte[] bytes = System.Convert.FromBase64String(imgbytes);
                    if (Upload.SaveImgToPng(bytes, foodObj.Food_Image) == false) return 3; // error in uplad img try again
                }
            }
            else
            {
                return 2;// error in save food
            }
            return foodObj.FID;
        }

        public List<Food> MessFoodList(int MID)
        {
            return Food.List.FindAll(x => x.MID == MID);
        }
        public int HubSignUnsignApproval( string obj)
        {
            TifinSubmiteHub TifinSubmithub = Newtonsoft.Json.JsonConvert.DeserializeObject<TifinSubmiteHub>(obj);
            if (TifinSubmithub.OtpNumber <= 0 && (TifinSubmithub.Img!=null|| TifinSubmithub.Img!=""))
            {
                byte[] img = System.Convert.FromBase64String(TifinSubmithub.Img);
                TifinSubmithub.Img = "";
                if (TifinSubmithub.Save() > 0)
                {
                    string path= "/TifinInfoImg/" + TifinSubmithub.TifinInfoId+".jpg";
                    if (Upload.SaveImgToPng(img, path) == false)
                    {
                        return 3; // error in uplad img try again
                    }
                }
                else
                {
                    return= 2;
                }
            }
            else
            {
                int OtpCode = TifinSubmithub.OtpNumber;
                // otp verification code here;

            }
            return 1;
        }

    }
}