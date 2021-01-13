using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class WarrantTemplateConfiguration : IEntityTypeConfiguration<Entities.WarrantTemplate>
    {
        private string nameDatebase;

        public WarrantTemplateConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.WarrantTemplate> builder)
        {
            builder.HasKey(e => e.IdWarrantTemplate);

            builder.ToTable("sp_warrant_template", nameDatebase);

            builder.Property(e => e.IdWarrantTemplate)
                .HasColumnName("id_warrant_template")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.WarrantTemplateName)
                .HasColumnName("warrant_template_name")
                .HasMaxLength(2000);

            builder.Property(e => e.WarrantTemplateBody)
                .HasColumnName("warrant_template")
                .HasMaxLength(2000);

            builder.Property(e => e.IdWarrantTemplateType)
                .HasColumnName("id_warrant_template_type")
                .HasColumnType("smallint(6)");

            builder.Property(e => e.Deleted)
                .HasColumnName("wasDeleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(0);

            builder.HasOne(r => r.WarrantTemplateType)
                .WithMany(r => r.WarrantTemplates)
                .HasForeignKey(r => r.IdWarrantTemplateType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
