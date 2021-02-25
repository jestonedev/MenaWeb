using MenaWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public class ContractVM: IContractReportVM
    {
        public Contract Contract { get; set; }
        public IList<Signer> Signers { get; set; }
        public IList<Models.Entities.Delegate> Delegates { get; set; }
        public IList<ProcessStatus> ProcessStatuses { get; set; }
        public IList<KladrStreet> Streets { get; set; }
        public IList<ApartmentType> ApartmentTypes { get; set; }
        public IList<Evaluator> Evaluators { get; set; }
        public IList<Predost> Predosts { get; set; }
        public IList<Osnovanie> Osnovanies { get; set; }
        public IList<CopyKgc> CopyKgcs { get; set; }
        public IList<PersonStatus> PersonStatuses { get; set; }
        public IList<Contractor> Contractors { get; set; }
        public IList<Document> Documents { get; set; }
        public IList<DocumentIssued> DocumentIssueds { get; set; }
        public IList<RedOrganization> RedOrganizations { get; set; }
        public IList<WarrantTemplate> WarrantsMunicipal { get; set; }
        public IList<WarrantTemplate> WarrantsOwnership { get; set; }
        public IList<WarrantTemplate> WarrantsPersons { get; set; }
        public IList<WarrantTemplateVM> WarrantTemplatesVM { get; set; }
        public IList<WarrantTemplate> WarrantTemplates { get; set; }
        public IList<Signer> ReportSigners { get; set; }    }
}
