using MenaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.DataServices
{
    public class ContractsService
    {
        private readonly DatabaseContext db;

        public ContractsService(DatabaseContext db)
        {
            this.db = db;
        }

        public void GetContracts()
        {
            var a = db.DocumentIssueds;
            var s = db.People.Include(r => r.DocumentIssued);
        }
    }
}
