using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class PersonStatus
    {
        public short IdPersonStatus { get; set; }
        public string Status { get; set; }
        public virtual List<Person> People { get; set; }
    }
}
