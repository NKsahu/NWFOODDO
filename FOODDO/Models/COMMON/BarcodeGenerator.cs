using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;
using System.Net;
//using System.Windows.Media;
using System.Drawing;
using System.IO;


namespace FOODDO.Models.COMMON
{
    public static class BarcodeGenerator
    {
        public  static string  Generate(string Text)
        {
            //https://stackoverflow.com/questions/6453640/how-to-include-external-font-in-wpf-application-without-installing-it/22669818
            string barCode = Text;
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    
                    string Fontpath=HttpContext.Current.Server.MapPath("~/fonts/fre3of9x.ttf");
                  //  FontFamily ffm = new FontFamily(Fontpath, 'fre3of9x');
                    //GlyphTypeface ttf = new GlyphTypeface(new Uri(Fontpath));
                    Font oFont = new Font("", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                
            }
            return imgBarCode.ImageUrl;


        }



        
    }
}