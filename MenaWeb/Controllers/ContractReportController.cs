using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.DataServices;
using MenaWeb.ReportServices;
using Microsoft.AspNetCore.Mvc;

namespace MenaWeb.Controllers
{
    public class ContractReportController : Controller
    {
        private readonly ContractReportService reportService;
        private readonly ContractsService contractService;

        private const string zipMime = "application/zip";
        private const string odtMime = "application/vnd.oasis.opendocument.text";
        private const string odsMime = "application/vnd.oasis.opendocument.spreadsheet";
        private const string xlsxMime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ContractReportController(ContractReportService reportService, ContractsService contractService)
        {
            this.reportService = reportService;
            this.contractService = contractService;
        }

        public IActionResult GetExample(int idContract, int idPreamble, int idCommittee)
        {
            try
            {
                var file = reportService.Example(idContract, idPreamble, idCommittee);
                return File(file, odtMime, string.Format(@"Пример отчета для договора № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex.Message;
                return View("Error");
            }
        }
    }
}