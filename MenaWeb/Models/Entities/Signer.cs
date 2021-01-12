using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Signer
    {
        public ushort IdSigner { get; set; }
        public string Post { get; set; }
        public string PostGenetive { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Phone { get; set; }
        public int IdSignerType { get; set; }
        public string ShortPost { get; set; }
        public string ShortPost2 { get; set; }
        public virtual List<Contract> Contracts { get; set; }
    }
}
