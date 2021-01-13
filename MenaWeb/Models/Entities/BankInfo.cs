using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class BankInfo
    {
        public uint IdBank { get; set; }
        public uint IdApartment { get; set; }
        public string Account { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public decimal? Sum { get; set; }
        public string SumString { get; set; }
        public bool Deleted { get; set; }
        public Apartment Apartment { get; set; }
    }
}
