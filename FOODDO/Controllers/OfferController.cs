using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models.COMMON;

namespace FOODDO.Controllers
{
    public class OfferController : Controller
    {
       
        public ActionResult Index()
        {
            List<Offers> OfferList = Offers.List.OrderBy(x=>x.FromAmount).ToList();
            return View(OfferList);
        }

        public ActionResult Create( int ID)
        {
            Offers Obj = new Offers();
            if (ID > 0)
            {
                Obj = Offers.List.Find(x => x.TitileId == ID);
            }
            return View(Obj);
        }
        [HttpPost]
        public ActionResult CreatePost(Offers offers)
        {
            offers.DeletedStatus = false;
            if (offers.Title==null || offers.Title.Replace(" ", "").Equals(""))
            {
                return Json(new {msg="Please Enter Title First"});
            }
            if(offers.Discription==null|| offers.Discription.Replace(" ", "").Equals(""))
            {
                return Json(new { msg = "Please Enter Discriptions " });
            }
            if (offers.Save() <= 0)
            {
                return Json(new { msg = "Can't Save Offer.. " });
            }

            return RedirectToAction("index");
        }
        
        public ActionResult DeletedOffer(int Id)
        {
            Offers offers = Offers.List.Find(x => x.TitileId == Id);
            if(Session["UID"]==null)
                return Json(new { msg = "Plese Login First " });
            offers.DeletedStatus = true;
            offers.Save();
            return RedirectToAction("index");
        }


    }
}