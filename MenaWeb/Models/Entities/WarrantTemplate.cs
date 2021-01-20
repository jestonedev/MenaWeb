using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class WarrantTemplate
    {
        public int IdWarrantTemplate { get; set; }
        public string WarrantTemplateName { get; set; }
        public string WarrantTemplateBody { get; set; }
        public short? IdWarrantTemplateType { get; set; }
        public bool Deleted { get; set; }
        public WarrantTemplateType WarrantTemplateType { get; set; }
        public List<WarrantApartment> WarrantApartments { get; set; }
        public List<TemplateVariableMeta> TemplateVariablesMeta { get; set; }
    }
}
