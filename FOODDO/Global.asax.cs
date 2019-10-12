using System.Web.Mvc;
using System.Web.Routing;
using FOODDO.Models;
using FOODDO.Models.COMMON;

namespace FOODDO
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RamReload();
        }

        public void RamReload()
        {
            //========== Statr Load RAM ===============
            Users.List = new Users().GetAll();
            Mess.List = new Mess().GetAll();
            Food.List = new Food().GetAll();
            Customer.List = new Customer().GetAll();
            Category.List = new Category().GetAll();
            Address.List = new Address().GetAll();
            Cart.List = new System.Collections.Generic.List<Cart>();
            Orders.List = new Orders().GetAll();
            OrderItem.List = new OrderItem().GetAll();
            Ledger.List = new Ledger().GetAll();
            FoodReview.List = new FoodReview().GetAll();
            DeliveryBoy.List = new DeliveryBoy().GetAll();
            TifinMaster.List = new TifinMaster().GetAll();
            TifinRackMaster.List= new TifinRackMaster().GetAll();
            //OrderItemCollection.List = new OrderItemCollection().GetAll();
            Settings.List = new Settings().GetAll();
            Menu.List = new Menu().GetAll();
            //========== End Load RAM ============= 
        }
        void Application_BeginRequest(object sender, System.EventArgs e)
        {
            System.Web.HttpContextBase context = new System.Web.HttpContextWrapper(System.Web.HttpContext.Current);
            RouteData rd = RouteTable.Routes.GetRouteData(context);
            if (rd != null)
            {
                string controllerName = rd.GetRequiredString("controller");
                string actionName = rd.GetRequiredString("action");
                //Response.Write("c:" + controllerName + " a:" + actionName);
                //Response.End();
            }
        }
    }
}
