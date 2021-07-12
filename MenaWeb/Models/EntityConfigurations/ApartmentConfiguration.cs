using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Entities.Apartment>
    {
        private string nameDatebase;

        public ApartmentConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Apartment> builder)
        {
            builder.HasKey(e => e.IdApartment);

            builder.ToTable("apartments", nameDatebase);

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartmentType)
                .HasColumnName("id_apartment_type")
                .HasColumnType("tinyint(3)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.IdStreet)
                .HasColumnName("id_street")
                .IsRequired()
                .HasMaxLength(17)
                .HasDefaultValue("00000000000000000");

            builder.Property(e => e.House)
                .HasColumnName("house")
                .HasMaxLength(5);

            builder.Property(e => e.Flat)
                .HasColumnName("flat")
                .HasMaxLength(10);

            builder.Property(e => e.Index)
                .HasColumnName("index")
                .HasMaxLength(15);

            builder.Property(e => e.TotalArea)
                .HasColumnName("total_area")
                .HasColumnType("decimal(8,2)");

            builder.Property(e => e.LivingArea)
                .HasColumnName("living_area")
                .HasColumnType("decimal(8,2)");

            builder.Property(e => e.Part)
                .HasColumnName("part")
                .HasMaxLength(5)
                .IsRequired()
                .HasDefaultValue("1");

            builder.Property(e => e.HouseFloor)
                .HasColumnName("house_floor")
                .HasMaxLength(5);

            builder.Property(e => e.Floor)
                .HasColumnName("floor")
                .HasMaxLength(5);

            builder.Property(e => e.InventoryNumber)
                .HasColumnName("inventory_number")
                .HasMaxLength(50);

            builder.Property(e => e.RoomCount)
                .HasColumnName("room_count")
                .HasMaxLength(5);

            builder.Property(e => e.Room)
                .HasColumnName("room")
                .HasMaxLength(5);

            builder.Property(e => e.CadastralPrice)
                .HasColumnName("cadastral_price")
                .HasColumnType("decimal(19,2)");
            
            builder.Property(e => e.DisasterHousing)
               .HasColumnName("disaster_housing")
               .HasColumnType("tinyint(1)")
               .IsRequired()
               .HasDefaultValue(0);
               
            builder.HasOne(r => r.ApartmentType)
                .WithMany(r => r.Apartments)
                .HasForeignKey(r => r.IdApartmentType)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
