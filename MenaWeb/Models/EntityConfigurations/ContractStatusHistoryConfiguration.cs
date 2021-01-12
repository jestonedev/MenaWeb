using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ContractStatusHistoryConfiguration : IEntityTypeConfiguration<Entities.ContractStatusHistory>
    {
        private string nameDatebase;

        public ContractStatusHistoryConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ContractStatusHistory> builder)
        {
            builder.HasKey(e => e.IdHistoryStatus);

            builder.ToTable("contract_status_history", nameDatebase);

            builder.Property(e => e.IdHistoryStatus)
                .HasColumnName("id_history_status")
                .IsRequired()
                .HasColumnType("int(11)");

            builder.Property(e => e.IdContract)
                .HasColumnName("id_contract")
                .IsRequired()
                .HasColumnType("int(11)");

            builder.Property(e => e.IdProcessStatus)
                .HasColumnName("id_process_status")
                .HasColumnType("tinyint(4)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.StatusDate)
                .HasColumnName("status_date")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(r => r.Contract)
                .WithMany(r => r.ContractStatusHistory)
                .HasForeignKey(r => r.IdContract)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ProcessStatus)
                .WithMany(r => r.ContractStatusHistory)
                .HasForeignKey(r => r.IdProcessStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
