using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class ApartmentEvaluation
    {
        public int IdApartmentEvaluation { get; set; }
        public int IdApartment { get; set; }
        public short? IdEvaluator { get; set; }
        public string EvaluationNumber { get; set; }
        public decimal? EvaluationPrice { get; set; }
        public DateTime? EvaluationDate { get; set; }
        public Apartment Apartment { get; set; }
        public Evaluator Evaluator { get; set; }
    }
}
