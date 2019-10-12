using System.Web.Mvc;
using FOODDO.Models;
using System.Collections.Generic;

namespace FOODDO.Controllers
{

    [FOODDO.Web.Attributes.SessionTimeout]
    public class FoodController : Controller
    {
         
        public ActionResult Index()
        {
            List<Food> FoodList = Food.List;
            if(Request.QueryString["Status"] != null)// see details from dashborad today food
            {
                FoodList = FoodList.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date);
            }
            return View(FoodList);
        }

        public ActionResult Create(int FID=0)
        {
            Food ObjFood = new Food();
            if (FID > 0)
                ObjFood=Food.List.Find(x => x.FID == FID);
            return View(ObjFood);
        }

        [HttpPost]
        public ActionResult Create(Food ObjFood, System.Web.HttpPostedFileBase FoodImg)
        {
            string UserType = Session["USERTYPE"].ToString();
            if (UserType != null && UserType == "ADMIN")
            {
                ObjFood.AdminAprovalStatus = 1;
            }
            else
            {
                ObjFood.AdminAprovalStatus = 0;
            }
            if (ObjFood.Save() < 1)
                return Json(new { msg = "Error in Update Mess" });
            else if (FoodImg != null)
                FoodImg.SaveAs(System.IO.Path.Combine(Server.MapPath("~/FoodImages/"), ObjFood.FID + ".jpg"));
            if (ObjFood.Food_Image.Equals(""))
            {
                ObjFood.Food_Image = "/FoodImages/" + ObjFood.FID + ".jpg";
                if (ObjFood.Save() < 1)
                    return Json(new { msg = "Error in Update Food" });
            }
            return RedirectToAction("Index");
        }

        public ActionResult LiveSearch()
        {
            return View();
        }
        public ActionResult UpdateApproval()
        {
            int Fid = int.Parse(Request.QueryString["Fid"]);
            int Status = int.Parse(Request.QueryString["Status"]);
            Food ObjFood = Food.List.Find(x => x.FID == Fid);
            if (ObjFood != null)
            {
                ObjFood.AdminAprovalStatus = Status;
                if (ObjFood.Save() <= 0)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json(new { msg = "Error in Update Food" },JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "Food Not Found" }, JsonRequestBehavior.AllowGet );
            }
           
            return Content("");
        }


    }
}