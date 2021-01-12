using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ApartmentTypeConfiguration : IEntityTypeConfiguration<Entities.ApartmentType>
    {
        private string nameDatebase;

        public ApartmentTypeConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ApartmentType> builder)
        {
            builder.HasKey(e => e.IdApartmentType);

            builder.ToTable("sp_apartment_type", nameDatebase);

            builder.Property(e => e.IdApartmentType)
                .HasColumnName("id_apartment_type")
                .HasColumnType("tinyint(3)")
                .IsRequired();

            builder.Property(e => e.ApartmentTypeName)
                .HasColumnName("apartment_type")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.ApartmentTypeRod)
                .HasColumnName("apartment_type_rod")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.ApartmentTypePlur)
                .HasColumnName("apartment_type_plur")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
