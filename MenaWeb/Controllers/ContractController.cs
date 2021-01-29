using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.DataServices;
using MenaWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MenaWeb.Extensions;
using MenaWeb.ViewOptions;
using Newtonsoft.Json;
using MenaWeb.Models;

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
            var vm = dataService.GetListViewModel(
                viewModel.OrderOptions,
                viewModel.PageOptions,
                viewModel.FilterOptions, out List<int> filteredContractsIds);
            AddSearchIdsToSession(vm.FilterOptions, filteredContractsIds);
            return View(vm);
        }

        public IActionResult Details(int? idContract, string returnUrl)
        {
            ViewBag.Action = ActionTypeEnum.Details;
            ViewBag.ReturnUrl = returnUrl;
            if (idContract == null)
                return NotFound();
            var contract = dataService.GetContract(idContract);
            if (contract == null)
                return NotFound();
            var vm = dataService.GetViewModel(contract);
            return View("Contract", vm);
        }

        public IActionResult Create(int? idContract)
        {
            ViewBag.Action = ActionTypeEnum.Create;
            var contract = dataService.CreateContract(idContract);
            return View("Contract", dataService.GetViewModel(contract));
        }

        [HttpPost]
        public ActionResult Create(ContractVM contractVM)
        {
            if (contractVM == null || contractVM.Contract == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                dataService.Create(contractVM.Contract);
                return RedirectToAction("Details", new { contractVM.Contract.IdContract });
            }
            ViewBag.Action = ActionTypeEnum.Create;
            return View("Contract", dataService.GetViewModel(contractVM.Contract));
        }

        [HttpGet]
        public IActionResult Edit(int? idContract, string returnUrl)
        {
            ViewBag.Action = ActionTypeEnum.Edit;
            ViewBag.ReturnUrl = returnUrl;
            if (idContract == null)
                return NotFound();
            var contract = dataService.GetContract(idContract);
            if (contract == null)
                return NotFound();
            var vm = dataService.GetViewModel(contract);
            return View("Contract", vm);
        }

        [HttpPost]
        public ActionResult Edit(ContractVM contractVM, string returnUrl)
        {
            if (contractVM == null || contractVM.Contract == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                dataService.Edit(contractVM.Contract);
                return RedirectToAction("Details", new { contractVM.Contract.IdContract });
            }
            ViewBag.Action = ActionTypeEnum.Edit;
            ViewBag.ReturnUrl = returnUrl;
            return View("Contract", dataService.GetViewModel(contractVM.Contract));
        }

        [HttpGet]
        public IActionResult Delete(int? idContract, string returnUrl)
        {
            ViewBag.Action = ActionTypeEnum.Delete;
            ViewBag.ReturnUrl = returnUrl;
            if (idContract == null)
                return NotFound();
            var contract = dataService.GetContract(idContract);
            if (contract == null)
                return NotFound();
            var vm = dataService.GetViewModel(contract);
            return View("Contract", vm);
        }

        [HttpPost]
        public ActionResult Delete(ContractVM contractVM)
        {
            if (contractVM == null || contractVM.Contract == null)
                return NotFound();

            dataService.Delete(contractVM.Contract.IdContract);
            return RedirectToAction("Index");
        }

        public IActionResult ContractReports(PageOptions pageOptions)
        {
            return null;
        }

        [HttpPost]
        public void SessionIdContracts(int idContract, bool isCheck)
        {
            List<int> ids = GetSessionContractsIds();

            if (isCheck)
                ids.Add(idContract);
            else if (ids.Any())
                ids.Remove(idContract);

            HttpContext.Session.Set("idContracts", ids);
        }

        private List<int> GetSessionContractsIds()
        {
            if (HttpContext.Session.Keys.Contains("idContracts"))
                return HttpContext.Session.Get<List<int>>("idContracts");
            return new List<int>();
        }

        [HttpPost]
        public List<int> GetSessionIds()
        {
            if (HttpContext.Session.Keys.Contains("idContracts"))
                return HttpContext.Session.Get<List<int>>("idContracts");
            return new List<int>();
        }

        public IActionResult ClearSessionIds()
        {
            HttpContext.Session.Remove("idContracts");
            ViewBag.Count = 0;
            return RedirectToAction("ContractReports");
        }

        public IActionResult RemoveSessionId(int id)
        {
            var ids = GetSessionIds();
            ids.Remove(id);
            ViewBag.Count = ids.Count();
            HttpContext.Session.Set("idContracts", ids);
            return RedirectToAction("ContractReports");
        }

        [HttpPost]
        public void CheckIdToSession(int id, bool isCheck)
        {
            List<int> ids = GetSessionIds();

            if (isCheck)
                ids.Add(id);
            else if (ids.Any())
                ids.Remove(id);

            HttpContext.Session.Set("idContracts", ids);
        }

        public void AddSearchIdsToSession(ContractFilterOptions filterOptions, List<int> filteredIds)
        {
            var filteredIdsDict = new Dictionary<string, List<int>>();

            if (HttpContext.Session.Keys.Contains("filteredContractsIdsDict"))
                filteredIdsDict = HttpContext.Session.Get<Dictionary<string, List<int>>>("filteredContractsIdsDict");

            var filterOptionsSerialized = JsonConvert.SerializeObject(filterOptions).ToString();

            if (filteredIdsDict.Keys.Contains(filterOptionsSerialized))
                filteredIdsDict[filterOptionsSerialized] = filteredIds;
            else filteredIdsDict.Add(filterOptionsSerialized, filteredIds);

            HttpContext.Session.Set("filteredContractsIdsDict", filteredIdsDict);
        }

        [HttpPost]
        public IActionResult AddSelectedAndFilteredIdsToSession(ContractFilterOptions filterOptions)
        {
            if (!HttpContext.Session.Keys.Contains("filteredContractsIdsDict"))
                return Json(0);

            var filteredIdsDict = HttpContext.Session.Get<Dictionary<string, List<int>>>("filteredContractsIdsDict");
            var filterOptionsSerialized = JsonConvert.SerializeObject(filterOptions).ToString();

            if (filteredIdsDict.Keys.Contains(filterOptionsSerialized))
            {
                List<int> filterOptionsIds = filteredIdsDict[filterOptionsSerialized];
                List<int> ids = GetSessionIds();
                ids.AddRange(filterOptionsIds);
                ids = ids.Distinct().ToList();
                HttpContext.Session.Set("idContracts", ids);
                return Json(0);
            }
            return Json(-1);
        }

        public IActionResult AddDocumentIssued(string documentIssuedName)
        {
            if (string.IsNullOrEmpty(documentIssuedName))
                return Json(-1);
            return Json(dataService.AddDocumentIssued(documentIssuedName));
        }

        public IActionResult AddPerson(ActionTypeEnum action)
        {
            ViewBag.Action = action;
            ViewBag.Index = 0;
            var contract = dataService.CreateContract(null);
            contract.ApartmentSide2.People.Add(new Models.Entities.Person());
            return PartialView("Person", dataService.GetViewModel(contract));
        }
    }
}