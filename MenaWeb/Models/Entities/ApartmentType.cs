using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class ApartmentType
    {
        public short IdApartmentType { get; set; }
        public string ApartmentTypeName { get; set; }
        public string ApartmentTypeRod { get; set; }
        public string ApartmentTypePlur { get; set; }
        public virtual List<Apartment> Apartments { get; set; }
    }
}
