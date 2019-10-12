using FOODDO.Models;
using System.Linq;
using System.Web.Mvc;

namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class OrderController : Controller
    {
        // GET: Order
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {

            System.Collections.Generic.List<Orders> ListOrders = Orders.List;//.FindAll(x => x.CID == System.Convert.ToInt64(Session["UID"]));
            if (Request.QueryString["Status"] != null&& Request.QueryString["Status"]=="1")
            {
                ListOrders = ListOrders.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date);
            }
            else if (Request.QueryString["Status"] != null && Request.QueryString["Status"] == "2")
            {
                ListOrders = ListOrders.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date.AddDays(-1));
            }

            if (Request.QueryString["Today"] != null && Request.QueryString["Today"] == "1")
            {
                ListOrders = ListOrders.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date);
            }
            if (Request.QueryString["Yesterday"] != null && Request.QueryString["Yesterday"] == "1")
            {
                ListOrders = ListOrders.FindAll(x => x.Create_Date.Date == System.DateTime.Now.Date.AddDays(-1));
            }



            // order list filter 
            if(Request.QueryString["Filter"]!=null&& bool.Parse(Request.QueryString["Filter"]) == true)
            {

           // data: { CID: CID, MessId: MessId,Ordertype: Ordertype},
           if(Request.QueryString["CID"]!=null&& Request.QueryString["CID"]!=""&& Request.QueryString["CID"] != "0")
                {
                    ListOrders = ListOrders.FindAll(x => x.CID == int.Parse(Request.QueryString["CID"]));
                }
                if (Request.QueryString["MessId"] != null && Request.QueryString["MessId"] != "" && Request.QueryString["MessId"] != "0")
                {
                    ListOrders = ListOrders.FindAll(x => x.MessIDs.Contains(Request.QueryString["MessId"]));
                }
                if (Request.QueryString["Ordertype"] != null && Request.QueryString["Ordertype"] != "" && Request.QueryString["Ordertype"] != "0")
                {
                    ListOrders = ListOrders.FindAll(x => x.Type==Request.QueryString["Ordertype"]);
                }

            }


            //ViewBag.OrderIDArr = ListOrders.Select(x => x.OID).ToArray();
            return View(ListOrders);
        } 
        public ActionResult CustomerReport()
        {
            return View("~/Views/Customer/CustomerWiseOrder.cshtml");
        }

        public ActionResult TotalOrderListFilter()
        {
            return View();
        }
        public ActionResult HubWiseOrderStatusReportFilter()
        {

            return View();
        }
    }
}