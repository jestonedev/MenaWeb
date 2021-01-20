using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class RedEvaluation
    {
        public int IdEvaluation { get; set; }
        public int? IdApartment { get; set; }
        public int? IdOrganization { get; set; }
        public string EvaluationPrice { get; set; }
        public bool Deleted { get; set; }
        public Apartment Apartment { get; set; }
        public RedOrganization RedOrganization { get; set; }
    }
}
