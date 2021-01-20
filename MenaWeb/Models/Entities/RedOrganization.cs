using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class RedOrganization
    {
        public int IdOrganization { get; set; }
        public string Name { get; set; }
        public List<RedEvaluation> RedEvaluations { get; set; }
    }
}
