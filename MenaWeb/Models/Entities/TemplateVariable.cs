using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class TemplateVariable
    {
        public int IdTemplateVariable { get; set; }
        public int IdTemplateVariableMeta { get; set; }
        public int IdObject { get; set; }
        public string Value { get; set; }
        public TemplateVariableMeta TemplateVariableMeta { get; set; }
    }
}
