using FOODDO.Models;
using System.Web.Mvc;

namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class LedgerController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filter()
        {
            System.Collections.Generic.List<Ledger> ListLedger = Ledger.List.FindAll(x => x.CID == System.Convert.ToInt64(Request.QueryString["LID"]));
                System.DateTime FD = System.Convert.ToDateTime(Request.QueryString["FD"]);
                System.DateTime TD = System.Convert.ToDateTime(Request.QueryString["TD"]);
                if (FD > TD)
                    return Json(new { msg = "To Date Should be greater than From Date" });
                ListLedger = ListLedger.FindAll(x => x.Entry_Date.Date >= FD && x.Entry_Date.Date <= TD);
           
            return View(ListLedger);
        }


        public ActionResult ShortInfo(FormCollection FC)
        {
            //System.Collections.Generic.List<Ledger> ListLedger = Ledger.List.FindAll(x => x.CID == System.Convert.ToInt64(FC["LID"]));
            //System.DateTime FD = System.Convert.ToDateTime(FC["FD"]);
            //System.DateTime TD = System.Convert.ToDateTime(FC["TD"]);
            //if (FD > TD)
            //    return Json(new { msg = "To Date Should be greater than From Date" });
            //ListLedger = ListLedger.FindAll(x => x.Entry_Date.Date >= FD && x.Entry_Date.Date <= TD);
            
            return View();
        }
    }
}