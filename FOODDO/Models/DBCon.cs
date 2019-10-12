namespace FOODDO.Models
{
    public class DBCon
    {
        public System.Data.SqlClient.SqlConnection Con;
        public DBCon()
        {
            Con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
        }
    }
}
namespace FOODDO.Web.Attributes
{
    public class SessionTimeoutAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            System.Web.HttpContext ctx = System.Web.HttpContext.Current;
            if (System.Web.HttpContext.Current.Session["UID"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectResult("~");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}