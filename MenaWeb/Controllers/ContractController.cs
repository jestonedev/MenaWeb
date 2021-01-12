using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenaWeb.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private readonly ContractsService service;

        public ContractController(ContractsService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            service.GetContracts();
            return View();
        }
    }
}