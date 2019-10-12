using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItem
        public ActionResult Index(int OrderId)
        {

            return View();
        }
    }
}