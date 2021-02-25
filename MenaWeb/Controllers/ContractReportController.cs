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
        private const string reportPath = "\\\\mcs.br\\adm\\server\\ActivityManager\\templates\\mena\\\\";

        public ContractReportController(ContractReportService reportService, ContractsService contractService)
        {
            this.reportService = reportService;
            this.contractService = contractService;
        }
    
        public IActionResult GetPreContract(int idContract, int idPreamble, int idDep )
        {
            try
            {
                var file = reportService.PreContract(idContract,idPreamble ,idDep , reportPath);
                return File(file, odtMime, string.Format(@"Предварительный договор № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractWithDate(int idContract)
        {
            try
            {
                var file = reportService.ContractWithDate(idContract, reportPath);
                return File(file, odtMime, string.Format(@"Договор мены с датами № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractWithoutDate(int idContract)
        {
            try
            {
                var file = reportService.ContractWithoutDate(idContract, reportPath);
                return File(file, odtMime, string.Format(@"Договор мены без дат № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractDksr(int idContract)
        {
            try
            {
                var file = reportService.DksrContract(idContract, reportPath);
                return File(file, odtMime, string.Format(@"Договор (ДКСР) № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetResolution(int idContract, int idWarrantTemplate, int idAgree,int idSigner ,int idPrepared , int Lawyer, int Executor)
        {
            try
            {
                var file = reportService.Resolution(idContract, idWarrantTemplate, idAgree, reportPath, idSigner ,idPrepared , Lawyer, Executor);
                return File(file, odtMime, string.Format(@"Постановление № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetNotifyMena(int idContract, int idSigner, int idPrepared, int notifyType)
        {
            try
            {
                var file = reportService.NotifyMena(idContract, idSigner, idPrepared, reportPath, notifyType);
                if (notifyType == 1)
                {
                    return File(file, odtMime, string.Format(@"Уведомление 'прибыть' на мену № {0}.odt", idContract));
                }
                if (notifyType == 2)
                {
                    return File(file, odtMime, string.Format(@"Уведомление 'прибыть' для суда-нотариус № {0}.odt", idContract));
                }
                if (notifyType == 3)
                {
                    return File(file, odtMime, string.Format(@"Уведомление 'прибыть' для суда - 1 собственник № {0}.odt", idContract));
                }
               else
                    return File(file, odtMime, string.Format(@"Уведомление 'правоустанавливающие документы по наследству'  № {0}.odt", idContract));
                
               
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetRequestToMvd(int idContract, int idSigner, int idPrepared, int requestType)
        {
            try
            {
                var file = reportService.RequestToMvd(idContract, idSigner, idPrepared,  reportPath, requestType);
                return File(file, odtMime, string.Format(@"Запрос в МВД № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }

        public IActionResult GetAgreement(int idContract, int idSigner, int agreementType, DateTime date)
        {
            try
            {
                var file = reportService.Agreement(idContract, idSigner, reportPath, agreementType, date);
                if (agreementType == 1)
                {
                    return File(file, odtMime, string.Format(@"Соглашение № {0}.odt", idContract));
                }
                else
                    return File(file, odtMime, string.Format(@"Соглашение о возмещении за изымаемое помещение № {0}.odt", idContract));


            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }

        public IActionResult GetAgreementTransfer(int idContract)
        {
            try
            {
                var file = reportService.AgreementTransfer(idContract, reportPath);
                return File(file, odtMime, string.Format(@"Соглашение о передаче в мун. собственность № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }

        public IActionResult GetRasp( int idContract, DateTime dateNote, string numberNote, DateTime dateRasp, string numberRasp)
        {
            try
            {
                var datenomRasp = dateRasp.ToString("dd/mm/yyyy") + " " + numberRasp;
                var datenomNote = dateNote.ToString("dd/mm/yyyy")+ " " + numberNote;
                var file = reportService.Rasp(idContract,  reportPath, datenomRasp, datenomNote);
                return File(file, odtMime, string.Format(@"Распоряжение № {0}.odt", idContract));


            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
    }
}