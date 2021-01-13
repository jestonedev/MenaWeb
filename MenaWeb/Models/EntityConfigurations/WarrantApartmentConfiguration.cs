using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class WarrantApartmentConfiguration : IEntityTypeConfiguration<Entities.WarrantApartment>
    {
        private string nameDatebase;

        public WarrantApartmentConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.WarrantApartment> builder)
        {
            builder.HasKey(e => e.IdWarrantApartment);

            builder.ToTable("warrant_apartment", nameDatebase);

            builder.Property(e => e.IdWarrantApartment)
                .HasColumnName("id_warrant_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdWarrantTemplate)
                .HasColumnName("id_warrant_template")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.WarrantApartments)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.WarrantTemplate)
                .WithMany(r => r.WarrantApartments)
                .HasForeignKey(r => r.IdWarrantTemplate)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
