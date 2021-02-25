using MenaWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public interface IContractReportVM
    {
        IList<Signer> ReportSigners { get; set; }
        IList<WarrantTemplate> WarrantTemplates { get; set; }
    }
}
