using MenaWeb.Models;
using MenaWeb.Models.Entities;
using MenaWeb.ViewModels;
using MenaWeb.ViewOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.DataServices
{
    public class ContractsService
    {
        private readonly DatabaseContext db;

        public ContractsService(DatabaseContext db)
        {
            this.db = db;
        }

        public IQueryable<Contract> GetQuery()
        {
            return db.Contracts
                .Include(c => c.ApartmentSide1)
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.People);
        }

        public ContractListVM GetListViewModel(OrderOptions orderOptions, PageOptions pageOptions, ContractFilterOptions filterOptions)
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
            vm.PageOptions.TotalPages = (int)Math.Ceiling(count / (double)vm.PageOptions.SizePage);
            vm.Contracts = GetQueryPage(query, vm.PageOptions).ToList();
            vm.Streets = GetActualStreets(vm.Contracts);
            return vm;
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
                .Include(c => c.ApartmentSide2).ThenInclude(a => a.People);
        }

        private IQueryable<Contract> GetQueryFilter(IQueryable<Contract> query, ContractFilterOptions filterOptions)
        {
            return query;
        }
    }
}
