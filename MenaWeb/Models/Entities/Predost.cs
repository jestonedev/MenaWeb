using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Predost
    {
        public short IdPredost { get; set; }
        public string PredostName { get; set; }
        public virtual List<Additional> Additionals { get; set; }
    }
}
