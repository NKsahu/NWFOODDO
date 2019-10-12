using FOODDO.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FOODDO.Controllers
{
   [FOODDO.Web.Attributes.SessionTimeout]
    public class AddressController : Controller
    {
        public ActionResult Index(){return View();}

        public ActionResult Filter(string FilterBy="",string value="")
        {
            List<Address> ListAdd = new List<Address>();
            if (FilterBy.Equals("CID"))
                ListAdd = Address.List.FindAll(x => x.CID == System.Convert.ToInt64(value));
            return View(ListAdd);
        }

        public string Notification()
        {

            var applicationID_ServerKey = "AAAA5_sPHX8:APA91bHDAXzfpWGrIXMebCCIySxJo7WY-t8ID4mylmgd-ZHRp65Ybbuk_HW0YZ_nOQkPYjUN83Y9OYv1Gh7WY6Kd8GEJ-xK3xaLz8Zt9BHwz59Ba4P6cwHX4XFd1f2krQYOEuV9hSy94";
            var senderId = "996349517183";
            //string deviceId = DeviceID;
            System.Net.WebRequest tRequest = System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var data = new
            {
                to = "/topics/AgroIndia",//deviceId,
                notification = new
                {
                    body = "mBody",
                    title = "MTitle",
                    icon = "myicon"
                }
            };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var json = serializer.Serialize(data);
            System.Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID_ServerKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;

            using (System.IO.Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (System.Net.WebResponse tResponse = tRequest.GetResponse())
                {
                    using (System.IO.Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader tReader = new System.IO.StreamReader(dataStreamResponse))
                        {
                            System.String sResponseFromServer = tReader.ReadToEnd();
                            return sResponseFromServer;
                        }
                    }
                }
            }
        }
    }
}