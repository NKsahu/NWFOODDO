using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class MessController : Controller
    {
         
        public ActionResult Index()
        {
            return View(Mess.List);
        }
        public ActionResult Create(int MID=0)
        {
            Mess ObjMess = new Mess();
            if (MID != 0)
                ObjMess = Mess.List.Find(x => x.MID == MID);
            return View(ObjMess);
        }
        [HttpPost]
        public ActionResult Create(Mess MessModel, System.Web.HttpPostedFileBase MessImg)
        {
           
            if (MessModel.Mobile.Length != 10)
                return Json(new { msg = "Enter Correct Mobile Number" });
            else if (MessModel.Aadhar_No.Length !=12)
                return Json(new { msg = "Enter Correct Aadhar Number" });
            else if (MessModel.MID < 1)
                MessModel.Create_By = System.Convert.ToInt32(Session["UID"]);
            else if(MessModel.MID > 0)
                MessModel.Update_By = System.Convert.ToInt32(Session["UID"]);
            if (MessModel.Save()<1)
                return Json(new { msg = "Server busy for Save Mess" });
            
            if (MessImg != null)
                MessImg.SaveAs(System.IO.Path.Combine(Server.MapPath("~/MessImages/"), MessModel.MID + ".jpg"));
            if(MessModel.Image.Equals(""))
            {
                MessModel.Image = "/MessImages/" + MessModel.MID + ".jpg";
                if (MessModel.Save() < 1)
                    return Json(new { msg = "Server busy for Update Mess" });
            }
            return RedirectToAction("Index");
        }
         
        public ActionResult  LiveSearch()
        {

          return  View();
        }
    }
}