using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class SignerConfiguration : IEntityTypeConfiguration<Entities.Signer>
    {
        private string nameDatebase;

        public SignerConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Signer> builder)
        {
            builder.HasKey(e => e.IdSigner);

            builder.ToTable("sp_signer", nameDatebase);

            builder.Property(e => e.IdSigner)
                .HasColumnName("id_signer")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Post)
                .HasColumnName("post")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PostGenetive)
                .HasColumnName("post_genitive")
                .HasMaxLength(255);

            builder.Property(e => e.Family)
                .HasColumnName("family")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Father)
                .HasColumnName("father")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20);

            builder.Property(e => e.IdSignerType)
                .HasColumnName("id_signer_type")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.ShortPost)
                .HasColumnName("short_post")
                .HasMaxLength(255);

            builder.Property(e => e.ShortPost2)
                .HasColumnName("short_post_2")
                .HasMaxLength(255);
        }
    }
}
