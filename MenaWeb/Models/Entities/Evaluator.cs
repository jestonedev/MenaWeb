using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Evaluator
    {
        public ushort IdEvaluator { get; set; }
        public string EvaluatorName { get; set; }
        public string EvaluatorBoss { get; set; }
        public virtual List<ApartmentEvaluation> ApartmentEvaluations { get; set; }
    }
}
