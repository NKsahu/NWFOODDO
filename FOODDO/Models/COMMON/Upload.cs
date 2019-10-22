using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace FOODDO.Models.COMMON
{
    public class Upload
    {
        public static bool UpladImage(byte[] imgbyte, string Fname)
        {
            try
            {
                MemoryStream ms = new MemoryStream(imgbyte);
                FileStream fs = new FileStream(System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FoodImages/") + Fname + "png"), FileMode.Create);
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
        public static bool SaveImgToPng(byte[] imgbytes, string imgname)
        {
            try
            {
                // MemoryStream ms = new MemoryStream(imgbytes);
                Image img =new Upload().byteArrayToImage(imgbytes);///Image.FromStream(ms);
                img.Save(HttpContext.Current.Server.MapPath("~/FoodImages/"+imgname+".jpg"));
              //  System.IO.File.Move("oldfilename", "newfilename");
                return true;
            }
            catch (Exception ex)
            {
                // return the error message if the operation fails
                return false;
            }


        }
        public Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream ms = new MemoryStream(bytesArr, 0, bytesArr.Length);
            ms.Write(bytesArr, 0, bytesArr.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

    }
}