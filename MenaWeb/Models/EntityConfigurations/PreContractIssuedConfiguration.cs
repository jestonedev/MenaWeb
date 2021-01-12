using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class PreContractIssuedConfiguration : IEntityTypeConfiguration<Entities.PreContractIssued>
    {
        private string nameDatebase;

        public PreContractIssuedConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.PreContractIssued> builder)
        {
            builder.HasKey(e => e.IdPreContractIssued);

            builder.ToTable("sp_pre_contract_issued", nameDatebase);

            builder.Property(e => e.IdPreContractIssued)
                .HasColumnName("id_pre_contract_issued")
                .HasColumnType("tinyint(4)")
                .IsRequired();

            builder.Property(e => e.PreContractName)
                .HasColumnName("pre_contract_name")
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(e => e.PreContractNameShort)
                .HasColumnName("pre_contract_name_short")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
