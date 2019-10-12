using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models
{
    public class MessOrdersApi
    {
        public System.Int64 OrderItemId { get; set; }
        public string FoodName { get; set; }
        public string FoodImg;
        public System.Int32 TotalItems { get; set; }
        public System.Int64 Status { get; set; }
        public string Type { get; set; }


    }



}