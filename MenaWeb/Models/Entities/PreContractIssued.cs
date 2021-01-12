using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class PreContractIssued
    {
        public ushort IdPreContractIssued { get; set; }
        public string PreContractName { get; set; }
        public string PreContractNameShort { get; set; }
        public virtual List<Contract> Contracts { get; set; }
    }
}
