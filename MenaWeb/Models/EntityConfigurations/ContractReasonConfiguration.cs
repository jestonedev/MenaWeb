using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ContractReasonConfiguration : IEntityTypeConfiguration<Entities.ContractReason>
    {
        private string nameDatebase;

        public ContractReasonConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ContractReason> builder)
        {
            builder.HasKey(e => e.IdContractReason);

            builder.ToTable("sp_contract_reasons", nameDatebase);

            builder.Property(e => e.IdContractReason)
                .HasColumnName("id_contract_reason")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Template)
                .HasColumnName("template")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
