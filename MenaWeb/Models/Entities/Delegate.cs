using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Delegate
    {
        public ushort IdDelegate { get; set; }
        public string Fio { get; set; }
        public DateTime Birth { get; set; }
        public string PassportSeria { get; set; }
        public string PassportNum { get; set; }
        public string PassportIssued { get; set; }
        public DateTime PassportIssuedDate { get; set; }
        public int IdTemplate { get; set; }
        public virtual List<Contract> Contracts { get; set; }
    }
}
