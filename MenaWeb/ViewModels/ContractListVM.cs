using MenaWeb.Models.Entities;
using MenaWeb.ViewOptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public class ContractListVM : ListVM<ContractFilterOptions>, IContractReportVM
    {
        public List<Contract> Contracts { get; set; }
        public IList<KladrStreet> Streets { get; set; }
        public IList<WarrantTemplate> WarrantTemplates { get; set; }
        public IList<Signer> ReportSigners { get; set; }
    }
}
