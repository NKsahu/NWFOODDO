using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models.FooddoModels
{
    // this class has been used for  generate class info if user go to hub for his order 
    public class QRInfo
    {
        public System.Int64 CustID { get; set; }
        public string TifinCodes { get; set; }
        public string HubName { get; set; }
        public System.Int64 OrderID { get; set; }
        public string OrderType { get; set; } 
    }
}