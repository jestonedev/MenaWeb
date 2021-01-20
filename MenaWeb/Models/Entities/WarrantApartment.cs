using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class WarrantApartment
    {
        public int IdWarrantApartment { get; set; }
        public int IdWarrantTemplate { get; set; }
        public int IdApartment { get; set; }
        public WarrantTemplate WarrantTemplate { get; set; }
        public Apartment Apartment { get; set; }
    }
}
