using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {

            Routes ObjRoute = new Routes();
            List<Routes> ListROutes = ObjRoute.RouteList();
            return View(ListROutes);
        }
        public ActionResult CreateRoute(int RouteID)
        {
            Routes ObjRoute = new Routes();
            if (RouteID > 0)
            {
                List<Routes> ListROutes = ObjRoute.RouteList();
                ObjRoute = ListROutes.Find(x => x.HubID == RouteID);
            }
            
            return View(ObjRoute);
        }
        [HttpPost]
        public ActionResult CreateRoutePost(Routes ObjRoute)
        {
            if(ObjRoute.HubID>0)
            {
                ObjRoute.Update();
            }
            else
            {
                ObjRoute.Insert();
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult LiveSearch()
        {
            return View();
        }
        
    }
}