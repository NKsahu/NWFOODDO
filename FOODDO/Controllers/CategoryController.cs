using System.Web.Mvc;
using FOODDO.Models;

namespace FOODDO.Controllers
{
    [FOODDO.Web.Attributes.SessionTimeout]
    public class CategoryController : Controller
    {
         
        public ActionResult Index()
        {
            return View(Category.List);
        }
       
        [HttpPost]
        public ActionResult Create(FormCollection FC)
        {
            Category ObjCategory = new Category();

            if (FC["Name"].Equals(""))
                return Json(new { msg = "Enter Category Name" });
            else if (!FC["Category_ID"].Equals(""))
                ObjCategory = Category.List.Find(x => x.Category_ID == System.Convert.ToInt32(FC["Category_ID"]));
            ObjCategory.Name = FC["Name"];

            if (ObjCategory.Save() < 1)
                return RedirectToAction("Error in Category save");

            return RedirectToAction("Index");

        }




        }
    }
