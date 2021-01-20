using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Land
    {
        public int IdLand { get; set; }
        public int IdApartment { get; set; }
        public string IdStreet { get; set; }
        public string House { get; set; }
        public string InventoryNumber { get; set; }
        public decimal? TotalArea { get; set; }
        public Apartment Apartment { get; set; }
    }
}
