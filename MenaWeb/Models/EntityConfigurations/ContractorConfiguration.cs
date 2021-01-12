using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ContractorConfiguration : IEntityTypeConfiguration<Entities.Contractor>
    {
        private string nameDatebase;

        public ContractorConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Contractor> builder)
        {
            builder.HasKey(e => e.IdContractor);

            builder.ToTable("sp_contractor", nameDatebase);

            builder.Property(e => e.IdContractor)
                .HasColumnName("id_contractor")
                .HasColumnType("tinyint(4)")
                .IsRequired();

            builder.Property(e => e.ContractorName)
                .HasColumnName("contractor")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.ContractorShort)
                .HasColumnName("contractor_short")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
