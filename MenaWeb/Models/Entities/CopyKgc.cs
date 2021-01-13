using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class CopyKgc
    {
        public ushort IdCopy { get; set; }
        public string CopyName { get; set; }
        public virtual List<Additional> Additionals { get; set; }
    }
}
