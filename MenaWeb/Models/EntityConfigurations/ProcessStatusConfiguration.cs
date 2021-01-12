using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ProcessStatusConfiguration : IEntityTypeConfiguration<Entities.ProcessStatus>
    {
        private string nameDatebase;

        public ProcessStatusConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ProcessStatus> builder)
        {
            builder.HasKey(e => e.IdProcessStatus);

            builder.ToTable("sp_process_status", nameDatebase);

            builder.Property(e => e.IdProcessStatus)
                .HasColumnName("id_process_status")
                .HasColumnType("tinyint(4)")
                .IsRequired();

            builder.Property(e => e.ProcessStatusName)
                .HasColumnName("process_status")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.ProcessStatusTemplate)
                .HasColumnName("process_status_template")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Step)
                .HasColumnName("step")
                .IsRequired()
                .HasMaxLength(5);
        }
    }
}
