using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class DocumentIssuedConfiguration : IEntityTypeConfiguration<Entities.DocumentIssued>
    {
        private string nameDatebase;

        public DocumentIssuedConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.DocumentIssued> builder)
        {
            builder.HasKey(e => e.IdDocumentIssued);

            builder.ToTable("sp_document_issued", nameDatebase);

            builder.Property(e => e.IdDocumentIssued)
                .HasColumnName("id_document_issued")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.DocumentIssuedName)
                .HasColumnName("document_issued")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
