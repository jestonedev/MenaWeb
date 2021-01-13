using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class TemplateVariableMeta
    {
        public int IdTemplateVariableMeta { get; set; }
        public int IdWarrantTemplate { get; set; }
        public string Patternt { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public WarrantTemplate WarrantTemplate { get; set; }
        public virtual IList<TemplateVariable> TemplateVariables { get; set; }
    }
}
