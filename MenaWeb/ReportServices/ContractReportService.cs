using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace MenaWeb.ReportServices
{
    public class ContractReportService : ReportService
    {
        public ContractReportService(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
        public byte[] Resolution(int idContract, int idWarrantTemplate, int idAgree, string reportPath)
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
        public byte[] DksrContract(List<int> ids, int? idContract, string reportPath)
        {
            if (idContract != null)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_contract", idContract },
                    { "reportPath", reportPath }
                };
                var fileNameReport = GenerateReport(arguments, "mena\\contract_dksr");
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arg, "mena\\contract_dksr");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                { File.Delete(files); }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] ContractWithDate(List<int> ids, int? idContract, string reportPath)
        {
            if (idContract != null)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_contract", idContract },
                    { "reportPath", reportPath }
                };
                var fileNameReport = GenerateReport(arguments, "mena\\contract_2");
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arg, "mena\\contract_2");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] ContractWithoutDate(List<int> ids, int? idContract, string reportPath)
        {
            if (idContract != null)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_contract", idContract },
                    { "reportPath", reportPath },
                };
                var fileNameReport = GenerateReport(arguments, "mena\\contract_date_2");
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arg, "mena\\contract_date_2");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] PreContract(List<int> ids, int? idContract, int idPreamble, int idDep, string reportPath)
        {
            if (idContract != null)
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
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "idPreamble", idPreamble },
                        { "idDep", idDep },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arg, "mena\\precontract");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] NotifyMena(List<int> ids, int? idContract, int idSigner, int idPrepared, string reportPath, int notifyType)
        {
            if (idContract != null)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_contract", idContract },
                    { "id_signer", idSigner },
                    { "id_worker", idPrepared },
                    { "reportPath", reportPath }
                };
                var fileName = "";
                switch (notifyType)
                {
                    case 1:
                        fileName = "mena\\notify_mena";
                        break;
                    case 2:
                        fileName = "mena\\notify_notary";
                        break;
                    case 3:
                        fileName = "mena\\notify_new";
                        break;
                    case 4:
                        fileName = "mena\\notify_docs";
                        break;
                }
                var fileNameReport = GenerateReport(arguments, fileName);
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "id_signer", idSigner },
                        { "id_worker", idPrepared },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileName = "";
                    switch (notifyType)
                    {
                        case 1:
                            fileName = "mena\\notify_mena";
                            break;
                        case 2:
                            fileName = "mena\\notify_notary";
                            break;
                        case 3:
                            fileName = "mena\\notify_new";
                            break;
                        case 4:
                            fileName = "mena\\notify_docs";
                            break;
                    }
                    var fileRep = GenerateMultiFileReport(arg, fileName);
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] RequestToMvd(List<int> ids, int? idContract, int idSigner, int idPrepared, string reportPath, int requestType)
        {
            if (idContract != null)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_contract", idContract },
                    { "signer_id", idSigner },
                    { "worker_id", idPrepared },
                    { "reportPath", reportPath }
                };
                var fileName = "";
                switch (requestType)
                {
                    case 1:
                        fileName = "mena\\requestNew";
                        break;
                    case 2:
                        fileName = "mena\\request";
                        break;
                }
                var fileNameReport = GenerateReport(arguments, fileName);
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "signer_id", idSigner },
                        { "worker_id", idPrepared },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileName = "";
                    switch (requestType)
                    {
                        case 1:
                            fileName = "mena\\requestNew";
                            break;
                        case 2:
                            fileName = "mena\\request";
                            break;
                    }
                    var fileRep = GenerateMultiFileReport(arg, fileName);
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
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
        public byte[] Rasp(List<int> ids,int? idContract, string reportPath, string datenomRasp, string datenomNote)
        {
            if (idContract != null)
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
            else
            {
                string destDirGuid = Guid.NewGuid().ToString();
                foreach (var file in ids)
                {
                    idContract = file;
                    var arg = new Dictionary<string, object>
                    {
                        { "id_contract", idContract },
                        { "reportPath", reportPath },
                        { "datenom_rasp", datenomRasp },
                        { "datenom_memo", datenomNote },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arg, "mena\\rasp");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
        public byte[] TakeoverAgreement(int idContract, string[] array_idsPersons, int idSigner, string reportPath, DateTime date)
        {
            if (array_idsPersons.Length == 1)
            {
                var arguments = new Dictionary<string, object>
                {
                    { "id_person", array_idsPersons[0] },
                    { "id_contract", idContract },
                    { "id_signer", idSigner },
                    { "agreeDate", date },
                    { "reportPath", reportPath }
                };
                var fileNameReport = GenerateReport(arguments, "mena\\takeover_agreement");
                return DownloadFile(fileNameReport);
            }
            else
            {
                string destDirGuid = Guid.NewGuid().ToString(); 
                foreach (var file in array_idsPersons)
                {
                    var idPerson = file;
                    var arguments = new Dictionary<string, object>
                    {
                        { "id_person", idPerson },
                        { "id_contract", idContract },
                        { "id_signer", idSigner },
                        { "agreeDate", date },
                        { "reportPath", reportPath },
                        { "destDirGuid", destDirGuid }
                    };
                    var fileRep = GenerateMultiFileReport(arguments, "mena\\takeover_agreement");
                }
                var destFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", destDirGuid);
                var destZipFile = destFileName + ".zip";
                if (File.Exists(destZipFile)) File.Delete(destZipFile);
                ZipFile.CreateFromDirectory(destFileName, destZipFile);
                foreach (var files in Directory.GetFiles(destFileName))
                {
                    File.Delete(files);
                }
                DirectoryInfo dir = new DirectoryInfo(destFileName);
                if (dir.Exists) dir.Delete(true);
                return DownloadFile(destZipFile);
            }
        }
    }
}
