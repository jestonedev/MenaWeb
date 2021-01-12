using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class PersonStatusConfiguration : IEntityTypeConfiguration<Entities.PersonStatus>
    {
        private string nameDatebase;

        public PersonStatusConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.PersonStatus> builder)
        {
            builder.HasKey(e => e.IdPersonStatus);

            builder.ToTable("sp_person_status", nameDatebase);

            builder.Property(e => e.IdPersonStatus)
                .HasColumnName("id_person_status")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(50);
        }
    }
}
