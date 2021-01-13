using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class ApartmentRedemption
    {
        public uint IdApartmentRedemption { get; set; }
        public uint IdApartment { get; set; }
        public DateTime? DateRedemption { get; set; }
        public bool Deleted { get; set; }
        public Apartment Apartment { get; set; }
    }
}
