using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewOptions
{
    public class ContractFilterOptions: FilterOptions
    {
        public string Address { get; set; }
        public string PersonSnp { get; set; }
        public DateTime? RegistrationDateFrom { get; set; }
        public DateTime? RegistrationDateTo { get; set; }
    }
}
