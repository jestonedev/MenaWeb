using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class ProcessStatus
    {
        public short IdProcessStatus { get; set; }
        public string ProcessStatusName { get; set; }
        public string ProcessStatusTemplate { get; set; }
        public string Step { get; set; }
        public virtual List<ContractStatusHistory> ContractStatusHistory { get; set; }
    }
}
