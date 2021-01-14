using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.DataServices;
using MenaWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenaWeb.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private readonly ContractsService dataService;

        public ContractController(ContractsService dataService)
        {
            this.dataService = dataService;
        }

        public IActionResult Index(ContractListVM viewModel)
        {
            return View(dataService.GetListViewModel(
                viewModel.OrderOptions,
                viewModel.PageOptions,
                viewModel.FilterOptions));
        }
    }
}