using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models.FooddoModels
{
    public class HubWiseTifin
    {
        public int HubId { get; set; }
        public string HubName { get; set; }
        public string MobileNo { get; set; }
        public int TifinCounts { get; set; }
        public double HubCode { get; set; }
        public List<HubTifins> TifinList { get; set; }

        public class HubTifins
        {   public System.Int64 OID { get; set; }
            public string TifinNo { get; set; }
        }

    }
}