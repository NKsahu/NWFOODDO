using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FOODDO.Models.COMMON
{
    public class Upload
    {
public static bool UpladImage(byte[] imgbyte,string Fname)
        {
            try
            {
                MemoryStream ms = new MemoryStream(imgbyte);

                FileStream fs = new FileStream(System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FoodImages/")) + Fname, FileMode.Create);
                ms.WriteTo(fs);
                ms.Close();
                fs.Close();
                fs.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                // return the error message if the operation fails
                return false;
            }
           
        }
    }
}