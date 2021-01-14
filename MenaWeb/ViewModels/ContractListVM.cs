using MenaWeb.Models.Entities;
using MenaWeb.ViewOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public class ContractListVM: ListVM<ContractFilterOptions>
    {
        public List<Contract> Contracts;
        public List<KladrStreet> Streets;
    }
}
