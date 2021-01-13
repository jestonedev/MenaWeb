using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class RedOrganizationConfiguration : IEntityTypeConfiguration<Entities.RedOrganization>
    {
        private string nameDatebase;

        public RedOrganizationConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.RedOrganization> builder)
        {
            builder.HasKey(e => e.IdOrganization);

            builder.ToTable("red_organization", nameDatebase);

            builder.Property(e => e.IdOrganization)
                .HasColumnName("id_organization")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(500);
        }
    }
}
