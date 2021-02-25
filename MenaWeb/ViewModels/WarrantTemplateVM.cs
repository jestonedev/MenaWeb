using MenaWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewModels
{
    public class WarrantTemplateVM
    {
        public int? IdWarrantTemplate { get; set; }
        public int? IdWarrantObject { get; set; }
        public int? IdObject { get; set; }
        public WarrantObjectType ObjectType { get; set; }
        public string WarrantTemplateBody { get; set; }

        public IList<TemplateVariable> Variables { get; set; }
        public string WarrantTemplateComplete {
            get
            {
                if (string.IsNullOrEmpty(WarrantTemplateBody)) return null;
                var tempBody = WarrantTemplateBody;
                foreach (var variable in Variables)
                {
                    tempBody = tempBody.Replace(variable.TemplateVariableMeta.Pattern, variable.Value);
                }
                return tempBody;
            }
        }
    }
}
