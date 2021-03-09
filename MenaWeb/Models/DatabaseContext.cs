using MenaWeb.Models.Entities;
using MenaWeb.Models.EntityConfigurations;
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

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Entities.Delegate> Delegates { get; set; }
        public DbSet<Signer> Signers { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<PreContractIssued> PreContractIssueds { get; set; }
        public DbSet<ContractReason> ContractReasons { get; set; }
        public DbSet<ApartmentEvaluation> ApartmentEvaluations { get; set; }
        public DbSet<Evaluator> Evaluators { get; set; }
        public DbSet<ContractStatusHistory> ContractStatusHistory { get; set; }
        public DbSet<ProcessStatus> ProcessStatuses { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<PersonStatus> PersonStatuses { get; set; }
        public DbSet<DocumentIssued> DocumentIssueds { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentSigner> DocumentSigners { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Predost> Predosts { get; set; }
        public DbSet<Osnovanie> Osnovanies { get; set; }
        public DbSet<CopyKgc> CopyKgcs { get; set; }
        public DbSet<ApartmentRedemption> ApartmentRedemptions { get; set; }
        public DbSet<BankInfo> BankInfos { get; set; }
        public DbSet<RedEvaluation> RedEvaluations { get; set; }
        public DbSet<RedOrganization> RedOrganizations { get; set; }
        public DbSet<WarrantTemplate> WarrantTemplates { get; set; }
        public DbSet<WarrantTemplateType> WarrantTemplateTypes { get; set; }
        public DbSet<WarrantApartment> WarrantApartments { get; set; }
        public DbSet<TemplateVariable> TemplateVariables { get; set; }
        public DbSet<TemplateVariableMeta> TemplateVariablesMeta { get; set; }
        public DbSet<KladrStreet> KladrStreets { get; set; }

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
            modelBuilder.ApplyConfiguration(new ContractConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new DelegateConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new SignerConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new DocumentSignerConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ApartmentTypeConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new PreContractIssuedConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ContractReasonConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ApartmentEvaluationConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new EvaluatorConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ContractStatusHistoryConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ProcessStatusConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new LandConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new PersonConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ContractorConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new PersonStatusConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new DocumentIssuedConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new DocumentConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new AdditionalConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new PredostConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new OsnovanieConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new CopyKgcConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new ApartmentRedemptionConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new BankInfoConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new RedEvaluationConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new RedOrganizationConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new WarrantTemplateConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new WarrantTemplateTypeConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new WarrantApartmentConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new TemplateVariableMetaConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new TemplateVariableConfiguration(nameDatebase));
            modelBuilder.ApplyConfiguration(new KladrStreetConfiguration(nameDatebase));
        }
    }
}
