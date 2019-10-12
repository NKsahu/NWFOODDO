using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fileup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string vTitle = "";
        string vDesc = "";
        string FilePath = Server.MapPath("/UsersImg");

        if (!string.IsNullOrEmpty(Request.Form["title"]))
        {
            vTitle = Request.Form["title"];
        }
        if (!string.IsNullOrEmpty(Request.Form["description"]))
        {
            vDesc = Request.Form["description"];
        }

        HttpFileCollection MyFileCollection = Request.Files;
        if (MyFileCollection.Count > 0)
        {
            // Save the File
            Response.Write(FilePath);

            MyFileCollection[0].SaveAs(FilePath + "/" + vTitle);
        }
    }
}