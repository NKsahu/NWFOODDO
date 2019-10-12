using System.Web.Mvc;
 
namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class DeliveryBoyController : Controller
    {
        public ActionResult Index()
        {
            return View(FOODDO.Models.DeliveryBoy.List);
        }
        public ActionResult Create(int DBID=0)
        {
            FOODDO.Models.DeliveryBoy ObjDB = new FOODDO.Models.DeliveryBoy();
            if (DBID > 0)
                ObjDB = FOODDO.Models.DeliveryBoy.List.Find(x => x.DBID == DBID);
            return View(ObjDB);
        }
        [HttpPost]
        public ActionResult Create(FOODDO.Models.DeliveryBoy Model)
        {
            if (Model.Save() != 0)
                return RedirectToAction("Index");
            return Json(new { msg = "Server Busy for Create Delivery Boy" });
        }

        public ActionResult LiveSearch()
        {
            return View();
        }
    }
}