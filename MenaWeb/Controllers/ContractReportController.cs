using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.DataServices;
using MenaWeb.Extensions;
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
    
        public IActionResult GetPreContract(int? idContract, int idPreamble, int idDep )
        {
            try
            {
                var ids = GetSessionIds();
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                      
                    }
                    var file = reportService.PreContract(ids, idContract, idPreamble, idDep, reportPath);
                    return File(file, odtMime, string.Format(@"Предварительный договор № {0}.odt", idContract));
                }
                contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                if (errorIds.Any())
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                        errorIds.Count == 1 ? "e" : "ах");
                    return View("Error");
                }
                var files = reportService.PreContract(ids,idContract,idPreamble ,idDep , reportPath);
                return File(files, zipMime, @"Предварительные договоры.zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractWithDate(int? idContract)
        {
            try
            {
                var ids = GetSessionIds();
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.ContractWithDate(ids, idContract, reportPath);
                    return File(file, odtMime, string.Format(@"Договор мены с датами № {0}.odt", idContract));
                }
                contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                if (errorIds.Any())
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                        errorIds.Count == 1 ? "e" : "ах");
                    return View("Error");
                }
                var files = reportService.ContractWithDate(ids,idContract, reportPath);
                return File(files, zipMime, @"Договоры мены с датами.zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractWithoutDate(int? idContract)
        {
            try
            {
                var ids = GetSessionIds();
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.ContractWithoutDate(ids, idContract, reportPath);
                    return File(file, odtMime, string.Format(@"Договор мены без дат № {0}.odt", idContract));
                }
                contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                if (errorIds.Any())
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                        errorIds.Count == 1 ? "e" : "ах");
                    return View("Error");
                }
                var files = reportService.ContractWithoutDate(ids,idContract, reportPath);
                return File(files, zipMime, @"Договоры мены без дат.zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetContractDksr(int? idContract)
        {
            try
            {
                var ids = GetSessionIds();
                if (idContract!= null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.DksrContract(ids, idContract, reportPath);
                    return File(file, odtMime, string.Format(@"Договор (ДКСР) № {0}.odt", idContract));
                }
                contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                if (errorIds.Any())
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                        errorIds.Count == 1 ? "e" : "ах");
                    return View("Error");
                }
                var files = reportService.DksrContract(processingIds, idContract, reportPath);
                return File(files, zipMime, @"Договоры (ДКСР).zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetResolution(int idContract, int idWarrantTemplate, int idAgree,int idSigner ,int idPrepared , int idLawyer, int idExecutor)
        {
            try
            {
                var typeForView = "res";
                contractService.UpdateDocumentSigners(idContract, idSigner, idPrepared, idLawyer, idExecutor, typeForView);
                var file = reportService.Resolution(idContract, idWarrantTemplate, idAgree, reportPath);
                return File(file, odtMime, string.Format(@"Постановление № {0}.odt", idContract));
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
        public IActionResult GetNotifyMena(int? idContract, int idSigner, int idPrepared, int notifyType)
        {
            try
            {
                var ids = GetSessionIds();
               
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.NotifyMena(ids, idContract, idSigner, idPrepared, reportPath, notifyType);
                    switch (notifyType)
                    {
                        case 1:
                            return File(file, odtMime, string.Format(@"Уведомление 'прибыть' на мену № {0}.odt", idContract));
                        case 2:
                            return File(file, odtMime, string.Format(@"Уведомление 'прибыть' для суда-нотариус № {0}.odt", idContract));
                        case 3:
                            return File(file, odtMime, string.Format(@"Уведомление 'прибыть' для суда - 1 собственник № {0}.odt", idContract));
                    }
                    return File(file, odtMime, string.Format(@"Уведомление 'правоустанавливающие документы по наследству'  № {0}.odt", idContract));
                }
                else
                {
                    contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                    if (errorIds.Any())
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                            errorIds.Count == 1 ? "e" : "ах");
                        return View("Error");
                    }
                    var file = reportService.NotifyMena(ids, idContract, idSigner, idPrepared, reportPath, notifyType);
                    switch (notifyType)
                    {
                        case 1:
                            return File(file, zipMime, @"Уведомления 'прибыть' на мену.zip");
                        case 2:
                            return File(file, zipMime, @"Уведомления 'прибыть' для суда-нотариус.zip");
                        case 3:
                            return File(file, zipMime, @"Уведомления 'прибыть' для суда-1 собственник.zip");
                    }
                    return File(file, zipMime, @"Уведомления 'правоустанавливающие документы по наследству'.zip");
                }
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }
       
        public IActionResult GetRequestToMvd(int? idContract, int idSigner, int idPrepared, int requestType)
        {
            try
            {
                var ids = GetSessionIds();
               
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.RequestToMvd(ids, idContract, idSigner, idPrepared, reportPath, requestType);
                    switch (requestType)
                    {
                        case 1:
                            return File(file, odtMime, string.Format(@"Запрос в МВД (новый) № {0}.odt", idContract));
                    }
                    return File(file, odtMime, string.Format(@"Запрос в МВД (старый) № {0}.odt", idContract));
                }
                else
                {
                    contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                    if (errorIds.Any())
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                            errorIds.Count == 1 ? "e" : "ах");
                        return View("Error");
                    }
                    var file = reportService.RequestToMvd(ids, idContract, idSigner, idPrepared, reportPath, requestType);
                    switch (requestType)
                    {
                        case 1:
                            return File(file, zipMime, @"Запросы в МВД (новый).zip");
                    }
                    return File(file, zipMime, @"Запросы в МВД (старый).zip");
                }
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
                var check = contractService.CheckForFullnessReport(idContract);
                if (check == false)
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                    return View("Error");
                }
                var file = reportService.Agreement(idContract, idSigner, reportPath, agreementType, date);
                switch(agreementType)
                {
                    case 1:
                        return File(file, odtMime, string.Format(@"Соглашение № {0}.odt", idContract));
                }
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
                var check = contractService.CheckForFullnessReport(idContract);
                if (check == false)
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                    return View("Error");
                }
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

        public IActionResult GetRasp(int? idContract, string dateNote, string numberNote, string dateRasp, string numberRasp, int idSigner, int idPrepared, int idLawyer, int idExecutor)
        {
            try
            {
                var ids = GetSessionIds();
                var datenomRasp = dateRasp + " " + numberRasp;
                var datenomNote = dateNote + " " + numberNote;
                var typeForView = "rasp";
                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    contractService.UpdateDocumentSigners(idContract, idSigner, idPrepared, idLawyer, idExecutor, typeForView);
                    var file = reportService.Rasp(ids, idContract, reportPath, datenomRasp, datenomNote);
                    return File(file, odtMime, string.Format(@"Распоряжение № {0}.odt", idContract));
                }
                contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                if (errorIds.Any())
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                        errorIds.Count == 1 ? "e" : "ах");
                    return View("Error");
                }
                contractService.UpdateMultiDocumentSigners(ids, idSigner, idPrepared, idLawyer, idExecutor);
                var files = reportService.Rasp(ids, idContract, reportPath, datenomRasp, datenomNote);
                return File(files, zipMime, @"Распоряжения.zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }

        public List<int> GetSessionIds()
        {
            if (HttpContext.Session.Keys.Contains("idContracts"))
                return HttpContext.Session.Get<List<int>>("idContracts");
            return new List<int>();
        }
        public IActionResult GetTakeoverAgreement(int idContract, int idSigner, DateTime date)
        {
            try
            {
                var check = contractService.CheckForFullnessReport(idContract);
                if (check == false)
                {
                    ViewData["Controller"] = "ContractReport";
                    ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                    return View("Error");
                }
                var persons = contractService.GetPersonsByContract(idContract);
                var files = reportService.TakeoverAgreement(idContract, idSigner, reportPath, date, persons);
                if (persons.Count == 1) {
                    return File(files, odtMime, string.Format(@"Соглашение об изъятии № {0}.odt", persons[0].IdPerson));
                } else  
                    return File(files, zipMime, @"Соглашения об изъятии.zip");
            }
            catch (Exception ex)
            {
                ViewData["Controller"] = "ContractReport";
                ViewData["TextError"] = ex;
                return View("Error");
            }
        }

        public IActionResult GetCoverLetterToTakeoverAgreement(int? idContract, int idSigner, int idPrepared)
        {
            try
            {
                var ids = GetSessionIds();

                if (idContract != null)
                {
                    var check = contractService.CheckForFullnessReport(idContract);
                    if (check == false)
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договоре №{0} необходимо заполнить адрес и/или участника", idContract);
                        return View("Error");
                    }
                    var file = reportService.CoverLetterToTakeoverAgreement(ids, idContract, idSigner, idPrepared, reportPath);
                    return File(file, odtMime, string.Format(@"Сопроводительное письмо к соглашению об изъятии имущества '  № {0}.odt", idContract));
                }
                else
                {
                    contractService.CheckForFullnessMultiReport(ids, out List<int> processingIds, out List<int> errorIds);
                    if (errorIds.Any())
                    {
                        ViewData["Controller"] = "ContractReport";
                        ViewData["TextError"] = string.Format("В договор{1} №{0} не указан адрес и/или участник", errorIds.Select(c => c.ToString()).Aggregate((a, s) => a + "," + s),
                            errorIds.Count == 1 ? "e" : "ах");
                        return View("Error");
                    }
                    var file = reportService.CoverLetterToTakeoverAgreement(ids, idContract, idSigner, idPrepared, reportPath);
                    return File(file, zipMime, @"Сопроводительные письма к соглашению об изъятии имущества.zip");
                }
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