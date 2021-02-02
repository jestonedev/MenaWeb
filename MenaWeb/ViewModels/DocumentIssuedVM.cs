using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.Models.Entities;
using MenaWeb.ViewOptions;

namespace MenaWeb.ViewModels
{
    public class DocumentIssuedVM<T>
    {
        public List<DocumentIssued> DocumentIssueds { get; set; }
        public DocumentIssued DocumentIssue { get; set; }
        public PageOptions PageOptions { get; set; }
    
    }
}
