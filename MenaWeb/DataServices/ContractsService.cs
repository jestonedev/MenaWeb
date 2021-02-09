﻿using MenaWeb.Models;
using MenaWeb.Models.Entities;
using MenaWeb.ViewModels;
using MenaWeb.ViewOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MenaWeb.DataServices
{
    public class ContractsService
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContractsService(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Contract> GetQuery()
        {
            return db.Contracts
                .Include(c => c.ApartmentSide1)
                .Include(c => c.ApartmentSide12)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.People);
        }

        public ContractListVM GetListViewModel(OrderOptions orderOptions, PageOptions pageOptions, ContractFilterOptions filterOptions, out List<int> filteredContractsIds)
        {
            var vm = new ContractListVM();
            vm.OrderOptions = orderOptions ?? vm.OrderOptions;
            vm.PageOptions = pageOptions ?? vm.PageOptions;
            vm.FilterOptions = filterOptions ?? vm.FilterOptions;
            var query = GetQuery();
            vm.PageOptions.TotalRows = query.Count();
            query = GetQueryFilter(query, vm.FilterOptions);
            query = query.OrderByDescending(r => r.IdContract);
            query = GetQueryIncludes(query);
            var count = query.Count();
            vm.PageOptions.Rows = count;
            vm.PageOptions.TotalPages = Math.Max((int)Math.Ceiling(count / (double)vm.PageOptions.SizePage), 1);

            filteredContractsIds = query.Select(r => r.IdContract).ToList();

            vm.PageOptions.CurrentPage = Math.Min(vm.PageOptions.CurrentPage, vm.PageOptions.TotalPages);
            vm.Contracts = GetQueryPage(query, vm.PageOptions).ToList();
            vm.Streets = GetActualStreets(vm.Contracts);
            return vm;
        }

        public Contract CreateContract(int? idContract)
        {
            Contract contract = null;
            if (idContract != null)
            {
                contract = GetContract(idContract);
                contract.IdContract = 0;
                contract.IdApartmentSide1 = null;
                contract.IdApartmentSide2 = null;
                contract.IdApartmentSide12 = null;
                contract.ApartmentSide1.IdApartment = 0;
                contract.ApartmentSide2.IdApartment = 0;
                contract.ApartmentSide12.IdApartment = 0;
                if (contract.ApartmentSide1.ApartmentEvaluations != null && contract.ApartmentSide1.ApartmentEvaluations.Any())
                {
                    contract.ApartmentSide1.ApartmentEvaluations[0].IdApartmentEvaluation = 0;
                    contract.ApartmentSide1.ApartmentEvaluations[0].IdApartment = 0;
                }
                if (contract.ApartmentSide12.ApartmentEvaluations != null && contract.ApartmentSide12.ApartmentEvaluations.Any())
                {
                    contract.ApartmentSide12.ApartmentEvaluations[0].IdApartmentEvaluation = 0;
                    contract.ApartmentSide12.ApartmentEvaluations[0].IdApartment = 0;
                }
                if (contract.ApartmentSide2.ApartmentEvaluations != null && contract.ApartmentSide2.ApartmentEvaluations.Any())
                {
                    contract.ApartmentSide2.ApartmentEvaluations[0].IdApartmentEvaluation = 0;
                    contract.ApartmentSide2.ApartmentEvaluations[0].IdApartment = 0;
                }
                if (contract.ApartmentSide2.Land != null && contract.ApartmentSide2.Land.Any())
                {
                    contract.ApartmentSide2.Land[0].IdLand = 0;
                    contract.ApartmentSide2.Land[0].IdApartment = 0;
                }
                if (contract.Additionals != null && contract.Additionals.Any())
                {
                    contract.Additionals[0].IdAddit = 0;
                    contract.Additionals[0].IdContract = 0;
                    contract.Additionals[0].IdApartment1 = 0;
                    contract.Additionals[0].IdApartment2 = 0;
                }
                if (contract.ApartmentSide2.ApartmentRedemptions != null && contract.ApartmentSide2.ApartmentRedemptions.Any())
                {
                    contract.ApartmentSide2.ApartmentRedemptions[0].IdApartmentRedemption = 0;
                    contract.ApartmentSide2.ApartmentRedemptions[0].IdApartment = 0;
                }
                if (contract.ApartmentSide2.RedEvaluations != null)
                {
                    foreach (var evaluation in contract.ApartmentSide2.RedEvaluations)
                    {
                        evaluation.IdApartment = 0;
                        evaluation.IdEvaluation = 0;
                    }
                }
                foreach (var status in contract.ContractStatusHistory)
                {
                    status.IdContract = 0;
                    status.IdHistoryStatus = 0;
                }
                foreach (var person in contract.ApartmentSide2.People)
                {
                    person.IdApartment = 0;
                    person.IdPerson = 0;
                }
                foreach (var bankInfo in contract.ApartmentSide2.BankInfos)
                {
                    bankInfo.IdApartment = 0;
                    bankInfo.IdBank = 0;
                }
            }
            if (contract == null)
            {
                contract = new Contract();
            }
            return contract;
        }

        internal void Edit(Contract contract)
        {
            contract.LastChangeDate = DateTime.Now;
            contract.LastChangeUser = httpContextAccessor.HttpContext.User.Identity.Name.ToLowerInvariant();

            if (contract.ApartmentSide12 != null && contract.ApartmentSide12.IsEmpty())
            {
                if (contract.ApartmentSide12.IdApartment != 0) db.Apartments.Remove(contract.ApartmentSide12);
                contract.ApartmentSide12 = null;
                contract.IdApartmentSide12 = null;
            }
            foreach(var status in db.ContractStatusHistory.Where(r => r.IdContract == contract.IdContract).AsNoTracking())
            {
                if (!contract.ContractStatusHistory.Any(r => r.IdHistoryStatus == status.IdHistoryStatus))
                {
                    db.ContractStatusHistory.Remove(status);
                }
            }
            foreach (var person in db.People.Where(r => r.IdApartment == contract.ApartmentSide2.IdApartment).AsNoTracking())
            {
                if (!contract.ApartmentSide2.People.Any(r => r.IdPerson == person.IdPerson))
                {
                    person.Deleted = true;
                    db.People.Update(person);
                }
            }
            foreach (var bankInfo in db.BankInfos.Where(r => r.IdApartment == contract.ApartmentSide2.IdApartment).AsNoTracking())
            {
                if (!contract.ApartmentSide2.BankInfos.Any(r => r.IdBank == bankInfo.IdBank))
                {
                    bankInfo.Deleted = true;
                    db.BankInfos.Update(bankInfo);
                }
            }
            foreach (var evaluation in db.RedEvaluations.Where(r => r.IdApartment == contract.ApartmentSide2.IdApartment).AsNoTracking())
            {
                if (!contract.ApartmentSide2.RedEvaluations.Any(r => r.IdEvaluation == evaluation.IdEvaluation))
                {
                    evaluation.Deleted = true;
                    db.RedEvaluations.Update(evaluation);
                }
            }
            db.Contracts.Update(contract);
            db.SaveChanges();
        }

        internal void Create(Contract contract)
        {
            contract.LastChangeDate = DateTime.Now;
            contract.LastChangeUser = httpContextAccessor.HttpContext.User.Identity.Name.ToLowerInvariant();
            if (contract.ApartmentSide12 != null && contract.ApartmentSide12.IsEmpty())
            {
                contract.ApartmentSide12 = null;
            }
            db.Contracts.Add(contract);
            db.SaveChanges();
        }

        public void Delete(int idContract)
        {
            var contracts = db.Contracts
                    .FirstOrDefault(c => c.IdContract == idContract);
            if (contracts != null)
            {
                contracts.Deleted = true;
                db.SaveChanges();
            }
        }

        public Contract GetContract(int? idContract)
        {
            return db.Contracts
                .Include(c => c.ContractStatusHistory)
                .Include(c => c.Additionals)
                .Include(c => c.ApartmentSide1).ThenInclude(a => a.WarrantApartments)
                .Include(c => c.ApartmentSide1).ThenInclude(a => a.ApartmentEvaluations)
                .Include(c => c.ApartmentSide12).ThenInclude(a => a.WarrantApartments)
                .Include(c => c.ApartmentSide12).ThenInclude(a => a.ApartmentEvaluations)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.People)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.WarrantApartments)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.ApartmentEvaluations)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.RedEvaluations)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.BankInfos)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.Land)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.ApartmentRedemptions)
                .FirstOrDefault(r => r.IdContract == idContract);
        }

        public List<TemplateVariableMeta> WarrantVariablesMeta(int idTemplate)
        {
            return db.TemplateVariablesMeta.Where(r => r.IdWarrantTemplate == idTemplate).ToList();
        }

        public ContractVM GetViewModel(Contract contract)
        {
            var vm = new ContractVM {
                Contract = contract,
                Delegates = db.Delegates.ToList(),
                Signers = db.Signers.Where(r => r.IdSignerType == 4).ToList(),
                ProcessStatuses = db.ProcessStatuses.ToList(),
                Streets = db.KladrStreets.ToList(),
                ApartmentTypes = db.ApartmentTypes.ToList(),
                Evaluators = db.Evaluators.ToList(),
                CopyKgcs = db.CopyKgcs.ToList(),
                Osnovanies = db.Osnovanies.ToList(),
                Predosts = db.Predosts.ToList(),
                Contractors = db.Contractors.ToList(),
                PersonStatuses = db.PersonStatuses.ToList(),
                Documents = db.Documents.ToList(),
                DocumentIssueds = db.DocumentIssueds.ToList(),
                RedOrganizations = db.RedOrganizations.ToList(),
                WarrantsMunicipal = db.WarrantTemplates.Where(r => r.IdWarrantTemplateType == 2).ToList(),
                WarrantsOwnership = db.WarrantTemplates.Where(r => r.IdWarrantTemplateType == 3).ToList(),
                WarrantsPersons = db.WarrantTemplates.Where(r => r.IdWarrantTemplateType == 4).ToList()
            };
            vm.WarrantTemplatesVM = new List<WarrantTemplateVM>();
            foreach(var warrant in contract.ApartmentSide1.WarrantApartments.Union(contract.ApartmentSide12.WarrantApartments).Union(
                contract.ApartmentSide2.WarrantApartments))
            {
                var warrantTemplate = db.WarrantTemplates.FirstOrDefault(r => r.IdWarrantTemplate == warrant.IdWarrantTemplate);
                var templateVM = new WarrantTemplateVM {
                    IdWarrantTemplate = warrant.IdWarrantTemplate,
                    IdWarrantObject = warrant.IdWarrantApartment,
                    IdObject = warrant.IdApartment,
                    ObjectType = WarrantObjectType.Apartment,
                    WarrantTemplateBody = db.WarrantTemplates.FirstOrDefault(r => r.IdWarrantTemplate == warrant.IdWarrantTemplate)?.WarrantTemplateBody,
                    Variables = db.TemplateVariables.Include(r => r.TemplateVariableMeta)
                        .Where(r => r.TemplateVariableMeta.IdWarrantTemplate == warrant.IdWarrantTemplate && r.IdObject == warrant.IdWarrantApartment)
                        .ToList()
                };
                vm.WarrantTemplatesVM.Add(templateVM);
            }
            foreach(var person in contract.ApartmentSide2.People)
            {
                var idWarrant = person.IdTemplate;
                if (idWarrant == null) continue;
                var warrantTemplate = db.WarrantTemplates.FirstOrDefault(r => r.IdWarrantTemplate == idWarrant);
                var templateVM = new WarrantTemplateVM
                {
                    IdWarrantTemplate = idWarrant.Value,
                    IdWarrantObject = person.IdPerson,
                    IdObject = person.IdPerson,
                    ObjectType = WarrantObjectType.Person,
                    WarrantTemplateBody = db.WarrantTemplates.FirstOrDefault(r => r.IdWarrantTemplate == idWarrant)?.WarrantTemplateBody,
                    Variables = db.TemplateVariables.Include(r => r.TemplateVariableMeta)
                        .Where(r => r.TemplateVariableMeta.IdWarrantTemplate == idWarrant && r.IdObject == person.IdPerson)
                        .ToList()
                };
                vm.WarrantTemplatesVM.Add(templateVM);
            }
            return vm;
        }

        public int AddDocumentIssued(string documentIssuedName)
        {
            var duplicates = db.DocumentIssueds.FirstOrDefault(r => r.DocumentIssuedName == documentIssuedName);
            if (duplicates != null)
            {
                return -3;
            }
            var documentIssued = new DocumentIssued { DocumentIssuedName = documentIssuedName };
            db.DocumentIssueds.Add(documentIssued);
            db.SaveChanges();
            return documentIssued.IdDocumentIssued;
        }

        private List<KladrStreet> GetActualStreets(List<Contract> contracts)
        {
            var ids = contracts.Where(r => r.ApartmentSide1 !=null).Select(r => r.ApartmentSide1.IdStreet)
                .Union(contracts.Where(r => r.ApartmentSide2 != null).Select(r => r.ApartmentSide2.IdStreet))
                .Union(contracts.Where(r => r.ApartmentSide12 != null).Select(r => r.ApartmentSide12.IdStreet));
            return db.KladrStreets.Where(r => ids.Contains(r.IdStreet)).ToList();
        }

        private IQueryable<Contract> GetQueryPage(IQueryable<Contract> query, PageOptions pageOptions)
        {
            return query
                .Skip((pageOptions.CurrentPage - 1) * pageOptions.SizePage)
                .Take(pageOptions.SizePage);
        }

        private IQueryable<Contract> GetQueryIncludes(IQueryable<Contract> query)
        {
            return query.Include(c => c.ApartmentSide1)
                .Include(c => c.ApartmentSide12)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.People);
        }

        private IQueryable<Contract> GetQueryFilter(IQueryable<Contract> query, ContractFilterOptions filterOptions)
        {
            query = FilterQueryByAddress(query, filterOptions.Address);
            query = FilterQueryByPersonSnp(query, filterOptions.PersonSnp);
            query = FilterQueryRegistrationPeriod(query, filterOptions.RegistrationDateFrom, filterOptions.RegistrationDateTo);
            return query;
        }

        private IQueryable<Contract> FilterQueryRegistrationPeriod(IQueryable<Contract> query, DateTime? from, DateTime? to)
        {
            if (from == null && to == null) return query;
            if (from != null && to != null) return query.Where(r => r.ContractRegistrationDate >= from && r.ContractRegistrationDate <= to);
            if (from != null) return query.Where(r => r.ContractRegistrationDate >= from);
            if (to != null) return query.Where(r => r.ContractRegistrationDate <= to);
            return query;
        }

        private IQueryable<Contract> FilterQueryByPersonSnp(IQueryable<Contract> query, string personSnp)
        {
            if (string.IsNullOrEmpty(personSnp)) return query;
            var snpParts = personSnp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToArray();
            var family = snpParts[0].ToLowerInvariant();
            if (snpParts.Length == 1)
            {
                var apartmentIds = db.People.Where(p => p.Family != null && p.Family.ToLowerInvariant().Contains(family)).Select(p => p.IdApartment).ToList();
                return query.Where(r => r.ApartmentSide2 != null && apartmentIds.Contains(r.ApartmentSide2.IdApartment));
            }
            var name = snpParts[1].ToLowerInvariant();
            if (snpParts.Length == 2)
            {
                var apartmentIds = db.People.Where(p =>
                    p.Family != null && p.Family.ToLowerInvariant().Contains(family) &&
                    p.Name != null && p.Name.ToLowerInvariant().Contains(name)).Select(p => p.IdApartment).ToList();
                return query.Where(r => r.ApartmentSide2 != null && apartmentIds.Contains(r.ApartmentSide2.IdApartment));
            }
            var father = snpParts[2].ToLowerInvariant();
            if (snpParts.Length >= 3)
            {
                var apartmentIds = db.People.Where(p =>
                        p.Family != null && p.Family.ToLowerInvariant().Contains(family) &&
                        p.Name != null && p.Name.ToLowerInvariant().Contains(name) &&
                        p.Father != null && p.Father.ToLowerInvariant().Contains(father)).Select(p => p.IdApartment).ToList();
                return query.Where(r => r.ApartmentSide2 != null && apartmentIds.Contains(r.ApartmentSide2.IdApartment));
            }
            return query;
        }

        private IQueryable<Contract> FilterQueryByAddress(IQueryable<Contract> query, string address)
        {
            if (string.IsNullOrEmpty(address)) return query;
            var match = Regex.Match(address, @"^(.*?)[,]*[ ]*(д\.?)?[ ]*(\d+[а-яА-Я]?([\\\/]\d+[а-яА-Я]?)?)?[ ,-]*(кв\.?)?[ ]*(\d+[а-яА-Я]?)?[ ]*$");
            var addressWordsList = new List<string>();
            if (!string.IsNullOrEmpty(match.Groups[1].Value))
            {
                addressWordsList.Add(match.Groups[1].Value);
            }
            if (!string.IsNullOrEmpty(match.Groups[3].Value))
            {
                addressWordsList.Add(match.Groups[3].Value);
            }
            if (!string.IsNullOrEmpty(match.Groups[6].Value))
            {
                addressWordsList.Add(match.Groups[6].Value);
            }
            var addressWords = addressWordsList.ToArray();
            var street = addressWords[0].ToLowerInvariant();
            var streetIds = db.KladrStreets.Where(r => r.StreetName.ToLowerInvariant().Contains(street)).Select(r => r.IdStreet).ToList();
            query = query.Where(r => (r.ApartmentSide1 != null && streetIds.Contains(r.ApartmentSide1.IdStreet)) ||
                    (r.ApartmentSide12 != null && streetIds.Contains(r.ApartmentSide12.IdStreet)) ||
                    (r.ApartmentSide2 != null && streetIds.Contains(r.ApartmentSide2.IdStreet)));
            if (addressWords.Length == 1) return query;
            var house = addressWords[1].ToLowerInvariant();
            query = query.Where(r => (r.ApartmentSide1 != null && r.ApartmentSide1.House != null && r.ApartmentSide1.House.ToLowerInvariant().Contains(house)) ||
                    (r.ApartmentSide12 != null && r.ApartmentSide12.House != null && r.ApartmentSide12.House.ToLowerInvariant().Contains(house)) ||
                    (r.ApartmentSide2 != null && r.ApartmentSide2.House != null && r.ApartmentSide2.House.ToLowerInvariant().Contains(house)));
            if (addressWords.Length == 2) return query;
            var flat = addressWords[2].ToLowerInvariant();
            query = query.Where(r => (r.ApartmentSide1 != null && r.ApartmentSide1.Flat != null && r.ApartmentSide1.Flat.ToLowerInvariant().Contains(flat)) ||
                    (r.ApartmentSide12 != null && r.ApartmentSide12.Flat != null && r.ApartmentSide12.Flat.ToLowerInvariant().Contains(flat)) ||
                    (r.ApartmentSide2 != null && r.ApartmentSide2.Flat != null && r.ApartmentSide2.Flat.ToLowerInvariant().Contains(flat)));
            return query;
        }
    }
}
