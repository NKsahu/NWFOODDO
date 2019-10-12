using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    public class TifinRackController : Controller
    {
        // GET: TifinRack
        public ActionResult Index()
        {
            List<TifinRackMaster> ListOfRack = TifinRackMaster.List.Values.ToList();

            return View(ListOfRack);
        }
    }
}