using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    public class TifinController : Controller
    {
        // GET: Tifin
        public ActionResult Index()
        {
            List<TifinMaster> TifinList = TifinMaster.List.Values.ToList();

            return View(TifinList);
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult CreateSave(TifinMaster ObjTifin)
        {
            if (ObjTifin.NumberOfTifin <= 0)
            {
                return Json(new { msg = "Number Of Tifin Must Be more than zero" });
            }
            else if (ObjTifin.NumberOfTifin > 20)
            {
                return Json(new { msg = "Number Of Tifin Not Be more than 20" });
            }
            if (ObjTifin.TifinID == 0)
            {
               
                for (int i = 0; i < ObjTifin.NumberOfTifin; i++)
                {
                    
                    TifinMaster TifinObj = new TifinMaster();
                    TifinObj.NumberOfTifin = ObjTifin.NumberOfTifin;
                    TifinObj.TifinColor = ObjTifin.TifinColor;
                    TifinObj.TifinStatus = "UnUsed";
                    TifinObj.TifinAtLocation = "CENTER";
                    TifinObj.TifinTakenBy = Session["UID"].ToString();
                    TifinObj.TifinID = 0;
                    if (TifinObj.Save() > 0) {
                        for (int ii = 0; ii < ObjTifin.TifinType; ii++)
                        {
                            TifinRackMaster ObjRackMaster = new TifinRackMaster();
                            ObjRackMaster.TifinId = TifinObj.TifinID;
                            ObjRackMaster.TifinRackStatus = "UnUsed";
                            ObjRackMaster.Save();
                        }
                }
                }

            }
          return  RedirectToAction("index");
        }
    }
}