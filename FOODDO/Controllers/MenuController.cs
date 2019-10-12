using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOODDO.Models.COMMON;
namespace FOODDO.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            List<Menu> MenuList = Menu.List.FindAll(x=>x.ParentMenuId==0);
            MenuList = MenuList.OrderBy(x => x.MenuOrderNo).ToList();
            return View(MenuList);

        }
        [HttpGet]
        public ActionResult Create(int Menu_Id=0)
        {
            Menu menu = new Menu();
            if (Menu_Id > 0)
            {
                menu = Menu.List.Find(x => x.Menu_Id == Menu_Id);
                if (menu.MultipleUserType == null || menu.MultipleUserType.Length==0)
                {
                    menu.MultipleUserType = menu.User_Types.Split(',');
                }
            }
            return View(menu);
        }
        [HttpPost]
        public ActionResult Create(Menu ObjMenu)
        {
            if(ObjMenu.MenuDisplayName==null|| ObjMenu.MenuDisplayName==""|| ObjMenu.MenuDisplayName == "0")
            {
                return Json(new {msg="Please Enter Valid Menu Name"});
            }
            string UserTypesstring = string.Join(",", ObjMenu.MultipleUserType);
            ObjMenu.User_Types = UserTypesstring;
            ObjMenu.Save();
            return RedirectToAction("index");
        }

        public ActionResult SubMenuIndex(int Sid)
        {
            List<Menu> MenuList = Menu.List.FindAll(x => x.ParentMenuId == Sid);
            MenuList = MenuList.OrderBy(x => x.MenuOrderNo).ToList();
            return View(MenuList);
        }
    }
}