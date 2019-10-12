using System.Web.Mvc; 
namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View(FOODDO.Models.Customer.List);
        }
        public ActionResult Create(System.Int64 CID=0)
        {
            FOODDO.Models.Customer Obj = new FOODDO.Models.Customer();
            if (CID > 0)
                Obj = FOODDO.Models.Customer.List.Find(x => x.CID == CID);
            return View(Obj);

        }
        [HttpPost]
        public ActionResult Create(FOODDO.Models.Customer Model)
        {
            if (Model.Save() == 0)
                return Json(new { msg = "Server Busy from Customer Work" });
            return RedirectToAction("Index");
        }

        public ActionResult LiveSearch()
        {

            return View();
        }
    }
}