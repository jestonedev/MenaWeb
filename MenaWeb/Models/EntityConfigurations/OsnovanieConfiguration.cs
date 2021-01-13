using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class OsnovanieConfiguration : IEntityTypeConfiguration<Entities.Osnovanie>
    {
        private string nameDatebase;

        public OsnovanieConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Osnovanie> builder)
        {
            builder.HasKey(e => e.IdOsnovanie);

            builder.ToTable("sp_osnovanie", nameDatebase);

            builder.Property(e => e.IdOsnovanie)
                .HasColumnName("id_osnovanie")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.OsnovanieName)
                .HasColumnName("osnovanie")
                .HasMaxLength(255);
        }
    }
}
