using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models;
namespace FOODDO.Controllers
{
    public class OrderItemCollectionController : Controller
    {
        [FOODDO.Web.Attributes.SessionTimeout]
        // GET: OrderItemCollection
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostCollectionItem()
        {
            int NumberOfRackInOrderItem = 0;
            string ReturnNumberOfElementInOID = "Server Busy,";// defaul one item count;for returning result to ajax
                int OrderItemID = int.Parse(Request.QueryString["OrderItemId"]);
                int ElementRackID = int.Parse(Request.QueryString["ElementCode"]);
                TifinRackMaster ObjElementRack = TifinRackMaster.List.Values.ToList().Find(x => x.TifinRackId == ElementRackID);
                OrderItem ObjOrderItem = OrderItem.List.Find(x => x.OIID == OrderItemID);
                if (ObjOrderItem != null)
                {
                    if (ObjElementRack != null&& ObjElementRack.TifinRackStatus!=("Used"))
                    {
                        int TotalItems = ObjOrderItem.Count;
                        
                        if (ObjOrderItem.TifinRackIds.Contains(","))
                        {
                            string[] TifinRackIds = ObjOrderItem.TifinRackIds.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            NumberOfRackInOrderItem = TifinRackIds.Length;
                        }
                        if (NumberOfRackInOrderItem > TotalItems)
                        {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                            return Json(new { msg = "Element Racks Cannot More Than Total Item in Order Item. Total Rack Element in Item is going to"+ NumberOfRackInOrderItem }, JsonRequestBehavior.AllowGet);
                        }
                        ObjOrderItem.Status = 2;// 2 for approved by foodoo admin
                        if(ObjOrderItem.TifinRackIds != null && ObjOrderItem.TifinRackIds != "")
                        {
                            ObjOrderItem.TifinRackIds = ObjOrderItem.TifinRackIds + ElementRackID.ToString()+",";
                        }
                        else
                        {
                            ObjOrderItem.TifinRackIds= ElementRackID.ToString()+",";
                        }
                        if (Session["UID"] == null)
                        {
                            ObjOrderItem.UpdatedBy = 0;
                        }
                        else
                        {
                            ObjOrderItem.UpdatedBy = int.Parse(Session["UID"].ToString());
                            ObjOrderItem.ItemCollectBy= int.Parse(Session["UID"].ToString());
                        }
                        ObjOrderItem.UpdationDate = System.DateTime.Now;
                        if ( ObjOrderItem.Save() > 0) {
                            ReturnNumberOfElementInOID = ObjOrderItem.TifinRackIds;
                            NumberOfRackInOrderItem += 1;// increament number of rack in as per OID count
                            ObjElementRack.TifinRackStatus ="Used";
                            ObjElementRack.Save();
                            List<OrderItem> TotalOrderItemList = OrderItem.List.FindAll(x => x.OID == ObjOrderItem.OID);
                            List<OrderItem> TotalOrderCompleted = TotalOrderItemList.FindAll(x => x.Status == 2);
                            if(TotalOrderCompleted.Count== TotalOrderItemList.Count)
                            {
                                Orders ObjOrder = Orders.List.Find(x => x.OID == ObjOrderItem.OID);
                                if (ObjOrder != null)
                                {
                                    ObjOrder.Status = "Order-Collected";
                                    if (ObjOrder.Save() <= 0) { }
                                   
                                }
                            }
                        }
                        else
                        {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                            return Json(new { msg = "Can't collect Item Server is Too Busy"}, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else if (ObjElementRack != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Json(new { msg = "This Element Is Aready Used" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Json(new{ msg = "Element Not Found Plese Check Element Code" },JsonRequestBehavior.AllowGet);
                    }
                        
                }
                   
            
            return Content(ReturnNumberOfElementInOID);
        }

        public ActionResult AssembleIndex()
        {
            string Status = Request.QueryString["Status"];

             if(Status != null && Status == "2")
            {
                Orders ObjOrder = new Orders();
                List<Orders> orderlist = Orders.List;
                Customer ObjCustomer = new Customer();
                int TifinRackId =int.Parse(Request.QueryString["TifinRackid"]);
                List<TifinRackMaster> ListTifinRack = TifinRackMaster.List.Values.ToList();
                TifinRackMaster ObjRack = ListTifinRack.Find(x => x.TifinRackId == TifinRackId);
                if (ObjRack != null)
                {
                    OrderItem orderItem = OrderItem.List.Find(x => x.TifinRackIds.Contains(ObjRack.TifinRackId.ToString()));
                    if (orderItem != null)
                    {
                        ViewData["OrderItemObj"] = orderItem;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Json(new { msg = "Please Collect Item First " }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json(new { msg = " Tifin Rack Code Not Exist in Our Record" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult PostDataAssemble()
        {
            
                int OrderId = int.Parse(Request.QueryString["OrderId"].ToString());
                int TifinCode = int.Parse(Request.QueryString["TifinCode"].ToString());
                int OrderItemId = int.Parse(Request.QueryString["OrderItemId"]);
                List<Orders> ListOfOrder = Orders.List;
                List<TifinMaster> ListOfTifin = TifinMaster.List.Values.ToList();
                OrderItem OrderItemObj = OrderItem.List.Find(x => x.OIID == OrderItemId);
                TifinMaster ObjTifin = ListOfTifin.Find(x => x.TifinID == TifinCode);
                Orders order = ListOfOrder.Find(x => x.OID == OrderId);
                Orders AlreadyTifinUsedInOtherOrder = ListOfOrder.Find(x => x.TifinIds.Contains(TifinCode.ToString()) && x.OID != order.OID &&( x.Status!= "Order-Completed"));
                if (ObjTifin != null&&order!=null)
                {
                    if (AlreadyTifinUsedInOtherOrder != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                        return Json(new { msg = "Tifin Number Already Used In Order No" + AlreadyTifinUsedInOtherOrder.OID.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    if (OrderItemObj != null)
                    {
                        OrderItemObj.TifinID = TifinCode;
                        OrderItemObj.ItemAssembleBy = int.Parse(Session["UID"].ToString());
                        if (OrderItemObj.Save() <= 0) {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                            return Json(new { msg = "Unable To Save Order " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (ObjTifin.TifinStatus.Contains("UnUsed")|| ! order.TifinIds.Contains(TifinCode.ToString()))
                    {
                        if(order.TifinIds!=null&& order.TifinIds != "")
                        {
                            order.TifinIds= TifinCode.ToString() + ",";
                        }
                        else
                        {
                            order.TifinIds = order.TifinIds+TifinCode.ToString() + ",";
                        }
                        if (order.Save() > 0)
                        {
                            List<OrderItem> TotalOrderItemList = OrderItem.List.FindAll(x => x.OID == OrderItemObj.OID);
                            List<OrderItem> TotalOrderCompleted = TotalOrderItemList.FindAll(x => x.Status ==2 && x.TifinID>0);// order  collected and tifins alloted in all OIID RackElements
                            if (TotalOrderCompleted.Count == TotalOrderItemList.Count)
                            {
                                Orders ObjOrder = Orders.List.Find(x => x.OID == OrderItemObj.OID);
                                if (ObjOrder != null)
                                {
                                    ObjOrder.Status = "Order-Assemble";
                                    if (ObjOrder.Save() <= 0) {
                                        Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                                        return Json(new { msg = "Server Busy For Set Status As Assemble" }, JsonRequestBehavior.AllowGet);
                                    }

                                }
                            }
                            if (ObjTifin.TifinStatus.Contains("UnUsed")|| ObjTifin.TifinAtLocation!="CENTER")
                            {
                                ObjTifin.TifinStatus ="Used";
                                ObjTifin.TifinAtLocation = "CENTER";
                                ObjTifin.TifinTakenBy = Session["UID"].ToString();
                                ObjTifin.Save();
                            }
                        }
                    }

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Json(new { msg ="Tifin Number or Order number is invalid please scan valid tifin code"}, JsonRequestBehavior.AllowGet);
                }
           
            

            


            return Content("");
        }

        public ActionResult AlloteTifin()// allote tifin to mess
        {
            return View();
        }
        [HttpGet]
        public ActionResult AlloteTifinToMess()
        {
            string TifinCode = Request.QueryString["TifinCode"];
            string MessId = Request.QueryString["MID"];

            Mess ObjMess = Mess.List.Find(x => x.MID == int.Parse(MessId));
            if (ObjMess == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "Please Select Mess " }, JsonRequestBehavior.AllowGet);
            }
            TifinMaster ObjTifin = TifinMaster.List.Values.ToList().Find(x => x.TifinID == int.Parse(TifinCode));
            if (ObjTifin == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "Invalid Tifin Code " }, JsonRequestBehavior.AllowGet);
            }
            // MAX TIFIN CONDITION TO MESS
            int TotolTifinAtMess = TifinMaster.List.Values.ToList().FindAll(x => x.TifinTakenBy == MessId).Count;
            if (TotolTifinAtMess>=ObjMess.NumberOfTifin)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "Total Tifin Limit Reached For Mess. Limit Is "+ObjMess.NumberOfTifin }, JsonRequestBehavior.AllowGet);
            }

            if (ObjTifin.TifinAtLocation != "CENTER")
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "This Tifin Already Used By " + ObjTifin.TifinAtLocation }, JsonRequestBehavior.AllowGet);
            }

            //change tifin status
            ObjTifin.TifinAtLocation = "MESS";
            ObjTifin.TifinTakenBy = ObjMess.MID.ToString();
            if (ObjTifin.Save() == 0)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new { msg = "Unable To Allote Tifin" }, JsonRequestBehavior.AllowGet);
            }
            return Content("Alloted");
        }
    }
}