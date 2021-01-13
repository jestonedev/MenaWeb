using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class Document
    {
        public short IdDocument { get; set; }
        public string DocumentName{ get; set; }
        public virtual List<Person> People { get; set; }
    }
}
