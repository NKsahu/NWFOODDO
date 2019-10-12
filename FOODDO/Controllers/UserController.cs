using System.Collections.Generic;
using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    public class UserController : Controller
    {
         
        public ActionResult Login(){
            return View(new Users());
        }

        [HttpPost]
        public ActionResult Login(Users ObjUser)
        {
            Users ModelUser = Users.List.Find(x => x.User_Name.Equals(ObjUser.User_Name) && x.Password.Equals(ObjUser.Password));
            if (ModelUser != null)
            {
                Session["UID"] = ModelUser.UID;
                Session["USERNAME"] = ModelUser.User_Name;
                Session["USERTYPE"] = ModelUser.User_Type;
                Session.Timeout = 120;
                return Json(new { url = "/User/Admin" });
            }
            return Json(new { msg = "Invalid Credential" });
        }

        // logout
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        public ActionResult Admin()
        {
            if (Session["UID"] == null)
                return RedirectToAction("Login");
            return View();
        }

        [FOODDO.Web.Attributes.SessionTimeout]
        public ActionResult Index()
        {
            return View(Users.List);
        }

        [FOODDO.Web.Attributes.SessionTimeout]
        public ActionResult Create(System.Int64 UID =0)
        {
            SelectMess();
            Users ObjUser = new Users();
            if (UID != 0)
                ObjUser = Users.List.Find(x => x.UID == UID);
            return View(ObjUser);
        }
                                    
        [HttpPost]
        public ActionResult Post(Users ObjUser)
        {
            if(Users.List.Find(x => x.User_Name.Equals(ObjUser.User_Name) && ObjUser.UID==0) != null)
                return Json(new { msg = ObjUser.User_Name+" User Name Already have" });
            if(ObjUser.User_Type== "MESS-ADMIN" && ObjUser.MESSID==0)
            {
                return Json(new { msg = "Please Select Mess Name First" });
            }

            if (ObjUser.Save() < 1)
                    return Json(new { msg = "Servr is busy for Save User" });                
            return RedirectToAction("Index");
        }

        // ====================== TestAPi Section ==========================
        public string TestApi()
        {

            // new IIFOOD().MessOrder("1", "Today");
            //  new IIFOOD().AddCart("1","1","2","0");
            //  new IIFOOD().AddCart("1","2","3","0");
            return new IIFOOD().PostAddress("1", "hh", "m", "2", "Lunch", "0", "0", "0");
            //return new IIFOOD().PostReview("1","1","5","cmt");
            //return new IIFOOD().PostReview("1","1","5","cmt");
            //FoodDoApi Obj = new FoodDoApi();
            // Newtonsoft.Json.Linq.JObject JObj= new FoodDoApi().PostOrder("1");
            //var v=Newtonsoft.Json.JsonConvert.SerializeObject(new IIFOOD().MessLogin("harishmess", "12345"));
            // return Content(v);
        }
        // ====================== End TestAPi Section ======================
        public string Reload()
        {
            MvcApplication ObjMA = new MvcApplication();
            ObjMA.RamReload();
            return "YES";
        }
        public void SelectMess()
        {
            
            ViewBag.Mess = new SelectList(Mess.List, "MID", "Mess_Name");


        }
        public ActionResult CallViewWithViewName(string VN)// this function is use for pass paramenter of view name to load view
        {
            return View(VN);
        }
        public ActionResult Account()
        {
            if (Session["UID"] == null)
                return RedirectToAction("Login");
            return View();
        }
    }
}