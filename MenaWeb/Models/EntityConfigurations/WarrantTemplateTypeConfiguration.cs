using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class WarrantTemplateTypeConfiguration : IEntityTypeConfiguration<Entities.WarrantTemplateType>
    {
        private string nameDatebase;

        public WarrantTemplateTypeConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.WarrantTemplateType> builder)
        {
            builder.HasKey(e => e.IdWarrantTemplateType);

            builder.ToTable("sp_warant_template_type", nameDatebase);

            builder.Property(e => e.IdWarrantTemplateType)
                .HasColumnName("id_warant_template_type")
                .HasColumnType("tinyint(3)")
                .IsRequired();

            builder.Property(e => e.WarrantTemplateTypeName)
                .HasColumnName("warant_template_type")
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
