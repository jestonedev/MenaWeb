using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.Models
{
    public class DatabaseContext: DbContext
    {
        private string nameDatebase;
        private string connString;

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            connString = httpContextAccessor.HttpContext.User.FindFirst("connString").Value;
            nameDatebase = config.GetValue<string>("Database");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
