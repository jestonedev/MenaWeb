using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class TemplateVariableMetaConfiguration : IEntityTypeConfiguration<Entities.TemplateVariableMeta>
    {
        private string nameDatebase;

        public TemplateVariableMetaConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.TemplateVariableMeta> builder)
        {
            builder.HasKey(e => e.IdTemplateVariableMeta);

            builder.ToTable("template_variables_meta", nameDatebase);

            builder.Property(e => e.IdTemplateVariableMeta)
                .HasColumnName("id_template_variable_meta")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdWarrantTemplate)
                .HasColumnName("id_template")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Pattern)
                .HasColumnName("pattern")
                .HasMaxLength(255);

            builder.Property(e => e.Label)
                .HasColumnName("label")
                .HasMaxLength(255);

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .HasMaxLength(255)
                .HasDefaultValue("edit");

            builder.HasOne(r => r.WarrantTemplate)
                .WithMany(r => r.TemplateVariablesMeta)
                .HasForeignKey(r => r.IdWarrantTemplate)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
