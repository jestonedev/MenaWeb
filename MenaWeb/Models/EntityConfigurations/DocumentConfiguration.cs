using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Entities.Document>
    {
        private string nameDatebase;

        public DocumentConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Document> builder)
        {
            builder.HasKey(e => e.IdDocument);

            builder.ToTable("sp_document", nameDatebase);

            builder.Property(e => e.IdDocument)
                .HasColumnName("id_document")
                .HasColumnType("smallint(6)")
                .IsRequired();

            builder.Property(e => e.DocumentName)
                .HasColumnName("document")
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("");
        }
    }
}
