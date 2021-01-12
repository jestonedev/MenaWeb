using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class ContractReason
    {
        public int IdContractReason { get; set; }
        public string Template { get; set; }
        public virtual List<Contract> Contracts { get; set; }
    }
}
