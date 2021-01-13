using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class TemplateVariableConfiguration : IEntityTypeConfiguration<Entities.TemplateVariable>
    {
        private string nameDatebase;

        public TemplateVariableConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.TemplateVariable> builder)
        {
            builder.HasKey(e => e.IdTemplateVariable);

            builder.ToTable("template_variables", nameDatebase);

            builder.Property(e => e.IdTemplateVariable)
                .HasColumnName("id_template_variable")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdTemplateVariableMeta)
                .HasColumnName("id_template_variable_meta")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdObject)
                .HasColumnName("id_object")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .HasMaxLength(255);

            builder.HasOne(r => r.TemplateVariableMeta)
                .WithMany(r => r.TemplateVariables)
                .HasForeignKey(r => r.IdTemplateVariableMeta)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
