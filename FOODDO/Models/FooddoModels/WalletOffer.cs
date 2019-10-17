using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models.FooddoModels
{
    public class WalletOffer
    {
        public string OfferTitle { get; set; }
        public string Discription { get; set; }
        public List<CashBacktemList> CashBackList { get; set; }

   public class CashBacktemList
        {
            public double FromAmt { get; set; }
            public double ToAmt { get; set; }
            public double BonusAmt { get; set; }

        }
    }
}