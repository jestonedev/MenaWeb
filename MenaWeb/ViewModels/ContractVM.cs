using MenaWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public class ContractVM
    {
        public Contract Contract { get; set; }
        public IList<Signer> Signers { get; set; }
        public IList<Models.Entities.Delegate> Delegates { get; set; }
        public IList<ProcessStatus> ProcessStatuses { get; set; }
        public IList<KladrStreet> Streets { get; set; }
        public IList<ApartmentType> ApartmentTypes { get; set; }
    }
}
