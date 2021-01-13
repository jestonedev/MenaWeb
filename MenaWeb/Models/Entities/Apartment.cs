using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Apartment
    {
        public uint IdApartment { get; set; }
        public ushort IdApartmentType { get; set; }
        public string IdStreet { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }
        public string Index { get; set; }
        public decimal? TotalArea { get; set; }
        public decimal? LivingArea { get; set; }
        public string Part { get; set; }
        public string HouseFloor { get; set; }
        public string Floor { get; set; }
        public string InventoryNumber { get; set; }
        public string RoomCount { get; set; }
        public string Room { get; set; }
        public decimal? CadastralPrice { get; set; }
        public virtual List<Contract> Side1Contracts { get; set; }
        public virtual List<Contract> Side2Contracts { get; set; }
        public virtual List<Contract> Side12Contracts { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public virtual List<ApartmentEvaluation> ApartmentEvaluations { get; set; }
        public virtual List<ApartmentRedemption> ApartmentRedemptions { get; set; }
        public virtual List<BankInfo> BankInfos { get; set; }
        public virtual List<RedEvaluation> RedEvaluations { get; set; }
        public virtual List<Land> Land { get; set; }
        public virtual List<Person> People { get; set; }
        public virtual List<WarrantApartment> WarrantApartments { get; set; }
    }
}
