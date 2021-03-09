using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace MenaWeb.Models.EntityConfigurations
{
    public class DocumentSignerConfiguration: IEntityTypeConfiguration<Entities.DocumentSigner>
    {
        private string nameDatebase;

        public DocumentSignerConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.DocumentSigner> builder)
        {
            builder.HasKey(e => e.IdDocument);

            builder.ToTable("document_signers", nameDatebase);

            builder.Property(e => e.IdDocument)
               .HasColumnName("id_document")
               .HasColumnType("int(11)")
               .IsRequired();

            builder.Property(e => e.IdContract)
               .HasColumnName("id_contract")
               .HasColumnType("int(11)")
               .IsRequired();

            builder.Property(e => e.IdOrderBoss)
                .HasColumnName("id_order_boss")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdOrderCommitetSigner)
               .HasColumnName("id_order_commitet_signer")
               .HasColumnType("int(11)");

            builder.Property(e => e.IdOrderVerifyLawyer)
               .HasColumnName("id_order_verify_lawer")
               .HasColumnType("int(11)");

            builder.Property(e => e.IdOrderWorker)
               .HasColumnName("id_order_worker")
               .HasColumnType("int(11)");

            builder.Property(e => e.IdRaspBoss)
                .HasColumnName("id_rasp_boss")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdRaspExecutor)
               .HasColumnName("id_rasp_executor")
               .HasColumnType("int(11)");

            builder.Property(e => e.IdRaspLawyer)
               .HasColumnName("id_rasp_lawer")
               .HasColumnType("int(11)");

            builder.Property(e => e.IdRaspVerify)
               .HasColumnName("id_rasp_verify")
               .HasColumnType("int(11)")
               .HasDefaultValue(6);
        }
    }
}
