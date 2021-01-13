using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class RedEvaluation
    {
        public uint IdEvaluation { get; set; }
        public uint? IdApartment { get; set; }
        public uint? IdOrganization { get; set; }
        public string EvaluationPrice { get; set; }
        public bool Deleted { get; set; }
        public Apartment Apartment { get; set; }
        public RedOrganization RedOrganization { get; set; }
    }
}
