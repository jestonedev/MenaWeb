using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class LandConfiguration : IEntityTypeConfiguration<Entities.Land>
    {
        private string nameDatebase;

        public LandConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Land> builder)
        {
            builder.HasKey(e => e.IdLand);

            builder.ToTable("land", nameDatebase);

            builder.Property(e => e.IdLand)
                .HasColumnName("id_land")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdStreet)
                .HasColumnName("id_street")
                .IsRequired()
                .HasMaxLength(17);

            builder.Property(e => e.House)
                .HasColumnName("house")
                .HasMaxLength(5);

            builder.Property(e => e.InventoryNumber)
                .HasColumnName("inventory_number")
                .HasMaxLength(50);

            builder.Property(e => e.TotalArea)
                .HasColumnName("total_area")
                .HasColumnType("decimal(8,2)");

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.Land)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
