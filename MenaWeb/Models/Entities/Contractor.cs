using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Contractor
    {
        public ushort IdContractor { get; set; }
        public string ContractorName { get; set; }
        public string ContractorShort { get; set; }
        public virtual List<Person> People { get; set; }
    }
}
