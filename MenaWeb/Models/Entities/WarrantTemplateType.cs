using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class WarrantTemplateType
    {
        public short IdWarrantTemplateType { get; set; }
        public string WarrantTemplateTypeName { get; set; }
        public virtual List<WarrantTemplate> WarrantTemplates { get; set; }
    }
}
