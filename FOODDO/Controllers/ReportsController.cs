using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOODDO.Controllers
{
    public class ReportsController : Controller
    {
        // GET: AccountSection
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult DailyReport()
        {

            return View();
        }
    }
}