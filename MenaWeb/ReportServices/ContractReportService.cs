using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ReportServices
{
    public class ContractReportService : ReportService
    {
        public ContractReportService(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
        public byte[] Resolution(int idContract, int idWarrantTemplate, int idAgree, string reportPath, int idSigner, int idPrepared, int Lawyer, int Executor)
        {

            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "id_warrant_template", idWarrantTemplate },
                { "verifyBoss",idAgree},
                { "reportPath", reportPath }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\order_new");
            return DownloadFile(fileNameReport);
        }
        public byte[] DksrContract(int idContract, string reportPath)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "reportPath", reportPath }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\contract_dksr");
            return DownloadFile(fileNameReport);
        }
        public byte[] ContractWithDate(int idContract, string reportPath)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "reportPath", reportPath }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\contract_2");
            return DownloadFile(fileNameReport);
        }
        public byte[] ContractWithoutDate(int idContract, string reportPath)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "reportPath", reportPath },
            };
            var fileNameReport = GenerateReport(arguments, "mena\\contract_date_2");
            return DownloadFile(fileNameReport);
        }
        public byte[] PreContract(int idContract, int idPreamble, int idDep, string reportPath)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "idPreamble", idPreamble },
                { "idDep", idDep },
                { "reportPath", reportPath }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\precontract");
            return DownloadFile(fileNameReport);
        }
        public byte[] NotifyMena(int idContract, int idSigner, int idPrepared, string reportPath, int notifyType)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "id_signer", idSigner },
                { "id_worker", idPrepared },
                { "reportPath", reportPath }
            };
            var fileName = "mena\\notify_mena";
            if (notifyType == 2)
            {
                fileName = "mena\\notify_notary";
            }
            if(notifyType == 3)
            {
                fileName = "mena\\notify_new";
            }
            if(notifyType == 4)
            {
                fileName = "mena\\notify_docs";
            }
            var fileNameReport = GenerateReport(arguments, fileName);
            return DownloadFile(fileNameReport);
        }
        public byte[] RequestToMvd(int idContract, int idSigner, int idPrepared, string reportPath, int requestType)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "signer_id", idSigner },
                { "worker_id", idPrepared },
                { "reportPath", reportPath }
            };
            var fileName = "mena\\requestNew";

            if (requestType == 2)
            {
                fileName = "mena\\request";
            }
            
            var fileNameReport = GenerateReport(arguments, fileName);
            return DownloadFile(fileNameReport);
        }
        public byte[] Agreement(int idContract, int idSigner,  string reportPath, int agreementType, DateTime date)
        {
            if (agreementType == 1)
            {
                var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "id_signer", idSigner },
                { "date_agr", date },
                { "reportPath", reportPath }
            };
                var fileNameReport = GenerateReport(arguments, "mena\\agreement2" );
                return DownloadFile(fileNameReport);
            }
            else
            {
                var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "id_signer", idSigner },
                { "agreeDate", date },
                { "reportPath", reportPath }
            };
                var fileNameReport = GenerateReport(arguments, "mena\\agreement_redemption_date");
                return DownloadFile(fileNameReport);
            }
        }
        public byte[] AgreementTransfer(int idContract, string reportPath)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "reportPath", reportPath }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\agreement_transfer");
            return DownloadFile(fileNameReport);
        }
        public byte[] Rasp(int idContract, string reportPath, string datenomRasp, string datenomNote)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "reportPath", reportPath },
                { "datenom_rasp", datenomRasp },
                { "datenom_memo", datenomNote }
            };
            var fileNameReport = GenerateReport(arguments, "mena\\rasp");
            return DownloadFile(fileNameReport);
        }
    }
}
