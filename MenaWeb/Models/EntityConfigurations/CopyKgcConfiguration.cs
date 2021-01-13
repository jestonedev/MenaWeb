using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class CopyKgcConfiguration : IEntityTypeConfiguration<Entities.CopyKgc>
    {
        private string nameDatebase;

        public CopyKgcConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.CopyKgc> builder)
        {
            builder.HasKey(e => e.IdCopy);

            builder.ToTable("sp_copykgc", nameDatebase);

            builder.Property(e => e.IdCopy)
                .HasColumnName("id_copy")
                .HasColumnType("tinyint(3)")
                .IsRequired();

            builder.Property(e => e.CopyName)
                .HasColumnName("copy")
                .HasMaxLength(255);
        }
    }
}
