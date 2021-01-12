using System;

namespace MenaWeb.Models.Entities
{
    public class ContractStatusHistory
    {
        public int IdHistoryStatus { get; set; }
        public int IdContract { get; set; }
        public short IdProcessStatus { get; set; }
        public DateTime StatusDate { get; set; }

        public Contract Contract { get; set; }
        public ProcessStatus ProcessStatus { get; set; }
    }
}