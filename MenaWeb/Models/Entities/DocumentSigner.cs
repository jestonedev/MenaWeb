using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class DocumentSigner
    {
        public int IdDocument { get; set; }
        public int IdContract { get; set; }
        public int? IdOrderBoss { get; set; }
        public int? IdOrderCommitetSigner { get; set; }
        public int? IdOrderVerifyLawyer { get; set; }
        public int? IdOrderWorker { get; set; }
        public int? IdRaspBoss { get; set; }
        public int? IdRaspLawyer { get; set; }
        public int? IdRaspVerify { get; set; }
        public int? IdRaspExecutor { get; set; }
    }
}
