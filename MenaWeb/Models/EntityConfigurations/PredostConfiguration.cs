using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class PredostConfiguration : IEntityTypeConfiguration<Entities.Predost>
    {
        private string nameDatebase;

        public PredostConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Predost> builder)
        {
            builder.HasKey(e => e.IdPredost);

            builder.ToTable("sp_predost", nameDatebase);

            builder.Property(e => e.IdPredost)
                .HasColumnName("id_predost")
                .HasColumnType("tinyint(3)")
                .IsRequired();

            builder.Property(e => e.PredostName)
                .HasColumnName("predost")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
