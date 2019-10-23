/*  */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FoodDoApi {
    public class FoodDoApi
    {
        private const string urlString = "http://192.168.1.12/FoodDoApi.ashx";
        private const int bufferSize = 4096;
        private string Load(string contents)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlString);
            req.AllowWriteStreamBuffering = true;
            req.Method = "POST";
            req.Timeout = 60000;
            Stream outStream = req.GetRequestStream();
            StreamWriter outStreamWriter = new StreamWriter(outStream);
            outStreamWriter.Write(contents);
            outStreamWriter.Flush();
            outStream.Close();
            WebResponse res = req.GetResponse();
            Stream httpStream = res.GetResponseStream();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                byte[] buff = new byte[bufferSize];
                int readedBytes = httpStream.Read(buff, 0, buff.Length);
                while (readedBytes > 0)
                {
                    memoryStream.Write(buff, 0, readedBytes);
                    readedBytes = httpStream.Read(buff, 0, buff.Length);
                }
            }
            finally
            {
                if (httpStream != null)
                {
                    httpStream.Close();
                }

                if (memoryStream != null)
                {
                    memoryStream.Close();
                }
            }
            byte[] data = memoryStream.ToArray();
            string result = Encoding.UTF8.GetString(data, 0, data.Length);
            return result;
        }

        public JObject Signup_Customer(string Name, string Birthday, string Email, string Mobile, string Password)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "Signup_Customer";
            p["Name"] = JToken.FromObject(Name);
            p["Birthday"] = JToken.FromObject(Birthday);
            p["Email"] = JToken.FromObject(Email);
            p["Mobile"] = JToken.FromObject(Mobile);
            p["Password"] = JToken.FromObject(Password);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject Login_Customer(string Mobile, string Password)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "Login_Customer";
            p["Mobile"] = JToken.FromObject(Mobile);
            p["Password"] = JToken.FromObject(Password);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject PostAddress(string CID, string Address1, string Landmark, string Hub, string Type, string Latitude, string Longitude, string Description)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "PostAddress";
            p["CID"] = JToken.FromObject(CID);
            p["Address1"] = JToken.FromObject(Address1);
            p["Landmark"] = JToken.FromObject(Landmark);
            p["Hub"] = JToken.FromObject(Hub);
            p["Type"] = JToken.FromObject(Type);
            p["Latitude"] = JToken.FromObject(Latitude);
            p["Longitude"] = JToken.FromObject(Longitude);
            p["Description"] = JToken.FromObject(Description);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetAddress(string CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetAddress";
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetCategory()
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetCategory";
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetMessOfFoodAndCategories(string SearchTerm, string CID, string FoodType)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetMessOfFoodAndCategories";
            p["SearchTerm"] = JToken.FromObject(SearchTerm);
            p["CID"] = JToken.FromObject(CID);
            p["FoodType"] = JToken.FromObject(FoodType);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetMessItem(string SearchTerm, string CID, string FoodType, string UID, string MealsType)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetMessItem";
            p["SearchTerm"] = JToken.FromObject(SearchTerm);
            p["CID"] = JToken.FromObject(CID);
            p["FoodType"] = JToken.FromObject(FoodType);
            p["UID"] = JToken.FromObject(UID);
            p["MealsType"] = JToken.FromObject(MealsType);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject SearchMessFood(string SearchTerm)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "SearchMessFood";
            p["SearchTerm"] = JToken.FromObject(SearchTerm);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject AddCart(string CID, string FID, string Cnt, string MessID, string MealType)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "AddCart";
            p["CID"] = JToken.FromObject(CID);
            p["FID"] = JToken.FromObject(FID);
            p["Cnt"] = JToken.FromObject(Cnt);
            p["MessID"] = JToken.FromObject(MessID);
            p["MealType"] = JToken.FromObject(MealType);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetCart(string CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetCart";
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject PostOrder(string CID, string CSVMessId)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "PostOrder";
            p["CID"] = JToken.FromObject(CID);
            p["CSVMessId"] = JToken.FromObject(CSVMessId);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetOrder(string CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetOrder";
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject HaveItemInCart(string CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "HaveItemInCart";
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject ChangePass(string CID, string OldPass, string NewPass)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "ChangePass";
            p["CID"] = JToken.FromObject(CID);
            p["OldPass"] = JToken.FromObject(OldPass);
            p["NewPass"] = JToken.FromObject(NewPass);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject ReOrder(string OrderID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "ReOrder";
            p["OrderID"] = JToken.FromObject(OrderID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject CheckBalance(string CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "CheckBalance";
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject AddBalance(string CID, string AMT)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "AddBalance";
            p["CID"] = JToken.FromObject(CID);
            p["AMT"] = JToken.FromObject(AMT);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject PostReview(string FID, string CID, string Rating, string comment)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "PostReview";
            p["FID"] = JToken.FromObject(FID);
            p["CID"] = JToken.FromObject(CID);
            p["Rating"] = JToken.FromObject(Rating);
            p["comment"] = JToken.FromObject(comment);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GetReview(string FID, int CID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GetReview";
            p["FID"] = JToken.FromObject(FID);
            p["CID"] = JToken.FromObject(CID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject ShowHideRating(int Fid, int CustID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "ShowHideRating";
            p["Fid"] = JToken.FromObject(Fid);
            p["CustID"] = JToken.FromObject(CustID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject GmailLogin_Customer(string Name, string Gmail)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "GmailLogin_Customer";
            p["Name"] = JToken.FromObject(Name);
            p["Gmail"] = JToken.FromObject(Gmail);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject DBLogin(string Mobile, string Password)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "DBLogin";
            p["Mobile"] = JToken.FromObject(Mobile);
            p["Password"] = JToken.FromObject(Password);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject CommonLogin(string MobileNo, string Password)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "CommonLogin";
            p["MobileNo"] = JToken.FromObject(MobileNo);
            p["Password"] = JToken.FromObject(Password);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject MessOrder(string MessId, string Status)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "MessOrder";
            p["MessId"] = JToken.FromObject(MessId);
            p["Status"] = JToken.FromObject(Status);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject MessOrderCount(string Messid)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "MessOrderCount";
            p["Messid"] = JToken.FromObject(Messid);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject MarkOrderItemPacked(int OrderItemID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "MarkOrderItemPacked";
            p["OrderItemID"] = JToken.FromObject(OrderItemID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject AdminLogin(string UserName, string Password)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "AdminLogin";
            p["UserName"] = JToken.FromObject(UserName);
            p["Password"] = JToken.FromObject(Password);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject CollectionFilter()
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "CollectionFilter";
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject ListOfOredItemCollect(int MessId, int FID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "ListOfOredItemCollect";
            p["MessId"] = JToken.FromObject(MessId);
            p["FID"] = JToken.FromObject(FID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject HubList()
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "HubList";
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject CusQrInfo(int CustId)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "CusQrInfo";
            p["CustId"] = JToken.FromObject(CustId);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject WalletOffers()
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "WalletOffers";
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject HubWiseTifinList()
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "HubWiseTifinList";
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject CreateMessFood(object[] food, string imgbytes)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "CreateMessFood";
            p["food"] = JToken.FromObject(food);
            p["imgbytes"] = JToken.FromObject(imgbytes);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

        public JObject MessFoodList(int MID)
        {
            JObject result = null;
            JObject o = new JObject();
            JObject p = new JObject();
            o["interface"] = "FoodDoApi";
            o["method"] = "MessFoodList";
            p["MID"] = JToken.FromObject(MID);
            o["parameters"] = p;
            string s = JsonConvert.SerializeObject(o);
            string r = Load(s);
            result = JObject.Parse(r);
            return result;
        }

    }


}
