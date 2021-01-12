using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class DelegateConfiguration : IEntityTypeConfiguration<Entities.Delegate>
    {
        private string nameDatebase;

        public DelegateConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Delegate> builder)
        {
            builder.HasKey(e => e.IdDelegate);

            builder.ToTable("sp_delegate", nameDatebase);

            builder.Property(e => e.IdDelegate)
                .HasColumnName("id_delegate")
                .HasColumnType("tinyint(4)")
                .IsRequired();

            builder.Property(e => e.Fio)
                .HasColumnName("fio")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Birth)
                .HasColumnName("birth")
                .HasColumnType("datetime");

            builder.Property(e => e.PassportSeria)
                .HasColumnName("passport_seria")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.PassportNum)
                .HasColumnName("passport_num")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.PassportIssued)
                .HasColumnName("passport_issued")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PassportIssuedDate)
                            .HasColumnName("passport_isssued_date")
                            .HasColumnType("datetime");

            builder.Property(e => e.IdTemplate)
                .HasColumnName("id_template")
                .HasColumnType("smallint(6)")
                .IsRequired();
        }
    }
}
