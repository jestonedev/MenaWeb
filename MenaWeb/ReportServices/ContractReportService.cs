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

        // Пример метода сервиса отчетов
        public byte[] Example(int idContract, int idPreamble, int idCommittee)
        {
            var arguments = new Dictionary<string, object>
            {
                { "id_contract", idContract },
                { "id_preamble", idPreamble },
                { "id_commitee", idCommittee }
            };
            var fileNameReport = GenerateReport(arguments, "registry\\tenancy\\district_committee_pre_contract");
            return DownloadFile(fileNameReport);
        }
    }
}
