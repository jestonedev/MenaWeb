using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models.Entities
{
    public class DocumentIssued
    {
        public int IdDocumentIssued { get; set; }
        [Required(ErrorMessage = "Укажите наименование органа")]
        public string DocumentIssuedName { get; set; }
        public virtual List<Person> People { get; set; }
    }
}
